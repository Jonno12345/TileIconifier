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
        private readonly Color _commonForeColor = Color.FromArgb(240, 240, 240);
        private readonly Color _commonDisabledForeColor = Color.FromArgb(130, 130, 130);        
        private readonly Color _commonFocusedBorderColor = SystemColors.Highlight;
        private readonly Color _commonDisabledBorderColor = Color.FromArgb(50, 50, 50);

        private readonly Color _lowBackColor = Color.FromArgb(20, 20, 20);
        private readonly Color _lowBorderColor = Color.FromArgb(70, 70, 70);
        private readonly Color _mediumBackColor = Color.FromArgb(50, 50, 50);
        private readonly Color _mediumBorderColor = Color.FromArgb(70, 70, 70);
        private readonly Color _highBackColor = Color.FromArgb(70, 70, 70);
        private readonly Color _highBorderColor = Color.FromArgb(90, 90, 90);
        #endregion

        #region "Basic properties"
        public override Color BackColor => _mediumBackColor;
        public override Color ForeColor => _commonForeColor;
        public override Color DisabledForeColor => _commonDisabledForeColor;
        public override Color HighlightBackColor => _commonFocusedBorderColor;
        public override Color ErrorForeColor => Color.Red;
        public override bool EnforceOnMessageBox => true;
            

        //These objects are potentially more expensive to create, so we cache them.
        public override Font Font { get; } = new Font("Segoe UI", 8);        
        #endregion

        #region "Button"
        public override FlatStyle ButtonFlatStyle => FlatStyle.Flat;
        public override Color ButtonForeColor => _commonForeColor;
        public override Color ButtonBackColor => _highBackColor;
        public override Color ButtonDisabledForeColor => _commonDisabledForeColor;
        public override Color ButtonFlatBorderColor => _highBorderColor;
        #endregion

        #region "TextBox"
        public override BorderStyle TextBoxBorderStyle => BorderStyle.FixedSingle;
        public override Color TextBoxBackColor => _lowBackColor;
        public override Color TextBoxForeColor => _commonForeColor;
        public override Color TextBoxReadOnlyBackColor => _mediumBackColor;
        public override Color TextBoxBorderColor => _lowBorderColor;
        public override Color TextBoxBorderFocusedColor => _commonFocusedBorderColor;
        public override Color TextBoxBorderDisabledColor => _commonDisabledBorderColor;
        #endregion

        #region "ListView"        
        public override bool ListViewUseExplorerStyle => false;
        public override BorderStyle ListViewBorderStyle => BorderStyle.FixedSingle;
        public override ListViewHeaderAppearance ListViewHeaderStyle => ListViewHeaderAppearance.Flat;
        public override Color ListViewBackColor => _lowBackColor;
        public override Color ListViewForeColor => _commonForeColor;
        public override Color ListViewHeaderBackColor => _mediumBackColor;
        public override Color ListViewHeaderForeColor => _commonForeColor;
        public override Color ListViewBorderColor => _lowBorderColor;
        public override Color ListViewBorderFocusedColor => _commonFocusedBorderColor;
        public override Color ListViewBorderDisabledColor => _commonDisabledBorderColor;
        #endregion

        #region "ComboBox"
        public override FlatStyle ComboBoxFlatStyle => FlatStyle.Flat;
        public override Color ComboBoxBackColor => _lowBackColor;
        public override Color ComboBoxForeColor => _commonForeColor;
        public override Color ComboBoxButtonBackColor => _mediumBackColor;
        public override Color ComboboxButtonForeColor => _commonForeColor;
        public override Color ComboBoxDisabledForeColor => _commonDisabledForeColor;
        public override Color ComboBoxButtonBorderColor => _mediumBorderColor;
        public override Color ComboBoxButtonBorderFocusedColor => _commonFocusedBorderColor;
        #endregion

        #region "TabControl"
        public override FlatStyle TabControlFlatStyle => FlatStyle.Flat;
        public override Color TabControlSelectedTabBackColor => _lowBackColor;
        public override Color TabControlSelectedTabForeColor => _commonForeColor;
        public override Color TabControlTabBorderColor => _lowBorderColor;
        #endregion

        #region "TrackBar"
        public override FlatStyle TrackBarFlatStyle => FlatStyle.Flat;
        public override Color TrackBarThumbBackColor => _commonFocusedBorderColor;
        public override Color TrackBarThumbBorderColor => _commonFocusedBorderColor;
        public override Color TrackBarThumbDisabledBackColor => _highBackColor;
        public override Color TrackBarThumbDisabledBorderColor => _commonDisabledBorderColor;
        public override Color TrackBarTrackColor => _mediumBorderColor;
        #endregion

        #region "ToolStrip"
        public override Color ToolStripMenuBarBackColor => _highBackColor;
        public override Color ToolStripPopupBackColor => _highBackColor;
        public override Color ToolStripMenuBarBorderColor => _highBorderColor;
        public override Color ToolStripPopupBorderColor => _highBorderColor;
        public override Color ToolStripHighlightBackColor => Color.FromArgb(100, 100, 100);
        public override Color ToolStripHighlightForeColor => Color.FromArgb(255, 255, 255);
        public override Color ToolStripMenuBarForeColor => _commonForeColor;
        public override Color ToolStripPopupForeColor => _commonForeColor;
        public override Color ToolStripDisabledForeColor => _commonDisabledForeColor;
        #endregion
    }
}