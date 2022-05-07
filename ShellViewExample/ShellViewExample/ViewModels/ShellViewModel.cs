using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShellViewExample.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private ViewModelBase _content;

        public ViewModelBase Content
        {
            get
            {
                return _content;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _content, value);
            }
        }

        private FirstSubViewModel _firstSubViewModel;

        private SecondSubViewModel _secondSubViewModel;

        public ShellViewModel()
        {
            _firstSubViewModel = new FirstSubViewModel();
            _secondSubViewModel = new SecondSubViewModel();
            _content = Content = _firstSubViewModel;

            SwitchViewsCommand = ReactiveCommand.Create(() =>
            {
                if (Content == _firstSubViewModel)
                {
                    Content = _secondSubViewModel;
                }
                else
                {
                    Content = _firstSubViewModel;
                }
            });
        }

        public ICommand SwitchViewsCommand { get; }
    }
}
