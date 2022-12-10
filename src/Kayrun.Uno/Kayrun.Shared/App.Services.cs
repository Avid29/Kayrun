// Adam Dernis 2022

using CommunityToolkit.Mvvm.Messaging;
using Kayrun.Client;
using Kayrun.Client.Services;
using Kayrun.Services.KeyStorageService;
using Kayrun.Services.ChatStorageService;
using Kayrun.Services.MessengerService;
using Kayrun.Services.StorageService;
using Kayrun.ViewModels;
using Kayrun.ViewModels.Host;
using Kayrun.ViewModels.Panels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Kayrun
{
    public partial class App
    {
        private IServiceProvider ConfigureServices()
        {
            // Register Services
            return new ServiceCollection()
            .AddSingleton<IMessenger, WeakReferenceMessenger>()
            .AddSingleton<IStorageService, StorageService>()
            .AddSingleton<IKeyStorage, KeyStorageService>()
            .AddSingleton<IChatStorageService, ChatStorageService>()
            .AddSingleton<IMessengerService, MessengerService>()

            // ViewModels
            .AddSingleton<WindowViewModel>()
            .AddSingleton<LoginPageViewModel>()
            .AddSingleton<ChatsViewModel>()
            .AddSingleton<MessagesViewModel>()

            // SubPages

            // Other
            .AddSingleton<KayrunClient>()

            .BuildServiceProvider();
        }
    }
}
