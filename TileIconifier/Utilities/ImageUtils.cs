using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using TileIconifier.Forms.Shared;

namespace TileIconifier.Utilities
{
    internal class ImageUtils
    {
        public static Bitmap LoadIconifiedBitmap(string path)
        {
            try
            {
                Bitmap logo;
                using (var bitmapFile = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    logo = new Bitmap(bitmapFile);
                }
                return logo;
            }
            catch
            {
                // ignored
            }
            return null;
        }

        public static byte[] LoadBitmapToByteArray(string path)
        {
            try
            {
                return File.ReadAllBytes(path);
            }
            catch
            {
                // ignored
            }
            return null;
        }

        public static byte[] GetImage(IWin32Window owner, string defaultPathForIconExtraction = "")
        {
            var iconSelector = new FrmIconSelector(defaultPathForIconExtraction);
            iconSelector.ShowDialog(owner);
            if (iconSelector.ReturnedBitmapBytes == null)
                throw new UserCancellationException();
            return iconSelector.ReturnedBitmapBytes;
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn?.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public static Image ByteArrayToImage(byte[] bytesIn)
        {
            if (bytesIn == null || bytesIn.Length == 0) return null;
            using (var ms = new MemoryStream(bytesIn))
            {
                return Image.FromStream(ms);
            }
        }

        public static bool BitmapsAreEqual(Bitmap image1, Bitmap image2)
        {
            try
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
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}