// Adam Dernis 2022

using CommunityToolkit.Diagnostics;
using Kayrun.API.Models.Messages;
using Kayrun.Services.StorageService;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kayrun.Services.MessageStorageService
{
    public class MessageStorageService : IMessageStorageService
    {
        private readonly IStorageService _storageService;

        public MessageStorageService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        /// <inheritdoc/>
        public async Task CreateChat(string email)
        {
            await _storageService.CreateFile(Pathify(email));
        }

        /// <inheritdoc/>
        public async Task<string[]> LoadChats()
        {
            var logs = await _storageService.QueryType(".log");
            return logs;
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
            var messages = await LoadMessages(email);
            messages.ToList().Add(message);

            // Save updated file
            await _storageService.SaveAsync(email, messages);
        }

        private string Pathify(string email)
            => $"{email}.log";
    }
}
