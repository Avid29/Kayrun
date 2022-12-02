﻿// Adam Dernis 2022

using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Windows.Storage;

namespace Kayrun.Services.StorageService
{
    public class StorageService : IStorageService
    {
        private readonly StorageFolder _storageFolder;
        private readonly JsonSerializerOptions _options;

        public StorageService()
        {
            _storageFolder = ApplicationData.Current.LocalFolder;

            // Adjust encoding settings so '+' is not encoded as '\u002B'
            var encoderSettings = new TextEncoderSettings();
            encoderSettings.AllowCharacters('+');
            encoderSettings.AllowRange(UnicodeRanges.BasicLatin);
            _options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
        }

        /// <inheritdoc/>
        public async Task<bool> HasFile(string filename)
        {
            var file = await _storageFolder.GetFileAsync(filename);
            return file is not null;
        }

        /// <inheritdoc/>
        public async Task<T?> LoadAsync<T>(string filename)
        {
            // Opens and deserializes the file, then closes it
            var file = await _storageFolder.GetFileAsync(filename);
            await using var stream = await file.OpenStreamForReadAsync();
            return await JsonSerializer.DeserializeAsync<T?>(stream, _options);
        }

        /// <inheritdoc/>
        public async Task SaveAsync<T>(string filename, T data)
        {
            try
            {
                // Creates or opens a file, writes to it, then closes it
                var file = await _storageFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
                await using var stream = await file.OpenStreamForWriteAsync();
                await JsonSerializer.SerializeAsync(stream, data, _options);
            }
            catch
            {
                return;
            }
        }
    }
}