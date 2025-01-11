using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionDemo
{
    /// <summary>
    /// Helper class for RSA encryption and decryption.
    /// </summary>
    public class RSAHelper
    {
        #region RSA Key Generation

        /// <summary>
        /// This method generates a new RSA key pair, which includes both the public key and private key.
        /// </summary>
        /// <param name="keySize">The size of the RSA key in bits (e.g., 2048 or 4096).</param>
        /// <returns>The RSA parameters containing the public and private keys.</returns>
        public static RSAParameters GenerateRSAKeyPair(int keySize = 2048)
        {
            //We use RSACryptoServiceProvider, a built-in class in C#, which generates RSA key pairs.
            using (var rsa = new RSACryptoServiceProvider(keySize))
            {
                // The ExportParameters(true) method gives us the key pair.
                // The true means both the public and private keys will be included.
                return rsa.ExportParameters(true);
            }
        }

        #endregion

        #region RSA Encryption

        /// <summary>
        /// Encrypts plaintext using RSA public key encryption.
        /// </summary>
        /// <param name="plainText">The plaintext to encrypt.</param>
        /// <param name="publicKey">The RSA public key to use for encryption.</param>
        /// <returns>The encrypted text as a Base64-encoded string.</returns>
        public static string Encrypt(string plainText, RSAParameters publicKey)
        {
            if (string.IsNullOrEmpty(plainText)) throw new ArgumentException("Plaintext cannot be null or empty.");

            // we create a new instance of RSACryptoServiceProvider,
            // which will allow us to use RSA encryption.
            using (var rsa = new RSACryptoServiceProvider())
            {
                //rsa.ImportParameters(publicKey) loads the public key into the RSA object
                //so that it can be used for encryption.
                rsa.ImportParameters(publicKey);

                // We convert the plain text into a byte array using Encoding.UTF8.GetBytes(plainText)
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

                // OAEP (Optimal Asymmetric Encryption Padding): A more secure and modern padding scheme.
                // Better than PKCS7 which i have used in AES
                // because it combines hashing and padding to prevent certain types of attacks,
                // such as chosen ciphertext attacks
                byte[] encryptedBytes = rsa.Encrypt(plainBytes, RSAEncryptionPadding.OaepSHA256);

                return Convert.ToBase64String(encryptedBytes);
            }
        }

        #endregion

        #region RSA Decryption

        /// <summary>
        /// Decrypts a Base64-encoded encrypted text using RSA private key decryption.
        /// </summary>
        /// <param name="cipherText">The Base64-encoded encrypted text.</param>
        /// <param name="privateKey">The RSA private key to use for decryption.</param>
        /// <returns>The decrypted plaintext.</returns>
        public static string Decrypt(string cipherText, RSAParameters privateKey)
        {
            if (string.IsNullOrEmpty(cipherText)) throw new ArgumentException("Ciphertext cannot be null or empty.");

            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(privateKey); // Load private key

                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                byte[] decryptedBytes = rsa.Decrypt(cipherBytes, RSAEncryptionPadding.OaepSHA256);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        #endregion

        #region RSA Demo

        /// <summary>
        /// Demonstrates RSA encryption and decryption.
        /// </summary>
        public static void RunRSADemo()
        {
            Console.WriteLine("RSA Encryption/Decryption Demonstration");
            Console.WriteLine("-----------------------------------------------------------");

            // Step 1: Define plaintext
            string plainText = "Hello, RSA Encryption!";

            // Step 2: Generate RSA key pair (public and private keys)
            RSAParameters rsaKeyPair = GenerateRSAKeyPair(2048);  // 2048-bit key

            // Step 3: Encrypt plaintext using the public key
            string encryptedText = Encrypt(plainText, rsaKeyPair);
            Console.WriteLine("Encrypted Text (Base64): " + encryptedText);

            // Step 4: Decrypt ciphertext using the private key
            string decryptedText = Decrypt(encryptedText, rsaKeyPair);
            Console.WriteLine("Decrypted Text: " + decryptedText);

            Console.WriteLine("-----------------------------------------------------------");
        }

        #endregion
    }

}
