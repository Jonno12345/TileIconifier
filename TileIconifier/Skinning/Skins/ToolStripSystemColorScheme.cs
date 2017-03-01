using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileIconifier.Skinning.Skins
{
    public class ToolStripSystemColorScheme
    {
        #region "Default colors"
        public static Color DefaultMenuBarBackColor { get { return SystemColors.MenuBar; } }
        public static Color DefaultPopupBackColor { get { return SystemColors.Menu; } }
        public static Color DefaultMenuBarBorderColor { get { return SystemColors.ControlDark; } }
        public static Color DefaultPopupBorderColor { get { return SystemColors.ControlDark; } }
        public static Color DefaultHighlightBackColor { get { return SystemColors.Highlight; } }
        public static Color DefaultHighlightForeColor { get { return SystemColors.HighlightText; } }
        public static Color DefaultMenuBarForeColor { get { return SystemColors.MenuText; } }
        public static Color DefaultPopupForeColor { get { return SystemColors.MenuText; } }
        public static Color DefaultDisabledForeColor { get { return SystemColors.GrayText; } }
        #endregion

        public virtual Color MenuBarBackColor { get { return DefaultMenuBarBackColor; } }
        public virtual Color PopupBackColor { get { return DefaultPopupBackColor; } }
        public virtual Color MenuBarBorderColor { get { return DefaultMenuBarBorderColor; } }
        public virtual Color PopupBorderColor { get { return DefaultPopupBorderColor; } }
        public virtual Color HighlightBackColor { get { return DefaultHighlightBackColor; } }
        public virtual Color HighlightForeColor { get { return DefaultHighlightForeColor; } }
        public virtual Color MenuBarForeColor { get { return DefaultMenuBarForeColor; } }
        public virtual Color PopupForeColor { get { return DefaultPopupForeColor; } }
        public virtual Color DisabledForeColor { get { return DefaultDisabledForeColor; } }
    }
}
