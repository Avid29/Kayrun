// Adam Dernis 2022

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Kayrun.Bindables.Chats.Abstract;
using Kayrun.Messages.Navigation;
using Kayrun.Services.MessengerService;

namespace Kayrun.ViewModels.Panels
{
    public class MessageBoxViewModel : ObservableRecipient
    {
        private readonly IMessenger _messenger;
        private readonly IMessengerService _messengerService;

        private BindableChat? _selectedChat;
        private string? _draftText;

        public MessageBoxViewModel(IMessenger messenger, IMessengerService messengerService)
        {
            _messenger = messenger;
            _messengerService = messengerService;

            SendMessageCommand = new RelayCommand(SendMessage);

            _messenger.Register<ChatSelectedMessage>(this, (_, m) => SelectedChat = m.Chat);
        }

        /// <summary>
        /// Gets or sets the drafted text in the message box.
        /// </summary>
        public string? DraftText
        {
            get => _draftText;
            set => SetProperty(ref _draftText, value);
        }

        public BindableChat? SelectedChat
        {
            get => _selectedChat;
            set => SetProperty(ref _selectedChat, value);
        }

        public RelayCommand SendMessageCommand { get; }

        private void SendMessage()
        {
            if (_selectedChat is not null && !string.IsNullOrEmpty(DraftText))
            {
                _messengerService.SendMessage(_selectedChat.Email, DraftText!);
                DraftText = string.Empty;
            }
        }
    }
}
