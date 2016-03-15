using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TileIconifier.Utilities;

namespace TileIconifier
{
    public static class ShortcutItemEnumeration
    {
        private static List<ShortcutItem> _shortcutsCache;

        /// <summary>
        /// Gather a list of ShortcutItems from the current environment
        /// </summary>
        /// <returns></returns>
        public static List<ShortcutItem> GetShortcuts(bool refreshCache = false)
        {
            if (refreshCache)
                _shortcutsCache = null;

            if (_shortcutsCache != null)
                return _shortcutsCache;

            var shortcutsList = new List<ShortcutItem>();

            List<string> PathsToScan = new List<string>()
            {
                @"%PROGRAMDATA%\Microsoft\Windows\Start Menu",
                @"%APPDATA%\Microsoft\Windows\Start Menu"
            };

            foreach (var pathToScan in PathsToScan)
            {
                //Recursively go through all folders of lnk files, adding each ShortcutItem to the list
                Action<string, Action<string>> ApplyAllFiles = null;
                ApplyAllFiles = (folder, fileAction) =>
                {
                    foreach (string file in Directory.GetFiles(folder)) fileAction(file);
                    foreach (string subDir in Directory.GetDirectories(folder))
                        try { ApplyAllFiles(subDir, fileAction); } catch { }
                };

                ApplyAllFiles(Environment.ExpandEnvironmentVariables(pathToScan), f =>
                {
                    if (!Path.GetExtension(f).Equals(".lnk", StringComparison.OrdinalIgnoreCase))
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
        public static List<ShortcutItem> TryGetShortcutsWithPinning(out Exception pinnedInformationException, bool refreshCache = false)
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
            var tempOutputPath = string.Format("{0}{1}\\", Path.GetTempPath(), "TileIconifier");
            Directory.CreateDirectory(tempOutputPath);

            var tempFilePath = string.Format("{0}{1}.xml", tempOutputPath, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));

            PowerShellUtils.DumpStartLayout(tempFilePath);
            PowerShellUtils.MarryAppIDs(_shortcutsCache);

            MarkPinnedShortcuts(tempFilePath);

            _shortcutsCache = _shortcutsCache.OrderByDescending(s => s.IsPinned)
                                            .ThenBy(s => s.ShortcutFileInfo.Name)
                                            .ToList();

            try
            {
                File.Delete(tempFilePath);
            }
            catch { }
        }

        private static void MarkPinnedShortcuts(string tempFilePath)
        {
            var startLayout = File.ReadAllText(tempFilePath);

            var regexMatches = Regex.Matches(startLayout, "<start:DesktopApplicationTile.*DesktopApplicationID=\"(.*)\".*");

            foreach (Match regexMatch in regexMatches)
            {
                try
                {
                    var groupData = regexMatch.Groups[1].Value;

                    var shortcutId = _shortcutsCache.Where(s => s.AppId == groupData)
                    .First();
                    shortcutId.IsPinned = true;
                }
                catch { }
            }
        }
    }
}
