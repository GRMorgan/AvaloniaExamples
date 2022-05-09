using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewExample.Models
{
    public class FolderModel : IFileFolderModel
    {
        public ObservableCollection<IFileFolderModel> Children { get; set; }

        public string FullPath { get; set; }

        public string Name { get; set; }

        public FolderModel(string dirPath)
        {
            if(!Directory.Exists(dirPath))
            {
                throw new Exception($"Directory '{dirPath}' does not exist");
            }

            var dirInfo = new DirectoryInfo(dirPath);

            Name = dirInfo.Name;
            FullPath = dirInfo.FullName;

            Children = new ObservableCollection<IFileFolderModel>();

            foreach (var dir in dirInfo.GetDirectories())
            {
                Children.Add(new FolderModel(dir.FullName));
            }

            foreach(var file in dirInfo.GetFiles())
            {
                Children.Add(new FileModel(file.FullName));
            }
        }
    }
}
