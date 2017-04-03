using System.Drawing;
using System.Windows.Forms;
using TileIconifier.Skinning.Skins;

namespace TileIconifier.Skinning
{
    /// <summary>
    /// Provides the colors internally used by a <see cref="ToolStripSystemRendererEx"/>.
    /// </summary>
    internal class ToolStripSystemColorTable
    {
        private BaseSkin skin;        

        #region "Constructor"
        internal ToolStripSystemColorTable(BaseSkin pSkin)
        {
            skin = pSkin;
        }
        #endregion          

        internal Color MenuBarBackColor { get { return skin.ToolStripMenuBarBackColor; } }
        internal Color PopupBackColor { get { return skin.ToolStripPopupBackColor; } }
        internal Color MenuBarBorderColor { get { return skin.ToolStripMenuBarBorderColor; } }
        internal Color PopupBorderColor { get { return skin.ToolStripPopupBorderColor; } }
        internal Color HighlightBackColor { get { return skin.ToolStripHighlightBackColor; } }
        internal Color HighlightForeColor { get { return skin.ToolStripHighlightForeColor; } }
        internal Color MenuBarForeColor { get { return skin.ToolStripMenuBarForeColor; } }
        internal Color PopupForeColor { get { return skin.ToolStripPopupForeColor; } }
        internal Color DisabledForeColor { get { return skin.ToolStripDisabledForeColor; } }

        #region "Default colors"
        internal static Color DefaultMenuBarBackColor => SystemColors.MenuBar;
        internal static Color DefaultPopupBackColor => SystemColors.Menu;
        internal static Color DefaultMenuBarBorderColor => SystemColors.ControlDark;
        internal static Color DefaultPopupBorderColor => SystemColors.ControlDark;
        internal static Color DefaultHighlightBackColor => SystemColors.Highlight;
        internal static Color DefaultHighlightForeColor => SystemColors.HighlightText;
        internal static Color DefaultMenuBarForeColor => SystemColors.MenuText;
        internal static Color DefaultPopupForeColor => SystemColors.MenuText;
        internal static Color DefaultDisabledForeColor => SystemColors.GrayText;
        #endregion
    }
}
