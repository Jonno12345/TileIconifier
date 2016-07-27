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
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TileIconifier.Controls.Eyedropper;
using TileIconifier.Core.Shortcut;
using TileIconifier.Core.Utilities;

namespace TileIconifier.Controls.IconifierPanel
{
    public partial class ColorPanel : UserControl
    {
        public event EventHandler OnUpdate;

        public ColorPanel()
        {
            InitializeComponent();
            AddEventHandlers();
        }

        public ShortcutItem CurrentShortcutItem { get; set; }

        private void eyedropperColorPicker_SelectedColorChanged(object sender, EventArgs e)
        {
            var eyedropper = (EyedropColorPicker) sender;
            txtBGColour.Text = ColorUtils.ColorToHex(eyedropper.SelectedColor);
        }

        private void txtBGColour_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text.Length != textBox.MaxLength)
            {
                return;
            }

            CurrentShortcutItem.Properties.CurrentState.BackgroundColor = txtBGColour.Text;
            RunFullUpdate();
        }

        private void cmbColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBGColour.Enabled = cmbColour.Text == @"Custom";
            CurrentShortcutItem.Properties.CurrentState.BackgroundColor = cmbColour.Text == @"Custom"
                ? txtBGColour.Text
                : cmbColour.Text;
            RunFullUpdate();
        }

        private void chkFGTxtEnabled_CheckedChanged(object sender, EventArgs e)
        {
            radFGDark.Enabled = chkFGTxtEnabled.Checked;
            radFGLight.Enabled = chkFGTxtEnabled.Checked;
            CurrentShortcutItem.Properties.CurrentState.ShowNameOnSquare150X150Logo = chkFGTxtEnabled.Checked;
            RunFullUpdate();
        }

        private void radFGLight_CheckedChanged(object sender, EventArgs e)
        {
            CurrentShortcutItem.Properties.CurrentState.ForegroundText = radFGLight.Checked ? "light" : "dark";

            RunFullUpdate();
        }

        public Color GetPictureBoxesBackColor()
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
            return Skinning.SkinHandler.GetCurrentSkin().BackColor;
        }

        private void RunFullUpdate()
        {
            UpdateControlsToCurrentShortcut();
            OnUpdate?.Invoke(this, null);
        }

        public void UpdateControlsToCurrentShortcut()
        {
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
        }

        private void btnColourPicker_Click(object sender, EventArgs e)
        {
            clrDialog.CustomColors = new[]
{ColorTranslator.ToOle(ColorUtils.HexToColor(ShortcutConstantsAndEnums.DefaultAccentColor))};
            clrDialog.Color = cmbColour.Text.ToLower() == "custom"
                ? ColorUtils.HexToColor(txtBGColour.Text)
                : Color.FromName(cmbColour.Text);

            if (clrDialog.ShowDialog(this) == DialogResult.OK)
            {
                txtBGColour.Text = ColorUtils.ColorToHex(clrDialog.Color);
            }
        }

        public void RemoveEventHandlers()
        {
            txtBGColour.TextChanged -= txtBGColour_TextChanged;
            cmbColour.SelectedIndexChanged -= cmbColour_SelectedIndexChanged;
            chkFGTxtEnabled.CheckedChanged -= chkFGTxtEnabled_CheckedChanged;
            radFGLight.CheckedChanged -= radFGLight_CheckedChanged;
        }

        public void AddEventHandlers()
        {
            txtBGColour.TextChanged += txtBGColour_TextChanged;
            cmbColour.SelectedIndexChanged += cmbColour_SelectedIndexChanged;
            chkFGTxtEnabled.CheckedChanged += chkFGTxtEnabled_CheckedChanged;
            radFGLight.CheckedChanged += radFGLight_CheckedChanged;
        }

        public bool ValidateControls()
        {
            var valid = true;

            Action<Control> controlInvalid = c =>
            {
                c.BackColor = Color.Red;
                valid = false;
            };

            if (cmbColour.Text == @"Custom" && !Regex.Match(txtBGColour.Text, @"^#[0-9a-fA-F]{6}$").Success)
            {
                controlInvalid(txtBGColour);
            }

            return valid;
        }

        public void ResetValidation()
        {
            txtBGColour.BackColor = Skinning.SkinHandler.GetCurrentSkin().BackColor;
        }
    }
}