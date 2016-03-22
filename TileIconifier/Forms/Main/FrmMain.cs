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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TileIconifier.Forms.CustomShortcutForms;
using TileIconifier.Properties;
using TileIconifier.Shortcut;
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

        protected override void ApplySkin()
        {
            base.ApplySkin();
            lblUnsaved.ForeColor = CurrentBaseSkin.ErrorColor;
            SetPictureBoxesBackColor();
        }

        private void frmDropper_Load(object sender, EventArgs e)
        {
            Show();
            StartFullUpdate();
            darkSkinToolStripMenuItem.Click += SkinToolStripMenuClick;
            defaultSkinToolStripMenuItem.Click += SkinToolStripMenuClick;
        }

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
                        @"A problem occurred with PowerShell functionality. It has been disabled
" + pinningException,
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
            srtlstShortcuts.Columns.Add("Shortcut Name", srtlstShortcuts.Width / 7 * 5 - 10, HorizontalAlignment.Left);
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

        private void SetPictureBoxesBackColor()
        {
            var color = GetPictureBoxesBackColor();
            pctMediumIcon.BackColor = color;
            pctSmallIcon.BackColor = color;
        }

        private Color GetPictureBoxesBackColor()
        {
            if (!string.Equals(cmbColour.Text, "custom", StringComparison.InvariantCultureIgnoreCase))
                return Color.FromName(cmbColour.Text);
            try
            {
                if (txtBGColour.Text.Length == txtBGColour.MaxLength)
                    return ColorUtils.HexToColor(txtBGColour.Text);
            }
            catch
            {
                // ignored
            }
            return CurrentBaseSkin.BackColor;
        }

        private void btnIconify_Click(object sender, EventArgs e)
        {
            if (!DoValidation())
                return;

            try
            {
                var showForegroundColourWarning = CurrentShortcutItem.ForegroundTextColourChanged;
                var tileIconify = new TileIcon(CurrentShortcutItem);
                tileIconify.RunIconify();
                CurrentShortcutItem.CommitChanges();
                UpdateShortcut();
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
            CurrentShortcutItem.ResetParameters();
            UpdateShortcut();
        }

        private bool DoValidation()
        {
            ResetValidation();

            return ValidateColour();
        }

        private void ResetValidation()
        {
            txtBGColour.BackColor = CurrentBaseSkin.BackColor;
            SetPictureBoxesBackColor();
        }

        private bool ValidateColour()
        {
            var valid = true;

            Action<Control> controlInvalid = c =>
            {
                c.BackColor = CurrentBaseSkin.ErrorColor;
                valid = false;
            };

            if (cmbColour.Text == @"Custom" && !Regex.Match(txtBGColour.Text, @"^#[0-9a-fA-F]{6}$").Success)
                controlInvalid(txtBGColour);

            if (CurrentShortcutItem.MediumImageBytes == null)
                controlInvalid(pctMediumIcon);

            if (CurrentShortcutItem.SmallImageBytes == null)
                controlInvalid(pctSmallIcon);

            return valid;
        }

        private void cmbColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBGColour.Enabled = cmbColour.Text == @"Custom";
            if (_currentShortcutListViewItem != null)
            {
                CurrentShortcutItem.BackgroundColor = cmbColour.Text == @"Custom" ? txtBGColour.Text : cmbColour.Text;
                UpdateShortcut();
            }
        }

        private void chkFGTxtEnabled_CheckedChanged(object sender, EventArgs e)
        {
            radFGDark.Enabled = chkFGTxtEnabled.Checked;
            radFGLight.Enabled = chkFGTxtEnabled.Checked;
            CurrentShortcutItem.ShowNameOnSquare150X150Logo = chkFGTxtEnabled.Checked;
            UpdateShortcut();
        }

        private void srtlstShortcuts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (srtlstShortcuts.SelectedItems.Count != 1)
                return;

            _currentShortcutListViewItem = (ShortcutItemListViewItem)srtlstShortcuts.SelectedItems[0];
            UpdateShortcut();
        }

        private void UpdateShortcut()
        {
            //disable event handlers whilst updating things programatically
            RemoveEventHandlers();

            //check if unsaved once per update
            var hasUnsavedChanges = CurrentShortcutItem.HasUnsavedChanges;

            //set shortcut path box to value stored in shortcut
            txtLnkPath.Text = CurrentShortcutItem.ShortcutFileInfo.FullName;
            txtLnkPath.SelectionStart = txtLnkPath.Text.Length;
            txtLnkPath.ScrollToCaret();

            //set exe path box to value stored in shortcut
            txtExePath.Text = CurrentShortcutItem.TargetFilePath;
            txtExePath.SelectionStart = txtExePath.Text.Length;
            txtExePath.ScrollToCaret();

            //only show remove if the icon is successfully iconified
            btnRemove.Enabled = CurrentShortcutItem.IsIconified;

            //update the picture boxes to show the relevant images
            pctMediumIcon.Image = CurrentShortcutItem.MediumImage();
            pctSmallIcon.Image = CurrentShortcutItem.MediumImage() != null
                ? CurrentShortcutItem.SmallImage()
                : null;


            //set relevant unsaved changes controls to required visibility/enabled states
            lblUnsaved.Visible = hasUnsavedChanges;
            btnIconify.Enabled = hasUnsavedChanges;
            btnReset.Enabled = hasUnsavedChanges;

            //reset the combo box - choose actual colour, or custom if none of the combobox items
            if (cmbColour.Items.Contains(CurrentShortcutItem.BackgroundColor))
            {
                cmbColour.SelectedItem = CurrentShortcutItem.BackgroundColor;
                txtBGColour.Enabled = false;
            }
            else
            {
                cmbColour.Text = @"Custom";
                txtBGColour.Enabled = true;
                txtBGColour.Text = CurrentShortcutItem.BackgroundColor;
            }

            //set the foreground text checkbox based on value stored for this shortcut
            chkFGTxtEnabled.Checked = CurrentShortcutItem.ShowNameOnSquare150X150Logo;

            //enable radio buttons if the foreground text is enabled
            radFGDark.Enabled = chkFGTxtEnabled.Checked;
            radFGLight.Enabled = chkFGTxtEnabled.Checked;

            //set the radio buttons based on the current shortcuts selection
            radFGDark.Checked = CurrentShortcutItem.ForegroundText == "dark";
            radFGLight.Checked = CurrentShortcutItem.ForegroundText == "light";

            //update the column view
            _currentShortcutListViewItem.UpdateColumns();
            var currentShortcutIndex = srtlstShortcuts.Items.IndexOf(_currentShortcutListViewItem);
            srtlstShortcuts.RedrawItems(
                currentShortcutIndex,
                currentShortcutIndex,
                false);

            //reset any validation failures
            ResetValidation();

            //re-add the event handlers now we've finished updating
            AddEventHandlers();
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

        private void pctMediumIcon_Click(object sender, EventArgs e)
        {
            if (_currentShortcutListViewItem == null) return;
            try
            {
                var imageToUse = ImageUtils.GetImage(this, CurrentShortcutItem.TargetFilePath);
                CurrentShortcutItem.MediumImageBytes = imageToUse;

                if (chkUseSameImg.Checked)
                    CurrentShortcutItem.SmallImageBytes = imageToUse;

                UpdateShortcut();
            }
            catch (UserCancellationException)
            {
            }
        }

        private void pctSmallIcon_Click(object sender, EventArgs e)
        {
            if (_currentShortcutListViewItem == null) return;

            try
            {
                var imageToUse = ImageUtils.GetImage(this, CurrentShortcutItem.TargetFilePath);
                CurrentShortcutItem.SmallImageBytes = imageToUse;

                if (chkUseSameImg.Checked)
                    CurrentShortcutItem.MediumImageBytes = imageToUse;

                UpdateShortcut();
            }
            catch (UserCancellationException)
            {
            }
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


            CurrentShortcutItem.BackgroundColor = txtBGColour.Text;
            UpdateShortcut();
        }

        private void radFGLight_CheckedChanged(object sender, EventArgs e)
        {
            CurrentShortcutItem.ForegroundText = radFGLight.Checked ? "light" : "dark";

            UpdateShortcut();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            CurrentShortcutItem.UndoChanges();

            UpdateShortcut();
        }

        private void AddEventHandlers()
        {
            txtBGColour.TextChanged += txtBGColour_TextChanged;
            cmbColour.SelectedIndexChanged += cmbColour_SelectedIndexChanged;
            chkFGTxtEnabled.CheckedChanged += chkFGTxtEnabled_CheckedChanged;
            radFGLight.CheckedChanged += radFGLight_CheckedChanged;
            srtlstShortcuts.SelectedIndexChanged += srtlstShortcuts_SelectedIndexChanged;
        }

        private void RemoveEventHandlers()
        {
            txtBGColour.TextChanged -= txtBGColour_TextChanged;
            cmbColour.SelectedIndexChanged -= cmbColour_SelectedIndexChanged;
            chkFGTxtEnabled.CheckedChanged -= chkFGTxtEnabled_CheckedChanged;
            radFGLight.CheckedChanged -= radFGLight_CheckedChanged;
            srtlstShortcuts.SelectedIndexChanged -= srtlstShortcuts_SelectedIndexChanged;
        }

        private async void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var updateDetails = await UpdateUtils.CheckForUpdate();

                if (updateDetails.UpdateAvailable)
                {
                    if (MessageBox.Show(
                        $@"An update is available! Would you like to visit the releases page? (Your version: {updateDetails
                            .CurrentVersion} - Latest version: {updateDetails.LatestVersion})",
                        @"New version available!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) == DialogResult.Yes
                        )
                    {
                        Process.Start("https://github.com/Jonno12345/TileIconify");
                    }
                }
                else
                {
                    MessageBox.Show(@"You are already on the latest version!", @"Up-to-date");
                }
            }
            catch
            {
                MessageBox.Show(
                    $@"An error occurred getting latest release information. Click Ok to visit the latest releases page to check manually. (Your version: {UpdateUtils
                        .CurrentVersion})",
                    @"Unable to check server!",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation);
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


        private static void CheckMenuItem(ToolStripDropDownItem mnu,
            ToolStripMenuItem checkedItem)
        {
            // Uncheck the menu items except checked_item.
            foreach (var menuItem in mnu.DropDownItems.OfType<ToolStripMenuItem>()
                .Select(item => item))
            {
                menuItem.Checked = Equals(menuItem, checkedItem);
            }
        }

        private void btnColourPicker_Click(object sender, EventArgs e)
        {
            if (clrDialog.ShowDialog(this) == DialogResult.OK)
            {
                txtBGColour.Text = ColorUtils.ColorToHex(clrDialog.Color);
            }
        }
    }
}