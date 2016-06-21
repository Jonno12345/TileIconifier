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
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TileIconifier.Core.Utilities;

namespace TileIconifier.Core.Shortcut
{
    public static class ShortcutItemEnumeration
    {
        private static List<ShortcutItem> _shortcutsCache;

        /// <summary>
        ///     Gather a list of ShortcutItems from the current environment
        /// </summary>
        /// <returns></returns>
        public static List<ShortcutItem> GetShortcuts(bool refreshCache = false)
        {
            if (refreshCache)
                _shortcutsCache = null;

            if (_shortcutsCache != null)
                return _shortcutsCache;

            var shortcutsList = new List<ShortcutItem>();

            var pathsToScan = new List<string>
            {
                @"%PROGRAMDATA%\Microsoft\Windows\Start Menu",
                @"%APPDATA%\Microsoft\Windows\Start Menu"
            };

            foreach (var pathToScan in pathsToScan)
            {
                //Recursively go through all folders of lnk files, adding each ShortcutItem to the list
                Action<string, Action<string>> applyAllFiles = null;
                applyAllFiles = (folder, fileAction) =>
                {
                    foreach (var file in Directory.GetFiles(folder)) fileAction(file);
                    foreach (var subDir in Directory.GetDirectories(folder))
                        try
                        {
                            applyAllFiles(subDir, fileAction);
                        }
                        catch
                        {
                            // ignored
                        }
                };

                applyAllFiles(Environment.ExpandEnvironmentVariables(pathToScan), f =>
                {
                    var extension = Path.GetExtension(f);
                    if (extension != null && !extension.Equals(".lnk", StringComparison.OrdinalIgnoreCase))
                        return;

                    var shortcutItem = new ShortcutItem(f);
                    if (shortcutItem.IsValidForIconification)
                        shortcutsList.Add(shortcutItem);
                });
            }

            //Order the list by name
            _shortcutsCache = shortcutsList.OrderBy(f => f.ShortcutFileInfo.Name).ToList();


            return _shortcutsCache;
        }

        public static List<ShortcutItem> TryGetShortcutsWithPinning(out Exception pinnedInformationException,
            bool refreshCache = false)
        {
            GetShortcuts(refreshCache);
            try
            {
                GetPinnedStartMenuInformation();
                pinnedInformationException = null;
            }
            catch (Exception ex)
            {
                pinnedInformationException = ex;
            }
            return _shortcutsCache;
        }


        private static void GetPinnedStartMenuInformation()
        {
            var tempOutputPath = $"{Path.GetTempPath()}{"TileIconifier"}\\";
            Directory.CreateDirectory(tempOutputPath);

            var tempFilePath = $"{tempOutputPath}{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.xml";

            PowerShellUtils.DumpStartLayout(tempFilePath);
            PowerShellUtils.MarryAppIDs(_shortcutsCache);

            MarkPinnedShortcuts(tempFilePath);

            _shortcutsCache = _shortcutsCache.ToList();

            try
            {
                File.Delete(tempFilePath);
            }
            catch
            {
                // ignored
            }
        }

        private static void MarkPinnedShortcuts(string tempFilePath)
        {
            var startLayout = File.ReadAllText(tempFilePath);
            
            var regexMatches = Regex.Matches(startLayout,
                "<start:DesktopApplicationTile.*DesktopApplicationID=\"(.*)\".*");

            foreach (var shortcutItem in _shortcutsCache)
                shortcutItem.IsPinned = false;

            foreach (Match regexMatch in regexMatches)
            {
                try
                {
                    var groupData = regexMatch.Groups[1].Value;
                    var x = _shortcutsCache.Where(s => s.AppId == groupData || Path.GetFullPath(s.TargetFilePath) == Path.GetFullPath(groupData));
                    var shortcutId = x.First();
                    shortcutId.IsPinned = true;
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}