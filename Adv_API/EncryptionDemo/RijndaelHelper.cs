using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionDemo
{
    /// <summary>
    /// Rijndael encryption in C# using the built-in RijndaelManaged class, 
    /// which is part of the System.Security.Cryptography namespace. 
    /// It works similarly to AES but can use different block sizes and key sizes. 
    /// AES is essentially a restricted version of Rijndael with a fixed block size of 128 bits (16 bytes)
    /// but Rijndael supports block sizes of 128, 192, or 256 bits.
    /// </summary>

    public class RijndaelHelper
    {
        /// <summary>
        /// Generates a secure random key of the specified size in bytes.
        /// </summary>
        /// <param name="size">Key size in bytes (e.g., 16 for 128-bit, 32 for 256-bit).</param>
        /// <returns>Randomly generated key as a byte array.</returns>
        public static byte[] GenerateRandomKey(int size)
        {
            byte[] key = new byte[size];
            using (var rng = RandomNumberGenerator.Create()) // Creates a secure random number generator.
            {
                rng.GetBytes(key); // Fill the array with random values.
            }
            return key;
        }

        /// <summary>
        /// Encrypts plaintext using Rijndael with the provided key and IV.
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <param name="key">The Rijndael key.</param>
        /// <param name="iv">The Rijndael initialization vector.</param>
        /// <param name="blockSize">Block size for encryption (128, 192, or 256 bits).</param>
        /// <returns>Base64-encoded encrypted text.</returns>
        public static string Encrypt(string plainText, byte[] key, byte[] iv, int blockSize)
        {
            if (string.IsNullOrEmpty(plainText)) throw new ArgumentNullException(nameof(plainText));
            if (key == null || key.Length == 0) throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length == 0) throw new ArgumentNullException(nameof(iv));

            using (var rijndael = new RijndaelManaged())
            {
                rijndael.Key = key; 
                rijndael.IV = iv; 
                
                // Set the custom block size (128, 192, or 256 bits).
                rijndael.BlockSize = blockSize; 

                using (var encryptor = rijndael.CreateEncryptor()) 
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
        /// Decrypts a Base64-encoded encrypted text using Rijndael with the provided key and IV.
        /// </summary>
        /// <param name="cipherText">The Base64-encoded encrypted text.</param>
        /// <param name="key">The Rijndael key.</param>
        /// <param name="iv">The Rijndael initialization vector.</param>
        /// <param name="blockSize">Block size for decryption (128, 192, or 256 bits).</param>
        /// <returns>The decrypted plaintext.</returns>
        public static string Decrypt(string cipherText, byte[] key, byte[] iv, int blockSize)
        {
            if (string.IsNullOrEmpty(cipherText)) throw new ArgumentNullException(nameof(cipherText));
            if (key == null || key.Length == 0) throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length == 0) throw new ArgumentNullException(nameof(iv));

            using (var rijndael = new RijndaelManaged())
            {
                rijndael.Key = key; 
                rijndael.IV = iv; 
                rijndael.BlockSize = blockSize; 

                using (var decryptor = rijndael.CreateDecryptor()) 
                using (var ms = new MemoryStream(Convert.FromBase64String(cipherText))) 
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read)) 
                {
                    byte[] plainBytes = new byte[ms.Length]; 
                    int count = cs.Read(plainBytes, 0, plainBytes.Length); 
                    return Encoding.UTF8.GetString(plainBytes, 0, count);
                }
            }
        }

        /// <summary>
        /// Demonstrates Rijndael encryption and decryption.
        /// </summary>
        public static void RunRijndaelDemo()
        {
            Console.WriteLine("Rijndael Encryption/Decryption Demonstration");
            Console.WriteLine("-----------------------------------------------------------");

            // Step 1: Define plaintext
            string plainText = "Hello, Rijndael Encryption with custom block size!";

            // Step 2: Generate Rijndael key and IV
            byte[] key = GenerateRandomKey(32); // 256-bit key for stronger encryption
            byte[] iv = GenerateRandomKey(16);  // 128-bit IV (required size for AES and Rijndael)

            // Step 3: Choose block size (128, 192, or 256)
            int blockSize = 128; // Set to 128, 192, or 256 bits based on your need

            Console.WriteLine("Generated Key (Base64): " + Convert.ToBase64String(key));
            Console.WriteLine("Generated IV (Base64): " + Convert.ToBase64String(iv));
            Console.WriteLine($"Block Size: {blockSize} bits");

            // Step 4: Encrypt plaintext
            string encryptedText = Encrypt(plainText, key, iv, blockSize);
            Console.WriteLine("Encrypted Text: " + encryptedText);

            // Step 5: Decrypt ciphertext
            string decryptedText = Decrypt(encryptedText, key, iv, blockSize);
            Console.WriteLine("Decrypted Text: " + decryptedText);

            Console.WriteLine("-----------------------------------------------------------");
        }
    }
}

