// Adam Dernis 2022

using Kayrun.API.Rest;
using Kayrun.Client.Services;

namespace Kayrun.Client
{
    public partial class KayrunClient
    {
        private const string BaseUrl = "http://kayrun.cs.rit.edu:5000/";

        private readonly IKeyStorage _keyStorage;
        private readonly IRestfulKeyService _restKeyService;
        private readonly IRestfulMessageService _restMessageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="KayrunClient"/> class.
        /// </summary>
        public KayrunClient(IKeyStorage keyStorage)
        {
            _keyStorage = keyStorage;

            var restFactory = new RestFactory(BaseUrl);
            _restKeyService = restFactory.CreateKeyService();
            _restMessageService = restFactory.CreateMessageService();
        }
    }
}
