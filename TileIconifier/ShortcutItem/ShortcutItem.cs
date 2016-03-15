using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using TileIconifier.Custom;
using TileIconifier.Steam;
using TileIconifier.Utilities;

namespace TileIconifier
{
    [Serializable]
    public class ShortcutItem : ListViewItem
    {
        #region Path properties
        private string _targetFilePath;
        public string TargetFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(_targetFilePath))
                    _targetFilePath = ShortcutUtils.ResolveShortcut(ShortcutFileInfo.FullName);
                return
                    Environment.ExpandEnvironmentVariables("%PATHEXT%").Split(';').Any(
                        e => string.Equals(Path.GetExtension(_targetFilePath), e, StringComparison.InvariantCultureIgnoreCase))
                    ? _targetFilePath
                    : null;
            }
        }

        public string VisualElementManifestPath
        { get { return string.Format("{0}{1}.VisualElementsManifest.xml", TargetFolderPath, Path.GetFileNameWithoutExtension(TargetFilePath)); } }

        public string TargetFolderPath
        { get { return Path.GetDirectoryName(TargetFilePath) + "\\"; } }

        public string VisualElementsPath
        { get { return TargetFolderPath + @"\VisualElements\"; } }

        public string MediumIconName
        { get { return string.Format("MediumIcon{0}.png", Path.GetFileNameWithoutExtension(TargetFilePath)); } }

        public string RelativeMediumIconPath
        { get { return string.Format("{0}\\{1}", Path.GetFileName(Path.GetDirectoryName(VisualElementsPath)), MediumIconName); } }

        public string FullMediumIconPath
        { get { return string.Format("{0}\\{1}", VisualElementsPath, MediumIconName); } }

        public string SmallIconName
        { get { return string.Format("SmallIcon{0}.png", Path.GetFileNameWithoutExtension(TargetFilePath)); } }

        public string RelativeSmallIconPath
        { get { return string.Format("{0}\\{1}", Path.GetFileName(Path.GetDirectoryName(VisualElementsPath)), SmallIconName); } }

        public string FullSmallIconPath
        { get { return string.Format("{0}\\{1}", VisualElementsPath, SmallIconName); } }
        #endregion

        public bool IsTileIconifierCustomShortcut
        {
            get
            {
                return new DirectoryInfo(TargetFolderPath).Parent.FullName + "\\" == CustomShortcutConstants.CUSTOM_SHORTCUT_VBS_PATH;
            }
        }

        public ShortcutUser ShortcutUser
        {
            get
            {
                if (ShortcutFileInfo.FullName.StartsWith(CustomShortcutConstants.CUSTOM_SHORTCUT_ALL_USERS_PATH))
                    return ShortcutUser.ALL_USERS;
                if (ShortcutFileInfo.FullName.StartsWith(CustomShortcutConstants.CUSTOM_SHORTCUT_CURRENT_USER_PATH))
                    return ShortcutUser.CURRENT_USER;
                return ShortcutUser.UNKNOWN;
            }
        }

        public bool IsValidForIconification
        {
            get
            {
                return !string.IsNullOrEmpty(TargetFilePath) && File.Exists(TargetFilePath);
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

        private Bitmap _standardIcon;
        public Bitmap StandardIcon
        {
            get
            {
                if (_standardIcon == null)
                {
                    try
                    {
                        _standardIcon = Icon.ExtractAssociatedIcon(ShortcutFileInfo.FullName).ToBitmap();
                    }
                    catch { }
                }
                return _standardIcon;
            }
        }

        public Bitmap MediumImage
        {
            get
            {
                if (NewParameters.MediumImage != null)
                    return NewParameters.MediumImage;

                if (OldParameters != null && OldParameters.MediumImage != null)
                    return OldParameters.MediumImage;

                return null;

            }
            set
            {
                NewParameters.MediumImage = value;
            }
        }

        public Bitmap SmallImage
        {
            get
            {
                if (NewParameters.SmallImage != null)
                    return NewParameters.SmallImage;

                if (OldParameters != null && OldParameters.SmallImage != null)
                    return OldParameters.SmallImage;

                return null;
            }
            set
            {
                NewParameters.SmallImage = value;
            }
        }

        public string BackgroundColor
        {
            get
            {
                return NewParameters.BackgroundColor;
            }
            set
            {
                NewParameters.BackgroundColor = value;
            }
        }

        public string ForegroundText
        {
            get
            {
                return NewParameters.ForegroundText;
            }
            set
            {
                NewParameters.ForegroundText = value;
            }
        }

        public bool ShowNameOnSquare150x150Logo
        {
            get
            {
                return NewParameters.ShowNameOnSquare150x150Logo == "on" ? true : false;
            }
            set
            {
                NewParameters.ShowNameOnSquare150x150Logo = (value ? "on" : "off");
            }
        }

        public FileInfo ShortcutFileInfo { get; set; }
        public string AppId { get; set; }
        public bool IsPinned { get; set; }

        protected ShortcutItem(SerializationInfo info, StreamingContext context) : base(info, context) { }
        private ShortcutIconParameters OldParameters { get; set; }
        private ShortcutIconParameters NewParameters { get; set; }

        public bool HasUnsavedChanges
        {
            get
            {
                return !NewParameters.Equals(OldParameters);
            }
        }
        
        public ShortcutItem(FileInfo shortcutFileInfo)
        {
            ShortcutFileInfo = shortcutFileInfo;
            LoadParameters();
        }
        public ShortcutItem(string shortcutPath)
        {
            ShortcutFileInfo = new FileInfo(shortcutPath);
            LoadParameters();
        }

        private void LoadParameters()
        {
            if (IsIconified)
            {
                var xmlDoc = XDocument.Load(VisualElementManifestPath);

                try
                {
                    var parameters = from b in xmlDoc.Descendants("VisualElements")
                                     select new ShortcutIconParameters()
                                     {
                                         BackgroundColor = b.Attribute("BackgroundColor").Value,
                                         ForegroundText = b.Attribute("ForegroundText").Value,
                                         ShowNameOnSquare150x150Logo = b.Attribute("ShowNameOnSquare150x150Logo").Value,
                                         MediumImage = ImageUtils.LoadIconifiedBitmap(TargetFolderPath + b.Attribute("Square150x150Logo").Value),
                                         SmallImage = ImageUtils.LoadIconifiedBitmap(TargetFolderPath + b.Attribute("Square70x70Logo").Value),
                                     };
                    OldParameters = parameters.Single();
                    NewParameters = OldParameters.Clone();
                }
                catch
                {
                    ResetParameters();
                }
            }
            else
            {
                ResetParameters();
            }
        }

        public void UndoChanges()
        {
            NewParameters = OldParameters.Clone();
        }

        public void CommitChanges()
        {
            OldParameters = NewParameters.Clone();
        }

        public void ResetParameters()
        {
            //defaults
            OldParameters = new ShortcutIconParameters()
            {
                BackgroundColor = "black",
                ForegroundText = "light",
                ShowNameOnSquare150x150Logo = "on"
            };
            NewParameters = OldParameters.Clone();
        }

        public override string ToString()
        {
            return Path.GetFileNameWithoutExtension(ShortcutFileInfo.Name) + (IsPinned ? " *" : "") + (IsIconified ? " #" : "");
        }
    }
}
