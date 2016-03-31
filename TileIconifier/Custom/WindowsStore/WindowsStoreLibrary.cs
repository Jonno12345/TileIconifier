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
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using TileIconifier.Utilities;

namespace TileIconifier.Custom.WindowsStore
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
                Registry.CurrentUser.OpenSubKey("Software\\Classes\\Extensions\\ContractId\\Windows.Protocol\\PackageId");

            if (rootKey == null) throw new WindowsStoreRegistryKeyNotFoundException();

            var storeApps = new List<WindowsStoreApp>();
            var appPackages = rootKey.GetSubKeyNames();

            //loop through each app
            foreach (var appPackage in appPackages)
            {
                var subPackagePath = rootKey.OpenSubKey($@"{appPackage}\ActivatableClassId");
                if (subPackagePath == null) continue;
                var mainProtocolKeys = subPackagePath.GetSubKeyNames();

                var storeApp = new WindowsStoreApp(appPackage);

                //loop through each protocol key
                foreach (var mainProtocolKey in mainProtocolKeys)
                {
                    var mainPackagePathKey = subPackagePath.OpenSubKey(mainProtocolKey);
                    if (mainPackagePathKey == null) continue;
                    var displayName = GetDisplayName((string) mainPackagePathKey.GetValue("DisplayName"));
                    if (string.IsNullOrEmpty(displayName)) continue;
                    var iconPath = GetIconPath((string) mainPackagePathKey.GetValue("Icon"));
                    var protocolKey = mainPackagePathKey.OpenSubKey("CustomProperties");
                    var protocolId = (string) protocolKey?.GetValue("Name");
                    if (!string.IsNullOrEmpty(protocolId))
                        storeApp.StoreAppProtocols.Add(new WindowsStoreAppProtocol
                        {
                            ProtocolId = protocolId,
                            DisplayName = displayName,
                            LogoPath = iconPath
                        });
                }
                if (storeApp.StoreAppProtocols.Any())
                    storeApps.Add(storeApp);
            }
            return storeApps;
        }

        private static string GetIconPath(string iconTag)
        {
            //this is a somewhat hit and miss method... get the highest resolution icon with the file name and in the folder specified.
            //TODO: improve this...

            var iconPathRegex = Regex.Match(iconTag, @"@{(.*)\?ms-resource://.*/Files/(.*)}");
            var iconPath = string.Empty;
            if (!iconPathRegex.Success) return iconPath;

            var tempFileInfo = new FileInfo(Environment.ExpandEnvironmentVariables(
                $@"%PROGRAMFILES%\WindowsApps\{iconPathRegex.Groups[1].Value}\{
                    iconPathRegex.Groups[2].Value.Replace("/", "\\")}"));
            var logoDirectoryInfo = tempFileInfo.Directory;
            var imageNameWithoutExtension = Path.GetFileNameWithoutExtension(tempFileInfo.Name);

            if (logoDirectoryInfo != null && !logoDirectoryInfo.Exists)
                return string.Empty;

            var matchingLogoFiles = logoDirectoryInfo?.GetFiles($@"{imageNameWithoutExtension}*");

            FileInfo largestLogo = null;
            if (matchingLogoFiles == null) return string.Empty;
            foreach (var logoFile in matchingLogoFiles)
            {
                try
                {
                    var image = ImageUtils.LoadFileToBitmap(logoFile.FullName);
                    if (largestLogo == null)

                    {
                        largestLogo = logoFile;
                    }
                    else
                    {
                        var currentLargestLogo = ImageUtils.LoadFileToBitmap(largestLogo.FullName);
                        if (image.Width*image.Height > currentLargestLogo.Width*currentLargestLogo.Height)
                            largestLogo = logoFile;
                        currentLargestLogo.Dispose();
                    }
                    image.Dispose();
                }
                catch
                {
                    // ignore
                }
            }
            return largestLogo?.FullName ?? string.Empty;
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