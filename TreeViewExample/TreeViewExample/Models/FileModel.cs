using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewExample.Models
{
    public class FileModel : IFileFolderModel
    {
        public string Name { get; set; }

        public string FullPath { get; set; }

        public FileModel(string filePath)
        {
            if(!File.Exists(filePath))
            {
                throw new Exception($"File '{filePath}' does not exist");
            }

            var fileInfo = new FileInfo(filePath);

            Name = fileInfo.Name;
            FullPath = fileInfo.FullName;
        }
    }
}
