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
using System.Drawing;
using Microsoft.Win32;
using TileIconifier.Core.TileIconify;

namespace TileIconifier.Core.Shortcut
{
    public static class ShortcutConstantsAndEnums
    {
        public static Size MediumShortcutOutputSize => new Size(300, 300);
        public static Size MediumShortcutDisplaySize => new Size(100, 100);
        public static Size SmallShortcutOutputSize => new Size(150, 150);
        public static Size SmallShortcutDisplaySize => new Size(50, 50);

        public static XyRatio MediumXyRatio
            =>
                new XyRatio((double) MediumShortcutOutputSize.Width/MediumShortcutDisplaySize.Width,
                    (double) MediumShortcutOutputSize.Height/MediumShortcutDisplaySize.Height);

        public static XyRatio SmallXyRatio
            =>
                new XyRatio((double) SmallShortcutOutputSize.Width/SmallShortcutDisplaySize.Width,
                    (double) SmallShortcutOutputSize.Height/SmallShortcutDisplaySize.Height);

        public static int RawAccentColor
        {
            get
            {
                var o = Registry.CurrentUser.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent")?
                    .GetValue("AccentColorMenu");
                return (int?) o ?? 0;
            }
        }

        public static string DefaultAccentColor
        {
            get
            {
                var accentColor =
                    RawAccentColor.ToString("X2");

                if (string.IsNullOrEmpty(accentColor) || accentColor.Length != 8)
                {
                    return null;
                }
                var returnString =
                    $@"#{accentColor.Substring(6, 2)}{accentColor.Substring(4, 2)}{accentColor.Substring(2, 2)}";
                return returnString;
            }
        }

        public static List<string> KnownShortcutTargetsWithIssues { get; } = new List<string>
        {
            "winword.exe",
            "excel.exe",
            "powerpnt.exe",
            "firefox.exe",
            "thunderbird.exe",
            "OUTLOOK.EXE",
            "devenv.exe"
        };
    }
}