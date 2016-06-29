using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileIconifier.Core.Custom.Chrome;

namespace TileIconifier.Controls.Custom.Chrome
{
    class ChromeAppListViewItemLibrary
    {
        private static List<ChromeAppListViewItem> _chromeAppListViewItems = new List<ChromeAppListViewItem>();

        public static List<ChromeAppListViewItem> ChromeAppListViewItems
        {
            get
            {
                RefreshList();
                return _chromeAppListViewItems;
            }
        }

        public static void RefreshList(bool force = false)
        {
            if(force || !_chromeAppListViewItems.Any())
            _chromeAppListViewItems = ChromeAppLibrary.GetChromeAppItems().Select(c => new ChromeAppListViewItem(c))
                    .ToList();
        }
    }
}