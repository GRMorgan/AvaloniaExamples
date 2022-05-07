using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ShellViewExample.Views
{
    public partial class SecondSubView : UserControl
    {
        public SecondSubView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
