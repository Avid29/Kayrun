// Adam Dernis 2022

using Refit;
using System;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Kayrun.API.Rest
{
    /// <summary>
    /// A factory for RESTful refit interfaces.
    /// </summary>
    public class RestFactory
    {
        private readonly string _baseUrl;
        private readonly RefitSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestFactory"/> class.
        /// </summary>
        /// <param name="baseUrl">The base url of the </param>
        public RestFactory(string baseUrl)
        {
            _baseUrl = baseUrl;

            // Adjust encoding settings so '+' is not encoded as '\u002B'
            var encoderSettings = new TextEncoderSettings();
            encoderSettings.AllowCharacters('+');
            encoderSettings.AllowRange(UnicodeRanges.BasicLatin);
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            _settings = new RefitSettings()
            {
                ContentSerializer = new SystemTextJsonContentSerializer(options)
            };
        }

        /// <summary>
        /// Creates an <see cref="IRestfulKeyService"/> RESTful interface.
        /// </summary>
        public IRestfulKeyService CreateKeyService()
            => RestService.For<IRestfulKeyService>(CreateHttpClient(), _settings);

        /// <summary>
        /// Creates an <see cref="IRestfulMessageService"/> RESTful interface.
        /// </summary>
        public IRestfulMessageService CreateMessageService()
            => RestService.For<IRestfulMessageService>(CreateHttpClient(), _settings);

        private HttpClient CreateHttpClient()
        {
            return new HttpClient
            {
                BaseAddress = new Uri(_baseUrl),
            };
        }
    }
}
