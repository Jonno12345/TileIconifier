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

namespace TileIconifier.Skinning.Skins
{
    public class BaseSkin
    {
        #region "Basic properties"
        public virtual Color BackColor { get { return SystemColors.Control; } }
        public virtual Color ForeColor { get { return SystemColors.ControlText; } }
        public virtual Color DisabledForeColor { get { return SystemColors.GrayText; } }
        public virtual Color HighlightBackColor { get { return SystemColors.Highlight; } }
        public virtual Color ErrorForeColor { get { return Color.Red; } }

        //These objects are potentially more expensive to create, se we cache them.
        public virtual Font Font { get; } = SystemFonts.DialogFont;
        public virtual ToolStripSystemRendererEx ToolStripRenderer { get; } = new ToolStripSystemRendererEx();
        #endregion

        //"not used" means that the given color is not actually used by the control, because
        //it is not applicable based on the control's FlatStyle, BorderStyle or HeaderUseVisualStyleColors property.

        #region "Button"
        public virtual FlatStyle ButtonFlatStyle { get { return FlatStyle.Standard; } }
        public virtual Color ButtonForeColor { get { return SystemColors.ControlText; } }
        public virtual Color ButtonBackColor { get { return SystemColors.Control; } }
        public virtual Color ButtonDisabledForeColor { get { return SystemColors.GrayText; } }
        public virtual Color ButtonFlatBorderColor { get { return Color.Empty; } } //not used
        #endregion

        #region "TextBox"
        public virtual BorderStyle TextBoxBorderStyle { get { return BorderStyle.Fixed3D; } }
        public virtual Color TextBoxBackColor { get { return SystemColors.Window; } }
        public virtual Color TextBoxForeColor { get { return SystemColors.WindowText; } }
        public virtual Color TextBoxReadOnlyBackColor { get { return SystemColors.Control; } }
        public virtual Color TextBoxBorderColor { get { return Color.Empty; } } //not used
        public virtual Color TextBoxBorderFocusedColor { get { return Color.Empty; } } //not used
        public virtual Color TextBoxBorderDisabledColor { get { return Color.Empty; } } //not used
        #endregion

        #region "ListView"        
        public virtual FlatStyle ListViewFlatStyle { get { return FlatStyle.Standard; } }
        public virtual Color ListViewBackColor { get { return SystemColors.Window; } }
        public virtual Color ListViewForeColor { get { return SystemColors.WindowText; } }
        public virtual Color ListViewHeaderBackColor { get { return SystemColors.Control; } } //not used
        public virtual Color ListViewHeaderForeColor { get { return SystemColors.ControlText; } } //not used
        public virtual Color ListViewBorderColor { get { return Color.Empty; } } //not used
        public virtual Color ListViewBorderFocusedColor { get { return Color.Empty; } } //not used
        public virtual Color ListViewBorderDisabledColor { get { return Color.Empty; } } //not used
        #endregion

        #region "ComboBox"
        public virtual FlatStyle ComboBoxFlatStyle { get { return FlatStyle.Standard; } }
        public virtual Color ComboBoxBackColor { get { return SystemColors.Window; } }
        public virtual Color ComboBoxForeColor { get { return SystemColors.WindowText; } }
        public virtual Color ComboBoxButtonBackColor { get { return SystemColors.Control; } } //not used
        public virtual Color ComboboxButtonForeColor { get { return SystemColors.ControlText; } } //not used
        public virtual Color ComboBoxDisabledForeColor { get { return SystemColors.GrayText; } } //not used
        public virtual Color ComboBoxButtonBorderColor { get { return SystemColors.ControlDark; } } //not used
        public virtual Color ComboBoxButtonBorderFocusedColor { get { return SystemColors.Highlight; } } //not used
        #endregion
    }
}