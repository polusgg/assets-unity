using System.IO;
using System.Security.Cryptography;

namespace Assets.Editor
{
    public static class StreamExtensions
    {
        public static byte[] MD5Hash(this Stream stream)
        {
            var md5 = MD5.Create();
            return md5.ComputeHash(stream);
        }
    }
}