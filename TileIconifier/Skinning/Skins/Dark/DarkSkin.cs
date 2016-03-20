using System.Drawing;

namespace TileIconifier.Skinning.Skins.Dark
{
    public class DarkSkin : BaseSkin
    {
        public override Color BackColor => Color.Black;
        public override Color ForeColor => Color.White;
        public override Color DisabledForeColor => Color.LightGray;
        public override Color DisabledBackColor => Color.DarkGray;

        public override Color SortableListViewBackColor => Color.Black;
    }
}
