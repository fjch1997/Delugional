using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace Delugional.Rpc
{
    public abstract class DelugeRpc : IDisposable
    {
        private readonly Dictionary<int, TaskCompletionSource<RpcMessage>> tasks = new Dictionary<int, TaskCompletionSource<RpcMessage>>();
        private ExceptionDispatchInfo exception;
        internal DelugeRpc(DelugeRpcConnection connection)
        {
            if (!connection.IsOpen)
                throw new InvalidOperationException("Connection not open");

            Connection = connection;

            BeginReceiving();
        }

        public DelugeRpcConnection Connection { get; }

        public void Close()
        {
            Connection.Close();
        }

        private RpcResponse CheckResponse(RpcMessage result)
        {
            var error = result as RpcError;
            if (error != null)
                throw new RpcErrorException(error);

            return (RpcResponse)result;
        }

        private IEnumerable<RpcResponse> CheckResponses(RpcMessage[] messages)
        {
            var exceptions = new List<Exception>();
            var responses = new List<RpcResponse>();

            foreach (RpcMessage message in messages)
            {
                try
                {
                    RpcResponse response = CheckResponse(message);
                    responses.Add(response);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Count == 1)
                throw exceptions[0];
            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            return responses;
        }

        private async void BeginReceiving()
        {
            try
            {
                while (Connection.IsOpen)
                {
                    RpcMessage[] messages = await Connection.Receive();

                    foreach (RpcMessage message in messages)
                    {
                        if (message.Type == MessageType.Response || message.Type == MessageType.Error)
                        {
                            if (tasks.TryGetValue(message.Id, out var task))
                            {
                                task.SetResult(message);
                            }
                        }
                        else if (message.Type == MessageType.Event)
                        {
                            // TODO Events
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Unknown errors fail all pending and subsequent calls of the RPC client.
                exception = ExceptionDispatchInfo.Capture(ex);
                foreach (var task in tasks.Values)
                {
                    task.SetException(ex);
                }
            }
        }

        protected Task<object> CallAsync(string method, params object[] args)
        {
            return CallAsync(method, null, args);
        }

        protected Task<object> CallAsync(int id, string method, params object[] args)
        {
            return CallAsync(method, null, args);
        }

        protected Task<object> CallAsync(string method, IDictionary<string, object> kwargs, params object[] args)
        {
            return CallAsync(IdGenerator.Default.Next(), method, kwargs, args);
        }

        protected Task<object> CallAsync(int id, string method, IDictionary<string, object> kwargs, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(method))
                throw new ArgumentException("Argument is null or whitespace", nameof(method));

            var request = new RpcRequest(id, method, args, kwargs);
            return CallAsync(request);
        }

        protected async Task<object> CallAsync(RpcRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            object[] results = await CallAsync(new[] { request });
            return results.First();
        }

        protected Task<object[]> CallAsync(params RpcRequest[] requests)
        {
            return CallAsync((IEnumerable<RpcRequest>)requests);
        }

        protected async Task<object[]> CallAsync(IEnumerable<RpcRequest> requests)
        {
            if (exception != null)
                exception.Throw();
            if (requests == null)
                throw new ArgumentNullException(nameof(requests));

            var requestsArray = requests.ToArray();

            if (!requests.Any())
                throw new ArgumentException("Argument is empty collection", nameof(requests));

            if (requests.Any(request => request == null))
                throw new ArgumentException("Argument contains null items", nameof(requests));

            var tcss = new TaskCompletionSource<RpcMessage>[requestsArray.Length];
            try
            {
                for (int i = 0; i < requestsArray.Length; i++)
                {
                    tcss[i] = new TaskCompletionSource<RpcMessage>();
                    tasks[requestsArray[i].Id] = tcss[i];
                }

                await Connection.Send(requests);
                CancellationTokenSource cts;
                if (Debugger.IsAttached)
                {
                    // No timeout while debugging.
                    cts = new CancellationTokenSource();
                }
                else
                {
                    cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                }
                try
                {
                    RpcMessage[] messages = await Task.WhenAll(tcss.Select(tcs => tcs.Task)).WaitAsync(cts.Token);

                    IEnumerable<RpcResponse> responses = CheckResponses(messages);

                    return responses.Select(response => response.Data).ToArray();
                }
                catch (OperationCanceledException)
                {
                    throw new TimeoutException("Deluge RPC call timed out.");
                }
            }
            finally
            {
                foreach (RpcRequest request in requestsArray)
                {
                    tasks.Remove(request.Id);
                }
            }
        }

        public async Task<AuthLevels> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Argument is null or whitespace", nameof(username));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Argument is null or whitespace", nameof(password));

            return (AuthLevels)await CallAsync("daemon.login", new Dictionary<string, object> { { "client_version", "2.2.0" } }, username, password);
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}