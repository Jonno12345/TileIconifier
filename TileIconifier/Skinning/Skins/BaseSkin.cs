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
    /// Skin for a <see cref="Forms.SkinnableForm"/> that uses the default appearance for controls.
    /// </summary>
    public class BaseSkin
    {
        #region "Basic properties"
        public virtual Color BackColor => SystemColors.Control;
        public virtual Color ForeColor => SystemColors.ControlText;
        public virtual Color DisabledForeColor => SystemColors.GrayText;
        public virtual Color HighlightBackColor => SystemColors.Highlight;
        public virtual Color ErrorForeColor => Color.Red;
        public virtual bool EnforceOnMessageBox => false;

        //These objects are potentially more expensive to create, se we cache them.
        public virtual Font Font { get; } = SystemFonts.DialogFont;       
        #endregion

        //"not used" means that the given color is not actually used by the control, because
        //it is not applicable based on the control's FlatStyle or BorderStyle property.

        #region "Button"
        public virtual FlatStyle ButtonFlatStyle => FlatStyle.Standard;
        public virtual Color ButtonForeColor => SystemColors.ControlText;
        public virtual Color ButtonBackColor => SystemColors.Control;
        public virtual Color ButtonDisabledForeColor => SystemColors.GrayText;
        public virtual Color ButtonFlatBorderColor => Color.Empty; //not used
        #endregion

        #region "TextBox"
        public virtual BorderStyle TextBoxBorderStyle => BorderStyle.Fixed3D;
        public virtual Color TextBoxBackColor => SystemColors.Window;
        public virtual Color TextBoxForeColor => SystemColors.WindowText;
        public virtual Color TextBoxReadOnlyBackColor => SystemColors.Control;
        public virtual Color TextBoxBorderColor => SystemColors.WindowFrame; //not used
        public virtual Color TextBoxBorderFocusedColor => Color.Empty; //not used
        public virtual Color TextBoxBorderDisabledColor => Color.Empty; //not used
        #endregion

        #region "ListView" 
        public virtual bool ListViewUseExplorerStyle => true;
        public virtual BorderStyle ListViewBorderStyle => BorderStyle.Fixed3D;
        public virtual ListViewHeaderAppearance ListViewHeaderStyle => ListViewHeaderAppearance.Standard;
        public virtual Color ListViewBackColor => SystemColors.Window;
        public virtual Color ListViewForeColor => SystemColors.WindowText;
        public virtual Color ListViewHeaderBackColor => SystemColors.Control; //not used
        public virtual Color ListViewHeaderForeColor => SystemColors.ControlText; //not used
        public virtual Color ListViewBorderColor => SystemColors.WindowFrame; //not used
        public virtual Color ListViewBorderFocusedColor => Color.Empty; //not used
        public virtual Color ListViewBorderDisabledColor => Color.Empty; //not used
        #endregion

        #region "ComboBox"
        public virtual FlatStyle ComboBoxFlatStyle => FlatStyle.Standard;
        public virtual Color ComboBoxBackColor => SystemColors.Window;
        public virtual Color ComboBoxForeColor => SystemColors.WindowText;
        public virtual Color ComboBoxButtonBackColor => SystemColors.Control; //not used
        public virtual Color ComboboxButtonForeColor => SystemColors.ControlText; //not used
        public virtual Color ComboBoxDisabledForeColor => SystemColors.GrayText; //not used
        public virtual Color ComboBoxButtonBorderColor => SystemColors.ControlDark; //not used
        public virtual Color ComboBoxButtonBorderFocusedColor => SystemColors.Highlight; //not used
        #endregion

        #region "TabControl"
        public virtual FlatStyle TabControlFlatStyle => FlatStyle.Standard;
        public virtual Color TabControlSelectedTabBackColor => SystemColors.Window; //not used
        public virtual Color TabControlSelectedTabForeColor => SystemColors.WindowText; //not used
        public virtual Color TabControlTabBorderColor => SystemColors.WindowFrame; //not used
        #endregion

        #region "TrackBar"
        public virtual FlatStyle TrackBarFlatStyle => FlatStyle.Standard;
        public virtual Color TrackBarThumbBackColor => SystemColors.Control; //not used
        public virtual Color TrackBarThumbBorderColor => SystemColors.WindowFrame; //not used
        public virtual Color TrackBarThumbDisabledBackColor => SystemColors.ControlLight; //not used
        public virtual Color TrackBarThumbDisabledBorderColor => SystemColors.ControlDark; //not used
        public virtual Color TrackBarTrackColor => SystemColors.ControlDarkDark; //not used        
        #endregion

        #region "ToolStrip"
        public virtual Color ToolStripMenuBarBackColor => ToolStripSystemColorTable.DefaultMenuBarBackColor;
        public virtual Color ToolStripPopupBackColor => ToolStripSystemColorTable.DefaultPopupBackColor;
        public virtual Color ToolStripMenuBarBorderColor => ToolStripSystemColorTable.DefaultMenuBarBorderColor;
        public virtual Color ToolStripPopupBorderColor => ToolStripSystemColorTable.DefaultPopupBorderColor;
        public virtual Color ToolStripHighlightBackColor => ToolStripSystemColorTable.DefaultHighlightBackColor;
        public virtual Color ToolStripHighlightForeColor => ToolStripSystemColorTable.DefaultHighlightForeColor;
        public virtual Color ToolStripMenuBarForeColor => ToolStripSystemColorTable.DefaultMenuBarForeColor;
        public virtual Color ToolStripPopupForeColor => ToolStripSystemColorTable.DefaultPopupForeColor;
        public virtual Color ToolStripDisabledForeColor => ToolStripSystemColorTable.DefaultDisabledForeColor;
        #endregion
    }
}