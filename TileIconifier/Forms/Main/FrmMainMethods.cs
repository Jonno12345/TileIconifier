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
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using TileIconifier.Controls.Shortcut;
using TileIconifier.Core.Shortcut;
using TileIconifier.Core.TileIconify;
using TileIconifier.Properties;
using TileIconifier.Skinning;
using TileIconifier.Skinning.Skins;
using TileIconifier.Skinning.Skins.Dark;
using TileIconifier.Utilities;

namespace TileIconifier.Forms
{
    public partial class FrmMain
    {
        private void StartFullUpdate()
        {
            FormUtils.DoBackgroundWorkWithSplash(this, FullUpdate, "Refreshing");
        }

        private void FullUpdate(object sender, DoWorkEventArgs e)
        {
            if (getPinnedItemsRequiresPowershellToolStripMenuItem.Checked)
            {
                Exception pinningException;
                _shortcutsList = ShortcutItemEnumeration.TryGetShortcutsWithPinning(out pinningException, true)
                    .Select(s => new ShortcutItemListViewItem(s))
                    .ToList();
                if (pinningException != null)
                {
                    MessageBox.Show(
                        $"A problem occurred with PowerShell functionality. It has been disabled\r\n{pinningException}",
                        @"PowerShell failure", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    getPinnedItemsRequiresPowershellToolStripMenuItem_Click(this, null);
                }
            }
            else
            {
                _shortcutsList = ShortcutItemEnumeration.GetShortcuts(true)
                    .Select(s => new ShortcutItemListViewItem(s))
                    .ToList();
            }

            if (srtlstShortcuts.InvokeRequired)
                srtlstShortcuts.Invoke(new Action(BuildShortcutList));
            else
                BuildShortcutList();
        }

        private void BuildShortcutList()
        {
            srtlstShortcuts.Items.Clear();
            srtlstShortcuts.Columns.Clear();
            srtlstShortcuts.Columns.Add("Shortcut Name", srtlstShortcuts.Width / 7 * 4 - 10, HorizontalAlignment.Left);
            srtlstShortcuts.Columns.Add("Is Custom?", srtlstShortcuts.Width / 7 - 2, HorizontalAlignment.Left);
            srtlstShortcuts.Columns.Add("Is Iconified?", srtlstShortcuts.Width / 7 - 2, HorizontalAlignment.Left);
            srtlstShortcuts.Columns.Add("Is Pinned?", srtlstShortcuts.Width / 7 - 4, HorizontalAlignment.Left);

            var smallImageList = new ImageList();
            for (var i = 0; i < _shortcutsList.Count; i++)
            {
                var shortcutItem = _shortcutsList[i];
                srtlstShortcuts.Items.Add(shortcutItem);
                smallImageList.Images.Add(shortcutItem.ShortcutItem.StandardIcon ??
                                          Resources.QuestionMark);
                shortcutItem.ImageIndex = i;
            }
            srtlstShortcuts.SmallImageList = smallImageList;

            if (srtlstShortcuts.Items.Count > 0)
                srtlstShortcuts.Items[0].Selected = true;
        }


        private static void CheckMenuItem(ToolStripDropDownItem mnu,
            ToolStripMenuItem checkedItem)
        {
            // Uncheck the menu items except checked item.
            foreach (var menuItem in mnu.DropDownItems.OfType<ToolStripMenuItem>()
                .Select(item => item))
            {
                menuItem.Checked = Equals(menuItem, checkedItem);
            }
        }

        private void UpdateSkin()
        {
            if (defaultSkinToolStripMenuItem.Checked)
            {
                SkinHandler.SetCurrentSkin(new BaseSkin());
                return;
            }
            if (!darkSkinToolStripMenuItem.Checked) return;
            SkinHandler.SetCurrentSkin(new DarkSkin());
        }

        private void UpdateFormControls()
        {
            //set path boxes to value stored in shortcut
            Action<TextBox, string> updateTextBox = (textBox, str) =>
            {
                textBox.Text = str;
                textBox.SelectionStart = txtLnkPath.Text.Length;
                textBox.ScrollToCaret();
            };
            updateTextBox(txtLnkPath, CurrentShortcutItem.ShortcutFileInfo.FullName);
            updateTextBox(txtExePath, CurrentShortcutItem.TargetFilePath);

            //only show remove if the icon is currently iconified
            btnRemove.Enabled = CurrentShortcutItem.IsIconified;

            //only enable Iconify button if shortcut has unsaved changes
            btnIconify.Enabled = CurrentShortcutItem.Properties.HasUnsavedChanges;

            //disable Build Custom Shortcut for items that are already custom shortcuts
            btnBuildCustomShortcut.Enabled = !CurrentShortcutItem.IsTileIconifierCustomShortcut;

            //disable Build Custom Shortcut for items that are already custom shortcuts
            btnDeleteCustomShortcut.Enabled = CurrentShortcutItem.IsTileIconifierCustomShortcut;

            //update the column view
            _currentShortcutListViewItem.UpdateColumns();
            var currentShortcutIndex = srtlstShortcuts.Items.IndexOf(_currentShortcutListViewItem);
            srtlstShortcuts.RedrawItems(
                currentShortcutIndex,
                currentShortcutIndex,
                false);
        }

        private void UpdateShortcut()
        {
            iconifyPanel.CurrentShortcutItem = CurrentShortcutItem;
            iconifyPanel.UpdateControlsToShortcut();
            UpdateFormControls();
        }

        private void JumpToShortcutItem(ShortcutItem shortcutItem)
        {
            var shortcutListViewItem =
                _shortcutsList.First(s => s.ShortcutItem.ShortcutFileInfo.FullName == shortcutItem.ShortcutFileInfo.FullName);
            var itemInListView = srtlstShortcuts.Items[srtlstShortcuts.Items.IndexOf(shortcutListViewItem)];
            itemInListView.Selected = true;
            itemInListView.EnsureVisible();
        }

        private TileIcon GenerateTileIcon()
        {
            return new TileIcon(CurrentShortcutItem);
        }
    }
}