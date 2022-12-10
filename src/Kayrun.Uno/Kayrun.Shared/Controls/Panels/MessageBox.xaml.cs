// Adam Dernis 2022

using Kayrun.ViewModels.Panels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Kayrun.Controls.Panels
{
    public sealed partial class MessageBox : UserControl
    {
        public MessageBox()
        {
            this.InitializeComponent();
            DataContext = App.Current.Services.GetRequiredService<MessageBoxViewModel>();
        }

        public MessageBoxViewModel ViewModel => (MessageBoxViewModel)DataContext;
    }
}
