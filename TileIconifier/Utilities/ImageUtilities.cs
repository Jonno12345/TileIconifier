using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileIconifier.Utilities
{
    class ImageUtilities
    {
        public static Bitmap LoadIconifiedBitmap(string path)
        {
            try
            {
                FileStream bitmapFile = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                Bitmap Logo = new Bitmap(bitmapFile);
                bitmapFile.Close();
                return Logo;
            }
            catch
            {
                try
                {
                    File.Move(path, path + "_BAD.png");
                }
                catch { }
            }
            return null;
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
            for (int x = 0; x < image1.Width; ++x)
            {
                for (int y = 0; y < image1.Height; ++y)
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
