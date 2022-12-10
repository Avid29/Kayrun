// Adam Dernis 2022

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Kayrun.Bindables.Chats;
using Kayrun.Bindables.Chats.Abstract;
using Kayrun.Client;
using Kayrun.Messages;
using Kayrun.Messages.Navigation;
using Kayrun.Services.ChatStorageService;
using Kayrun.Services.MessengerService;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Kayrun.ViewModels.Panels
{
    public class ChatsViewModel : ObservableRecipient
    {
        private readonly IMessenger _messenger;
        private readonly IMessengerService _messengerService;
        private readonly KayrunClient _kayrunClient;
        private readonly IChatStorageService _chatStorageService;

        private BindableChat? _selectedChat;

        public ChatsViewModel(
            IMessenger messenger,
            IMessengerService messengerService,
            KayrunClient kayrunClient,
            IChatStorageService chatStorageService)
        {
            _messenger = messenger;
            _messengerService = messengerService;
            _kayrunClient = kayrunClient;
            _chatStorageService = chatStorageService;

            OutgoingChats = new ObservableCollection<BindableOutgoingChat>();
            CreateChatCommand = new RelayCommand<string>(x => _ = CreateChat(x!));

            _messenger.Register<ChatCreatedMessage>(this, (_, m) => AddChat(m.Email));

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

        public RelayCommand<string> CreateChatCommand { get; }

        private async Task CreateChat(string email)
        {
            await _chatStorageService.CreateOutgoingChat(email);

            _messenger.Send(new ChatCreatedMessage(email));
        }

        private async Task LoadChats()
        {
            var chats = await _chatStorageService.LoadOutgoingChats();
            foreach (var chat in chats)
            {
                AddChat(chat);
            }
        }

        private void AddChat(string email)
        {
            OutgoingChats.Add(new BindableOutgoingChat(_messenger, _messengerService, _kayrunClient, email));
        }
    }
}
