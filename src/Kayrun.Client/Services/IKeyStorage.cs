using Kayrun.Client.Enums;
using Kayrun.Client.Helpers;
using Kayrun.Client.RSA;
using System.Threading.Tasks;

namespace Kayrun.Client.Services
{
    /// <summary>
    /// An interface for a class that stores and loads keys from storage.
    /// </summary>
    public interface IKeyStorage
    {
        /// <summary>
        /// Gets a key from local storage.
        /// </summary>
        /// <param name="email">The email the key is associated with.</param>
        /// <returns>The key as loaded from storage, or an error.</returns>
        Task<RoE<string, Error>> LoadKey(string email);

        /// <summary>
        /// Gets the public key from storage.
        /// </summary>
        Task<RoE<string, Error>> LoadPublicKey();

        /// <summary>
        /// Gets the private key from storage.
        /// </summary>
        /// <param name="email">The email the private key must match.</param>
        Task<RoE<string, Error>> LoadPrivateKey(string email);

        /// <summary>
        /// Associates an email with the private key.
        /// </summary>
        /// <param name="email">The email to associate with the key.</param>
        /// <returns><see cref="Error.None"/> on success, or an error value if failed.</returns>
        Task<Error> AssociateToPrivateKey(string email);

        /// <summary>
        /// Stores a key in local storage.
        /// </summary>
        /// <param name="email">The email the key is associated with.</param>
        /// <param name="key">The key to store.</param>
        Task StoreKey(string email, string key);

        /// <summary>
        /// Stores a key pair.
        /// </summary>
        /// <param name="keyPair">The pair of keys to store.</param>
        Task StoreKeyPair(KeyPair keyPair);
    }
}
