using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EncryptionDemo
{
    public class AESHelper
    {
        /// <summary>
        /// Generates a secure random key of the specified size in bytes.
        /// </summary>
        /// <param name="size">Key size in bytes (e.g., 16 for 128-bit, 32 for 256-bit).</param>
        /// <returns>Randomly generated key as a byte array.</returns>
        public static byte[] GenerateRandomKey(int size)
        {
            // Create a byte array to hold the key
            byte[] key = new byte[size];

            //RandomNumberGenerator.Create() creates a random number generator
            //that is very secure (better than simple random functions like Random())
            // Fill the array with secure random bytes
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }

            return key;
        }

        /// <summary>
        /// Encrypts plaintext using AES with the provided key and IV.
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <param name="key">The AES key.</param>
        /// <param name="iv">The AES initialization vector.</param>
        /// <returns>Base64-encoded encrypted text.</returns>
        public static string Encrypt(string plainText, byte[] key, byte[] iv)
        {
            // [PreCheck] Check input validity
            if (string.IsNullOrEmpty(plainText)) throw new ArgumentException("Plaintext cannot be null or empty.");
            if (key == null || key.Length == 0) throw new ArgumentException("Key cannot be null or empty.");
            if (iv == null || iv.Length == 0) throw new ArgumentException("IV cannot be null or empty.");

            // Perform AES encryption
            // We are creating obj of Aes using Aes.Create() from System.Security.Cryptography namespace.
            using (var aes = Aes.Create())
            {
                // filling key and iv to aes obj
                aes.Key = key;
                aes.IV = iv;

                // Create an encryptor
                // aes.CreateEncryptor() creates a "machine" (encryptor)
                // that knows how to turn plain text into encrypted text
                // based on the key and IV.

                // A MemoryStream is a temporary place to hold data while we work on it

                // A CryptoStream is like a pipeline that encrypts the data as it flows into the memory.
                // We convert the plain text into bytes(using Encoding.UTF8.GetBytes())
                // and feed it through the crypto stream
                using (var encryptor = aes.CreateEncryptor())
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    // Convert plaintext to bytes
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

                    // Encrypt the data
                    // cs.write(bytes[] , offset(from where to start) , length);
                    cs.Write(plainBytes, 0, plainBytes.Length);
                    // clearing buffer as encryption is done
                    cs.FlushFinalBlock();

                    // Return encrypted data as Base64 string
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// Decrypts a Base64-encoded encrypted text using AES with the provided key and IV.
        /// </summary>
        /// <param name="cipherText">The Base64-encoded encrypted text.</param>
        /// <param name="key">The AES key.</param>
        /// <param name="iv">The AES initialization vector.</param>
        /// <returns>The decrypted plaintext.</returns>
        public static string Decrypt(string cipherText, byte[] key, byte[] iv)
        {
            // Check input validity
            if (string.IsNullOrEmpty(cipherText)) throw new ArgumentException("Ciphertext cannot be null or empty.");
            if (key == null || key.Length == 0) throw new ArgumentException("Key cannot be null or empty.");
            if (iv == null || iv.Length == 0) throw new ArgumentException("IV cannot be null or empty.");

            // Perform AES decryption
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                // Create a decryptor
                using (var decryptor = aes.CreateDecryptor())
                using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    // Decrypt the data
                    byte[] plainBytes = new byte[ms.Length];
                    int bytesRead = cs.Read(plainBytes, 0, plainBytes.Length);

                    // Return the decrypted data as a string
                    return Encoding.UTF8.GetString(plainBytes, 0, bytesRead);
                }
            }
        }

        /// <summary>
        /// Demonstrates AES encryption and decryption.
        /// </summary>
        public static void RunAESDemo()
        {
            Console.WriteLine("AES Encryption/Decryption Demonstration");
            Console.WriteLine("-----------------------------------------------------------");

            // Step 1: Define plaintext
            string plainText = "Hello, AES Encryption!";

            // Step 2: Generate AES key and IV
            // AES can use three different key sizes:

            // 128 - bit key(16 bytes) : 10 rounds
            // 192 - bit key(24 bytes) : 12 rounds
            // 256 - bit key(32 bytes) : 14 rounds
            // if we using 32 byte key its stronger but as no. of rounds are more
            // it will take more time to encrypt or decrypt
            byte[] key = GenerateRandomKey(16);

            // The IV size must always be 128 bits (16 bytes), regardless of the key size.
            // coz IV size == ip block size of AES (128 bit / 16 bytes / 4 words)
            byte[] iv = GenerateRandomKey(16);  // 128-bit IV

            Console.WriteLine("Generated Key (Base64): " + Convert.ToBase64String(key));
            Console.WriteLine("Generated IV (Base64): " + Convert.ToBase64String(iv));

            // Step 3: Encrypt plaintext
            string encryptedText = Encrypt(plainText, key, iv);
            Console.WriteLine("Encrypted Text: " + encryptedText);

            // Step 4: Decrypt ciphertext
            string decryptedText = Decrypt(encryptedText, key, iv);
            Console.WriteLine("Decrypted Text: " + decryptedText);


            Console.WriteLine("-----------------------------------------------------------");

            // Modes of Operation
            
            // ECB (Electronic Codebook): Simple, not recommended (same key for all blocks, no IV). Vulnerable to patterns.
            // aes.Mode = CipherMode.ECB; 

            // CBC (Cipher Block Chaining): Secure, requires IV (Initialization Vector). Recommended for most uses.
            // aes.Mode = CipherMode.CBC;  
            // this is default 

            // CFB (Cipher Feedback) : Turns block cipher into a stream cipher. Good for real-time encryption.
            // aes.Mode = CipherMode.CFB;  

            

            // Padding
            
            // PKCS7: Common padding method. Adds extra bytes to fit data into block size.
            // aes.Padding = PaddingMode.PKCS7;  
            // Default and widely used

            // NoPadding: Data must fit exactly into block size. Throws error if not.
            // aes.Padding = PaddingMode.None;  
            // Use only when data is exact size

        }
    }
}
