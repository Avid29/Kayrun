// Adam Dernis 2022

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Kayrun.Bindables.Chats;
using Kayrun.Bindables.Chats.Abstract;
using Kayrun.Messages.Navigation;
using Kayrun.Services.ChatStorageService;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Kayrun.ViewModels.Panels
{
    public class ChatsViewModel : ObservableRecipient
    {
        private readonly IMessenger _messenger;
        private readonly IChatStorageService _chatStorageService;

        private BindableChat? _selectedChat;

        public ChatsViewModel(
            IMessenger messenger,
            IChatStorageService chatStorageService)
        {
            _messenger = messenger;
            _chatStorageService = chatStorageService;

            OutgoingChats = new ObservableCollection<BindableOutgoingChat>();
            _ = LoadChats();
        }

        public BindableChat? SelectedChat
        {
            get => _selectedChat;
            set
            {
                if (value is null) return;

                var old = _selectedChat;

                if (SetProperty(ref _selectedChat, value))
                {
                    if (old is not null) old.IsSelected = false;

                    value.IsSelected = true;

                    _messenger.Send(new ChatSelectedMessage(value));
                }
            }
        }

        public ObservableCollection<BindableOutgoingChat> OutgoingChats { get; }

        private async Task LoadChats()
        {
            var chats = await _chatStorageService.LoadOutgoingChats();
            foreach (var chat in chats)
            {
                OutgoingChats.Add(chat);
            }
        }
    }
}
