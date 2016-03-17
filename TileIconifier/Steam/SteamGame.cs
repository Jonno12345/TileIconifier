using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Microsoft.Win32;
using TileIconifier.Properties;

namespace TileIconifier.Steam
{
    [Serializable]
    public class SteamGame : ListViewItem
    {
        private string _iconPath;

        protected SteamGame(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public SteamGame(string appId, string gameName, string appManifestPath)
        {
            AppId = appId;
            GameName = gameName;
            AppManifestPath = appManifestPath;
            Text = AppId;
            SubItems.Add(GameName);
        }

        public string AppId { get; }
        public string GameName { get; }
        public string AppManifestPath { get; private set; }

        public string IconPath
        {
            get
            {
                if (_iconPath != null) return _iconPath;
                var defaultRegistryKey =
                    string.Format(
                        @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Steam App {0}",
                        AppId);
                var defaultRegistryKey32BitOs =
                    string.Format(
                        @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App {0}",
                        AppId);

                _iconPath = (string) Registry.GetValue(defaultRegistryKey, "DisplayIcon", null) ??
                            (string) Registry.GetValue(defaultRegistryKey32BitOs, "DisplayIcon", null);
                return _iconPath;
            }
        }

        public Bitmap IconAsBitmap
        {
            get
            {
                if (IconPath != null)
                {
                    try
                    {
                        var readImage = new FileStream(IconPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                        return new Bitmap(readImage);
                    }
                    catch
                    {
                        // ignored
                    }
                }
                return Resources.SteamIco.ToBitmap();
            }
        }

        public string GameExecutionArgument => string.Format("-applaunch \"{0}\"", AppId);

        public override string ToString()
        {
            return AppId;
        }
    }
}