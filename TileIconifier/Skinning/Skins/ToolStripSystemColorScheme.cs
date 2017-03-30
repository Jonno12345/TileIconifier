using System.Drawing;

namespace TileIconifier.Skinning.Skins
{
    /// <summary>
    /// Provides a basic and overridable color scheme for a <see cref="ToolStripSystemRendererEx"/>.
    /// </summary>
    public class ToolStripSystemColorScheme
    {
        #region "Default colors"
        public static Color DefaultMenuBarBackColor => SystemColors.MenuBar;
        public static Color DefaultPopupBackColor => SystemColors.Menu;
        public static Color DefaultMenuBarBorderColor => SystemColors.ControlDark;
        public static Color DefaultPopupBorderColor => SystemColors.ControlDark;
        public static Color DefaultHighlightBackColor => SystemColors.Highlight;
        public static Color DefaultHighlightForeColor => SystemColors.HighlightText;
        public static Color DefaultMenuBarForeColor => SystemColors.MenuText;
        public static Color DefaultPopupForeColor => SystemColors.MenuText;
        public static Color DefaultDisabledForeColor => SystemColors.GrayText;

        #endregion

        public virtual Color MenuBarBackColor => DefaultMenuBarBackColor;
        public virtual Color PopupBackColor => DefaultPopupBackColor;
        public virtual Color MenuBarBorderColor => DefaultMenuBarBorderColor;
        public virtual Color PopupBorderColor => DefaultPopupBorderColor;
        public virtual Color HighlightBackColor => DefaultHighlightBackColor;
        public virtual Color HighlightForeColor => DefaultHighlightForeColor;
        public virtual Color MenuBarForeColor => DefaultMenuBarForeColor;
        public virtual Color PopupForeColor => DefaultPopupForeColor;
        public virtual Color DisabledForeColor => DefaultDisabledForeColor;
    }
}
