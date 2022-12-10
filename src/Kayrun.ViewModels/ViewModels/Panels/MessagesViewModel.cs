// Adam Dernis 2022

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Kayrun.Bindables.Chats;
using Kayrun.Bindables.Chats.Abstract;
using Kayrun.Messages.Navigation;
using Kayrun.Services.ChatStorageService;
using System.Threading.Tasks;

namespace Kayrun.ViewModels.Panels
{
    public class MessagesViewModel : ObservableRecipient
    {
        private readonly IMessenger _messenger;
        private readonly IChatStorageService _chatStorageService;

        private BindableChat _selectedChat;

        public MessagesViewModel(IMessenger messenger, IChatStorageService chatStorageService)
        {
            _messenger = messenger;
            _chatStorageService = chatStorageService;

            _messenger.Register<ChatSelectedMessage>(this, (_, m) => SelectedChat = m.Chat);
        }

        public BindableChat SelectedChat
        {
            get => _selectedChat;
            set
            {
                if (SetProperty(ref _selectedChat, value))
                {
                    OnPropertyChanged(nameof(IsOutgoingChat));

                    _ = LoadMessages();
                }
            }
        }

        public bool IsOutgoingChat => SelectedChat is BindableOutgoingChat;

        private async Task LoadMessages()
        {
            var messages = await _chatStorageService.LoadMessages(_selectedChat.Email);
        }
    }
}
