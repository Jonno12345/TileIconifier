#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2021 Johnathon M
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
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TileIconifier.Core.Utilities;

namespace TileIconifier.Core.Shortcut
{
    [Serializable]
    [XmlRoot("ShortcutItemImage")]
    public class ShortcutItemImage
    {
        private Image _imageCache;
        private byte[] _imageCacheBytes;
        [XmlElement("OriginalBytes")] public byte[] Bytes;

        [XmlElement("OriginalPath")] public string Path;
        [XmlElement("Height")] public int Height;


        [XmlElement("Width")] public int Width;

        [XmlElement("X")] public int X;

        [XmlElement("Y")] public int Y;

        public ShortcutItemImage()
        {
        }

        public ShortcutItemImage(Size size)
        {
            Width = size.Width;
            Height = size.Height;
        }

        public void SetImage(byte[] imageBytes, Size shortcutSize, int? x = null, int? y = null)
        {
            var tempImage = ImageUtils.ByteArrayToImage(imageBytes);

            //very high resolution images cause GDI+ exceptions, and logos shouldn't need to be this high res anyway
            if (tempImage.Width > 300 || tempImage.Height > 300)
                tempImage = ImageUtils.ScaleImage(tempImage, 300, 300);
            var scaledSize = ImageUtils.GetScaledWidthAndHeight(tempImage.Width, tempImage.Height, shortcutSize.Width,
                shortcutSize.Height);
            Width = scaledSize.Width;
            Height = scaledSize.Height;
            Bytes = ImageUtils.ImageToByteArray(tempImage);
            tempImage.Dispose();
            if (x != null) X = (int) x;
            if (y != null) Y = (int) y;
        }

        public Image CachedImage()
        {
            if (_imageCacheBytes == Bytes) return _imageCache;

            if (_imageCacheBytes == Bytes &&
                _imageCacheBytes.SequenceEqual(Bytes))
                return _imageCache;

            _imageCache = ImageUtils.ByteArrayToImage(Bytes);
            _imageCacheBytes = Bytes?.ToArray();

            return _imageCache;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != typeof (ShortcutItemImage))
                return false;
            var other = (ShortcutItemImage) obj;

            return Bytes == other.Bytes &&
                   X == other.X &&
                   Y == other.Y &&
                   Width == other.Width &&
                   Height == other.Height;
        }

        public override int GetHashCode()
        {
            // Getting hash codes from volatile variables doesn't seem a good move... //TODO Find an immutable way?
            var hashCode = Bytes?.GetHashCode() ?? 0;
            hashCode = (hashCode*397) ^ X;
            hashCode = (hashCode*397) ^ Y;
            hashCode = (hashCode*397) ^ Width;
            hashCode = (hashCode*397) ^ Height;
            return hashCode;
        }

        public void Save(string filePath)
        {
            var x = new XmlSerializer(typeof (ShortcutItemImage));
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                x.Serialize(fs, this);
            }
        }

        public static ShortcutItemImage Load(string filePath)
        {
            var x = new XmlSerializer(typeof (ShortcutItemImage));
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                return (ShortcutItemImage) x.Deserialize(fs);
            }
        }

        protected bool Equals(ShortcutItemImage other)
        {
            return Equals(Bytes, other.Bytes) && X == other.X && Y == other.Y && Width == other.Width &&
                   Height == other.Height;
        }
    }
}