// Adam Dernis 2022

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Kayrun.Bindables.Chats;
using Kayrun.Services.ChatStorageService;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Kayrun.ViewModels.Panels
{
    public class ChatsViewModel : ObservableRecipient
    {
        private readonly IChatStorageService _chatStorageService;

        public ChatsViewModel(
            IMessenger messenger,
            IChatStorageService chatStorageService)
        {
            OutgoingChats = new ObservableCollection<BindableOutgoingChat>();
            _chatStorageService = chatStorageService;

            _ = LoadChats();
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
