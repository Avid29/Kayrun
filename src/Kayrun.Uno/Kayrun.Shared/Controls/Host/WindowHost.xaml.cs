using Kayrun.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Kayrun.Controls.Host
{
    public sealed partial class WindowHost : UserControl
    {
        public WindowHost()
        {
            this.InitializeComponent();
            DataContext = App.Current.Services.GetRequiredService<WindowViewModel>();
        }

        public WindowViewModel ViewModel => (WindowViewModel)DataContext;
    }
}
