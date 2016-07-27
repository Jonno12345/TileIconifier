using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TileIconifier.Controls.Shortcut;
using TileIconifier.Properties;

namespace TileIconifier.Forms.Main
{
    public partial class FrmBatchShortcut : SkinnableForm
    {
        private List<ShortcutItemListViewItem> _iconifiedItems; 

        public FrmBatchShortcut()
        {
            InitializeComponent();
        }

        private void FrmBatchShortcut_Load(object sender, System.EventArgs e)
        {
            GetIconifiedShortcuts();
            UpdateListViewBox();
        }

        private void GetIconifiedShortcuts()
        {
            _iconifiedItems = ShortcutItemListViewItemLibrary.LibraryAsListViewItems.Where(s => s.ShortcutItem.IsIconified).ToList();
        }

        private void UpdateListViewBox()
        {
            BuildListBoxColumns();
            UpdateListViewBoxItems();

        }

        private void BuildListBoxColumns()
        {
            lstIconifiedItems.Columns.Add("Shortcut Name", lstIconifiedItems.Width);
        }

        private void UpdateListViewBoxItems()
        {
            var smallImageList = new ImageList();
            for (var i = 0; i < _iconifiedItems.Count; i++)
            {
                var shortcutItem = _iconifiedItems[i];
                lstIconifiedItems.Items.Add(shortcutItem);
                smallImageList.Images.Add(shortcutItem.ShortcutItem.StandardIcon ??
                                          Resources.QuestionMark);
                shortcutItem.ImageIndex = i;
            }
            lstIconifiedItems.SmallImageList = smallImageList;
        }

        private void btnSelectAll_Click(object sender, System.EventArgs e)
        {
            foreach (ListViewItem item in lstIconifiedItems.Items)
            {
                item.Checked = true;
            }
        }

        private void btnSelectNone_Click(object sender, System.EventArgs e)
        {
            foreach (ListViewItem item in lstIconifiedItems.Items)
            {
                item.Checked = false;
            }
        }
    }
}
