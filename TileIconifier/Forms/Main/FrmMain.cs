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
using System.Diagnostics;
using System.Windows.Forms;
using TileIconifier.Controls.PannablePictureBox;
using TileIconifier.Forms.CustomShortcutForms;
using TileIconifier.Shortcut;
using TileIconifier.Shortcut.Controls;
using TileIconifier.Skinning;
using TileIconifier.Skinning.Skins;
using TileIconifier.Skinning.Skins.Dark;
using TileIconifier.TileIconify;
using TileIconifier.Utilities;

namespace TileIconifier.Forms
{
    public partial class FrmMain : SkinnableForm
    {
        private ShortcutItemListViewItem _currentShortcutListViewItem;
        private List<ShortcutItemListViewItem> _shortcutsList;

        public FrmMain()
        {
            InitializeComponent();
            AddEventHandlers();
        }

        private ShortcutItem CurrentShortcutItem => _currentShortcutListViewItem.ShortcutItem;

        protected override void ApplySkin(object sender, EventArgs e)
        {
            base.ApplySkin(sender, e);
            lblUnsaved.ForeColor = CurrentBaseSkin.ErrorColor;
            SetPictureBoxesBackColor();
        }

        private void frmDropper_Load(object sender, EventArgs e)
        {
            darkSkinToolStripMenuItem.Click += SkinToolStripMenuClick;
            defaultSkinToolStripMenuItem.Click += SkinToolStripMenuClick;
            BuildPannableShortcutBoxControlPanels();
            Show();
            StartFullUpdate();
        }

        private void btnIconify_Click(object sender, EventArgs e)
        {
            if (!DoValidation())
                return;

            try
            {
                var showForegroundColourWarning = CurrentShortcutItem.Properties.ForegroundTextColourChanged;
                var tileIconify = new TileIcon(CurrentShortcutItem);
                tileIconify.RunIconify();
                CurrentShortcutItem.Properties.CommitChanges();
                UpdateControlsToShortcut();
                if (showForegroundColourWarning)
                    MessageBox.Show(
                        @"Foreground colour changes don't always instantly apply. If this change hasn't applied, try unpinning and repinning the shortcut.",
                        @"Foreground Colour Change", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (UserCancellationException)
            {
                // ignore if user cancelled
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (!DoValidation())
                return;

            if (MessageBox.Show(@"Are you sure you wish to remove iconification?", @"Confirm", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var tileDeIconify = new TileIcon(CurrentShortcutItem);
            tileDeIconify.DeIconify();
            CurrentShortcutItem.Properties.ResetParameters();
            UpdateControlsToShortcut();
        }


        private void cmbColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBGColour.Enabled = cmbColour.Text == @"Custom";
            if (_currentShortcutListViewItem == null) return;
            CurrentShortcutItem.Properties.CurrentState.BackgroundColor = cmbColour.Text == @"Custom"
                ? txtBGColour.Text
                : cmbColour.Text;
            UpdateControlsToShortcut();
        }

        private void chkFGTxtEnabled_CheckedChanged(object sender, EventArgs e)
        {
            radFGDark.Enabled = chkFGTxtEnabled.Checked;
            radFGLight.Enabled = chkFGTxtEnabled.Checked;
            CurrentShortcutItem.Properties.CurrentState.ShowNameOnSquare150X150Logo = chkFGTxtEnabled.Checked;
            UpdateControlsToShortcut();
        }

        private void srtlstShortcuts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (srtlstShortcuts.SelectedItems.Count != 1)
                return;

            _currentShortcutListViewItem = (ShortcutItemListViewItem) srtlstShortcuts.SelectedItems[0];
            UpdateControlsToShortcut();
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void getPinnedItemsRequiresPowershellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (getPinnedItemsRequiresPowershellToolStripMenuItem.Checked)
            {
                if (InvokeRequired)
                    Invoke(new Action(() => { getPinnedItemsRequiresPowershellToolStripMenuItem.Checked = false; }));
                else
                    getPinnedItemsRequiresPowershellToolStripMenuItem.Checked = false;
            }
            else
            {
                if (
                    MessageBox.Show(
                        @"Note- This feature uses Powershell and may take slightly longer to refresh. Continue?",
                        @"Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    getPinnedItemsRequiresPowershellToolStripMenuItem.Checked = true;
                }
            }
            StartFullUpdate();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUtils.ShowCenteredDialogForm<FrmAbout>(this);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUtils.ShowCenteredDialogForm<FrmHelp>(this);
        }

        private void customShortcutManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var customShortcutManager = new FrmCustomShortcutManagerMain())
            {
                customShortcutManager.ShowDialog(this);
            }
            StartFullUpdate();
        }

        private void refreshAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartFullUpdate();
        }

        private void txtBGColour_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text.Length != textBox.MaxLength)
                return;


            CurrentShortcutItem.Properties.CurrentState.BackgroundColor = txtBGColour.Text;
            UpdateControlsToShortcut();
        }

        private void radFGLight_CheckedChanged(object sender, EventArgs e)
        {
            CurrentShortcutItem.Properties.CurrentState.ForegroundText = radFGLight.Checked ? "light" : "dark";

            UpdateControlsToShortcut();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            CurrentShortcutItem.Properties.UndoChanges();

            UpdateControlsToShortcut();
        }


        private void PanPctMediumIcon_OnPannablePictureImagePropertyChange(object sender, EventArgs e)
        {
            var item = (PannablePictureBoxImage) sender;
            _currentShortcutListViewItem.ShortcutItem.Properties.CurrentState.MediumImage.X = item.X;
            _currentShortcutListViewItem.ShortcutItem.Properties.CurrentState.MediumImage.Y = item.Y;
            _currentShortcutListViewItem.ShortcutItem.Properties.CurrentState.MediumImage.Width = item.Width;
            _currentShortcutListViewItem.ShortcutItem.Properties.CurrentState.MediumImage.Height = item.Height;

            UpdateControlsToShortcut();
        }

        private void PanPctSmallIcon_OnPannablePictureImagePropertyChange(object sender, EventArgs e)
        {
            var item = (PannablePictureBoxImage) sender;
            _currentShortcutListViewItem.ShortcutItem.Properties.CurrentState.SmallImage.X = item.X;
            _currentShortcutListViewItem.ShortcutItem.Properties.CurrentState.SmallImage.Y = item.Y;
            _currentShortcutListViewItem.ShortcutItem.Properties.CurrentState.SmallImage.Width = item.Width;
            _currentShortcutListViewItem.ShortcutItem.Properties.CurrentState.SmallImage.Height = item.Height;

            UpdateControlsToShortcut();
        }


        private async void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var updateDetails = await UpdateUtils.CheckForUpdate();

                if (updateDetails.UpdateAvailable)
                {
                    if (MessageBox.Show(
                        $@"An update is available! Would you like to visit the releases page? (Your version: {
                            updateDetails
                                .CurrentVersion} - Latest version: {updateDetails.LatestVersion})",
                        @"New version available!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) == DialogResult.Yes
                        )
                    {
                        Process.Start("https://github.com/Jonno12345/TileIconifier/releases");
                    }
                }
                else
                {
                    MessageBox.Show(@"You are already on the latest version!", @"Up-to-date");
                }
            }
            catch
            {
                if (MessageBox.Show(
                    $@"An error occurred getting latest release information. Click Ok to visit the latest releases page to check manually. (Your version: {
                        UpdateUtils
                            .CurrentVersion})",
                    @"Unable to check server!",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation) == DialogResult.OK)
                {
                    Process.Start("https://github.com/Jonno12345/TileIconifier/releases");
                }
            }
        }

        private void SkinToolStripMenuClick(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            CheckMenuItem(skinToolStripMenuItem, item);
            UpdateSkin();
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

        private void btnColourPicker_Click(object sender, EventArgs e)
        {
            if (clrDialog.ShowDialog(this) == DialogResult.OK)
            {
                txtBGColour.Text = ColorUtils.ColorToHex(clrDialog.Color);
            }
        }

        private void panPctMediumIcon_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs) e).Button != MouseButtons.Right)
                return;

            var contextMenu = new ContextMenu();
            var menuItem = new MenuItem("Change image...",
                (o, args) => { MediumIconSet(); });
            contextMenu.MenuItems.Add(menuItem);
            menuItem = new MenuItem("Center image",
                (o, args) => { panPctMediumIcon.CenterImage(); });
            contextMenu.MenuItems.Add(menuItem);
            contextMenu.Show(panPctMediumIcon, ((MouseEventArgs) e).Location);
        }

        private void panPctMediumIcon_DoubleClick(object sender, EventArgs e)
        {
            MediumIconSet();
        }


        private void panPctSmallIcon_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs) e).Button != MouseButtons.Right)
                return;

            var contextMenu = new ContextMenu();
            var menuItem = new MenuItem("Change image...",
                (o, args) => { SmallIconSet(); });
            contextMenu.MenuItems.Add(menuItem);
            menuItem = new MenuItem("Center image",
                (o, args) => { panPctSmallIcon.CenterImage(); });
            contextMenu.MenuItems.Add(menuItem);
            contextMenu.Show(panPctSmallIcon, ((MouseEventArgs) e).Location);
        }

        private void panPctSmallIcon_DoubleClick(object sender, EventArgs e)
        {
            SmallIconSet();
        }
    }
}