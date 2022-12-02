// Adam Dernis 2022

using Kayrun.ViewModels.Panels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Kayrun.Controls.Panels
{
    public sealed partial class ChatsPanel : UserControl
    {
        public ChatsPanel()
        {
            this.InitializeComponent();
            DataContext = App.Current.Services.GetRequiredService<ChatsViewModel>();
        }

        public ChatsViewModel ViewModel => (ChatsViewModel)DataContext;
    }
}
