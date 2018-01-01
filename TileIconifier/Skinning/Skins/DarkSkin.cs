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
using TileIconifier.Controls;

namespace TileIconifier.Skinning.Skins
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
        private readonly Color CommonFocusedBorderColor = SystemColors.Highlight;
        private readonly Color CommonDisabledBorderColor = Color.FromArgb(50, 50, 50);

        private readonly Color LowBackColor = Color.FromArgb(20, 20, 20);
        private readonly Color LowBorderColor = Color.FromArgb(70, 70, 70);
        private readonly Color MediumBackColor = Color.FromArgb(50, 50, 50);
        private readonly Color MediumBorderColor = Color.FromArgb(70, 70, 70);
        private readonly Color HighBackColor = Color.FromArgb(70, 70, 70);
        private readonly Color HighBorderColor = Color.FromArgb(90, 90, 90);
        #endregion

        #region "Basic properties"
        public override Color BackColor => MediumBackColor;
        public override Color ForeColor => CommonForeColor;
        public override Color DisabledForeColor => CommonDisabledForeColor;
        public override Color HighlightBackColor => CommonFocusedBorderColor;
        public override Color ErrorForeColor => Color.Red;
        public override bool EnforceOnMessageBox => true;
            

        //These objects are potentially more expensive to create, so we cache them.
        public override Font Font { get; } = new Font("Segoe UI", 8);        
        #endregion

        #region "Button"
        public override FlatStyle ButtonFlatStyle => FlatStyle.Flat;
        public override Color ButtonForeColor => CommonForeColor;
        public override Color ButtonBackColor => HighBackColor;
        public override Color ButtonDisabledForeColor => CommonDisabledForeColor;
        public override Color ButtonFlatBorderColor => HighBorderColor;
        #endregion

        #region "TextBox"
        public override BorderStyle TextBoxBorderStyle => BorderStyle.FixedSingle;
        public override Color TextBoxBackColor => LowBackColor;
        public override Color TextBoxForeColor => CommonForeColor;
        public override Color TextBoxReadOnlyBackColor => MediumBackColor;
        public override Color TextBoxBorderColor => LowBorderColor;
        public override Color TextBoxBorderFocusedColor => CommonFocusedBorderColor;
        public override Color TextBoxBorderDisabledColor => CommonDisabledBorderColor;
        #endregion

        #region "ListView"        
        public override bool ListViewUseExplorerStyle => false;
        public override BorderStyle ListViewBorderStyle => BorderStyle.FixedSingle;
        public override ListViewHeaderAppearance ListViewHeaderStyle => ListViewHeaderAppearance.Flat;
        public override Color ListViewBackColor => LowBackColor;
        public override Color ListViewForeColor => CommonForeColor;
        public override Color ListViewHeaderBackColor => MediumBackColor;
        public override Color ListViewHeaderForeColor => CommonForeColor;
        public override Color ListViewBorderColor => LowBorderColor;
        public override Color ListViewBorderFocusedColor => CommonFocusedBorderColor;
        public override Color ListViewBorderDisabledColor => CommonDisabledBorderColor;
        #endregion

        #region "ComboBox"
        public override FlatStyle ComboBoxFlatStyle => FlatStyle.Flat;
        public override Color ComboBoxBackColor => LowBackColor;
        public override Color ComboBoxForeColor => CommonForeColor;
        public override Color ComboBoxButtonBackColor => MediumBackColor;
        public override Color ComboboxButtonForeColor => CommonForeColor;
        public override Color ComboBoxDisabledForeColor => CommonDisabledForeColor;
        public override Color ComboBoxButtonBorderColor => MediumBorderColor;
        public override Color ComboBoxButtonBorderFocusedColor => CommonFocusedBorderColor;
        #endregion

        #region "TabControl"
        public override FlatStyle TabControlFlatStyle => FlatStyle.Flat;
        public override Color TabControlSelectedTabBackColor => LowBackColor;
        public override Color TabControlSelectedTabForeColor => CommonForeColor;
        public override Color TabControlTabBorderColor => LowBorderColor;
        #endregion

        #region "TrackBar"
        public override FlatStyle TrackBarFlatStyle => FlatStyle.Flat;
        public override Color TrackBarThumbBackColor => CommonFocusedBorderColor;
        public override Color TrackBarThumbBorderColor => CommonFocusedBorderColor;
        public override Color TrackBarThumbDisabledBackColor => HighBackColor;
        public override Color TrackBarThumbDisabledBorderColor => CommonDisabledBorderColor;
        public override Color TrackBarTrackColor => MediumBorderColor;
        #endregion

        #region "ToolStrip"
        public override Color ToolStripMenuBarBackColor => HighBackColor;
        public override Color ToolStripPopupBackColor => HighBackColor;
        public override Color ToolStripMenuBarBorderColor => HighBorderColor;
        public override Color ToolStripPopupBorderColor => HighBorderColor;
        public override Color ToolStripHighlightBackColor => Color.FromArgb(100, 100, 100);
        public override Color ToolStripHighlightForeColor => Color.FromArgb(255, 255, 255);
        public override Color ToolStripMenuBarForeColor => CommonForeColor;
        public override Color ToolStripPopupForeColor => CommonForeColor;
        public override Color ToolStripDisabledForeColor => CommonDisabledForeColor;
        #endregion
    }
}