using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TileIconifier.Forms;

namespace TileIconifier.Utilities
{
    internal class ImageUtils
    {
        public static Bitmap LoadIconifiedBitmap(string path)
        {
            try
            {
                var bitmapFile = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var logo = new Bitmap(bitmapFile);
                bitmapFile.Close();
                return logo;
            }
            catch
            {
                // ignored
            }
            return null;
        }

        public static Bitmap GetImage(IWin32Window owner, string defaultPathForIconExtraction = "")
        {
            var iconSelector = new FrmIconSelector(defaultPathForIconExtraction);
            iconSelector.ShowDialog(owner);
            if (iconSelector.ReturnedBitmap == null)
                throw new UserCancellationException();
            return iconSelector.ReturnedBitmap;
        }

        public static bool BitmapsAreEqual(Bitmap image1, Bitmap image2)
        {
            if (ReferenceEquals(image1, image2))
                return true;

            if (image1 == null || image2 == null)
                return false;

            if (!image1.Size.Equals(image2.Size))
            {
                return false;
            }
            for (var x = 0; x < image1.Width; ++x)
            {
                for (var y = 0; y < image1.Height; ++y)
                {
                    if (image1.GetPixel(x, y) != image2.GetPixel(x, y))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}