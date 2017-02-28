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

        #region "Button, CheckBoxes and RadioButtons"
        public override FlatStyle ButtonFlatStyle
        {
            get
            {
                return FlatStyle.Flat;
            }
        }

        public override Color ButtonForeColor
        {
            get
            {
                return Color.White;
            }
        }

        public override Color ButtonBackColor
        {
            get
            {
                return Color.FromArgb(70, 70, 70);
            }
        }

        public override Color ButtonForeColorDisabled
        {
            get
            {
                return Color.Gray;
            }
        }

        public override Color ButtonFlatBorderColor
        {
            get
            {
                return Color.FromArgb(90, 90, 90);
            }
        }
        #endregion "Button"

        #region "TextBox"
        public override BorderStyle TextBoxBorderStyle
        {
            get
            {
                return BorderStyle.FixedSingle;
            }
        }

        public override Color TextBoxBackColor
        {
            get
            {
                return Color.FromArgb(30, 30, 30);
            }
        }

        public override Color TextBoxForeColor
        {
            get
            {
                return Color.White;
            }
        }

        public override Color TextBoxBackColorReadOnly
        {
            get
            {
                return Color.FromArgb(50, 50, 50);
            }
        }

        public override Color TextBoxBorderColor
        {
            get
            {
                return Color.Gray;
            }
        }
        #endregion
    }
}