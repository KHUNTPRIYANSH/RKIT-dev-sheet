using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionDemo
{
    public class DESHelper
    {
        /// <summary>
        /// Generates a secure random key of the specified size in bytes.
        /// </summary>
        public static byte[] GenerateRandomKey(int size)
        {
            byte[] key = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            return key;
        }

        /// <summary>
        /// Encrypts plaintext using DES with the provided key and IV.
        /// </summary>
        public static string Encrypt(string plainText, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(plainText)) throw new ArgumentException("Plaintext cannot be null or empty.");
            if (key == null || key.Length == 0) throw new ArgumentException("Key cannot be null or empty.");
            if (iv == null || iv.Length == 0) throw new ArgumentException("IV cannot be null or empty.");

            using (var des = DES.Create())
            {
                des.Key = key;
                des.IV = iv;
                using (var encryptor = des.CreateEncryptor())
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    cs.Write(plainBytes, 0, plainBytes.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// Decrypts a Base64-encoded encrypted text using DES with the provided key and IV.
        /// </summary>
        public static string Decrypt(string cipherText, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(cipherText)) throw new ArgumentException("Ciphertext cannot be null or empty.");
            if (key == null || key.Length == 0) throw new ArgumentException("Key cannot be null or empty.");
            if (iv == null || iv.Length == 0) throw new ArgumentException("IV cannot be null or empty.");

            using (var des = DES.Create())
            {
                des.Key = key;
                des.IV = iv;
                using (var decryptor = des.CreateDecryptor())
                using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    byte[] plainBytes = new byte[ms.Length];
                    int bytesRead = cs.Read(plainBytes, 0, plainBytes.Length);
                    return Encoding.UTF8.GetString(plainBytes, 0, bytesRead);
                }
            }
        }

        /// <summary>
        /// Demonstrates DES encryption and decryption.
        /// </summary>
        public static void RunDESDemo()
        {
            Console.WriteLine("DES Encryption/Decryption Demonstration");
            Console.WriteLine("-----------------------------------------------------------");

            string plainText = "Hello, DES Encryption!";
            byte[] key = GenerateRandomKey(8);  // DES uses 8-byte keys
            byte[] iv = GenerateRandomKey(8);   // IV is also 8 bytes for DES

            Console.WriteLine("Generated Key (Base64): " + Convert.ToBase64String(key));
            Console.WriteLine("Generated IV (Base64): " + Convert.ToBase64String(iv));

            string encryptedText = Encrypt(plainText, key, iv);
            Console.WriteLine("Encrypted Text (Base64): " + encryptedText);

            string decryptedText = Decrypt(encryptedText, key, iv);
            Console.WriteLine("Decrypted Text: " + decryptedText);

            Console.WriteLine("-----------------------------------------------------------");
        }
    }

    public class TripleDESHelper
    {
        /// <summary>
        /// Generates a secure random key for Triple DES (3DES).
        /// </summary>
        public static byte[] GenerateRandomKey(int size)
        {
            byte[] key = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            return key;
        }

        /// <summary>
        /// Encrypts plaintext using Triple DES with the provided key and IV.
        /// </summary>
        public static string Encrypt(string plainText, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(plainText)) throw new ArgumentException("Plaintext cannot be null or empty.");
            if (key == null || key.Length == 0) throw new ArgumentException("Key cannot be null or empty.");
            if (iv == null || iv.Length == 0) throw new ArgumentException("IV cannot be null or empty.");

            using (var tripleDES = TripleDES.Create())
            {
                tripleDES.Key = key;
                tripleDES.IV = iv;
                using (var encryptor = tripleDES.CreateEncryptor())
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    cs.Write(plainBytes, 0, plainBytes.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// Decrypts a Base64-encoded encrypted text using Triple DES with the provided key and IV.
        /// </summary>
        public static string Decrypt(string cipherText, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(cipherText)) throw new ArgumentException("Ciphertext cannot be null or empty.");
            if (key == null || key.Length == 0) throw new ArgumentException("Key cannot be null or empty.");
            if (iv == null || iv.Length == 0) throw new ArgumentException("IV cannot be null or empty.");

            using (var tripleDES = TripleDES.Create())
            {
                tripleDES.Key = key;
                tripleDES.IV = iv;
                using (var decryptor = tripleDES.CreateDecryptor())
                using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    byte[] plainBytes = new byte[ms.Length];
                    int bytesRead = cs.Read(plainBytes, 0, plainBytes.Length);
                    return Encoding.UTF8.GetString(plainBytes, 0, bytesRead);
                }
            }
        }

        /// <summary>
        /// Demonstrates Triple DES (3DES) encryption and decryption.
        /// </summary>
        public static void RunTripleDESDemo()
        {
            Console.WriteLine("Triple DES (3DES) Encryption/Decryption Demonstration");
            Console.WriteLine("-----------------------------------------------------------");

            string plainText = "Hello, Triple DES Encryption!";
            byte[] key = GenerateRandomKey(24);  // 3DES uses 24-byte keys (3 x 8-byte keys)
            byte[] iv = GenerateRandomKey(8);    // IV is 8 bytes for 3DES

            Console.WriteLine("Generated Key (Base64): " + Convert.ToBase64String(key));
            Console.WriteLine("Generated IV (Base64): " + Convert.ToBase64String(iv));

            string encryptedText = Encrypt(plainText, key, iv);
            Console.WriteLine("Encrypted Text (Base64): " + encryptedText);

            string decryptedText = Decrypt(encryptedText, key, iv);
            Console.WriteLine("Decrypted Text: " + decryptedText);

            Console.WriteLine("-----------------------------------------------------------");
        }
    }
}
