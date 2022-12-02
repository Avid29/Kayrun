// Adam Dernis 2022

using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kayrun.Client.RSA;
using Kayrun.Services.KeyStorageService;
using Kayrun.Services.MessengerService;
using Kayrun.ViewModels.Enums;
using System.Threading.Tasks;

namespace Kayrun.ViewModels
{
    public partial class WindowViewModel : ObservableRecipient
    {
        private readonly IMessengerService _messengerService;
        private readonly IKeyStorageService _keyStorageService;
        private WindowHostState _windowState;

        public WindowViewModel(IMessengerService messengerService, IKeyStorageService keyStorageService)
        {
            WindowState = WindowHostState.LoggedOut;
            _messengerService = messengerService;
            _keyStorageService = keyStorageService;

            LoginCommand = new AsyncRelayCommand<string>(async x =>
            {
                Guard.IsNotNull(x);
                await Login(x);
            });
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
        public bool IsLoggedOut => _windowState == WindowHostState.LoggedOut;

    }
}
