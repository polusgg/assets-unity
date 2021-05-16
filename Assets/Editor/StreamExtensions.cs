using System;
using System.IO;
using System.Security.Cryptography;

namespace Assets.Editor
{
    public static class StreamExtensions
    {
        public static string SHA256Hash(this Stream stream)
        {
            var sha256hash = SHA256.Create().ComputeHash(stream);
            return BitConverter.ToString(sha256hash).Replace("-", "");
        }
    }
}