using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Kayrun.Bindables;
using Kayrun.Services.MessageStorageService;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Kayrun.ViewModels.Panels
{
    public class ChatsViewModel : ObservableRecipient
    {
        private readonly IMessageStorageService _messageStorageService;

        public ChatsViewModel(
            IMessenger messenger,
            IMessageStorageService messageStorageService)
        {
            Chats = new ObservableCollection<BindableChat>();
            _messageStorageService = messageStorageService;

            //_messageStorageService.CreateChat("ace4971");


            
            _ = LoadChats();
        }

        public ObservableCollection<BindableChat> Chats { get; }

        private async Task LoadChats()
        {
            var chats = (await _messageStorageService.LoadChats()).Select(x => new BindableChat(x));
            foreach (var chat in chats)
            {
                Chats.Add(chat);
            }
        }
    }
}
