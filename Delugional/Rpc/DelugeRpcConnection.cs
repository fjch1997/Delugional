using Delugional.Utility;
using rencodesharp;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Delugional.Rpc
{
    public class DelugeRpcConnection : IDisposable
    {
        private const int BufferSize = 4096;

        private readonly string host;
        private readonly int port;

        private DelugeVersion version = DelugeVersion.None;
        private Stream stream;
        private TcpClient client;

        public DelugeRpcConnection(IPEndPoint endPoint)
            : this(endPoint.Address, endPoint.Port)
        {
        }

        public DelugeRpcConnection(IPAddress ipAddress, int port = 58846)
            : this(ipAddress.ToString(), port)
        {
        }

        public DelugeRpcConnection(string host = "localhost", int port = 58846)
        {
            this.host = host;
            this.port = port;
        }

        public bool IsOpen => stream != null && client != null;

        public Stream Stream => stream;

        public string DaemonVersion { get; private set; }

        public async Task OpenAsync()
        {
            var client = new TcpClient(host, port);

            var sslStream = new SslStream(client.GetStream(), false, (sender, certificate, chain, errors) => true);
            await sslStream.AuthenticateAsClientAsync(host, null, System.Security.Authentication.SslProtocols.Tls12, false);

            stream = sslStream;
            this.client = client;

            await Send(DelugeVersion.V1, new[] { new RpcRequest("daemon.info") });
            await Send(DelugeVersion.V2, new[] { new RpcRequest("daemon.info") });
            await Send(DelugeVersion.V2_1, new[] { new RpcRequest("daemon.info") });
            await DetectVersion();
        }

        private async Task DetectVersion()
        {
            var buffer = new byte[BufferSize];

            int read = await stream.ReadAsync(buffer, 0, BufferSize);
            if (read == 0)
                return;

            RpcMessage[] messages;
            if (buffer[0] == (byte)'D')
            {
                version = DelugeVersion.V2;
                messages = ParseRencodeZlibBuffer(buffer, 5, read - 5);
            }
            else if (buffer[0] == 1)
            {
                version = DelugeVersion.V2_1;
                messages = ParseRencodeZlibBuffer(buffer, 5, read - 5);
            }
            else
            {
                version = DelugeVersion.V1;
                messages = ParseRencodeZlibBuffer(buffer, 0, read);
            }

            if (messages.Length == 0)
                throw new Exception("Failed to detect protocol version. No messages received.");

            var message = messages[0];
            if (!(message is RpcResponse response))
                throw new Exception("Failed to detect protocol version. Expected a response message.");
            if (response.Data is not string versionString)
                throw new Exception("Failed to detect protocol version. Expected a string in the response data.");
            if (!new Regex("\\d{1,2}\\.\\d{1,2}\\.\\d{1,2}").IsMatch(versionString))
                throw new Exception("Failed to detect protocol version. Invalid version string format.");
            DaemonVersion = versionString;
        }

        private async Task Send(DelugeVersion version, IEnumerable<RpcRequest> requests)
        {
            var formatted = FormatRequestMessages(requests);
            var encoded = Rencode.Encode(formatted);
            using var memoryStream = new MemoryStream();
            using (var zlib = new ZLibStream(memoryStream, CompressionMode.Compress, true))
            {
                zlib.Write(encoded, 0, encoded.Length);
            }
            var messageBytes = memoryStream.ToArray();
            switch (version)
            {
                case DelugeVersion.None:
                    throw new InvalidOperationException("Protocol version not detected. Cannot send messages.");
                case DelugeVersion.V1:
                    break;
                case DelugeVersion.V2:
                    {
                        // The first byte is the letter D.
                        // The next four bytes are the length of the message in big-endian 32-bit integer.
                        var header = new byte[] { (byte)'D', 0, 0, 0, 0 };
                        BinaryPrimitives.WriteInt32BigEndian(header.AsSpan().Slice(1), messageBytes.Length);
                        Stream.Write(header, 0, header.Length);
                        break;
                    }
                case DelugeVersion.V2_1:
                    {
                        // The first byte is the protocol number. The only supported number is 1.
                        // The next four bytes are the length of the message in big-endian 32-bit unsigned integer.
                        var header = new byte[] { 1, 0, 0, 0, 0 };
                        BinaryPrimitives.WriteUInt32BigEndian(header.AsSpan().Slice(1), (uint)messageBytes.Length);
                        Stream.Write(header, 0, header.Length);
                        break;
                    }
                default:
                    throw new InvalidOperationException("Unsupported protocol version.");
            }
            await Stream.WriteAsync(messageBytes, 0, messageBytes.Length);
        }

        private RpcMessage[] ParseRencodeZlibBuffer(byte[] buffer, int offset, int length)
        {
            var inflated = Zlib.Inflate(buffer, offset, length);
            var result = Rencode.Decode(inflated) as object[];
            if (result == null)
                return null;

            var messages = new List<RpcMessage>();
            switch (version)
            {
                case DelugeVersion.V1:
                    const int partsPerMessage = 3;
                    for (int skip = 0; skip < result.Length; skip += partsPerMessage)
                    {
                        object[] messageParts = result.Skip(skip).Take(partsPerMessage).ToArray();
                        RpcMessage message = RpcMessage.Create(version, messageParts);
                        messages.Add(message);
                    }

                    return messages.ToArray();
                case DelugeVersion.V2:
                case DelugeVersion.V2_1:
                    return new[] { RpcMessage.Create(version, result) };
                default:
                    throw new Exception("Protocol version not detected. Cannot parse messages.");
            }
        }

        public Task Send(RpcRequest request)
        {
            return Send(new[] { request });
        }

        public async Task Send(IEnumerable<RpcRequest> requests)
        {
            await Send(version, requests);
        }

        public async Task<RpcMessage[]> Receive()
        {
            switch (version)
            {
                case DelugeVersion.V1:
                    {
                        var buffer = new byte[BufferSize];
                        int read = await stream.ReadAsync(buffer, 0, BufferSize);

                        if (read == 0)
                            return null;

                        return ParseRencodeZlibBuffer(buffer, 0, read);
                    }
                case DelugeVersion.V2:
                case DelugeVersion.V2_1:
                    {
                        byte[] headerByteBuffer = new byte[1];
                        await stream.ReadAsync(headerByteBuffer, 0, 1);
                        var headerByte = headerByteBuffer[0];
                        if (headerByte != 'D' && headerByte != 1)
                            throw new Exception("Invalid message format. Expected D or protocol number as the first byte in the reply message.");
                        var sizeBuffer = new byte[4];
                        await stream.ReadExactlyAsync(sizeBuffer, 0, 4);
                        int size = BinaryPrimitives.ReadInt32BigEndian(sizeBuffer);
                        var buffer = new byte[size];
                        await stream.ReadExactlyAsync(buffer, 0, size);
                        return ParseRencodeZlibBuffer(buffer, 0, size);
                    }
                default:
                    throw new InvalidOperationException("Protocol version not detected. Cannot receive messages.");
            }
        }

        public void Close()
        {
            Dispose();
        }

        private static object FormatRequestMessages(IEnumerable<RpcRequest> requests)
        {
            return requests.Select(request => new object[]
            {
                request.Id,
                request.Method,
                request.Args.ToArray(),
                request.Kwargs.ToObjectDictionary()
            }).ToObjectArray();
        }

        public void Dispose()
        {
            stream?.Dispose();
            stream = null;
            client?.Dispose();
            client = null;
        }
    }
}