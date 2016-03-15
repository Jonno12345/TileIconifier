using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TileIconifier.Properties;

namespace TileIconifier.Steam
{
    [Serializable]
    public class SteamGame : ListViewItem
    {
        public string AppId { get; private set; }
        public string GameName { get; private set; }
        public string AppManifestPath { get; private set; }

        protected SteamGame(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public SteamGame(string appId, string gameName, string appManifestPath)
        {
            AppId = appId;
            GameName = gameName;
            AppManifestPath = appManifestPath;
            Text = AppId;
            SubItems.Add(GameName);
        }

        private string _iconPath;
        public string IconPath
        {
            get
            {
                if (_iconPath == null)
                {
                    var defaultRegistryKey = string.Format(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Steam App {0}", AppId);
                    var defaultRegistryKey32BitOs = string.Format(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App {0}", AppId);

                    _iconPath = (string)Registry.GetValue(defaultRegistryKey, "DisplayIcon", null) ?? (string)Registry.GetValue(defaultRegistryKey32BitOs, "DisplayIcon", null);
                }
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
                        FileStream readImage = new FileStream(IconPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                        return new Bitmap(readImage);
                    }
                    catch { }
                }
                return Resources.SteamIco.ToBitmap();
            }
        }

        public string GameExecutionArgument
        {
            get
            {
                return string.Format("-applaunch \"{0}\"", AppId);
            }
        }

        public override string ToString()
        {
            return AppId;
        }
    }
}
