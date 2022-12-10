// Adam Dernis 2022

using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.Messaging;
using Kayrun.API.Models.Messages;
using Kayrun.Bindables.Chats;
using Kayrun.Client;
using Kayrun.Services.MessengerService;
using Kayrun.Services.StorageService;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kayrun.Services.ChatStorageService
{
    public class ChatStorageService : IChatStorageService
    {
        private readonly IStorageService _storageService;
        private readonly IMessenger _messenger;
        private readonly IMessengerService _messengerService;
        private readonly KayrunClient _kayrunClient;

        public ChatStorageService(
            IStorageService storageService,
            IMessenger messenger,
            IMessengerService messengerService,
            KayrunClient kayrunClient)
        {
            _messenger = messenger;
            _messengerService = messengerService;
            _storageService = storageService;
            _kayrunClient = kayrunClient;
        }

        /// <inheritdoc/>
        public async Task<BindableOutgoingChat> CreateOutgoingChat(string email)
        {
            await _storageService.CreateFile(Pathify(email));
            return InitOutgoingChat(email);
        }

        public async Task<BindableOutgoingChat[]> LoadOutgoingChats()
        {
            var logs = await _storageService.QueryType(".log");
            return logs.Select(InitOutgoingChat).ToArray();
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

        private BindableOutgoingChat InitOutgoingChat(string email)
            => new(_messenger, _messengerService, _kayrunClient, email);
    }
}
