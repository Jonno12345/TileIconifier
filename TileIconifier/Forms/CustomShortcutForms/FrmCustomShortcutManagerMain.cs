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
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TileIconifier.Custom;
using TileIconifier.Properties;
using TileIconifier.Utilities;

namespace TileIconifier.Forms.CustomShortcutForms
{
    public partial class FrmCustomShortcutManagerMain : SkinnableForm
    {
        private List<CustomShortcutListViewItem> _customShortcutsList;

        public FrmCustomShortcutManagerMain()
        {
            InitializeComponent();
            RefreshCustomShortcuts();
        }

        private void RefreshCustomShortcuts()
        {
            var customShortcuts = LoadCustomShortcuts();
            _customShortcutsList = customShortcuts.Select(c => new CustomShortcutListViewItem(c)).ToList();
            lstCustomShortcuts.Clear();
            lstCustomShortcuts.Columns.Clear();

            lstCustomShortcuts.Columns.Add("Shortcut Name", lstCustomShortcuts.Width/4*2 - 2, HorizontalAlignment.Left);
            lstCustomShortcuts.Columns.Add("Shortcut Type", lstCustomShortcuts.Width/4 - 1, HorizontalAlignment.Left);
            lstCustomShortcuts.Columns.Add("Shortcut User", lstCustomShortcuts.Width/4 - 1, HorizontalAlignment.Left);

            var smallImageList = new ImageList();
            for (var i = 0; i < _customShortcutsList.Count; i++)
            {
                smallImageList.Images.Add(_customShortcutsList[i].CustomShortcut.ShortcutItem.MediumImage() ??
                                          (_customShortcutsList[i].CustomShortcut.ShortcutItem.StandardIcon ??
                                           Resources.QuestionMark));
                _customShortcutsList[i].ImageIndex = i;
                lstCustomShortcuts.Items.Add(_customShortcutsList[i]);
            }
            lstCustomShortcuts.SmallImageList = smallImageList;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUtils.ShowCenteredDialogForm<FrmCustomShortcutManagerHelp>(this);
        }

        private void frmCustomShortcutManagerMain_Load(object sender, EventArgs e)
        {
        }

        private static IEnumerable<CustomShortcut> LoadCustomShortcuts()
        {
            if (!Directory.Exists(CustomShortcutGetters.CustomShortcutVbsPath))
                return new List<CustomShortcut>();

            //get all VBS files built by TileIconifier
            return new DirectoryInfo(CustomShortcutGetters.CustomShortcutVbsPath)
                .GetFiles("*.vbs", SearchOption.AllDirectories)
                .Select(vbsFile => CustomShortcut.Load(vbsFile.FullName))
                .Where(customShortcut => customShortcut.ShortcutItem.ShortcutUser != ShortcutUser.Unknown)
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

            var customShortcut = (CustomShortcutListViewItem) lstCustomShortcuts.SelectedItems[0];

            if (
                MessageBox.Show(
                    $"Are you sure you wish to delete the custom shortcut for {customShortcut.Text.QuoteWrap()}?",
                    @"Are you sure?",
                    MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            try
            {
                customShortcut.CustomShortcut.Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Unable to clear up shortcuts." + ex);
            }

            //update our lists and refresh
            RefreshCustomShortcuts();
        }
    }
}