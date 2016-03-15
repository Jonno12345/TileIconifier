using Callysto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TileIconifier.Utilities;

namespace TileIconifier.Steam
{
    class SteamLibrary
    {
        /// <summary>
        /// Singleton class instantiator
        /// </summary>
        private static SteamLibrary _instance;
        public static SteamLibrary Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SteamLibrary();
                return _instance;
            }
        }

        private SteamLibrary() { }
        
        private readonly string[] defaultInstallationPaths = new[]
        {
            @"C:\Program Files (x86)\Steam\",
            @"C:\Program Files\Steam\"
        };

        private ShortcutItem _steamShortcutItem;
        public ShortcutItem SteamShortcutItem {
            get
            {
                if (_steamShortcutItem == null)
                    _steamShortcutItem = ShortcutItemEnumeration.GetShortcuts().Where(s => Path.GetFileNameWithoutExtension(s.ShortcutFileInfo.Name) == "Steam").FirstOrDefault();
                return _steamShortcutItem;
            }
        }

        private string _steamInstallationFolderPath;
        private string _steamExecutablePath;
        private List<string> _steamLibraryFolders = new List<string>();

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
            else
            {
                foreach (var defaultInstallationPath in defaultInstallationPaths)
                    if (Directory.Exists(defaultInstallationPath))
                    {
                        _steamInstallationFolderPath = defaultInstallationPath;
                        return _steamInstallationFolderPath;
                    }
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
                AddLibraryFolder(new FileInfo(GetLibraryFoldersVdf()).Directory.Parent.FullName + @"\");
            }
            catch { }

            KeyValues kv = new KeyValues("LibraryFolders");
            kv.LoadFromFile(GetLibraryFoldersVdf());
            foreach (var keyValuePair in kv.KeyNameValues)
            {
                //library folders are marked by a consecutive integer
                if (Regex.Match(keyValuePair.Key, @"\d+").Success)
                {
                    try
                    {
                        var libraryFolder = keyValuePair.Value.Replace(@"\\", @"\") + "\\";
                        AddLibraryFolder(libraryFolder);
                    }
                    catch (SteamLibraryPathNotFoundException) { }
                }
            }

            return _steamLibraryFolders;
        }

        public void AddLibraryFolder(string libraryFolder)
        {
            if(Directory.Exists(libraryFolder + "steamapps\\"))
                _steamLibraryFolders.Add(libraryFolder + "steamapps\\");
            else
                throw new SteamLibraryPathNotFoundException();
        }
        
        public List<SteamGame> GetAllSteamGames()
        {
            var steamGames = new List<SteamGame>();

            foreach (var libraryFolder in GetLibraryFolders())
            {
                var acfFiles = new DirectoryInfo(libraryFolder).GetFiles("appmanifest*.acf");
                foreach (var acfFile in acfFiles)
                {
                    KeyValues kv = new KeyValues("AppState");
                    kv.LoadFromFile(acfFile.FullName);
                    var appId = kv.KeyNameValues.Where(k => k.Key == "appid").Single().Value;
                    var gameName = kv.KeyNameValues.Where(k => k.Key == "name").Single().Value;
                    steamGames.Add(new SteamGame(appId, gameName, acfFile.FullName)); ;
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
            else
            {
                var assumedSteamExePath = _steamInstallationFolderPath + "Steam.exe";
                if (File.Exists(assumedSteamExePath))
                    return assumedSteamExePath;
            }

            throw new SteamExecutableNotFoundException();
        }

        public void SetSteamExePath(string filePath)
        {
            if (File.Exists(filePath))
                _steamExecutablePath = filePath;
        }
    }

}
