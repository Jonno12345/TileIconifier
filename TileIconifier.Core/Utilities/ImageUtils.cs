#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2016 Johnathon M
// 
//         Permission is hereby granted, free of charge, to any person obtaining a copy
//         of this software and associated documentation files (the "Software"), to deal
//         in the Software without restriction, including without limitation the rights
//         to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//         copies of the Software, and to permit persons to whom the Software is
//         furnished to do so, subject to the following conditions:
// 
//         The above copyright notice and this permission notice shall be included in
//         all copies or substantial portions of the Software.
// 
//         THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//         IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//         FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//         AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//         LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//         OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//         THE SOFTWARE.
// 
// */

#endregion

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace TileIconifier.Core.Utilities
{
    public class ImageUtils
    {
        public static Bitmap LoadFileToBitmap(string path)
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

        public static byte[] LoadFileToByteArray(string path)
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

        public static byte[] ImageToByteArray(Image imageIn)
        {
            //seems to error less?
            var converter = new ImageConverter();
            using (var tmp = new Bitmap(imageIn))
            {
                return (byte[]) converter.ConvertTo(tmp.Clone(), typeof (byte[]));
            }

            //using (var ms = new MemoryStream())
            //{
            //    imageIn?.Save(ms, ImageFormat.Png);
            //    return ms.ToArray();
            //}
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

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static Size GetScaledWidthAndHeight(int curWidth, int curHeight, int maxWidth, int maxHeight)
        {
            var ratioX = (double) maxWidth/curWidth;
            var ratioY = (double) maxHeight/curHeight;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int) (curWidth*ratio);
            var newHeight = (int) (curHeight*ratio);

            return new Size(newWidth, newHeight);
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var newSize = GetScaledWidthAndHeight(image.Width, image.Height, maxWidth, maxHeight);

            //resize the image to the specified height and width

            var newImage = new Bitmap(newSize.Width, newSize.Height);

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                //draw the image into the target bitmap
                graphics.DrawImage(image, 0, 0, newImage.Width, newImage.Height);
            }

            return newImage;
        }

        /// <summary>
        /// Converts a PNG image to a icon (ico)
        /// </summary>
        /// <param name="input">The input stream</param>
        /// <param name="output">The output stream</param>
        /// <param name="size">The size (16x16 px by default)</param>
        /// <param name="preserveAspectRatio">Preserve the aspect ratio</param>
        /// <returns>Wether or not the icon was succesfully generated</returns>
        public static void ConvertToIcon(Image input, Stream output, int size = 16, bool preserveAspectRatio = true)
        {
            var inputBitmap = (Bitmap)input;

            int width = size, height = preserveAspectRatio ? inputBitmap.Height / inputBitmap.Width * size : size;

            var newBitmap = new Bitmap(inputBitmap, new Size(width, height));

            // save the resized png into a memory stream for future use
            using (var memoryStream = new MemoryStream())
            {
                newBitmap.Save(memoryStream, ImageFormat.Png);

                var iconWriter = new BinaryWriter(output);

                // 0-1 reserved, 0
                iconWriter.Write((byte)0);
                iconWriter.Write((byte)0);

                // 2-3 image type, 1 = icon, 2 = cursor
                iconWriter.Write((short)1);

                // 4-5 number of images
                iconWriter.Write((short)1);

                // image entry 1
                // 0 image width
                iconWriter.Write((byte)width);
                // 1 image height
                iconWriter.Write((byte)height);

                // 2 number of colors
                iconWriter.Write((byte)0);

                // 3 reserved
                iconWriter.Write((byte)0);

                // 4-5 color planes
                iconWriter.Write((short)0);

                // 6-7 bits per pixel
                iconWriter.Write((short)32);

                // 8-11 size of image data
                iconWriter.Write((int)memoryStream.Length);

                // 12-15 offset of image data
                iconWriter.Write(6 + 16);

                // write image data
                // png data must contain the whole png data file
                iconWriter.Write(memoryStream.ToArray());

                iconWriter.Flush();
            }
        }
    }
}