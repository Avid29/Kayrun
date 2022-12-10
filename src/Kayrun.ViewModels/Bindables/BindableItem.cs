// Adam Dernis 2022

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Kayrun.Client;
using Kayrun.Services.MessengerService;

namespace Kayrun.Bindables
{
    /// <summary>
    /// An item that can be bound to the UI.
    /// </summary>
    public abstract class BindableItem : ObservableRecipient
    {
        /// <summary>
        /// Gets the <see cref="IMessenger"/> for the <see cref="BindableItem"/>.
        /// </summary>
        protected readonly IMessenger _messenger;

        /// <summary>
        /// Gets the <see cref="IMessengerService"/> for the <see cref="BindableItem"/>.
        /// </summary>
        protected readonly IMessengerService _messengerService;

        /// <summary>
        /// Gets the <see cref="KayrunClient"/> for the <see cref="BindableItem"/>.
        /// </summary>
        protected readonly KayrunClient _kayrunClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableItem"/> class.
        /// </summary>
        protected BindableItem(
            IMessenger messenger,
            IMessengerService messengerService,
            KayrunClient kayrunClient)
        {
            _messenger = messenger;
            _messengerService = messengerService;
            _kayrunClient = kayrunClient;
        }
    }
}
