// Adam Dernis 2022

using System.Threading.Tasks;
using Kayrun.API.Models.Messages;

namespace Kayrun.Services.MessageStorageService
{
    public interface IMessageStorageService
    {
        /// <summary>
        /// Creates a chat history for messages to be saved to and loaded from.
        /// </summary>
        /// <param name="email">The email of the chat.</param>
        Task CreateChat(string email);

        /// <summary>
        /// Loads a list of chats.
        /// </summary>
        Task<string[]> LoadChats();

        /// <summary>
        /// Loads message history with a certain email.
        /// </summary>
        /// <param name="email">The email to load messages for.</param>
        Task<MessageBase[]> LoadMessages(string email);

        /// <summary>
        /// Saves a message to history.
        /// </summary>
        /// <param name="message">The message to save.</param>
        Task SaveMessage(MessageBase message);
    }
}
