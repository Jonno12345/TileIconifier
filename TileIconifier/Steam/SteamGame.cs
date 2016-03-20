using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Microsoft.Win32;
using TileIconifier.Properties;
using TileIconifier.Utilities;

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
                    $@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Steam App {AppId}";
                var defaultRegistryKey32BitOs =
                    $@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App {AppId}";

                _iconPath = (string) Registry.GetValue(defaultRegistryKey, "DisplayIcon", null) ??
                            (string) Registry.GetValue(defaultRegistryKey32BitOs, "DisplayIcon", null);
                return _iconPath;
            }
        }

        public byte[] IconAsBytes
        {
            get
            {
                if (IconPath == null) return ImageUtils.ImageToByteArray(Resources.SteamIco.ToBitmap());

                try
                {
                    using (var readImage = new FileStream(IconPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        return ImageUtils.ImageToByteArray(new Bitmap(readImage));
                }
                catch
                {
                    // ignored
                }
                return ImageUtils.ImageToByteArray(Resources.SteamIco.ToBitmap());
            }
        }

        public string GameExecutionArgument => $"-applaunch \"{AppId}\"";

        public override string ToString()
        {
            return AppId;
        }
    }
}