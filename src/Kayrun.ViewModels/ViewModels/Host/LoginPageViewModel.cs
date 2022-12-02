using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kayrun.Client.RSA;
using Kayrun.Services.KeyStorageService;
using Kayrun.Services.MessengerService;
using Kayrun.ViewModels.Enums;
using System.Threading.Tasks;

namespace Kayrun.ViewModels.Host
{
    public class LoginPageViewModel : ObservableRecipient
    {
        private readonly IMessengerService _messengerService;
        private readonly IKeyStorageService _keyStorageService;

        private string _email;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPageViewModel"/> class.
        /// </summary>
        public LoginPageViewModel(IMessengerService messengerService, IKeyStorageService keyStorageService)
        {
            _email = string.Empty;
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
            // TODO: Send login status messages
            //WindowState = WindowHostState.Loading;

            // Check for existing private key
            var key = await _keyStorageService.LoadPrivateKey(email);

            if (!key.Success)
            {
                // No key found
                // Establish new login

                // Generate and store a key pair
                // TODO: Key size settings option
                var pair = KeyPair.GenerateKeyPair(2048);
                await _keyStorageService.StoreKeyPair(pair);

                // Upload the key to the server
                await _messengerService.UploadKey(email);
            }

            //WindowState = WindowHostState.LoggedIn;
        }
    }
}
