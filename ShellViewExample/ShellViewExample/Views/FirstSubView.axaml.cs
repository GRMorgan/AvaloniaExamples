using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ShellViewExample.Views
{
    public partial class FirstSubView : UserControl
    {
        public FirstSubView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
