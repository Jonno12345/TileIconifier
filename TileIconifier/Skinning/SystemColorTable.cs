using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TileIconifier.Skinning.Skins;

namespace TileIconifier.Skinning
{
    class SystemColorTable
    {
        private ToolStripSystemColorScheme userScheme;
        private ToolStripSystemColorScheme hcScheme;

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

        #region "Contructors"
        internal SystemColorTable()
        {
            
        }

        internal SystemColorTable(ToolStripSystemColorScheme pSysColorScheme)
        {
            userScheme = pSysColorScheme;
        }
        #endregion

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
