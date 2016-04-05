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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using TileIconifier.Core.Utilities;

namespace TileIconifier.Core.Custom.WindowsStoreShellMethod
{
    public static class WindowsStoreLibrary
    {
        private static string ExtractStringFromPriFile(string resourcePath)
        {
            var stringOut = new StringBuilder(1024);
            NativeMethods.SHLoadIndirectString(resourcePath, stringOut, stringOut.Capacity, IntPtr.Zero);
            return stringOut.ToString();
        }

        public static List<WindowsStoreApp> GetAppKeysFromRegistry()
        {
            //This whole method is horrible... I'm suspicious there's a proper function for this (see PackageManager.FindPackagesForUser() 
            //unsure which library to reference for this though.) - registry method for now.
            var rootKey =
                Registry.CurrentUser.OpenSubKey("Software\\Classes\\");

            if (rootKey == null) throw new WindowsStoreRegistryKeyNotFoundException();

            var storeApps = new List<WindowsStoreApp>();
            var appPackages = rootKey.GetSubKeyNames();

            // loop through each app
            //
            // valid keys will have a subkey of "Application". This will have either a default value which is a human readable displayname
            // or will have a resource file display name in "FriendlyTypeName". We also need AppUserModelID, and get the logo from ApplicationIcon
            foreach (var storeApp in from appPackage in appPackages
                select rootKey.OpenSubKey(appPackage)
                into appPackageSubKey
                where
                    appPackageSubKey != null &&
                    appPackageSubKey.GetSubKeyNames()
                        .Any(s => string.Equals(s, "application", StringComparison.InvariantCultureIgnoreCase))
                let appPackageApplicationKey =
                    (string) appPackageSubKey.OpenSubKey("Application")?.GetValue("AppUserModelID")
                let appPackageIconPath =
                    GetIconPath((string) appPackageSubKey.OpenSubKey("Application")?.GetValue("ApplicationIcon"))
                let appPackageDisplayName = appPackageSubKey.GetValueNames().Contains("FriendlyTypeName")
                    ? GetDisplayName((string) appPackageSubKey.GetValue("FriendlyTypeName"))
                    : (string) appPackageSubKey.GetValue(null)
                where !string.IsNullOrEmpty(appPackageDisplayName) && !string.IsNullOrEmpty(appPackageApplicationKey)
                select new WindowsStoreApp(appPackageDisplayName, appPackageIconPath, appPackageApplicationKey)
                into storeApp
                where !storeApps.Contains(storeApp, storeApp)
                select storeApp)
            {
                storeApps.Add(storeApp);
            }
            return storeApps;
        }

        private static string GetIconPath(string iconTag)
        {
            //this is a somewhat hit and miss method... get the highest resolution icon with the file name and in the folder specified.
            //TODO: improve this...

            var iconPathRegex = Regex.Match(iconTag, @"@{(.*?\..*?)_.*\?ms-resource://.*/Files/(.*)}");
            var iconPath = string.Empty;
            if (!iconPathRegex.Success) return iconPath;

            Func<string, string> getLogoPath = p =>
            {
                var folderPaths = new DirectoryInfo(Environment.ExpandEnvironmentVariables(
                    $@"{p}")).GetDirectories($@"{iconPathRegex.Groups[1].Value}*");

                var imageNameWithoutExtension = Path.GetFileNameWithoutExtension(iconPathRegex.Groups[2].Value);

                if (folderPaths.Length == 0)
                    return string.Empty;

                FileInfo largestLogoPath = null;
                Image largestLogoImage = null;

                foreach (
                    var matchingLogoFiles in
                        folderPaths.Select(folderPath => folderPath?.GetFiles($@"{imageNameWithoutExtension}*",
                            SearchOption.AllDirectories)))
                {
                    if (matchingLogoFiles == null) return string.Empty;
                    foreach (var logoFile in matchingLogoFiles)
                    {
                        try
                        {
                            var image = ImageUtils.LoadFileToBitmap(logoFile.FullName);
                            if (largestLogoPath == null)
                            {
                                largestLogoPath = logoFile;
                                largestLogoImage = (Image) image.Clone();
                            }
                            else
                            {
                                if (image.Width*image.Height > largestLogoImage?.Width*largestLogoImage?.Height)
                                {
                                    largestLogoPath = logoFile;
                                    largestLogoImage.Dispose();
                                    largestLogoImage = (Image) image.Clone();
                                }
                            }
                            image.Dispose();
                        }
                        catch
                        {
                            // ignore
                        }
                    }
                }
                largestLogoImage?.Dispose();
                return largestLogoPath?.FullName ?? string.Empty;
            };

            var logoPath = getLogoPath(@"%PROGRAMFILES%\WindowsApps\");
            return string.IsNullOrEmpty(logoPath) ? getLogoPath(@"%SYSTEMROOT%\SystemApps\") : logoPath;
        }

        private static string GetDisplayName(string displayName)
        {
            //display name is either plain text or in a resource string
            if (!Regex.Match(displayName, @"^@{.*?\?.*?}$").Success) return displayName;
            var resourceTag = displayName;
            displayName = ExtractStringFromPriFile(resourceTag);
            return displayName;
        }
    }
}