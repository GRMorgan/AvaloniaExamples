using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ShellViewExample.Views
{
    public partial class ShellView : UserControl
    {
        public ShellView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
