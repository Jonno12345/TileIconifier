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
using System.Xml.Linq;
using TileIconifier.Core.Shortcut;
using TileIconifier.Core.Utilities;

namespace TileIconifier.Core.TileIconify
{
    public class TileIcon
    {
        private readonly ShortcutItem _shortcutItem;

        public TileIcon(ShortcutItem shortcutItem)
        {
            _shortcutItem = shortcutItem;
        }

        public void RunIconify()
        {
            BuildFilesAndFolders();
            SaveMetadata();
            SaveIcons();
            RebuildLnkInStartMenu();
        }

        public void DeIconify()
        {
            DeleteFilesAndFolders();
            RebuildLnkInStartMenu();
        }

        private void BuildFilesAndFolders()
        {
            var xNamespace = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");

            var xDoc = new XDocument(
                new XElement("Application",
                    new XAttribute(XNamespace.Xmlns + "xsi", xNamespace),
                    new XElement("VisualElements",
                        new XAttribute("ShowNameOnSquare150x150Logo",
                            _shortcutItem.Properties.CurrentState.ShowNameOnSquare150X150Logo ? "on" : "off"),
                        new XAttribute("Square150x150Logo", _shortcutItem.RelativeMediumIconPath),
                        new XAttribute("Square70x70Logo", _shortcutItem.RelativeSmallIconPath),
                        new XAttribute("ForegroundText", _shortcutItem.Properties.CurrentState.ForegroundText),
                        new XAttribute("BackgroundColor", _shortcutItem.Properties.CurrentState.BackgroundColor)
                        )));

            if (!Directory.Exists(_shortcutItem.VisualElementsPath))
                Directory.CreateDirectory(_shortcutItem.VisualElementsPath);

            xDoc.Save(_shortcutItem.VisualElementManifestPath);
        }

        private void DeleteFilesAndFolders()
        {
            if (Directory.Exists(_shortcutItem.VisualElementsPath))
                Directory.Delete(_shortcutItem.VisualElementsPath, true);

            if (File.Exists(
                $"{_shortcutItem.TargetFolderPath}\\{Path.GetFileNameWithoutExtension(_shortcutItem.TargetFilePath)}.VisualElementsManifest.xml"))
                File.Delete(
                    $"{_shortcutItem.TargetFolderPath}\\{Path.GetFileNameWithoutExtension(_shortcutItem.TargetFilePath)}.VisualElementsManifest.xml");
        }


        private void SaveMetadata()
        {
            _shortcutItem.Properties.SaveMediumIconMetadata(_shortcutItem.MediumImageResizeMetadataPath);
            _shortcutItem.Properties.SaveSmallIconMetadata(_shortcutItem.SmallImageResizeMetadataPath);
        }

        private void SaveIcons()
        {
            BuildIcon(_shortcutItem.FullMediumIconPath, ShortcutConstantsAndEnums.MediumShortcutSize.Width,
                ShortcutConstantsAndEnums.MediumShortcutSize.Height,
                _shortcutItem.Properties.CurrentState.MediumImage.Bytes,
                _shortcutItem.Properties.CurrentState.MediumImage.Width,
                _shortcutItem.Properties.CurrentState.MediumImage.Height,
                _shortcutItem.Properties.CurrentState.MediumImage.X, _shortcutItem.Properties.CurrentState.MediumImage.Y);

            BuildIcon(_shortcutItem.FullSmallIconPath, ShortcutConstantsAndEnums.SmallShortcutSize.Width,
                ShortcutConstantsAndEnums.SmallShortcutSize.Height,
                _shortcutItem.Properties.CurrentState.SmallImage.Bytes,
                _shortcutItem.Properties.CurrentState.SmallImage.Width,
                _shortcutItem.Properties.CurrentState.SmallImage.Height,
                _shortcutItem.Properties.CurrentState.SmallImage.X, _shortcutItem.Properties.CurrentState.SmallImage.Y);
        }

        private void BuildIcon(string filePath, int width, int height, byte[] imageBytes, int imageWidth,
            int imageHeight, int imageX, int imageY)
        {
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                var outputBitmap = new Bitmap(width, height);

                var tempImage = ImageUtils.ByteArrayToImage(imageBytes);
                tempImage = ImageUtils.ScaleImage(tempImage, imageWidth, imageHeight);

                using (var graphics = Graphics.FromImage(outputBitmap))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(tempImage,
                        new PointF(imageX, imageY));
                }

                outputBitmap.Save(fs, ImageFormat.Png);
                outputBitmap.Dispose();
                tempImage.Dispose();
            }
        }

        private void RebuildLnkInStartMenu()
        {
            File.SetLastWriteTime(_shortcutItem.ShortcutFileInfo.FullName, DateTime.Now);
        }
    }
}