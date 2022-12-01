// Adam Dernis 2022

using System.Threading.Tasks;
using Kayrun.API.Models.Keys;
using Kayrun.Client.Enums;

namespace Kayrun.ViewModels.Services.MessengerService
{
    /// <summary>
    /// An interface for a service to send RESTful requests.
    /// </summary>
    public interface IMessengerService
    {
        /// <summary>
        /// Gets a key from the messaging server.
        /// </summary>
        /// <param name="email">The email to get the associated key for.</param>
        /// <returns>The requested key entry, if available.</returns>
        Task<RoE<KeyEntry, Error>> GetKey(string email);

        /// <summary>
        /// Sets the key for an email on the messaging server.
        /// </summary>
        /// <param name="email">The email the key is associated with.</param>
        /// <returns><see cref="Error.None"/> on success, or an error value if failed.</returns>
        Task<Error> SetKey(string email);

        /// <summary>
        /// Gets a message from the messaging server.
        /// </summary>
        /// <param name="email">The email the message is addressed to.</param>
        /// <returns>The content of the message sent to the requested email or an error.</returns>
        Task<RoE<string, Error>> GetMessage(string email);

        /// <summary>
        /// Sends a message to the messaging server.
        /// </summary>
        /// <param name="email">The email the message to send the message to.</param>
        /// <param name="plaintext">The content of the message to send.</param>
        /// <returns><see cref="Error.None"/> on success, or an error value if failed.</returns>
        Task<Error> SendMessage(string email, string plaintext);
    }
}