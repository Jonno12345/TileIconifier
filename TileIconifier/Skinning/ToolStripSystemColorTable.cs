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
        private ToolStripSystemColorScheme userScheme;
        private ToolStripSystemColorScheme hcScheme;

        #region "Contructors"
        internal ToolStripSystemColorTable()
        {

        }

        internal ToolStripSystemColorTable(ToolStripSystemColorScheme pSysColorScheme)
        {
            userScheme = pSysColorScheme;
        }
        #endregion

        private ToolStripSystemColorScheme Scheme
        {
            get
            {
                if (userScheme == null || SystemInformation.HighContrast)
                {
                    if (hcScheme == null)
                        hcScheme = new ToolStripSystemColorScheme();
                    return hcScheme;
                }                    
                else
                    return userScheme;
            }
        }        

        internal Color MenuBarBackColor { get { return Scheme.MenuBarBackColor; } }
        internal Color PopupBackColor { get { return Scheme.PopupBackColor; } }
        internal Color MenuBarBorderColor { get { return Scheme.MenuBarBorderColor; } }
        internal Color PopupBorderColor { get { return Scheme.PopupBorderColor; } }
        internal Color HighlightBackColor { get { return Scheme.HighlightBackColor; } }
        internal Color HighlightForeColor { get { return Scheme.HighlightForeColor; } }
        internal Color MenuBarForeColor { get { return Scheme.MenuBarForeColor; } }
        internal Color PopupForeColor { get { return Scheme.PopupForeColor; } }
        internal Color DisabledForeColor { get { return Scheme.DisabledForeColor; } }
    }
}
