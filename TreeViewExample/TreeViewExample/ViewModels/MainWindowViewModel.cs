using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TreeViewExample.Models;

namespace TreeViewExample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<IFileFolderModel> RootFolder { get; }

        private const string RootFolderPath = @"I:\TV";

        public MainWindowViewModel()
        {
            RootFolder = new ObservableCollection<IFileFolderModel>();
            RootFolder.Add(new FolderModel(RootFolderPath));
        }
    }
}
