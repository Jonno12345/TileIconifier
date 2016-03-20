using System.IO;
using System.Windows.Forms;

namespace TileIconifier.Shortcut
{
    internal class ShortcutItemListViewItem : ListViewItem
    {
        public ShortcutItem ShortcutItem { get; }

        public ShortcutItemListViewItem(ShortcutItem shortcutItem)
        {
            ShortcutItem = shortcutItem;
            UpdateColumns();
        }

        public void UpdateColumns()
        {
            SubItems.Clear();
            Text = Path.GetFileNameWithoutExtension(ShortcutItem.ShortcutFileInfo.Name);
            SubItems.Add(ShortcutItem.IsIconified ? "✔" : "✘");
            var shortcutPinnedString = ShortcutItem.IsPinned == null ? "?" : ShortcutItem.IsPinned == true ? "✔" : "✘";
            SubItems.Add(shortcutPinnedString);
        }


        //public override string ToString()
        //{
        //    return Path.GetFileNameWithoutExtension(ShortcutItem.ShortcutFileInfo.Name) + (ShortcutItem.IsPinned ? " *" : "") +
        //           (ShortcutItem.IsIconified ? " #" : "");
        //}

    }
}
