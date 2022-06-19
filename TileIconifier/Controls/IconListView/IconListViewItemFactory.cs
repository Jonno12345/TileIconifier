using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileIconifier.Core.IconExtractor;

namespace TileIconifier.Controls.IconListView
{
    internal class IconListViewItemFactory
    {
        public List<IconListViewItem> Create(IEnumerable<Icon> icons) 
        {
            //Build the list view items
            var items = new List<IconListViewItem>();
            foreach (Icon icon in icons)
            {
                var splitIcons = IconUtil.Split(icon);

                var largestIcon = splitIcons.OrderByDescending(k => k.Width)
                    .ThenByDescending(k => Math.Max(k.Height, k.Width))
                    .First();
                Bitmap bmp;
                try
                {
                    bmp = IconUtil.ToBitmap(largestIcon);
                }
                catch
                {
                    //icon failed to convert to bitmap
                    continue;
                }
                items.Add(new IconListViewItem(bmp));

                //Icon cleanup
                icon.Dispose();
                Array.ForEach(splitIcons, ic => ic.Dispose());
                //The listview creates its own copy of the bitmap
                bmp.Dispose();
            }
            return items;
        }
    }
}
