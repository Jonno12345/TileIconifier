#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2017 Johnathon M
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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TileIconifier.Controls.Shortcut;
using TileIconifier.Core.Shortcut;
using TileIconifier.Core.TileIconify;
using TileIconifier.Core.Utilities;
using TileIconifier.Properties;
using TileIconifier.Utilities;

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
            ilsIconifiedItemsSmallIcons.ImageSize = SystemInformation.SmallIconSize;
            BuildListBoxColumns();

            GetIconifiedShortcuts();
            UpdateListViewBox();
            colorPanel_ColorUpdate(this, null);
        }

        /// <summary>
        ///     Initialize the columns of the list view with the shortcut items.
        /// </summary>
        private void BuildListBoxColumns()
        {
            lstIconifiedItems.Columns.Add("Shortcut Name", lstIconifiedItems.ClientSize.Width);
        }

        private void GetIconifiedShortcuts()
        {
            _iconifiedItems =
                ShortcutItemListViewItemLibrary.LibraryAsListViewItems.Where(s => s.ShortcutItem.IsIconified).ToList();
        }

        /// <summary>
        ///     Updates the items as well as the icons of the list view with the shortcut items.
        /// </summary>
        private void UpdateListViewBox()
        {
            UpdateListViewImageList();
            UpdateListViewBoxItems();
        }        

        /// <summary>
        ///     Updates the items of the list view with the shortcut items, excluding the icons.
        /// </summary>
        private void UpdateListViewBoxItems()
        {
            lstIconifiedItems.Items.Clear();
            for (var i = 0; i < _iconifiedItems.Count; i++)
            {
                var shortcutItem = _iconifiedItems[i];
                lstIconifiedItems.Items.Add(shortcutItem);                
                shortcutItem.ImageIndex = i;
            }            
        }

        /// <summary>
        ///     Updates the icons of the list view with the shortcut items.
        /// </summary>
        private void UpdateListViewImageList()
        {
            ilsIconifiedItemsSmallIcons.Images.Clear();
            for (var i = 0; i < _iconifiedItems.Count; i++)
            {
                var shortcutItem = _iconifiedItems[i];
                ilsIconifiedItemsSmallIcons.Images.Add(shortcutItem.ShortcutItem.StandardIcon ??
                                          Resources.QuestionMark);
            }
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
            pctColorPreview.BackColor = ColorUtils.HexOrNameToColor(result.BackgroundColor);
        }

        private void btnBatchAmendBackgroundColor_Click(object sender, EventArgs e)
        {
            var colorPanelResult = colorPanel.GetColorPanelResult();
            if (colorPanelResult == null)
            {
                FormUtils.ShowMessage(this, Strings.SelectValidColor);
                return;
            }

            if (RunBulkAction(item =>
            {
                item.Properties.CurrentState.BackgroundColor = colorPanelResult.BackgroundColor;

                new TileIcon(item).RunIconify();
            }))
            {
                FormUtils.ShowMessage(this, Strings.Completed, Strings.Completed);
            }
        }

        private List<ShortcutItem> ValidateListSelection()
        {
            //one more validation that the items are already iconified.
            var selectedItems = lstIconifiedItems.CheckedItems.Cast<ShortcutItemListViewItem>()
                .Where(s => s.ShortcutItem.IsIconified)
                .Select(s => s.ShortcutItem)
                .ToList();

            if (!selectedItems.Any())
            {
                FormUtils.ShowMessage(this, Strings.NoItemsHaveBeenSelected);
                return null;
            }

            if (
                FormUtils.ShowMessage(this,
                    $"You have selected {selectedItems.Count} shortcut(s) to be amended. Are you sure you wish to continue?",
                    Strings.Confirm,
                    MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return null;
            }

            return selectedItems;
        }

        private bool RunBulkAction(Action<ShortcutItem> bulkAction)
        {
            var batchSelection = ValidateListSelection();
            if (batchSelection == null)
            {
                return false;
            }

            FormUtils.DoBackgroundWorkWithSplash(this, (sender, args) =>
            {
                foreach (var shortcutItem in batchSelection)
                {
                    bulkAction(shortcutItem);
                }
            }, "Running batch operations");
            return true;
        }

        private void lstIconifiedItems_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            var contextMenu = new ContextMenu();
            var menuItem = new MenuItem("Get Background Color",
                (o, args) =>
                {
                    var item = lstIconifiedItems.GetItemAt(e.X, e.Y) as ShortcutItemListViewItem;
                    if (item == null)
                    {
                        return;
                    }
                    var shortcutItem = item.ShortcutItem;
                    colorPanel.SetBackgroundColor(shortcutItem.Properties.CurrentState.BackgroundColor);
                    colorPanel_ColorUpdate(this, null);
                });
            contextMenu.MenuItems.Add(menuItem);
            contextMenu.Show(lstIconifiedItems, e.Location);
        }

        private void btnAmendForegroundText_Click(object sender, EventArgs e)
        {
            var colorPanelResult = colorPanel.GetColorPanelResult();
            if (colorPanelResult == null)
            {
                FormUtils.ShowMessage(this, Strings.SelectValidColor);
                return;
            }

            if (RunBulkAction(item =>
            {
                item.Properties.CurrentState.ShowNameOnSquare150X150Logo = colorPanelResult.DisplayForegroundText;

                new TileIcon(item).RunIconify();
            }))
            {
                FormUtils.ShowMessage(this, Strings.Completed, Strings.Completed);
            }
        }

        private void btnAmendForegroundColor_Click(object sender, EventArgs e)
        {
            var colorPanelResult = colorPanel.GetColorPanelResult();
            if (colorPanelResult == null)
            {
                FormUtils.ShowMessage(this, Strings.SelectValidColor);
                return;
            }

            if (RunBulkAction(item =>
            {
                item.Properties.CurrentState.ForegroundText = colorPanelResult.ForegroundColor;

                new TileIcon(item).RunIconify();
            }))
            {
                FormUtils.ShowMessage(this, Strings.Completed, Strings.Completed);
            }
        }
    }
}