// Adam Dernis 2022

using System.Threading.Tasks;
using Kayrun.API.Models.Keys;
using Kayrun.API.Models.Messages;
using Kayrun.Client;
using Kayrun.Client.Enums;
using Kayrun.Client.Helpers;
using Kayrun.Services.ChatStorageService;

namespace Kayrun.Services.MessengerService
{
    public class MessengerService : IMessengerService
    {
        private readonly IChatStorageService _chatStorageService;
        private readonly KayrunClient _client;

        public MessengerService(IChatStorageService chatStorageService, KayrunClient client)
        {
            _chatStorageService = chatStorageService;
            _client = client;
        }

        public async Task<RoE<KeyEntry, Error>> DownloadKey(string email)
        {
            return await _client.DownloadKey(email);
        }

        public async Task<Error> UploadKey(string email)
        {
            return await _client.UploadKey(email);
        }

        public async Task<RoE<string, Error>> GetMessage(string email)
        {
            return await _client.GetMessage(email);
        }

        public async Task<Error> SendMessage(string email, string plaintext)
        {
            var error = await _client.SendMessage(email, plaintext);

            if (error is Error.None)
            {
                await _chatStorageService.SaveMessage(new OutgoingMessage
                {
                    Email = email,
                    Content = plaintext,
                });
            }

            return error;
        }
    }
}
