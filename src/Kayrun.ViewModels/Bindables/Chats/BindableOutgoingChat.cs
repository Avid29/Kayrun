// Adam Dernis 2022

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Kayrun.Bindables.Chats.Abstract;
using Kayrun.Client;
using Kayrun.Services.MessengerService;

namespace Kayrun.Bindables.Chats
{
    public class BindableOutgoingChat : BindableChat
    {
        public BindableOutgoingChat(
            IMessenger messenger,
            IMessengerService messengerService,
            KayrunClient kayrunClient,
            string email) : base(messenger, messengerService, kayrunClient, email)
        {
            RefreshPublicKeyCommand = new RelayCommand(RefreshPublicKey);
        }

        public RelayCommand RefreshPublicKeyCommand { get; }

        public void RefreshPublicKey()
        {
            _messengerService.DownloadKey(Email);
        }
    }
}
