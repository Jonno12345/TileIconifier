using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileIconifier.Core.Custom.WindowsStoreShellMethod;

namespace TileIconifier.Controls.Custom.WindowsStoreShellMethod
{
    class WindowsStoreAppListViewItemLibrary
    {
        private static List<WindowsStoreAppListViewItemGroup> _windowsStoreAppListViewItemGroups = new List<WindowsStoreAppListViewItemGroup>();

        public static List<WindowsStoreAppListViewItemGroup> WindowsStoreAppListViewItemGroups
        {
            get
            {
                RefreshList();
                return _windowsStoreAppListViewItemGroups;
            }
        }

        public static void RefreshList(bool force = false)
        {
            if(force || !_windowsStoreAppListViewItemGroups.Any())
            _windowsStoreAppListViewItemGroups = WindowsStoreLibrary.GetAppKeysFromRegistry().Select(windowsStoreApp => new WindowsStoreAppListViewItemGroup(windowsStoreApp)).ToList();
        }

    }
}
