using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileIconifier.Utilities;

namespace TileIconifier
{
    public class ShortcutItem
    {
        public FileInfo ShortcutFileInfo;

        private string _exeFilePath;
        public string ExeFilePath
        {
            get
            {
                if(string.IsNullOrEmpty(_exeFilePath))
                    _exeFilePath = ShortcutUtils.GetShortcutTarget(ShortcutFileInfo.FullName);
                return Path.GetExtension(_exeFilePath) == ".exe" ? _exeFilePath : null;
            }
        }

        private string _exeFolderPath;
        public string ExeFolderPath
        {
            get
            {
                if (string.IsNullOrEmpty(_exeFolderPath))
                    _exeFolderPath = Path.GetDirectoryName(ExeFilePath);
                return _exeFolderPath;
            }
        }

        public string _visualElementsPath;
        public string VisualElementsPath
        {
            get
            {
                if(string.IsNullOrEmpty(_visualElementsPath))
                    _visualElementsPath = ExeFolderPath + @"\VisualElements\";
                return _visualElementsPath;
            }
        }

        public string AppId { get; set; }
        public bool IsPinned { get; set; }


        public override string ToString()
        {
            return Path.GetFileNameWithoutExtension(ShortcutFileInfo.Name) + (IsPinned ? " *" : "");
        }
    }
}
