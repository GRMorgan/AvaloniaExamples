using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShellViewExample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ShellViewModel _shellViewModel;

        public ShellViewModel ShellViewModel
        {
            get
            {
                return _shellViewModel;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _shellViewModel, value);
            }
        }

        public MainWindowViewModel()
        {
            _shellViewModel = ShellViewModel = new ShellViewModel();
        }
    }
}
