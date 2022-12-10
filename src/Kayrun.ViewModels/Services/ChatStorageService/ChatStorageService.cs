// Adam Dernis 2022

using CommunityToolkit.Diagnostics;
using Kayrun.API.Models.Messages;
using Kayrun.Services.StorageService;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kayrun.Services.ChatStorageService
{
    public class ChatStorageService : IChatStorageService
    {
        private readonly IStorageService _storageService;

        public ChatStorageService(
            IStorageService storageService)
        {
            _storageService = storageService;
        }

        /// <inheritdoc/>
        public async Task CreateOutgoingChat(string email)
        {
            await _storageService.CreateFile(Pathify(email));
        }

        public async Task<string[]> LoadOutgoingChats()
        {
            return await _storageService.QueryType(".log");
        }

        /// <inheritdoc/>
        public async Task<MessageBase[]> LoadMessages(string email)
        {
            var messages = await _storageService.LoadAsync<MessageBase[]>(Pathify(email));
            return messages ?? Array.Empty<MessageBase>();
        }

        /// <inheritdoc/>
        public async Task SaveMessage(MessageBase message)
        {
            // Get email
            var email = message.Email;
            Guard.IsNotNull(email);

            // Append new message
            // This is super inefficient...
            //var messages = await LoadMessages(email);
            //messages.ToList().Add(message);

            var messages = new[] { message };

            // Save updated file
            await _storageService.SaveAsync(Pathify(email), messages);
        }

        private string Pathify(string email)
            => $"{email}.log";
    }
}
