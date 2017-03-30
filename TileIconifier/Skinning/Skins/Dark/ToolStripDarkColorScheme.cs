using System.Drawing;

namespace TileIconifier.Skinning.Skins.Dark
{
    class ToolStripDarkColorScheme : ToolStripSystemColorScheme
    {
        public override Color MenuBarBackColor { get { return Color.FromArgb(70, 70, 70); } }
        public override Color PopupBackColor { get { return Color.FromArgb(70, 70, 70); } }
        public override Color MenuBarBorderColor { get { return Color.FromArgb(90, 90, 90); } }
        public override Color PopupBorderColor { get { return Color.FromArgb(90, 90, 90); } }
        public override Color HighlightBackColor { get { return Color.FromArgb(100, 100, 100); } }
        public override Color HighlightForeColor { get { return Color.FromArgb(255, 255, 255); } }
        public override Color MenuBarForeColor { get { return Color.FromArgb(230, 230, 230); } }
        public override Color PopupForeColor { get { return Color.FromArgb(240, 240, 240); } }
        public override Color DisabledForeColor { get { return Color.FromArgb(130, 130, 130); } }
    }
}
