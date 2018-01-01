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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TileIconifier.Controls.Custom;
using TileIconifier.Core.Custom;
using TileIconifier.Core.Shortcut;
using TileIconifier.Properties;
using TileIconifier.Utilities;

namespace TileIconifier.Forms.CustomShortcutForms
{
    public partial class FrmCustomShortcutManagerMain : SkinnableForm
    {
        private List<CustomShortcut> _customShortcutsList;
        public ShortcutItem GotoShortcutItem;

        public FrmCustomShortcutManagerMain()
        {
            InitializeComponent();
            BuildListBoxColumns();
            //This clears all the images, so its really important that we do it before the list view is filled.
            ilsCustomShortcutsSmallIcons.ImageSize = SystemInformation.SmallIconSize;

            RefreshCustomShortcuts();
        }

        private void BuildListBoxColumns()
        {
            lstCustomShortcuts.BeginUpdate();

            lstCustomShortcuts.Columns.Clear();
            lstCustomShortcuts.Columns.Add(Strings.ShortcutName, lstCustomShortcuts.Width / 4 * 2 - 2,
                HorizontalAlignment.Left);
            lstCustomShortcuts.Columns.Add(Strings.ShortcutType, lstCustomShortcuts.Width / 4 - 1,
                HorizontalAlignment.Left);
            lstCustomShortcuts.Columns.Add(Strings.ShortcutUser, lstCustomShortcuts.Width / 4 - 1,
                HorizontalAlignment.Left);

            lstCustomShortcuts.EndUpdate();
        }

        /// <summary>
        ///     Refreshes the internal list of custom shortcuts as well as all the elements in the listview.
        /// </summary>
        private void RefreshCustomShortcuts()
        {
            //Refresh the list of custom shortcuts
            _customShortcutsList = LoadCustomShortcuts().ToList();

            //Refresh the image list
            BuildListViewImageList();

            //Refresh the list view items
            lstCustomShortcuts.Items.Clear();
            for (var i = 0; i < _customShortcutsList.Count; i++)
            {
                var item = new CustomShortcutListViewItem(_customShortcutsList[i]);
                item.ImageIndex = i;
                lstCustomShortcuts.Items.Add(item);
            }
        }

        private void BuildListViewImageList()
        {
            ilsCustomShortcutsSmallIcons.Images.Clear();

            for (var i = 0; i < _customShortcutsList.Count; i++)
            {
                ilsCustomShortcutsSmallIcons.Images.Add(
                    _customShortcutsList[i].ShortcutItem.Properties.CurrentState.MediumImage.CachedImage() ??
                    (_customShortcutsList[i].ShortcutItem.StandardIcon ??
                     Resources.QuestionMark));
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUtils.ShowCenteredDialogForm<FrmCustomShortcutManagerHelp>(this);
        }

        private static IEnumerable<CustomShortcut> LoadCustomShortcuts()
        {
            if (!Directory.Exists(CustomShortcutGetters.CustomShortcutVbsPath))
                return new List<CustomShortcut>();

            //get all VBS files built by TileIconifier
            return new DirectoryInfo(CustomShortcutGetters.CustomShortcutVbsPath)
                .GetFiles("*.vbs", SearchOption.AllDirectories)
                .Select(vbsFile =>
                {
                    try
                    {
                        return CustomShortcut.Load(vbsFile.FullName);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(customShortcut => customShortcut != null && customShortcut.ShortcutItem.ShortcutUser != ShortcutUser.Unknown)
                .ToList();
        }

        private void btnCreateNewShortcut_Click(object sender, EventArgs e)
        {
            using (var newShortcutForm = new FrmCustomShortcutManagerNew())
            {
                newShortcutForm.ShowDialog(this);
            }
            RefreshCustomShortcuts();
        }

        private void btnDeleteCustomShortcut_Click(object sender, EventArgs e)
        {
            if (lstCustomShortcuts.SelectedItems.Count == 0)
                return;

            var customShortcut = (CustomShortcutListViewItem)lstCustomShortcuts.SelectedItems[0];

            if (
                FormUtils.ShowMessage(this,
                    string.Format(Strings.ConfirmDeleteCustomShortcut,
                        customShortcut.Text.QuoteWrap()),
                    Strings.AreYouSure,
                    MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            try
            {
                customShortcut.CustomShortcut.Delete();
            }
            catch (Exception ex)
            {
                FormUtils.ShowMessage(this, Strings.UnableToClearUpShortcuts + ex);
            }

            //update our lists and refresh
            RefreshCustomShortcuts();
        }

        private void btnGotoShortcut_Click(object sender, EventArgs e)
        {
            if (lstCustomShortcuts.SelectedItems.Count == 0)
                return;

            GotoShortcutItem =
                ((CustomShortcutListViewItem)lstCustomShortcuts.SelectedItems[0]).CustomShortcut.ShortcutItem;
            Close();
        }

        private void FrmCustomShortcutManagerMain_Resize(object sender, EventArgs e)
        {
            BuildListBoxColumns();
        }
    }
}