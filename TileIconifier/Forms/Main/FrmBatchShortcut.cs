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

using System;
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

        private void FrmBatchShortcut_Load(object sender, EventArgs e)
        {
            GetIconifiedShortcuts();
            UpdateListViewBox();
        }

        private void GetIconifiedShortcuts()
        {
            _iconifiedItems =
                ShortcutItemListViewItemLibrary.LibraryAsListViewItems.Where(s => s.ShortcutItem.IsIconified).ToList();
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

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            SetAllItemsCheckedState(true);
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            SetAllItemsCheckedState(false);
        }

        private void SetAllItemsCheckedState(bool checkedState)
        {
            foreach (ListViewItem item in lstIconifiedItems.Items)
            {
                item.Checked = checkedState;
            }
        }

        private void colorPanel_ColorUpdate(object sender, EventArgs e)
        {
            var result = colorPanel.GetColorPanelResult();
            if (result == null)
            {
                return;
            }
            //pictureBox1.BackColor = result.BackgroundColor;
        }
    }
}