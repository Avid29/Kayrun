// Adam Dernis 2022

using System;
using System.Numerics;
using System.Text;

namespace Kayrun.Client.RSA
{
    /// <summary>
    /// A class of helper methods to encrypt or decrypt a message using RSA keys.
    /// </summary>
    public static class RSAEncryption
    {
        /// <summary>
        /// Encrypts a plaintext message.
        /// </summary>
        /// <param name="plaintext">The plaintext string of the message.</param>
        /// <param name="key">The public key to use for encryption.</param>
        /// <returns>A base64 ciphertext string.</returns>
        public static string Encrypt(string plaintext, string key)
        {
            var bytes = Encoding.UTF8.GetBytes(plaintext);
            var encrypted = EncryptDecryptImpl(bytes, key);
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Decrypts a ciphertext message.
        /// </summary>
        /// <param name="ciphertext">The base64 ciphertext string.</param>
        /// <param name="key">The private key to use for decryption.</param>
        /// <returns>A utf8 plaintext string.</returns>
        public static string Decrypt(string ciphertext, string key)
        {
            var bytes = Convert.FromBase64String(ciphertext);
            var decrypted = EncryptDecryptImpl(bytes, key);
            return Encoding.UTF8.GetString(decrypted);
        }

        private static byte[] EncryptDecryptImpl(byte[] bytes, string key)
        {
            // Variables are named for encryption,
            // but the operation is equivalent in decryption
            var k = Key.FromBase64(key);
            var m = new BigInteger(bytes);
            var c = BigInteger.ModPow(m, k.E, k.N);
            return c.ToByteArray();
        }
    }
}
