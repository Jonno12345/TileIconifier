#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2021 Johnathon M
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
using TileIconifier.Core.Shortcut;

namespace TileIconifier.Core.Custom.Steam
{
    public class SteamLibrary
    {
        private readonly string[] _defaultInstallationPaths =
        {
            Environment.ExpandEnvironmentVariables(@"%programfiles(x86)%\Steam\"),
            Environment.ExpandEnvironmentVariables(@"%programfiles%\Steam\")
        };

        private readonly List<string> _steamLibraryFolders = new List<string>();
        private string _steamExecutablePath;
        private string _steamInstallationFolderPath;
        private ShortcutItem _steamShortcutItem;

        private SteamLibrary()
        {
        }

        public static SteamLibrary Instance { get; } = new SteamLibrary();

        public ShortcutItem SteamShortcutItem => _steamShortcutItem ??
                                                 (_steamShortcutItem =
                                                     ShortcutItemEnumeration.GetShortcuts()
                                                         .FirstOrDefault(
                                                             s =>
                                                                 Path.GetFileNameWithoutExtension(
                                                                     s.ShortcutFileInfo.Name) == "Steam"));

        public string GetSteamInstallationFolder()
        {
            if (_steamInstallationFolderPath != null)
                return _steamInstallationFolderPath;

            if (SteamShortcutItem != null)
            {
                _steamInstallationFolderPath = SteamShortcutItem.TargetFolderPath;
                return _steamInstallationFolderPath;
            }
            foreach (var defaultInstallationPath in _defaultInstallationPaths.Where(Directory.Exists))
            {
                _steamInstallationFolderPath = defaultInstallationPath;
                return _steamInstallationFolderPath;
            }

            throw new SteamInstallationPathNotFoundException();
        }

        public void SetSteamInstallationFolder(string selectedPath)
        {
            _steamInstallationFolderPath = selectedPath;
        }

        public List<string> GetLibraryFolders()
        {
            if (_steamLibraryFolders.Count > 0)
                return _steamLibraryFolders;

            try
            {
                AddLibraryFolder(new FileInfo(GetLibraryFoldersVdf()).Directory?.Parent?.FullName + @"\");
            }
            catch
            {
                // ignored
            }

            var kv = new KeyValues.KeyValues("LibraryFolders");
            kv.LoadFromFile(GetLibraryFoldersVdf());
            var flattenedKvp = kv.GetFlattenedKeyValuePairs();
            foreach (
                var keyValuePair in
                    flattenedKvp.Where(keyValuePair => keyValuePair.Key == "path"))
            {
                try
                {
                    var libraryFolder = keyValuePair.Value.Replace(@"\\", @"\") + "\\";
                    AddLibraryFolder(libraryFolder);
                }
                catch (SteamLibraryPathNotFoundException)
                {
                    // ignored
                }
            }

            return _steamLibraryFolders;
        }

        public void AddLibraryFolder(string libraryFolder)
        {
            var steamAppsPath = Path.Combine(libraryFolder, "steamapps");
            if (Directory.Exists(steamAppsPath))
                _steamLibraryFolders.Add(steamAppsPath);
            else
                throw new SteamLibraryPathNotFoundException();
        }

        public List<SteamGame> GetAllSteamGames()
        {
            var steamGames = new List<SteamGame>();

            if (!_steamLibraryFolders.Any())
                return steamGames;

            foreach (
                var acfFile in
                    GetLibraryFolders()
                        .Select(libraryFolder => new DirectoryInfo(libraryFolder).GetFiles("appmanifest*.acf"))
                        .SelectMany(acfFiles => acfFiles))
            {
                try
                {
                    var kv = new KeyValues.KeyValues("AppState");
                    kv.LoadFromFile(acfFile.FullName);

                    //Empty list of key name values, skip
                    if (!kv.KeyNameValues.Any())
                        continue;
                    var appId =
                        kv.KeyNameValues.Single(
                            k => k.Key.Equals("appid", StringComparison.InvariantCultureIgnoreCase)).Value;
                    var gameName = GetGameName(kv);
                    if (gameName == null)
                        continue;
                    steamGames.Add(new SteamGame(appId, gameName, acfFile.FullName));
                }
                catch (Exception ex)
                {
                    // get a better stack trace if we do have an exception including the contents of the failed file information
                    var fileContents = "UNKNOWN";
                    try
                    {
                        fileContents = File.ReadAllText(acfFile.FullName);
                    }
                    catch
                    {
                        // ignored
                    }
                    throw new Exception(
                        $@"An issue occured handling Steam acf file {acfFile.FullName} - Contents{"\r\n"}{
                            fileContents}"
                        , ex);
                }
            }
            return steamGames;
        }

        public string GetSteamExePath()
        {
            if (_steamExecutablePath != null)
                return _steamExecutablePath;

            if (SteamShortcutItem != null)
            {
                return SteamShortcutItem.TargetInfo.FilePath;
            }
            var assumedSteamExePath = _steamInstallationFolderPath + "Steam.exe";
            if (File.Exists(assumedSteamExePath))
                return assumedSteamExePath;

            throw new SteamExecutableNotFoundException();
        }

        public void SetSteamExePath(string filePath)
        {
            if (File.Exists(filePath))
                _steamExecutablePath = filePath;
        }


        private string GetLibraryFoldersVdf()
        {
            var assumedVdfPath = GetSteamInstallationFolder() + @"steamapps\libraryfolders.vdf";

            if (!File.Exists(assumedVdfPath))
                throw new SteamLibraryPathNotFoundException();

            return assumedVdfPath;
        }

        private static string GetGameName(KeyValues.KeyValues kv)
        {
            while (true)
            {
                var kvp =
                    kv.KeyNameValues.FirstOrDefault(
                        k => k.Key.Equals("name", StringComparison.InvariantCultureIgnoreCase));
                if (kvp != null)
                    return kvp.Value;

                var userConfigKv =
                    kv.KeyChilds.FirstOrDefault(
                        c => c.Name.Equals("UserConfig", StringComparison.InvariantCultureIgnoreCase));
                if (userConfigKv == null) return null;
                kv = userConfigKv;
            }
        }
    }
}