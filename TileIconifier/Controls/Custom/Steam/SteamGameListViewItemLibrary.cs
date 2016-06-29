using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileIconifier.Core.Custom.Steam;
using TileIconifier.Properties;

namespace TileIconifier.Controls.Custom.Steam
{
    class SteamGameListViewItemLibrary
    {
        private static List<SteamGameListViewItem> _steamGameListViewItems = new List<SteamGameListViewItem>();

        public static List<SteamGameListViewItem> SteamGameListViewItems
        {
            get
            {
                RefreshList();
                return _steamGameListViewItems;
            }
        }

        public static void RefreshList(bool force = false)
        {
            if(force || !_steamGameListViewItems.Any())
            _steamGameListViewItems =
                SteamLibrary.Instance.GetAllSteamGames().Select(s => new SteamGameListViewItem(s)).ToList();
        }
        
        public static string SteamInstallationPathResolvedString()
        {
            try
            {
                return Strings.SteamInstallationPath + ": " + SteamLibrary.Instance.GetSteamInstallationFolder();
            }
            catch
            {
                return Strings.SteamInstallationPathNotFound;
            }
        }

        public static string SteamExecutablePathResolvedString()
        {
            try
            {
                return Strings.SteamExecutablePath + ": " + SteamLibrary.Instance.GetSteamExePath();
            }
            catch
            {
                return Strings.SteamExecutablePathNotFound;
            }
        }

        public static string SteamLibraryPathsResolvedString()
        {
            try
            {
                var steamLibraryFolders = SteamLibrary.Instance.GetLibraryFolders();
                return steamLibraryFolders.Count > 0 ? $"{Strings.SteamLibraryFolders}: {string.Join("; ", steamLibraryFolders)}" : Strings.SteamLibraryFoldersNotFound;
            }
            catch
            {
                return Strings.SteamLibraryFoldersNotFound;
            }
        }
    }
}
