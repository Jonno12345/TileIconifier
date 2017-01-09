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
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TileIconifier.Controls.Eyedropper;
using TileIconifier.Core.Shortcut;
using TileIconifier.Core.Utilities;
using TileIconifier.Skinning;

namespace TileIconifier.Controls.IconifierPanel
{
    public partial class ColorPanel : UserControl
    {
        private readonly Color[] _dropDownColors =
        {
            Color.Black,
            Color.Silver,
            Color.Gray,
            Color.White,
            Color.Maroon,
            Color.Red,
            Color.Purple,
            Color.Fuchsia,
            Color.Green,
            Color.Lime,
            Color.Olive,
            Color.Yellow,
            Color.Navy,
            Color.Blue,
            Color.Teal,
            Color.Aqua
        };

        public ColorPanel()
        {
            InitializeComponent();
            AddEventHandlers();
        }

        public event EventHandler ColorUpdate;

        /// <summary>
        ///     Validate and get the output from ColorPanel. Returns null if validation fails
        /// </summary>
        /// <returns>null or new ColorPanelResult</returns>
        public ColorPanelResult GetColorPanelResult()
        {
            var validateControls = ValidateControls();
            //a validation failure should return a null value instead of a valid ColorPanelResult
            if (!validateControls)
            {
                return null;
            }

            return new ColorPanelResult
            {
                BackgroundColor = GetBackgroundColor(),
                DisplayForegroundText = chkFGTxtEnabled.Checked,
                ForegroundColor = radFGLight.Checked ? "light" : "dark"
            };
        }

        public void SetBackgroundColor(string color)
        {
            RemoveEventHandlers();
            //reset the combo box -choose actual color, or custom if none of the combobox items match
            var matchingColor = _dropDownColors.FirstOrDefault(c => string.Equals(c.Name, color, StringComparison.InvariantCultureIgnoreCase));
            if (matchingColor != Color.Empty)
            {
                cmbColour.SelectedItem = matchingColor.Name.ToLower();
                txtBGColour.Enabled = false;
            }
            else
            {
                cmbColour.Text = @"Custom";
                txtBGColour.Enabled = true;
                txtBGColour.Text = color;
            }
            AddEventHandlers();
        }

        public void SetForegroundTextShow(bool b)
        {
            RemoveEventHandlers();
            
            chkFGTxtEnabled.Checked = b;
            radFGDark.Enabled = b;
            radFGLight.Enabled = b;

            AddEventHandlers();
        }

        public void SetForegroundColorRadio(bool light)
        {
            RemoveEventHandlers();

            radFGDark.Checked = !light;
            radFGLight.Checked = light;

            AddEventHandlers();
        }

        public bool ValidateControls()
        {
            var valid = true;

            Action<Control> controlInvalid = c =>
            {
                c.BackColor = SkinHandler.GetCurrentSkin().ErrorColor;
                valid = false;
            };

            //if a custom color has been specified, check it's valid hex REGEX
            if (cmbColour.Text == @"Custom" && !Regex.Match(txtBGColour.Text, @"^#[0-9a-fA-F]{6}$").Success)
            {
                controlInvalid(txtBGColour);
            }

            return valid;
        }

        public void ResetValidation()
        {
            txtBGColour.BackColor = SkinHandler.GetCurrentSkin().BackColor;
        }

        private void RemoveEventHandlers()
        {
            txtBGColour.TextChanged -= txtBGColour_TextChanged;
            cmbColour.SelectedIndexChanged -= cmbColour_SelectedIndexChanged;
            chkFGTxtEnabled.CheckedChanged -= chkFGTxtEnabled_CheckedChanged;
            radFGLight.CheckedChanged -= radFGLight_CheckedChanged;
        }

        private void AddEventHandlers()
        {
            txtBGColour.TextChanged += txtBGColour_TextChanged;
            cmbColour.SelectedIndexChanged += cmbColour_SelectedIndexChanged;
            chkFGTxtEnabled.CheckedChanged += chkFGTxtEnabled_CheckedChanged;
            radFGLight.CheckedChanged += radFGLight_CheckedChanged;
        }

        private void eyedropperColorPicker_SelectedColorChanged(object sender, EventArgs e)
        {
            var eyedropper = (EyedropColorPicker) sender;
            txtBGColour.Text = ColorUtils.ColorToHex(eyedropper.SelectedColor);
        }

        private void txtBGColour_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox) sender;
            //if the textbox isn't filled it's definitely not a valid color, don't fire event
            if (textBox != null && textBox.Text.Length != textBox.MaxLength)
            {
                return;
            }

            cmbColour.Text = @"Custom";
            RunFullUpdate();
        }

        private void cmbColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBGColour.Enabled = cmbColour.Text == @"Custom";
            RunFullUpdate();
        }

        private string GetBackgroundColor()
        {
            return cmbColour.Text == @"Custom"
                ? txtBGColour.Text
                : cmbColour.Text;
        }

        private void chkFGTxtEnabled_CheckedChanged(object sender, EventArgs e)
        {
            radFGDark.Enabled = chkFGTxtEnabled.Checked;
            radFGLight.Enabled = chkFGTxtEnabled.Checked;
            RunFullUpdate();
        }

        private void radFGLight_CheckedChanged(object sender, EventArgs e)
        {
            RunFullUpdate();
        }

        private void RunFullUpdate()
        {
            ColorUpdate?.Invoke(this, null);
        }

        private void btnColourPicker_Click(object sender, EventArgs e)
        {
            clrDialog.CustomColors = new[]
            {
                ColorTranslator.ToOle(ColorUtils.HexToColor(ShortcutConstantsAndEnums.DefaultAccentColor))
            };

            clrDialog.Color = cmbColour.Text.ToLower() == @"custom"
                ? ColorUtils.HexToColor(txtBGColour.Text)
                : Color.FromName(cmbColour.Text);

            if (clrDialog.ShowDialog(this) == DialogResult.OK)
            {
                txtBGColour.Text = ColorUtils.ColorToHex(clrDialog.Color);
            }
        }

        private void ColorPanel_Load(object sender, EventArgs e)
        {
            cmbColour.Items.AddRange(_dropDownColors.Select(c => c.Name.ToLower()).ToArray<object>());
            cmbColour.Items.Add("Custom");
            cmbColour.SelectedItem = "Custom";
        }
    }

    public class ColorPanelResult
    {
        public string BackgroundColor;
        public bool DisplayForegroundText;
        public string ForegroundColor;
    }
}