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
        public virtual Color BackColor => SystemColors.Control;
        public virtual Color ForeColor => SystemColors.ControlText;
        public virtual Color DisabledForeColor => SystemColors.GrayText;
        public virtual Color DisabledBackColor => Color.LightGray;

        public virtual Color HighlightColor => SystemColors.Highlight;

        public virtual Color SortableListViewBackColor => Color.White;

        public virtual Color ErrorColor => Color.Red;

        public virtual Font Font => new Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);

        public virtual ToolStripSystemRendererEx ToolStripRenderer { get; } = new ToolStripSystemRendererEx();

        #region "Button"
        public virtual FlatStyle ButtonFlatStyle
        {
            get
            {
                return FlatStyle.Standard;
            }
        }

        public virtual Color ButtonForeColor
        {
            get
            {
                return SystemColors.ControlText;
            }
        }

        public virtual Color ButtonBackColor
        {
            get
            {
                return SystemColors.Control;
            }
        }

        public virtual Color ButtonForeColorDisabled
        {
            get
            {
                return SystemColors.GrayText;
            }
        }

        public virtual Color ButtonFlatBorderColor
        {
            get
            {
                return Color.Empty;
            }
        }
        #endregion

        #region "TextBox"
        public virtual BorderStyle TextBoxBorderStyle
        {
            get
            {
                return BorderStyle.Fixed3D;
            }
        }

        public virtual Color TextBoxBackColor
        {
            get
            {
                return SystemColors.Window;
            }
        }

        public virtual Color TextBoxForeColor
        {
            get
            {
                return SystemColors.WindowText;
            }
        }

        public virtual Color TextBoxBackColorReadOnly
        {
            get
            {
                return SystemColors.Control;
            }
        }

        public virtual Color TextBoxBorderColor
        {
            get
            {
                return Color.Empty;
            }
        }
        #endregion
    }
}