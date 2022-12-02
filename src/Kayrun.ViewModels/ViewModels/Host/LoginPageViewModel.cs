// Adam Dernis 2022

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Kayrun.Client.RSA;
using Kayrun.Client.Services;
using Kayrun.Messages;
using Kayrun.Services.MessengerService;
using Kayrun.ViewModels.Enums;
using System.Threading.Tasks;

namespace Kayrun.ViewModels.Host
{
    public class LoginPageViewModel : ObservableRecipient
    {
        private readonly IMessenger _messenger;
        private readonly IMessengerService _messengerService;
        private readonly IKeyStorage _keyStorageService;

        private string _email;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPageViewModel"/> class.
        /// </summary>
        public LoginPageViewModel(
            IMessenger messenger,
            IMessengerService messengerService,
            IKeyStorage keyStorageService)
        {
            _email = string.Empty;
            _messenger = messenger;
            _messengerService = messengerService;
            _keyStorageService = keyStorageService;

            LoginCommand = new AsyncRelayCommand(async _ => await Login(Email));
        }

        /// <summary>
        /// Gets or sets the email to login to.
        /// </summary>
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public AsyncRelayCommand LoginCommand { get; }

        public async Task Login(string email)
        {
            _messenger.Send(new WindowStateChangedMessage(WindowHostState.Loading));

            // Check for existing private key
            var key = await _keyStorageService.LoadPrivateKey(email);

            if (!key.Success)
            {
                // No key found
                // Establish new login

                // Generate and store a key pair
                // TODO: Key size settings option
                var pair = KeyPair.GenerateKeyPair(1024);
                await _keyStorageService.StoreKeyPair(pair);
                await _keyStorageService.AssociateToPrivateKey(email);

                // Upload the key to the server
                await _messengerService.UploadKey(email);
            }

            _messenger.Send(new WindowStateChangedMessage(WindowHostState.LoggedIn));
        }
    }
}
