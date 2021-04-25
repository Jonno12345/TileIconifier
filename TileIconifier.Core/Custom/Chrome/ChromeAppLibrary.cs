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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TileIconifier.Core.Custom.Chrome
{
    public class ChromeAppLibrary
    {
        public static string AppLibraryPath { get; set; } = CustomShortcutGetters.GoogleAppLibraryPath;
        public static string ChromeInstallationPath { get; set; } = GetChromeInstallationPath();

        public static string GetChromeInstallationPath()
        {
            try
            {
                return
                    (string)
                        Registry.GetValue(
                            @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe",
                            string.Empty, null);
            }
            catch
            {
                foreach (
                    var defaultChromeInstallationPath in
                        CustomShortcutGetters.DefaultChromeInstallationPaths.Where(File.Exists))
                    return defaultChromeInstallationPath;
            }

            throw new FileNotFoundException();
        }

        public static bool ChromeInstallationPathExists()
        {
            try
            {
                return File.Exists(ChromeInstallationPath);
            }
            catch
            {
                return false;
            }
        }

        public static bool ChromeAppLibraryPathExists()
        {
            return Directory.Exists(AppLibraryPath);
        }

        public static List<ChromeApp> GetChromeAppItems()
        {
            var returnList = new List<ChromeApp>();
            if (!ChromeAppLibraryPathExists())
            {
                return returnList;
            }
            //loop through all extension app Id folders
            foreach (var directory in new DirectoryInfo(AppLibraryPath).GetDirectories())
            {
                string combinedLogoPath;
                string appName;

                var subDirectories = directory.GetDirectories();
                if (!subDirectories.Any())
                    continue;

                //grab the versioned folder within this appId
                var mainFolder = subDirectories.OrderByDescending(d => d.Name).First();
                //manifest file for app information extraction
                var manifestJsonPath = Path.Combine(mainFolder.FullName, "manifest.json");
                if (!File.Exists(manifestJsonPath))
                    continue;

                //get contents of the manifest
                Dictionary<string, dynamic> manifestContents;
                try
                {
                    manifestContents =
                        JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(File.ReadAllText(manifestJsonPath));
                }
                catch
                {
                    // unable to read manifest file, skip
                    continue;
                }

                try
                {
                    //get the highest res icon
                    var largestRelativeLogoPath =
                        ((JObject) manifestContents["icons"]).Properties().OrderBy(k => k.Name).First().Value;
                    //get absolute path instead of relative
                    combinedLogoPath = Path.Combine(mainFolder.FullName, largestRelativeLogoPath.Value<string>());
                    //if the absolute path doesn't exist, leave as default
                    if (!File.Exists(combinedLogoPath))
                        combinedLogoPath = string.Empty;
                }
                catch
                {
                    //unable to get logo path, leave as default
                    combinedLogoPath = string.Empty;
                }

                try
                {
                    //standard locales path (may not exist, if not, use non locale name from catch)
                    var localePath = Path.Combine(mainFolder.FullName, "_locales");
                    //get the default locale name (may not exist, if not, use non locale name from catch)
                    var defaultLocale = manifestContents["default_locale"];
                    //get the default locale absolute path (may not exist, if not, use non locale name from catch)
                    var fullLocalePath = Path.Combine(localePath, defaultLocale);
                    //the locale json file (may not exist, if not, use non locale name from catch)
                    var messagesJsonPath = Path.Combine(fullLocalePath, "messages.json");
                    //extract the locale strings (may not exist, if not, use non locale name from catch)
                    Dictionary<string, object> messagesContents =
                        JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(File.ReadAllText(messagesJsonPath));

                    //appName is the first string where the key contains either APP or EXT, and NAME (seems to work. Again, if fail, default to the non-locale name)
                    appName = ((JObject) messagesContents.First(p =>
                        (p.Key.ToUpper().Contains("APP") || p.Key.ToUpper().Contains("EXT")) &&
                        p.Key.ToUpper().Contains("NAME")).Value)["message"].Value<string>();
                }
                catch
                {
                    try
                    {
                        appName = manifestContents["name"];
                    }
                    catch
                    {
                        //couldn't get any name - skip this item.
                        continue;
                    }
                }

                returnList.Add(new ChromeApp {AppId = directory.Name, AppName = appName, IconPath = combinedLogoPath});
            }
            return returnList;
        }
    }
}