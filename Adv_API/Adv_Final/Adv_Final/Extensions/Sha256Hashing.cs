using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Adv_Final.Extensions
{
    public class Sha256Hashing
    {
        private static readonly string EncryptionKey = "MySecureEncryptionKey123"; // 24 bytes (but SHA-256 does not use a key directly)

        /// <summary>
        /// Generates a SHA-256 hash of the given plain text.
        /// </summary>
        public static string Hash(string plainText)
        {
            try
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] hashBytes = sha256.ComputeHash(inputBytes);

                    // Convert the hash bytes to a hexadecimal string
                    StringBuilder hashStringBuilder = new StringBuilder();
                    foreach (byte b in hashBytes)
                    {
                        hashStringBuilder.Append(b.ToString("x2")); // Convert byte to hexadecimal string
                    }

                    return hashStringBuilder.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hashing error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Verifies if the plain text matches the given hash.
        /// </summary>
        public static bool Verify(string plainText, string hashedText)
        {
            try
            {
                string computedHash = Hash(plainText);
                return string.Equals(computedHash, hashedText, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}
