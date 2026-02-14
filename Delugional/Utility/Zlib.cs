using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Delugional.Utility
{
    public static class Zlib
    {
        public static string InflateString(byte[] bytes)
        {
            return InflateString(bytes, Encoding.Default);
        }

        public static string InflateString(byte[] bytes, int offset, int length)
        {
            return InflateString(bytes, offset, length, Encoding.Default);
        }

        public static string InflateString(byte[] bytes, Encoding encoding)
        {
            return InflateString(bytes, 0, bytes.Length, encoding);
        }

        public static string InflateString(byte[] bytes, int offset, int length, Encoding encoding)
        {
            return encoding.GetString(Inflate(bytes, offset, length));
        }

        public static byte[] Inflate(byte[] bytes)
        {
            return Inflate(bytes, 0, bytes.Length);
        }

        public static byte[] Inflate(byte[] bytes, int offset, int length)
        {
            using (var streamIn = new MemoryStream(bytes, offset, length))
            using (var streamOut = new MemoryStream())
            using (var inflateStream = new ZLibStream(streamIn, CompressionMode.Decompress))
            {
                inflateStream.CopyTo(streamOut);
                return streamOut.ToArray();
            }

        }

        public static byte[] DeflateString(string s)
        {
            return DeflateString(s, Encoding.Default);
        }

        public static byte[] DeflateString(string s, Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(s);
            return Deflate(bytes);
        }

        public static byte[] Deflate(byte[] bytes)
        {
            return Deflate(bytes, 0, bytes.Length);
        }

        public static byte[] Deflate(byte[] bytes, int offset, int length)
        {
            using (var ms = new MemoryStream())
            {
                using (var deflateStream = new ZLibStream(ms, CompressionMode.Compress))
                {
                    deflateStream.Write(bytes, offset, length);

                    return ms.ToArray();
                }
            }
        }
    }
}
