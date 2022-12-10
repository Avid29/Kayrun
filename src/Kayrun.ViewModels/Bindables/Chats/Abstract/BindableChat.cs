// Adam Dernis 2022

using CommunityToolkit.Mvvm.Messaging;
using Kayrun.Bindables.Abstract;
using Kayrun.Client;
using Kayrun.Services.MessengerService;

namespace Kayrun.Bindables.Chats.Abstract
{
    public abstract class BindableChat : SelectableItem
    {
        public BindableChat(
            IMessenger messenger,
            IMessengerService messengerService,
            KayrunClient kayrunClient,
            string email) : base(messenger, messengerService, kayrunClient)
        {
            Email = email;
        }

        public string Email { get; }
    }
}
