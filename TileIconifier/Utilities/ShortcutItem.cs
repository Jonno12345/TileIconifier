using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TileIconifier.Utilities;

namespace TileIconifier
{
    public class ShortcutItem : ListViewItem
    {

        private string _exeFilePath;
        public string ExeFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(_exeFilePath))
                    _exeFilePath = ShortcutUtils.GetShortcutTarget(ShortcutFileInfo.FullName);
                return Path.GetExtension(_exeFilePath) == ".exe" ? _exeFilePath : null;
            }
        }

        public string VisualElementManifestPath
        {
            get
            {
                return string.Format("{0}{1}.VisualElementsManifest.xml", ExeFolderPath, Path.GetFileNameWithoutExtension(ExeFilePath));
            }
        }

        public string ExeFolderPath
        {
            get
            {
                return Path.GetDirectoryName(ExeFilePath) + "\\";
            }
        }

        public string VisualElementsPath
        {
            get
            {
                return ExeFolderPath + @"\VisualElements\";
            }
        }

        public string MediumIconName
        {
            get
            {
                return string.Format("MediumIcon{0}.png", Path.GetFileNameWithoutExtension(ExeFilePath));
            }
        }

        public string RelativeMediumIconPath
        {
            get
            {
                return string.Format("{0}\\{1}", Path.GetFileName(Path.GetDirectoryName(VisualElementsPath)), MediumIconName);
            }
        }

        public string FullMediumIconPath
        {
            get
            {
                return string.Format("{0}\\{1}", VisualElementsPath, MediumIconName);
            }
        }

        public string SmallIconName
        {
            get
            {
                return string.Format("SmallIcon{0}.png", Path.GetFileNameWithoutExtension(ExeFilePath));
            }
        }

        public string RelativeSmallIconPath
        {
            get
            {
                return string.Format("{0}\\{1}", Path.GetFileName(Path.GetDirectoryName(VisualElementsPath)), SmallIconName);
            }
        }

        public string FullSmallIconPath
        {
            get
            {
                return string.Format("{0}\\{1}", VisualElementsPath, SmallIconName);
            }
        }


        public bool IsIconified
        {
            get
            {
                return File.Exists(VisualElementManifestPath)
                    && Directory.Exists(VisualElementsPath)
                    && File.Exists(FullMediumIconPath)
                    && File.Exists(FullSmallIconPath);
            }
        }

        public Icon StandardIcon
        {
            get
            {
                return Icon.ExtractAssociatedIcon(ShortcutFileInfo.FullName);
            }
        }

        private Bitmap _mediumImage;
        public Bitmap MediumImage
        {
            get
            {
                if (_mediumImage != null)
                    return _mediumImage;

                if (IsIconified)
                {
                    try
                    {
                        FileStream bitmapFile = new FileStream(FullMediumIconPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        Bitmap Logo = new Bitmap(bitmapFile);
                        bitmapFile.Close();
                        return Logo;
                    }
                    catch
                    {
                        File.Move(FullSmallIconPath, FullSmallIconPath + "_BAD.png");
                    }
                    return null;
                }
                else
                    return null;
            }
            set
            {
                _mediumImage = value;
            }
        }

        private Bitmap _smallImage;
        public Bitmap SmallImage
        {
            get
            {
                if (_smallImage != null)
                    return _smallImage;

                if (IsIconified)
                {
                    try
                    {
                        FileStream bitmapFile = new FileStream(FullSmallIconPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        Bitmap Logo = new Bitmap(bitmapFile);
                        bitmapFile.Close();
                        return Logo;
                    }
                    catch
                    {
                        File.Move(FullSmallIconPath, FullSmallIconPath + "_BAD.png");
                    }
                    return null;
                }
                else
                    return null;
            }
            set
            {
                _smallImage = value;
            }
        }

        public FileInfo ShortcutFileInfo { get; set; }
        public string AppId { get; set; }
        public bool IsPinned { get; set; }

        public bool HasUnsavedChanges
        {
            get
            {
                return _mediumImage != null || _smallImage != null;
            }
        }

        public override string ToString()
        {
            return Path.GetFileNameWithoutExtension(ShortcutFileInfo.Name) + (IsPinned ? " *" : "") + (IsIconified ? " #" : "");
        }
    }
}
