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
    /// <summary>
    /// Skin for a <see cref="Forms.SkinnableForm"/> with a flat appearance, a bright foreground and a dark background.
    /// </summary>
    public class DarkSkin : BaseSkin
    {
        #region "Common colors"
        //These colors are applied to various controls.
        //Simplifies skinning for similar UI elements.
        private readonly Color CommonForeColor = Color.FromArgb(240, 240, 240);
        private readonly Color CommonDisabledForeColor = Color.FromArgb(130, 130, 130);
        private readonly Color CommonBorderColor = Color.FromArgb(70, 70, 70);
        private readonly Color CommonFocusedBorderColor = SystemColors.Highlight;        

        private readonly Color LowBackColor = Color.FromArgb(20, 20, 20);
        private readonly Color MediumBackColor = Color.FromArgb(50, 50, 50);
        private readonly Color HighBackColor = Color.FromArgb(70, 70, 70);
        private readonly Color HighBorderColor = Color.FromArgb(90, 90, 90);
        #endregion

        #region "Basic properties"
        public override Color BackColor { get { return MediumBackColor; } }
        public override Color ForeColor { get { return CommonForeColor; } }
        public override Color DisabledForeColor { get { return CommonDisabledForeColor; } }
        public override Color HighlightBackColor { get { return CommonFocusedBorderColor; } }
        public override Color ErrorForeColor { get { return Color.Red; } }

        //These objects are potentially more expensive to create, so we cache them.
        public override Font Font { get; } = new Font("Segoe UI", 8);        
        #endregion

        #region "Button"
        public override FlatStyle ButtonFlatStyle { get { return FlatStyle.Flat; } }
        public override Color ButtonForeColor { get { return CommonForeColor; } }
        public override Color ButtonBackColor { get { return HighBackColor; } }
        public override Color ButtonDisabledForeColor { get { return CommonDisabledForeColor; } }
        public override Color ButtonFlatBorderColor { get { return HighBorderColor; } }
        #endregion

        #region "TextBox"
        public override BorderStyle TextBoxBorderStyle { get { return BorderStyle.FixedSingle; } }
        public override Color TextBoxBackColor { get { return LowBackColor; } }
        public override Color TextBoxForeColor { get { return CommonForeColor; } }
        public override Color TextBoxReadOnlyBackColor { get { return MediumBackColor; } }
        public override Color TextBoxBorderColor { get { return CommonBorderColor; } }
        public override Color TextBoxBorderFocusedColor { get { return CommonFocusedBorderColor; } }
        public override Color TextBoxBorderDisabledColor { get { return CommonDisabledForeColor; } } //
        #endregion

        #region "ListView"        
        public override FlatStyle ListViewFlatStyle { get { return FlatStyle.Flat; } }
        public override Color ListViewBackColor { get { return LowBackColor; } }
        public override Color ListViewForeColor { get { return CommonForeColor; } }
        public override Color ListViewHeaderBackColor { get { return MediumBackColor; } }
        public override Color ListViewHeaderForeColor { get { return CommonForeColor; } }
        public override Color ListViewBorderColor { get { return CommonBorderColor; } }
        public override Color ListViewBorderFocusedColor { get { return CommonFocusedBorderColor; } }
        public override Color ListViewBorderDisabledColor { get { return CommonDisabledForeColor; } } //
        #endregion

        #region "ComboBox"
        public override FlatStyle ComboBoxFlatStyle { get { return FlatStyle.Flat; } }
        public override Color ComboBoxBackColor { get { return LowBackColor; } }
        public override Color ComboBoxForeColor { get { return CommonForeColor; } }
        public override Color ComboBoxButtonBackColor { get { return MediumBackColor; } }
        public override Color ComboboxButtonForeColor { get { return CommonForeColor; } }
        public override Color ComboBoxDisabledForeColor { get { return CommonDisabledForeColor; } }
        public override Color ComboBoxButtonBorderColor { get { return CommonBorderColor; } }
        public override Color ComboBoxButtonBorderFocusedColor { get { return CommonFocusedBorderColor; } }
        #endregion

        #region "TabControl"
        public override FlatStyle TabControlFlatStyle { get { return FlatStyle.Flat; } }
        public override Color TabControlSelectedTabBackColor { get { return LowBackColor; } }
        public override Color TabControlSelectedTabForeColor { get { return CommonForeColor; } }
        public override Color TabControlTabBorderColor { get { return CommonBorderColor; } }
        #endregion

        #region "ToolStrip"
        public override Color ToolStripMenuBarBackColor { get { return Color.FromArgb(70, 70, 70); } }
        public override Color ToolStripPopupBackColor { get { return Color.FromArgb(70, 70, 70); } }
        public override Color ToolStripMenuBarBorderColor { get { return Color.FromArgb(90, 90, 90); } }
        public override Color ToolStripPopupBorderColor { get { return Color.FromArgb(90, 90, 90); } }
        public override Color ToolStripHighlightBackColor { get { return Color.FromArgb(100, 100, 100); } }
        public override Color ToolStripHighlightForeColor { get { return Color.FromArgb(255, 255, 255); } }
        public override Color ToolStripMenuBarForeColor { get { return Color.FromArgb(230, 230, 230); } }
        public override Color ToolStripPopupForeColor { get { return Color.FromArgb(240, 240, 240); } }
        public override Color ToolStripDisabledForeColor { get { return Color.FromArgb(130, 130, 130); } }
        #endregion
    }
}