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

namespace TileIconifier.Core.Custom.WindowsStoreShellMethod
{
    public class WindowsStoreApp :IEqualityComparer<WindowsStoreApp>
    {
        public WindowsStoreApp(string displayName, string logoPath, string appUserModelId)
        {
            DisplayName = displayName;
            LogoPath = logoPath;
            AppUserModelId = appUserModelId;
        }

        public string DisplayName { get; }
        public string LogoPath { get; }
        public string AppUserModelId { get; }

        public bool Equals(WindowsStoreApp x, WindowsStoreApp y)
        {
            if (x == null || y == null)
                return false;

            if (ReferenceEquals(x, y))
                return true;

            if (x == y)
                return true;

            return x.DisplayName == y.DisplayName &&
                   x.LogoPath == y.LogoPath &&
                   x.AppUserModelId == y.AppUserModelId;
        }

        public int GetHashCode(WindowsStoreApp obj)
        {
            return DisplayName.GetHashCode() ^ LogoPath.GetHashCode() ^ AppUserModelId.GetHashCode();
        }
    }
}