// Adam Dernis 2022

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Kayrun.Bindables.Chats.Abstract;
using Kayrun.Messages.Navigation;

namespace Kayrun.ViewModels.Panels
{
    public class MessagesViewModel : ObservableRecipient
    {
        private readonly IMessenger _messenger;

        private BindableChat _selectedChat;

        public MessagesViewModel(IMessenger messenger)
        {
            _messenger = messenger;

            _messenger.Register<ChatSelectedMessage>(this, (_, m) => SelectedChat = m.Chat);
        }

        public BindableChat SelectedChat
        {
            get => _selectedChat;
            set
            {
                if (SetProperty(ref _selectedChat, value))
                {
                    // TODO: Load chat
                }
            }
        }
    }
}
