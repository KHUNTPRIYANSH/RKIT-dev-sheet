using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FinalDemo.Helpers
{
    public class EncryptionHelper
    {
        #region 

        /// <summary>
        /// Initialization Vector (IV) used for AES encryption.
        /// </summary>
        private static readonly string _iv = "parthsheladiya12";

        /// <summary>
        /// Key used for AES encryption.
        /// </summary>
        private static readonly string _key = "sheladiyaparth123456789098765432";

        /// <summary>
        /// AES (Advanced Encryption Standard) encryption object for secure password handling.
        /// </summary>
        private static readonly Aes _objAes;

        #endregion Private Fields

        #region Constructor

        /// <summary>
        /// Static constructor to initialize the static instance of the <see cref="BLEncryption"/>
        /// </summary>
        static EncryptionHelper()
        {
            _objAes = Aes.Create();

            _objAes.Key = Encoding.UTF8.GetBytes(_key);
            _objAes.IV = Encoding.UTF8.GetBytes(_iv);
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Encrypts a password using AES encryption.
        /// </summary>
        /// <param name="plaintext">The plaintext to be encrypted.</param>
        /// <returns>Encrypted ciphertext.</returns>
        public static string GetEncryptPassword(string plaintext)
        {
            ICryptoTransform encryptor = _objAes.CreateEncryptor(_objAes.Key, _objAes.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor,
                    CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plaintext);
                    }
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }

        #endregion
    }
}