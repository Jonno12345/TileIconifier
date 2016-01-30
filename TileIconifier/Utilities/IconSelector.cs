using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TsudaKageyu;

namespace TileIconifier.Utilities
{
    public partial class IconSelector : Form
    {
        public int SelectionIndex = -1;

        private class IconListViewItem : ListViewItem
        {
            public Bitmap Bitmap { get; set; }
        }

        public IconSelector(Icon[] icons)
        {
            InitializeComponent();
            BuildListView(icons);
        }

        private void BuildListView(Icon[] icons)
        {
            lvwIcons.BeginUpdate();

            foreach (var i in icons)
            {
                var item = new IconListViewItem();
                var size = i.Size;
                var bits = IconUtil.GetBitCount(i);
                //item.ToolTipText = String.Format("{0}x{1}, {2} bits", size.Width, size.Height, bits);
                item.Bitmap = IconUtil.ToBitmap(i);
                i.Dispose();

                lvwIcons.Items.Add(item);
            }

            lvwIcons.EndUpdate();
        }

        private void lvwIcons_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            var item = e.Item as IconListViewItem;

            // Draw item

            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.Clip = new Region(e.Bounds);

            if (e.Item.Selected)
                e.Graphics.FillRectangle(SystemBrushes.MenuHighlight, e.Bounds);
            else
                e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);

            int w = Math.Min(128, item.Bitmap.Width);
            int h = Math.Min(128, item.Bitmap.Height);

            int x = e.Bounds.X + (e.Bounds.Width - w) / 2;
            int y = e.Bounds.Y + (e.Bounds.Height - h) / 2;
            var dstRect = new Rectangle(x, y, w, h);
            var srcRect = new Rectangle(Point.Empty, item.Bitmap.Size);


            e.Graphics.DrawImage(item.Bitmap, dstRect, srcRect, GraphicsUnit.Pixel);

            var textRect = new Rectangle(
                e.Bounds.Left, e.Bounds.Bottom - Font.Height - 4,
                e.Bounds.Width, Font.Height + 2);
            TextRenderer.DrawText(e.Graphics, item.ToolTipText, Font, textRect, ForeColor);

            e.Graphics.Clip = new Region();
            e.Graphics.DrawRectangle(SystemPens.ControlLight, e.Bounds);
        }

        private void lvwIcons_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectionIndex = lvwIcons.SelectedItems[0].Index;
            Close();
        }

        private void IconSelector_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SelectionIndex == -1)
                SelectionIndex = -2;
        }
    }
}
