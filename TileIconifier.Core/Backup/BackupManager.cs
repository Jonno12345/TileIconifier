using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileIconifier.Core.Shortcut;

namespace TileIconifier.Core.Backup
{
    class BackupManager
    {
        private List<ShortcutItem> _shortcutItems = new List<ShortcutItem>();
        
        public void AddShortcutsToBackup(List<ShortcutItem> shortcutItems)
        {
            foreach (var shortcutItem in shortcutItems.Where(shortcutItem => shortcutItem.IsIconified))
            {
                _shortcutItems.Add(shortcutItem);
            }
        }
    }
}
