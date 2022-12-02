// Adam Dernis 2022

using CommunityToolkit.Diagnostics;
using Kayrun.API.Models.Keys;
using Kayrun.API.Models.Messages;
using Kayrun.Client.Enums;
using Kayrun.Client.Helpers;
using Kayrun.Client.RSA;
using Refit;
using System.Threading.Tasks;

namespace Kayrun.Client
{
    public partial class KayrunClient
    {
        /// <summary>
        /// Gets a key from the messaging server.
        /// </summary>
        /// <remarks>
        /// The key will automatically be stored after download.
        /// </remarks>
        /// <param name="email">The email to get the associated key for.</param>
        /// <returns>The requested key entry, if available.</returns>
        public async Task<RoE<KeyEntry, Error>> DownloadKey(string email)
        {
            try
            {
                // Download key
                var key = await _restKeyService.GetKey(email);

                // Guards
                Guard.IsNotNull(key);
                Guard.IsNotNull(key.Email);
                Guard.IsEqualTo(key.Email, email);

                // Store and return key
                await _keyStorage.StoreKey(key.Email, key.Key);
                return key;
            }
            catch (ApiException)
            {
                // Catch RESTful exceptions
                return Error.RESTfulError;
            }
            catch
            {
                // Catch other exceptions
                return Error.Unknown;
            }
        }

        /// <summary>
        /// Sets the key for an email on the messaging server.
        /// </summary>
        /// <param name="email">The email the key is associated with.</param>
        /// <returns><see cref="Error.None"/> on success, or an error value if failed.</returns>
        public async Task<Error> UploadKey(string email)
        {
            try
            {
                // Load public key
                var roE = await _keyStorage.LoadPublicKey();
                if (!roE.Success) return Error.MissingKey;
                var key = roE.Result; 
                Guard.IsNotNull(key);

                // Create and send entry
                var entry = new KeyEntry { Email = email, Key = key };
                await _restKeyService.SetKey(email, entry);
                return Error.None;
            }
            catch (ApiException)
            {
                // Catch RESTful exceptions
                return Error.RESTfulError;
            }
            catch
            {
                // Catch other exceptions
                return Error.Unknown;
            }
        }

        /// <summary>
        /// Gets a message from the messaging server.
        /// </summary>
        /// <param name="email">The email the message is addressed to.</param>
        /// <returns>The content of the message sent to the requested email or an error.</returns>
        public async Task<RoE<string, Error>> GetMessage(string email)
        {
            try
            {
                // Download message
                var message = await _restMessageService.GetMessage(email);
                if (message.Content is null) return Error.MissingMessage;

                // Load key
                var roe = await _keyStorage.LoadPrivateKey(message.Content);
                if (!roe.Success)
                {
                    return roe.Error;
                }

                var key = roe.Result;
                Guard.IsNotNull(key);

                // Decrypt
                return RSAEncryption.Decrypt(message.Content, key);
            }
            catch (ApiException)
            {
                // Catch RESTful exceptions
                return Error.RESTfulError;
            }
            catch
            {
                // Catch other exceptions
                return Error.Unknown;
            }
        }

        /// <summary>
        /// Sends a message to the messaging server.
        /// </summary>
        /// <param name="email">The email the message to send the message to.</param>
        /// <param name="plaintext">The content of the message to send.</param>
        /// <returns><see cref="Error.None"/> on success, or an error value if failed.</returns>
        public async Task<Error> SendMessage(string email, string plaintext)
        {
            try
            {
                // Load key
                var roe = await _keyStorage.LoadKey(email);
                if (!roe.Success) return roe.Error;
                var key = roe.Result;
                Guard.IsNotNull(key);

                // Encrypt text and construct payload
                var ciphertext = RSAEncryption.Encrypt(plaintext, key);
                var payload = new OutgoingMessage
                {
                    Email = email,
                    Content = ciphertext,
                };

                // Send message
                await _restMessageService.SendMessage(email, payload);
                return Error.None;
            }
            catch (ApiException)
            {
                // Catch RESTful exceptions
                return Error.RESTfulError;
            }
            catch
            {
                // Catch other exceptions
                return Error.Unknown;
            }
        }
    }
}
