#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2016 Johnathon M
// 
//         Permission is hereby granted, free of charge, to any person obtaining a copy
//         of this software and associated documentation files (the "Software"), to deal
//         in the Software without restriction, including without limitation the rights
//         to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//         copies of the Software, and to permit persons to whom the Software is
//         furnished to do so, subject to the following conditions:
// 
//         The above copyright notice and this permission notice shall be included in
//         all copies or substantial portions of the Software.
// 
//         THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//         IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//         FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//         AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//         LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//         OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//         THE SOFTWARE.
// 
// */

#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TileIconifier.Core.Custom;
using TileIconifier.Core.Shortcut.State;
using TileIconifier.Core.Utilities;

namespace TileIconifier.Core.Shortcut
{
    [Serializable]
    public class ShortcutItem
    {
        private Bitmap _standardIcon;

        [NonSerialized] public ShortcutItemStateController Properties = new ShortcutItemStateController();

        public ShortcutItem(FileInfo shortcutFileInfo)
        {
            ShortcutFileInfo = shortcutFileInfo;
            IsPinned = null;
            Properties.LoadParameters(IsIconified, VisualElementManifestPath, MediumImageResizeMetadataPath,
                SmallImageResizeMetadataPath, TargetFolderPath);
        }

        public ShortcutItem(string shortcutPath) : this(new FileInfo(shortcutPath))
        {
            //ShortcutFileInfo = 
            //IsPinned = null;
            //Properties.LoadParameters(IsIconified, VisualElementManifestPath, MediumImageResizeMetadataPath,
            //    SmallImageResizeMetadataPath, TargetFolderPath);
        }

        public bool IsTileIconifierCustomShortcut => new DirectoryInfo(TargetFolderPath).Parent?.FullName + "\\" ==
                                                     CustomShortcutGetters.CustomShortcutVbsPath;

        public CustomShortcut CustomShortcut
        {
            get
            {
                if (!IsTileIconifierCustomShortcut)
                {
                    return null;
                }
                try
                {
                    return CustomShortcut.Load(TargetFilePath);
                }
                catch
                {
                    return null;
                }
            }
        }

        public ShortcutUser ShortcutUser
        {
            get
            {
                if (ShortcutFileInfo.FullName.StartsWith(CustomShortcutGetters.CustomShortcutAllUsersPath))
                    return ShortcutUser.AllUsers;
                if (ShortcutFileInfo.FullName.StartsWith(CustomShortcutGetters.CustomShortcutCurrentUserPath))
                    return ShortcutUser.CurrentUser;
                return ShortcutUser.Unknown;
            }
        }

        public bool IsValidForIconification => !string.IsNullOrEmpty(TargetFilePath) && File.Exists(TargetFilePath);

        //todo: merge this with property IconifiedByTileIconifier at some point, should be a better indicator but won't work retroactively
        public bool IsIconified => File.Exists(VisualElementManifestPath)
                                   && Directory.Exists(VisualElementsPath)
                                   && File.Exists(FullMediumIconPath)
                                   && File.Exists(FullSmallIconPath);

        public bool IconifiedByTileIconifier
        {
            get
            {
                if (!File.Exists(VisualElementManifestPath))
                {
                    return false;
                }

                var xmlDoc = XDocument.Load(VisualElementManifestPath);
                try
                {
                    return (bool)xmlDoc.Root.Attribute("GeneratedByTileIconifier");
                }
                catch
                {
                    //ignore
                }
                return false;
            }
        }


        public Bitmap StandardIcon
        {
            get
            {
                if (_standardIcon != null) return _standardIcon;
                try
                {
                    _standardIcon = Icon.ExtractAssociatedIcon(ShortcutFileInfo.FullName)?.ToBitmap();
                }
                catch
                {
                    // ignored
                }
                return _standardIcon;
            }
        }


        public FileInfo ShortcutFileInfo { get; set; }
        public string AppId { get; set; }
        public bool? IsPinned { get; set; }

        #region Path properties

        private string _targetFilePath;

        public string TargetFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(_targetFilePath))
                    _targetFilePath = ShortcutUtils.GetTargetPath(ShortcutFileInfo.FullName);
                return
                    Environment.ExpandEnvironmentVariables("%PATHEXT%").Split(';').Any(
                        e =>
                            string.Equals(Path.GetExtension(_targetFilePath), e,
                                StringComparison.InvariantCultureIgnoreCase))
                        ? _targetFilePath
                        : null;
            }
        }

        public string VisualElementManifestPath =>
            $"{TargetFolderPath}{Path.GetFileNameWithoutExtension(TargetFilePath)}.VisualElementsManifest.xml";

        public string VisualElementManifestPathOriginalBackup =>
            VisualElementManifestPath + ".originalbak";

        public string TargetFolderPath => Path.GetDirectoryName(TargetFilePath) + "\\";

        public string VisualElementsPath => TargetFolderPath + @"\VisualElements\";

        public string MediumIconName
            => $"MediumIcon{Path.GetFileNameWithoutExtension(TargetFilePath)}.png";

        public string RelativeMediumIconPath
            => $"{Path.GetFileName(Path.GetDirectoryName(VisualElementsPath))}\\{MediumIconName}";

        public string FullMediumIconPath => $"{VisualElementsPath}{MediumIconName}";

        public string SmallIconName
            => $"SmallIcon{Path.GetFileNameWithoutExtension(TargetFilePath)}.png";

        public string RelativeSmallIconPath
            => $"{Path.GetFileName(Path.GetDirectoryName(VisualElementsPath))}\\{SmallIconName}";

        public string FullSmallIconPath => $"{VisualElementsPath}{SmallIconName}";

        public string MediumImageResizeMetadataPath
            => $"{VisualElementsPath}{Path.GetFileNameWithoutExtension(MediumIconName)}_Metadata.xml";

        public string SmallImageResizeMetadataPath
            => $"{VisualElementsPath}{Path.GetFileNameWithoutExtension(SmallIconName)}_Metadata.xml";

        #endregion
    }

    public class ShortcutItemEqualityComparer : IEqualityComparer<ShortcutItem>
    {
        public bool Equals(ShortcutItem x, ShortcutItem y)
        {
            if (x == null || y == null)
                return false;
            if (ReferenceEquals(x, y))
                return true;

            return x.ShortcutFileInfo.FullName == y.ShortcutFileInfo.FullName;
        }

        public int GetHashCode(ShortcutItem obj)
        {
            return 0;
        }
    }
}