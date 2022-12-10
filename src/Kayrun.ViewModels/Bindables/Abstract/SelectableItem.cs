// Adam Dernis 2022

using CommunityToolkit.Mvvm.Messaging;
using Kayrun.Client;
using Kayrun.Services.MessengerService;

namespace Kayrun.Bindables.Abstract
{
    public class SelectableItem : BindableItem
    {
        private bool _isSelected;

        public SelectableItem(
            IMessenger messenger,
            IMessengerService messengerService,
            KayrunClient kayrunClient) : base(messenger, messengerService, kayrunClient) { }


        /// <summary>
        /// Gets or sets whether or not the item is selected.
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}
