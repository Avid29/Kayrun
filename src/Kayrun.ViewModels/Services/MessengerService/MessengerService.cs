// Adam Dernis 2022

using System.Threading.Tasks;
using Kayrun.API.Models.Keys;
using Kayrun.Client;
using Kayrun.Client.Enums;
using Kayrun.Client.Helpers;

namespace Kayrun.ViewModels.Services.MessengerService
{
    public class MessengerService : IMessengerService
    {
        private readonly KayrunClient _client;

        public MessengerService(KayrunClient client)
        {
            _client = client;
        }

        public Task<RoE<KeyEntry, Error>> DownloadKey(string email)
        {
            return _client.DownloadKey(email);
        }

        public Task<Error> UploadKey(string email)
        {
            return _client.UploadKey(email);
        }

        public Task<RoE<string, Error>> GetMessage(string email)
        {
            return _client.GetMessage(email);
        }

        public Task<Error> SendMessage(string email, string plaintext)
        {
            return _client.SendMessage(email, plaintext);
        }
    }
}
