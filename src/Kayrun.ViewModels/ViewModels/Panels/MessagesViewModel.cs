// Adam Dernis 2022

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Kayrun.ViewModels.Panels
{
    public class MessagesViewModel : ObservableRecipient
    {
        private readonly IMessenger _messenger;

        public MessagesViewModel(IMessenger messenger)
        {
            _messenger = messenger;
        }
    }
}
