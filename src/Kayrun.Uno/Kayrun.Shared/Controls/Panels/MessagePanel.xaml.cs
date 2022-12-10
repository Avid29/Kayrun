// Adam Dernis 2022

using Kayrun.ViewModels.Panels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Kayrun.Controls.Panels
{
    public sealed partial class MessagePanel : UserControl
    {
        public MessagePanel()
        {
            this.InitializeComponent();
            DataContext = App.Current.Services.GetRequiredService<MessagesViewModel>();
        }

        public MessagesViewModel ViewModel => (MessagesViewModel)DataContext;
    }
}
