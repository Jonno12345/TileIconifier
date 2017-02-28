using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileIconifier.Skinning.Skins.Dark
{
    class ToolStripDarkColorScheme : ToolStripSystemColorScheme
    {
        public override Color MenuBarBackColor { get { return Color.FromArgb(70, 70, 70); } }
        public override Color PopupBackColor { get { return Color.FromArgb(95, 95, 95); } }
        public override Color PopupBorderColor { get { return Color.FromArgb(100, 100, 100); } }
        public override Color HighlightBackColor { get { return Color.DarkBlue; } }
        public override Color HighlightForeColor { get { return Color.White; } }
        public override Color MenuBarForeColor { get { return Color.FromArgb(230, 230, 230); } }
        public override Color PopupForeColor { get { return Color.White; } }
        public override Color DisabledForeColor { get { return Color.Gray; } }
    }
}
