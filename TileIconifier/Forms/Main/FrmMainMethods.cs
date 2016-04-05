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
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TileIconifier.Controls.PannablePictureBox;
using TileIconifier.Core;
using TileIconifier.Core.Shortcut;
using TileIconifier.Core.Utilities;
using TileIconifier.Forms.Shared;
using TileIconifier.Properties;
using TileIconifier.Shortcut.Controls;
using TileIconifier.Utilities;

namespace TileIconifier.Forms
{
    public partial class FrmMain
    {
        private void StartFullUpdate()
        {
            FormUtils.DoBackgroundWorkWithSplash(this, FullUpdate, "Refreshing");
        }

        private void UpdateControlsToShortcut()
        {
            //disable event handlers whilst updating things programatically
            RemoveEventHandlers();

            //check if unsaved once per update
            var hasUnsavedChanges = CurrentShortcutItem.Properties.HasUnsavedChanges;

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
            panPctMediumIcon.SetImage(CurrentShortcutItem.MediumImage(),
                CurrentShortcutItem.Properties.CurrentState.MediumImage.Width,
                CurrentShortcutItem.Properties.CurrentState.MediumImage.Height,
                CurrentShortcutItem.Properties.CurrentState.MediumImage.X,
                CurrentShortcutItem.Properties.CurrentState.MediumImage.Y);

            panPctSmallIcon.SetImage(CurrentShortcutItem.SmallImage(),
                CurrentShortcutItem.Properties.CurrentState.SmallImage.Width,
                CurrentShortcutItem.Properties.CurrentState.SmallImage.Height,
                CurrentShortcutItem.Properties.CurrentState.SmallImage.X,
                CurrentShortcutItem.Properties.CurrentState.SmallImage.Y);

            //update the picture box control panels
            pannablePictureBoxControlPanelMedium.UpdateControls();
            pannablePictureBoxControlPanelSmall.UpdateControls();

            //set relevant unsaved changes controls to required visibility/enabled states
            lblUnsaved.Visible = hasUnsavedChanges;
            btnIconify.Enabled = hasUnsavedChanges;
            btnReset.Enabled = hasUnsavedChanges;

            //reset the combo box - choose actual colour, or custom if none of the combobox items
            if (cmbColour.Items.Contains(CurrentShortcutItem.Properties.CurrentState.BackgroundColor))
            {
                cmbColour.SelectedItem = CurrentShortcutItem.Properties.CurrentState.BackgroundColor;
                txtBGColour.Enabled = false;
            }
            else
            {
                cmbColour.Text = @"Custom";
                txtBGColour.Enabled = true;
                txtBGColour.Text = CurrentShortcutItem.Properties.CurrentState.BackgroundColor;
            }

            //set the foreground text checkbox based on value stored for this shortcut
            chkFGTxtEnabled.Checked = CurrentShortcutItem.Properties.CurrentState.ShowNameOnSquare150X150Logo;

            //enable radio buttons if the foreground text is enabled
            radFGDark.Enabled = chkFGTxtEnabled.Checked;
            radFGLight.Enabled = chkFGTxtEnabled.Checked;

            //set the radio buttons based on the current shortcuts selection
            radFGDark.Checked = CurrentShortcutItem.Properties.CurrentState.ForegroundText == "dark";
            radFGLight.Checked = CurrentShortcutItem.Properties.CurrentState.ForegroundText == "light";

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
            srtlstShortcuts.Columns.Add("Shortcut Name", srtlstShortcuts.Width/7*5 - 10, HorizontalAlignment.Left);
            srtlstShortcuts.Columns.Add("Is Iconified?", srtlstShortcuts.Width/7 - 2, HorizontalAlignment.Left);
            srtlstShortcuts.Columns.Add("Is Pinned?", srtlstShortcuts.Width/7 - 4, HorizontalAlignment.Left);

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
            Action<PannablePictureBox> setBackColor = b =>
            {
                b.BackColor = b.PannablePictureBoxImage.Image == null ? CurrentBaseSkin.BackColor : color;
                b.Refresh();
            };
            setBackColor(panPctMediumIcon);
            setBackColor(panPctSmallIcon);
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
                c.BackColor = Color.Red;
                valid = false;
            };

            if (cmbColour.Text == @"Custom" && !Regex.Match(txtBGColour.Text, @"^#[0-9a-fA-F]{6}$").Success)
                controlInvalid(txtBGColour);

            if (CurrentShortcutItem.Properties.CurrentState.MediumImage.Bytes == null)
                controlInvalid(panPctMediumIcon);

            if (CurrentShortcutItem.Properties.CurrentState.SmallImage.Bytes == null)
                controlInvalid(panPctMediumIcon);

            return valid;
        }

        private void AddEventHandlers()
        {
            txtBGColour.TextChanged += txtBGColour_TextChanged;
            cmbColour.SelectedIndexChanged += cmbColour_SelectedIndexChanged;
            chkFGTxtEnabled.CheckedChanged += chkFGTxtEnabled_CheckedChanged;
            radFGLight.CheckedChanged += radFGLight_CheckedChanged;
            srtlstShortcuts.SelectedIndexChanged += srtlstShortcuts_SelectedIndexChanged;
            panPctMediumIcon.OnPannablePictureImagePropertyChange +=
                PanPctMediumIcon_OnPannablePictureImagePropertyChange;
            panPctSmallIcon.OnPannablePictureImagePropertyChange += PanPctSmallIcon_OnPannablePictureImagePropertyChange;
        }

        private void RemoveEventHandlers()
        {
            txtBGColour.TextChanged -= txtBGColour_TextChanged;
            cmbColour.SelectedIndexChanged -= cmbColour_SelectedIndexChanged;
            chkFGTxtEnabled.CheckedChanged -= chkFGTxtEnabled_CheckedChanged;
            radFGLight.CheckedChanged -= radFGLight_CheckedChanged;
            srtlstShortcuts.SelectedIndexChanged -= srtlstShortcuts_SelectedIndexChanged;
            panPctMediumIcon.OnPannablePictureImagePropertyChange -=
                PanPctMediumIcon_OnPannablePictureImagePropertyChange;
            panPctSmallIcon.OnPannablePictureImagePropertyChange -= PanPctSmallIcon_OnPannablePictureImagePropertyChange;
        }

        private void MediumIconSet()
        {
            try
            {
                var imageToUse = FrmIconSelector.GetImage(this, CurrentShortcutItem.TargetFilePath);
                CurrentShortcutItem.Properties.CurrentState.MediumImage.SetImage(imageToUse,
                    ShortcutConstantsAndEnums.MediumShortcutSize);

                if (chkUseSameImg.Checked)
                {
                    CurrentShortcutItem.Properties.CurrentState.SmallImage.SetImage(imageToUse,
                        ShortcutConstantsAndEnums.SmallShortcutSize);
                    panPctSmallIcon.CenterImage();
                }

                UpdateControlsToShortcut();

                panPctMediumIcon.CenterImage();
            }
            catch (UserCancellationException)
            {
            }
        }

        private void SmallIconSet()
        {
            try
            {
                var imageToUse = FrmIconSelector.GetImage(this, CurrentShortcutItem.TargetFilePath);

                CurrentShortcutItem.Properties.CurrentState.SmallImage.SetImage(imageToUse,
                    ShortcutConstantsAndEnums.SmallShortcutSize);

                if (chkUseSameImg.Checked)
                {
                    CurrentShortcutItem.Properties.CurrentState.MediumImage.SetImage(imageToUse,
                        ShortcutConstantsAndEnums.MediumShortcutSize);
                }

                UpdateControlsToShortcut();
            }
            catch (UserCancellationException)
            {
            }
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

        private void BuildPannableShortcutBoxControlPanels()
        {
            pannablePictureBoxControlPanelMedium.SetPannablePictureBoxControl(panPctMediumIcon);
            pannablePictureBoxControlPanelSmall.SetPannablePictureBoxControl(panPctSmallIcon);
            pannablePictureBoxControlPanelMedium.ChangeImageClick += (sender, args) => { MediumIconSet(); };
            pannablePictureBoxControlPanelSmall.ChangeImageClick += (sender, args) => { SmallIconSet(); };
            pannablePictureBoxControlPanelMedium.UpdateTrackBarAndZoom();
            pannablePictureBoxControlPanelSmall.UpdateTrackBarAndZoom();
        }
    }
}