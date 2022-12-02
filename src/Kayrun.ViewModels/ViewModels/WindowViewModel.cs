// Adam Dernis 2022

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Kayrun.Messages;
using Kayrun.ViewModels.Enums;

namespace Kayrun.ViewModels
{
    public partial class WindowViewModel : ObservableRecipient
    {
        private WindowHostState _windowState;

        public WindowViewModel(
            IMessenger messenger)
        {
            WindowState = WindowHostState.LoggedOut;

            messenger.Register<WindowStateChangedMessage>(this,
                (_, e) => WindowState = e.WindowState);
        }

        /// <summary>
        /// Gets or sets the windows state.
        /// </summary>
        public WindowHostState WindowState
        {
            get => _windowState;
            set
            {
                if (SetProperty(ref _windowState, value))
                {
                    OnPropertyChanged(nameof(IsLoggedOut));
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not the app is logged out.
        /// </summary>
        public bool IsLoggedOut => _windowState is WindowHostState.LoggedOut;

    }
}
