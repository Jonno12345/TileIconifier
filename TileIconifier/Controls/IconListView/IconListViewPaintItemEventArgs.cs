using System.Drawing;
using System.Windows.Forms;

namespace TileIconifier.Controls.IconListView
{
    class IconListViewPaintItemEventArgs : PaintEventArgs
    {
        public IconListViewPaintItemEventArgs(Graphics graphics, Rectangle clipRect, IconListViewItem item) : base(graphics, clipRect)
        {
            Item = item;
        }

        public IconListViewItem Item { get; }
    }
}
