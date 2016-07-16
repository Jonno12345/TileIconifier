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

using System.Collections.Generic;
using System.Linq;
using TileIconifier.Core.Custom.Steam;
using TileIconifier.Properties;

namespace TileIconifier.Controls.Custom.Steam
{
    internal class SteamGameListViewItemLibrary
    {
        private static List<SteamGameListViewItem> _steamGameListViewItems = new List<SteamGameListViewItem>();

        public static List<SteamGameListViewItem> Items
        {
            get
            {
                RefreshList();
                return _steamGameListViewItems;
            }
        }

        public static void RefreshList(bool force = false)
        {
            if (force || !_steamGameListViewItems.Any())
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
                return steamLibraryFolders.Count > 0
                    ? $"{Strings.SteamLibraryFolders}: {string.Join("; ", steamLibraryFolders)}"
                    : Strings.SteamLibraryFoldersNotFound;
            }
            catch
            {
                return Strings.SteamLibraryFoldersNotFound;
            }
        }
    }
}