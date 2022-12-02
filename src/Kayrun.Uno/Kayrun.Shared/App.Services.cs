// Adam Dernis 2022

using System;
using Kayrun.Client;
using Kayrun.Services.KeyStorageService;
using Kayrun.Services.MessengerService;
using Kayrun.Services.StorageService;
using Kayrun.ViewModels;
using Kayrun.ViewModels.Host;
using Microsoft.Extensions.DependencyInjection;

namespace Kayrun
{
    public partial class App
    {
        private IServiceProvider ConfigureServices()
        {
            // Register Services
            return new ServiceCollection()
            .AddSingleton<IStorageService, StorageService>()
            .AddSingleton<IKeyStorageService, KeyStorageService>()
            .AddSingleton<IMessengerService, MessengerService>()

            // ViewModels
            .AddSingleton<WindowViewModel>()
            .AddSingleton<LoginPageViewModel>()

            // SubPages

            // Other
            .AddSingleton<KayrunClient>()

            .BuildServiceProvider();
        }
    }
}
