// Adam Dernis 2022

using CommunityToolkit.Diagnostics;
using Kayrun.API.Models.Keys;
using Kayrun.Client.Enums;
using Kayrun.Client.Helpers;
using Kayrun.Client.RSA;
using Kayrun.Client.Services;
using Kayrun.Services.StorageService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kayrun.Services.KeyStorageService
{
    /// <summary>
    /// An implementation of the <see cref="IKeyStorage"/>.
    /// </summary>
    public class KeyStorageService : IKeyStorage
    {
        private const string PublicKey = "public.key";
        private const string PrivateKey = "private.key";

        private readonly IStorageService _storageService;

        public KeyStorageService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        /// <inheritdoc/>
        public async Task<RoE<string, Error>> LoadKey(string email)
            => await LoadKeyByName<KeyEntry>(Pathify(email));

        /// <inheritdoc/>
        public async Task<RoE<string, Error>> LoadPublicKey()
            => await LoadKeyByName<KeyEntry>(PublicKey);

        /// <inheritdoc/>
        public async Task<RoE<string, Error>> LoadPrivateKey(string email)
        {
            // Load entry
            var entry = await LoadKeyEntryByName<PrivateKeyEntry>(PrivateKey);
            if (!entry.Success)
            {
                return entry.Error;
            }

            // Ensure entry is associated with the email
            Guard.IsNotNull(entry.Result);
            if (entry.Result.Email is null || !entry.Result.Email.Contains(email))
            {
                return Error.MissingKey;
            }

            return entry.Result.Key;
        }

        /// <inheritdoc/>
        public async Task<Error> AssociateToPrivateKey(string email)
        {
            // Load private key entry
            var entry = await LoadKeyEntryByName<PrivateKeyEntry>(PrivateKey);
            if (!entry.Success)
            {
                return entry.Error;
            }

            // Associate to entry
            Guard.IsNotNull(entry.Result);
            entry.Result.Email ??= new HashSet<string>();
            entry.Result.Email.Add(email);

            // Save entry
            await StoreKey(PrivateKey, entry.Result);
            return Error.None;
        }

        /// <inheritdoc/>
        public async Task StoreKey(string email, string key)
        {
            var entry = new KeyEntry
            {
                Email = email,
                Key = key,
            };

            await StoreKey(Pathify(email), entry);
        }

        /// <inheritdoc/>
        public async Task StoreKeyPair(KeyPair keyPair)
        {
            await StoreKey(PublicKey, new KeyEntry(keyPair.PublicKey.ToBase64()));
            await StoreKey(PrivateKey, new KeyEntry(keyPair.PrivateKey.ToBase64()));
        }

        private async Task<RoE<string, Error>> LoadKeyByName<T>(string filename)
            where T : KeyEntry
        {
            // Load entry
            var roe = await LoadKeyEntryByName<T>(filename);
            if (!roe.Success)
            {
                return roe.Error;
            }

            // Return entry key
            Guard.IsNotNull(roe.Result);
            return roe.Result.Key;
        }

        private async Task<RoE<T, Error>> LoadKeyEntryByName<T>(string filename)
            where T : KeyEntry
        {
            // Check file existence
            if (!await _storageService.HasFile(filename)) return Error.MissingKey;

            // Check that the entry was properly deserialized
            var entry = await _storageService.LoadAsync<T>(filename);
            if (entry is null) return Error.MissingKey;

            return entry;
        }

        private async Task StoreKey<T>(string filename, T entry)
            where T : KeyEntry
            => await _storageService.SaveAsync(filename, entry);

        private string Pathify(string email)
            => $"{email}.key";
    }
}
