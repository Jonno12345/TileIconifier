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
using System.IO;
using System.Text.RegularExpressions;
using TileIconifier.Core.Shortcut;
using TileIconifier.Core.Utilities;

namespace TileIconifier.Core.Custom
{
    public class CustomShortcut
    {
        private string _basicShortcutIcon;

        private CustomShortcut()
        {
        }

        internal CustomShortcut(
            string shortcutName,
            string targetPath,
            string targetArguments,
            CustomShortcutType shortcutType,
            WindowType windowType,
            string shortcutRootFolder,
            string basicIconToUse = null,
            string workingFolder = null
            )
        {
            var vbsFolderPath =
                DirectoryUtils.GetUniqueDirName(CustomShortcutGetters.CustomShortcutVbsPath + shortcutName) + "\\";
            var shortcutPath = $"{shortcutRootFolder}{new DirectoryInfo(vbsFolderPath).Name}\\{shortcutName}.lnk";


            ShortcutName = shortcutName.CleanInvalidFilenameChars();
            ShortcutItem = new ShortcutItem(shortcutPath);
            TargetPath = targetPath;
            TargetArguments = targetArguments;
            ShortcutType = shortcutType;
            VbsFolderPath = vbsFolderPath;
            WorkingFolder = workingFolder;
            WindowType = windowType;

            Directory.CreateDirectory(VbsFolderPath);

            if (basicIconToUse != null && File.Exists(basicIconToUse)) BasicShortcutIcon = basicIconToUse;
        }

        public ShortcutItem ShortcutItem { get; internal set; }
        public string VbsFilePath { get; internal set; }
        public string VbsFolderPath { get; internal set; }
        public string ShortcutName { get; internal set; }
        public string TargetPath { get; internal set; }
        public string TargetArguments { get; internal set; }
        public string WorkingFolder { get; internal set; }
        public CustomShortcutType ShortcutType { get; internal set; }
        public WindowType WindowType { get; internal set; }

        internal string BasicShortcutIcon
        {
            get { return !string.IsNullOrEmpty(_basicShortcutIcon) ? _basicShortcutIcon : TargetPath; }
            set { _basicShortcutIcon = value; }
        }

        public void Delete()
        {
            if (ShortcutItem.ShortcutFileInfo.Directory != null && ShortcutItem.ShortcutFileInfo.Directory.Exists)
            {
                try
                {
                    ShortcutItem.ShortcutFileInfo.Directory.Delete(true);
                }
                catch
                {
                    // ignored
                }
            }

            if (!Directory.Exists(VbsFolderPath)) return;
            try
            {
                Directory.Delete(VbsFolderPath, true);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///     Parses data from a VBS file and returns a custom shortcut
        /// </summary>
        /// <exception cref="InvalidCustomShortcutException">If a property Regex could not be matched</exception>
        public static CustomShortcut Load(string vbsFilePath)
        {
            if (!File.Exists(vbsFilePath))
                throw new FileNotFoundException(vbsFilePath);

            var vbsFileContents = File.ReadAllText(vbsFilePath);

            var regexMatch = Regex.Match(vbsFileContents,
                "'Custom Shortcut Type = \"(.*)\".*'Shortcut Name = \"(.*)\".*'Shortcut Path = \"(.*)\".*targetPath = \"(.*)\".*targetArguments = \"(.*)\"\r?\n.*",
                RegexOptions.Singleline);

            if (!regexMatch.Success)
            {
                throw new InvalidCustomShortcutException();
            }

            var directoryInfo = new FileInfo(vbsFilePath).Directory;
            if (directoryInfo == null) throw new DirectoryNotFoundException();

            return new CustomShortcut
            {
                ShortcutName = regexMatch.Groups[2].Value.UnescapeVba(),
                ShortcutItem = new ShortcutItem(regexMatch.Groups[3].Value.UnescapeVba()),
                TargetPath = regexMatch.Groups[4].Value.UnescapeVba(),
                TargetArguments = regexMatch.Groups[5].Value.UnescapeVba(),
                ShortcutType =
                    (CustomShortcutType) Enum.Parse(typeof (CustomShortcutType), regexMatch.Groups[1].Value, true),
                VbsFilePath = vbsFilePath,
                VbsFolderPath = directoryInfo.FullName + "\\"
            };
        }
    }
}