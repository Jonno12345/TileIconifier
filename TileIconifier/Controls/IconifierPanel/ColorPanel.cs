#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2021 Johnathon M
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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TileIconifier.Controls.Eyedropper;
using TileIconifier.Core;
using TileIconifier.Core.Enums;
using TileIconifier.Core.Shortcut;
using TileIconifier.Core.Utilities;
using TileIconifier.Skinning;

namespace TileIconifier.Controls.IconifierPanel
{
    public partial class ColorPanel : UserControl
    {
        private readonly List<ColorSelection> colorSelections = new List<ColorSelection>
        {
            ColorSelection.Black,
        ColorSelection.Silver,
        ColorSelection.Gray,
        ColorSelection.White,
        ColorSelection.Maroon,
        ColorSelection.Red,
        ColorSelection.Purple,
        ColorSelection.Fuchsia,
        ColorSelection.Green,
        ColorSelection.Lime,
        ColorSelection.Olive,
        ColorSelection.Yellow,
        ColorSelection.Navy,
        ColorSelection.Blue,
        ColorSelection.Teal,
        ColorSelection.Aqua,
ColorSelection.Custom
        };

        private ColorSelection? CurrentColorSelection
        {
            get
            {
                if (Enum.TryParse<ColorSelection>(cmbColour.Text, out ColorSelection colorSelection))
                {
                    return colorSelection;
                }
                return null;
            }
        }
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
                ColorSelection = CurrentColorSelection,
                BackgroundColor = GetBackgroundColor(),
                DisplayForegroundText = chkFGTxtEnabled.Checked,
                ForegroundColor = radFGLight.Checked ? "light" : "dark"
            };
        }

        public void SetBackgroundColor(ColorSelection color, string customColor)
        {
            RemoveEventHandlers();
            //reset the combo box -choose actual color, or custom if none of the combobox items match
            if (color == ColorSelection.Custom)
            {
                cmbColour.Text = ColorSelection.Custom.ToString();
                txtBGColour.Enabled = true;
                txtBGColour.Text = customColor;

            }
            else if (color == ColorSelection.Default)
            {
                cmbColour.Text = ColorSelection.Default.ToString();
            }
            else
            {
                cmbColour.Text = color.ToString();
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
                c.BackColor = SkinHandler.GetCurrentSkin().ErrorForeColor;
                valid = false;
            };

            //if a custom color has been specified, check it's valid hex REGEX
            if (CurrentColorSelection == ColorSelection.Custom && !Regex.Match(txtBGColour.Text, @"^#[0-9a-fA-F]{6}$").Success)
            {
                controlInvalid(txtBGColour);
            }

            return valid;
        }

        public void ResetValidation()
        {
            txtBGColour.BackColor = SkinHandler.GetCurrentSkin().TextBoxBackColor;
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
            var eyedropper = (EyedropColorPicker)sender;
            txtBGColour.Text = ColorUtils.ColorToHex(eyedropper.SelectedColor);
        }

        private void txtBGColour_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            //if the textbox isn't filled it's definitely not a valid color, don't fire event
            if (textBox != null && textBox.Text.Length != textBox.MaxLength)
            {
                return;
            }

            cmbColour.Text = ColorSelection.Custom.ToString();
            RunFullUpdate();
        }

        private void cmbColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBGColour.Enabled = CurrentColorSelection == ColorSelection.Custom;
            RunFullUpdate();
        }

        private string GetBackgroundColor()
        {
            if (CurrentColorSelection == ColorSelection.Custom)
            {
                return txtBGColour.Text;
            }
            if (CurrentColorSelection == ColorSelection.Default)
            {
                return ShortcutConstantsAndEnums.DefaultAccentColor;
            }
            return cmbColour.Text.ToLower();
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
            if (Config.Instance.CustomColors == null)
            {
                clrDialog.CustomColors = new[]
                {ColorTranslator.ToOle(ColorUtils.HexToColor(ShortcutConstantsAndEnums.DefaultAccentColor))};
            }
            else
            {
                clrDialog.CustomColors = Config.Instance.CustomColors;
            }

            clrDialog.Color = cmbColour.Text == ColorSelection.Custom.ToString()
                ? ColorUtils.HexToColor(txtBGColour.Text)
                : Color.FromName(cmbColour.Text);

            if (clrDialog.ShowDialog(this) == DialogResult.OK)
            {
                txtBGColour.Text = ColorUtils.ColorToHex(clrDialog.Color);
                Config.Instance.CustomColors = clrDialog.CustomColors;
                Config.Instance.SaveConfig();
            }
        }

        private void ColorPanel_Load(object sender, EventArgs e)
        {
            if (Config.StartMenuUpgradeEnabled)
            {
                colorSelections.Add(ColorSelection.Default);
            }
            cmbColour.Items.AddRange(colorSelections.Select(t => t.ToString()).ToArray());
            cmbColour.SelectedItem = ColorSelection.Custom.ToString();
        }
    }

    public class ColorPanelResult
    {
        public string BackgroundColor { get; set; }
        public bool DisplayForegroundText { get; set; }
        public string ForegroundColor { get; set; }

        public ColorSelection? ColorSelection { get; set; }
    }
}