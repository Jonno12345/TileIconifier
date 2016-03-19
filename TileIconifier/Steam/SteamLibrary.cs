using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TileIconifier.Shortcut;

namespace TileIconifier.Steam
{
    internal class SteamLibrary
    {
        /// <summary>
        ///     Singleton class instantiator
        /// </summary>
        private static SteamLibrary _instance;

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

        public static SteamLibrary Instance => _instance ?? (_instance = new SteamLibrary());

        public ShortcutItem SteamShortcutItem => _steamShortcutItem ??
                       (_steamShortcutItem =
                           ShortcutItemEnumeration.GetShortcuts()
                               .FirstOrDefault(s => Path.GetFileNameWithoutExtension(s.ShortcutFileInfo.Name) == "Steam"));


        private string GetLibraryFoldersVdf()
        {
            var assumedVdfPath = GetSteamInstallationFolder() + @"steamapps\libraryfolders.vdf";

            if (!File.Exists(assumedVdfPath))
                throw new SteamLibraryPathNotFoundException();

            return assumedVdfPath;
        }

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
            foreach (var keyValuePair in kv.KeyNameValues.Where(keyValuePair => Regex.Match(keyValuePair.Key, @"\d+").Success))
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
            if (Directory.Exists(libraryFolder + "steamapps\\"))
                _steamLibraryFolders.Add(libraryFolder + "steamapps\\");
            else
                throw new SteamLibraryPathNotFoundException();
        }

        private string GetGameName(KeyValues.KeyValues kv)
        {
            var kvp = kv.KeyNameValues.FirstOrDefault(k => k.Key.Equals("name", StringComparison.InvariantCultureIgnoreCase));
            if (kvp != null)
                return kvp.Value;
            
            var userConfigKv = kv.KeyChilds.FirstOrDefault(c => c.Name.Equals("UserConfig", StringComparison.InvariantCultureIgnoreCase));
            if (userConfigKv != null)
                return GetGameName(userConfigKv);

            return null;
        }

        public List<SteamGame> GetAllSteamGames()
        {
            var steamGames = new List<SteamGame>();

            foreach (var libraryFolder in GetLibraryFolders())
            {
                var acfFiles = new DirectoryInfo(libraryFolder).GetFiles("appmanifest*.acf");
                foreach (var acfFile in acfFiles)
                {
                    var kv = new KeyValues.KeyValues("AppState");
                    kv.LoadFromFile(acfFile.FullName);
                    var appId = kv.KeyNameValues.Single(k => k.Key == "appid").Value;
                    var gameName = GetGameName(kv);
                    if (gameName == null)
                        continue;
                    steamGames.Add(new SteamGame(appId, gameName, acfFile.FullName));
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
                return SteamShortcutItem.TargetFilePath;
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
    }
}