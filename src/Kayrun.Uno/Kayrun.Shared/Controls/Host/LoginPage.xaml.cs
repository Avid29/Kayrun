// Adam Dernis 2022

using Kayrun.ViewModels.Host;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Kayrun.Controls.Host
{
    public sealed partial class LoginPage : UserControl
    {
        public LoginPage()
        {
            this.InitializeComponent();
            DataContext = App.Current.Services.GetRequiredService<LoginPageViewModel>();
        }

        public LoginPageViewModel ViewModel => (LoginPageViewModel)DataContext;
    }
}
