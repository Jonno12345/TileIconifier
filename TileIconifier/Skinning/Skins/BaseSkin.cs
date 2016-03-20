using System.Drawing;

namespace TileIconifier.Skinning.Skins
{
    public class BaseSkin
    {
        public virtual Color BackColor => SystemColors.Control;
        public virtual Color ForeColor => SystemColors.ControlText;
        public virtual Color DisabledForeColor => SystemColors.GrayText;
        public virtual Color DisabledBackColor => Color.LightGray;

        public virtual Color HighlightColor => SystemColors.Highlight;

        public virtual Color SortableListViewBackColor => Color.White;

        public virtual Color ErrorColor => Color.Red;

        public virtual Font Font => new Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
    }
}
