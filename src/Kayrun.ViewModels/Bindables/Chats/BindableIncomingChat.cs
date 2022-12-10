// Adam Dernis 2022

using CommunityToolkit.Mvvm.Messaging;
using Kayrun.Bindables.Chats.Abstract;
using Kayrun.Client;
using Kayrun.Services.MessengerService;

namespace Kayrun.Bindables.Chats
{
    public class BindableIncomingChat : BindableChat
    {
        public BindableIncomingChat(
            IMessenger messenger,
            IMessengerService messengerService,
            KayrunClient kayrunClient,
            string email) : base(messenger, messengerService, kayrunClient, email) { }
    }
}
