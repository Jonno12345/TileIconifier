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

using System.Drawing;
using System.Windows.Forms;

namespace TileIconifier.Skinning.Skins.Dark
{
    public class DarkSkin : BaseSkin
    {
        #region "Common colors"
        //These colors are applied to various controls.
        //Simplifies skinning for similar UI elements.
        private Color LowBackColor = Color.FromArgb(20, 20, 20);
        private Color LowForeColor = Color.FromArgb(240, 240, 240);
        private Color LowDisabledForeColor = Color.FromArgb(130, 130, 130);
        private Color LowBorderColor = Color.FromArgb(70, 70, 70);

        private Color MediumBackColor = Color.FromArgb(50, 50, 50);
        private Color MediumForeColor = Color.FromArgb(240, 240, 240);
        private Color MediumDisabledForeColor = Color.FromArgb(140, 140, 140);
        private Color MediumBorderColor = Color.FromArgb(70, 70, 70);

        private Color HighBackColor = Color.FromArgb(70, 70, 70);
        private Color HighForeColor = Color.FromArgb(240, 240, 240);
        private Color HighDisabledForeColor = Color.FromArgb(150, 150, 150);
        private Color HighBorderColor = Color.FromArgb(90, 90, 90);

        private Color FocusedBorderColor = SystemColors.Highlight;
        #endregion

        #region "Basic properties"
        public override Color BackColor { get { return MediumBackColor; } }
        public override Color ForeColor { get { return MediumForeColor; } }
        public override Color DisabledForeColor { get { return MediumDisabledForeColor; } }
        public override Color HighlightBackColor { get { return FocusedBorderColor; } }
        public override Color ErrorForeColor { get { return Color.Red; } }

        //These objects are potentially more expensive to create, so we cache them.
        public override Font Font { get; } = new Font("Segoe UI", 8);
        public override ToolStripSystemRendererEx ToolStripRenderer { get; } = new ToolStripSystemRendererEx(new ToolStripDarkColorScheme());
        #endregion

        #region "Button"
        public override FlatStyle ButtonFlatStyle { get { return FlatStyle.Flat; } }
        public override Color ButtonForeColor { get { return HighForeColor; } }
        public override Color ButtonBackColor { get { return HighBackColor; } }
        public override Color ButtonDisabledForeColor { get { return HighDisabledForeColor; } }
        public override Color ButtonFlatBorderColor { get { return HighBorderColor; } }
        #endregion

        #region "TextBox"
        public override BorderStyle TextBoxBorderStyle { get { return BorderStyle.FixedSingle; } }
        public override Color TextBoxBackColor { get { return LowBackColor; } }
        public override Color TextBoxForeColor { get { return LowForeColor; } }
        public override Color TextBoxReadOnlyBackColor { get { return MediumBackColor; } }
        public override Color TextBoxBorderColor { get { return LowBorderColor; } }
        public override Color TextBoxBorderFocusedColor { get { return FocusedBorderColor; } }
        public override Color TextBoxBorderDisabledColor { get { return LowDisabledForeColor; } }
        #endregion

        #region "ListView"
        public override bool ListViewHeadersUseVisualStyleColors { get { return false; } }
        public override BorderStyle ListViewBorderStyle { get { return BorderStyle.FixedSingle; } }
        public override Color ListViewBackColor { get { return LowBackColor; } }
        public override Color ListViewForeColor { get { return LowForeColor; } }
        public override Color ListViewHeaderBackColor { get { return MediumBackColor; } }
        public override Color ListViewHeaderForeColor { get { return MediumForeColor; } }
        #endregion

        #region "ComboBox"
        public override FlatStyle ComboBoxFlatStyle { get { return FlatStyle.Flat; } }
        public override Color ComboBoxBackColor { get { return LowBackColor; } }
        public override Color ComboBoxForeColor { get { return LowForeColor; } }
        public override Color ComboBoxButtonBackColor { get { return MediumBackColor; } }
        public override Color ComboboxButtonForeColor { get { return MediumForeColor; } }
        public override Color ComboBoxDisabledForeColor { get { return MediumDisabledForeColor; } }
        public override Color ComboBoxButtonBorderColor { get { return MediumBorderColor; } }
        public override Color ComboBoxButtonBorderFocusedColor { get { return FocusedBorderColor; } }
        #endregion
    }
}