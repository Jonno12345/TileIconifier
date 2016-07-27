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
using System.Linq;
using TileIconifier.Core.Custom.Chrome;

namespace TileIconifier.Controls.Custom.Chrome
{
    internal class ChromeAppListViewItemLibrary
    {
        private static List<ChromeApp> _chromeApps =  new List<ChromeApp>();

        public static List<ChromeAppListViewItem> LibraryAsListViewItems
        {
            get
            {
                RefreshList(false);
                return _chromeApps.Select(c => new ChromeAppListViewItem(c))
                            .ToList();
            }
        }

        public static void RefreshList(bool force = true)
        {
            try
            {
                if (!ChromeAppLibrary.ChromeAppLibraryPathExists() || !ChromeAppLibrary.ChromeInstallationPathExists())
                {
                    return;
                }

                if (force || !_chromeApps.Any())
                {
                    _chromeApps = ChromeAppLibrary.GetChromeAppItems();
                }
            }
            catch
            {
                //ignore
            }
        }
    }
}