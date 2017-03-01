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
        public override Color BackColor => ColorTranslator.FromHtml("#323232");
        public override Color ForeColor => ColorTranslator.FromHtml("#E3E3E3");
        //public override Color DisabledForeColor => Color.LightGray;
        public override Color DisabledBackColor => Color.DarkGray;

        public override Color SortableListViewBackColor => Color.Black;

        public override ToolStripSystemRendererEx ToolStripRenderer { get; } =
            new ToolStripSystemRendererEx(new ToolStripDarkColorScheme());

        #region "Button"
        public override FlatStyle ButtonFlatStyle { get { return FlatStyle.Standard; } }
        public override Color ButtonForeColor { get { return SystemColors.ControlText; } }
        public override Color ButtonBackColor { get { return SystemColors.Control; } }
        public override Color ButtonDisabledForeColor { get { return SystemColors.GrayText; } }
        public override Color ButtonFlatBorderColor { get { return Color.Empty; } } //not used
        #endregion

        #region "TextBox"
        public override BorderStyle TextBoxBorderStyle { get { return BorderStyle.Fixed3D; } }
        public override Color TextBoxBackColor { get { return SystemColors.Window; } }
        public override Color TextBoxForeColor { get { return SystemColors.WindowText; } }
        public override Color TextBoxReadOnlyBackColor { get { return SystemColors.Control; } }
        public override Color TextBoxBorderColor { get { return Color.Empty; } } //not used
        #endregion

        #region "ListView"
        public override bool ListViewHeadersUseVisualStyleColors { get { return true; } }
        public override BorderStyle ListViewBorderStyle { get { return BorderStyle.Fixed3D; } }
        public override Color ListViewBackColor { get { return SystemColors.Window; } }
        public override Color ListViewForeColor { get { return SystemColors.WindowText; } }
        public override Color ListViewHeaderBackColor { get { return SystemColors.Control; } } //not used
        public override Color ListViewHeaderForeColor { get { return SystemColors.ControlText; } } //not used
        #endregion

        #region "ComboBox"
        public override FlatStyle ComboBoxFlatStyle { get { return FlatStyle.Standard; } }
        public override Color ComboBoxBackColor { get { return SystemColors.Window; } }
        public override Color ComboBoxForeColor { get { return SystemColors.WindowText; } }
        public override Color ComboBoxButtonBackColor { get { return SystemColors.Control; } } //not used
        public override Color ComboboxButtonForeColor { get { return SystemColors.ControlText; } } //not used
        public override Color ComboBoxDisabledForeColor { get { return SystemColors.GrayText; } } //not used
        public override Color ComboBoxButtonBorderColor { get { return SystemColors.ControlDark; } } //not used
        public override Color ComboBoxButtonBorderFocusedColor { get { return SystemColors.Highlight; } } //not used
        #endregion
    }
}