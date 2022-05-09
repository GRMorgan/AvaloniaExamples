using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewExample.Models
{
    public interface IFileFolderModel
    {
        public string FullPath { get; }

        public string Name { get; }
    }
}
