using System.Windows.Forms;

namespace TileIconifier.Custom
{
    class CustomShortcutListViewItem : ListViewItem
    {
        public CustomShortcut CustomShortcut { get; }
        
        public CustomShortcutListViewItem(CustomShortcut customShortcut)
        {
            CustomShortcut = customShortcut;
            Text = CustomShortcut.ShortcutName;
            SubItems.Add(CustomShortcut.ShortcutType.ToString());
            SubItems.Add(CustomShortcut.ShortcutItem.ShortcutUser.ToString());
        }
    }
}
