using System;
using System.Security.Cryptography;
using System.Text;

namespace Initial.Api.Util
{
    public class CryptoHelper
    {
        public static class Algorithms
        {
            public static readonly HashAlgorithm MD5 = new MD5CryptoServiceProvider();

            public static readonly HashAlgorithm SHA1 = new SHA1Managed();

            public static readonly HashAlgorithm SHA256 = new SHA256Managed();

            public static readonly HashAlgorithm SHA384 = new SHA384Managed();

            public static readonly HashAlgorithm SHA512 = new SHA512Managed();
        }

        public static string Hash(string text, HashAlgorithm algorithm)
        {
            return BitConverter.ToString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", string.Empty);
        }

        public static string Hash(string text)
        {
            var algorithm = new MD5CryptoServiceProvider();

            return Hash(text, algorithm);
        }

        public static Guid Guid(string text)
        {
            if (text == null)
                text = string.Empty;

            var bytes = Encoding.Default.GetBytes(text);

            var data = Algorithms.MD5.ComputeHash(bytes);

            return new Guid(data);
        }

        public static bool Compare(string text, Guid guid)
        {
            try
            {
                return Guid(text) == guid;
            }
            catch
            {
                return false;
            }
        }
    }
}
