// Adam Dernis 2022

using Kayrun.API.Models.Messages;
using Kayrun.Bindables.Chats;
using System.Threading.Tasks;

namespace Kayrun.Services.ChatStorageService
{
    public interface IChatStorageService
    {
        /// <summary>
        /// Creates a chat history for messages to be saved to and loaded from.
        /// </summary>
        /// <param name="email">The email of the chat.</param>
        Task<BindableOutgoingChat> CreateOutgoingChat(string email);

        /// <summary>
        /// Loads a list of outgoing chats.
        /// </summary>
        Task<BindableOutgoingChat[]> LoadOutgoingChats();

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
