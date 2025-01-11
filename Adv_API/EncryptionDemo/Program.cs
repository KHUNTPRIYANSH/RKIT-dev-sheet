
using System;

namespace EncryptionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Run AES Demonstration
            AESHelper.RunAESDemo();

            // Run DES demo
            DESHelper.RunDESDemo();

            // Run 3DES demo
            TripleDESHelper.RunTripleDESDemo();

            // Run RSA demo
            RSAHelper.RunRSADemo();

            // Running the Rijndael demo
            RijndaelHelper.RunRijndaelDemo();
        }

    }
}
