using System;
using System.Security.Cryptography;
using System.Text;

namespace Initial.Api.Util
{
    /// <summary>
    /// Classe de cripografia
    /// </summary>
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

        /// <summary>
        /// Transforma um texto em uma Hash MD5
        /// </summary>
        public static string Hash(string text, HashAlgorithm algorithm)
        {
            return BitConverter
                .ToString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", string.Empty);
        }

        /// <summary>
        /// Transforma um texto em uma Hash MD5
        /// </summary>
        public static string Hash(string text)
        {
            return Hash(text, Algorithms.MD5);
        }

        /// <summary>
        /// Transforma um texto em uma Guid MD5
        /// </summary>
        public static Guid Guid(string text)
        {
            if (text == null)
                text = string.Empty;

            var bytes = Encoding.Default.GetBytes(text);

            var data = Algorithms.MD5.ComputeHash(bytes);

            return new Guid(data);
        }

        /// <summary>
        /// Compara se um texto é equivalente a uma Guid MD5
        /// </summary>
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
