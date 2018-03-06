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
            BackupOriginalVisualManifest();
            BuildFilesAndFolders();
            SaveMetadata();
            SaveIcons();
            _shortcutItem.Properties.CommitChanges();
            RebuildLnkInStartMenu();
        }

        public void DeIconify()
        {
            DeleteFilesAndFolders();
            RestoreOriginalVisualManifest();
            RebuildLnkInStartMenu();
        }

        /// <summary>
        /// Back up any original Visual Manifest XML in case of rollback
        /// </summary>
        private void BackupOriginalVisualManifest()
        {
            if(!File.Exists(_shortcutItem.VisualElementManifestPath) || 
                _shortcutItem.IsIconified ||
                _shortcutItem.IconifiedByTileIconifier)
            {
                return;
            }

            try
            {
                File.Copy(_shortcutItem.VisualElementManifestPath, _shortcutItem.VisualElementManifestPathOriginalBackup);
            }
            catch
            {

            }
        }


        /// <summary>
        /// Restore a backed up Visual Manifest XML file after a rollback
        /// </summary>
        private void RestoreOriginalVisualManifest()
        {
            if(!File.Exists(_shortcutItem.VisualElementManifestPathOriginalBackup))
            {
                return;
            }

            try
            {
                File.Move(_shortcutItem.VisualElementManifestPathOriginalBackup, _shortcutItem.VisualElementManifestPath);
            }
            catch
            {

            }
        }

        private void BuildFilesAndFolders()
        {
            var xNamespace = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
            
            var xDoc = new XDocument(
                new XElement("Application",
                    new XAttribute(XNamespace.Xmlns + "xsi", xNamespace),
                    new XAttribute("GeneratedByTileIconifier", true),
                    new XElement("VisualElements",
                        new XAttribute("ShowNameOnSquare150x150Logo",
                            _shortcutItem.Properties.CurrentState.ShowNameOnSquare150X150Logo ? "on" : "off"),
                        new XAttribute("Square150x150Logo", _shortcutItem.RelativeMediumIconPath),
                        new XAttribute("Square70x70Logo", _shortcutItem.RelativeSmallIconPath),
                        new XAttribute("ForegroundText", _shortcutItem.Properties.CurrentState.ForegroundText),
                        new XAttribute("BackgroundColor", _shortcutItem.Properties.CurrentState.BackgroundColor)
                        )));
            
            if (!Directory.Exists(_shortcutItem.VisualElementsPath))
            {
                Directory.CreateDirectory(_shortcutItem.VisualElementsPath);
            }

            IoUtils.FileActionRetainingAttributes(_shortcutItem.VisualElementManifestPath,
                () => xDoc.Save(_shortcutItem.VisualElementManifestPath));

        }

        private void DeleteFilesAndFolders()
        {
            if (Directory.Exists(_shortcutItem.VisualElementsPath))
            {
                Directory.Delete(_shortcutItem.VisualElementsPath, true);
            }

            var manifestPath =
                $"{_shortcutItem.TargetFolderPath}\\{Path.GetFileNameWithoutExtension(_shortcutItem.TargetFilePath)}.VisualElementsManifest.xml";

            if (File.Exists(manifestPath))
            {
                File.Delete(manifestPath);
            }
        }

        private void SaveMetadata()
        {
            _shortcutItem.Properties.SaveMediumIconMetadata(_shortcutItem.MediumImageResizeMetadataPath);
            _shortcutItem.Properties.SaveSmallIconMetadata(_shortcutItem.SmallImageResizeMetadataPath);
        }

        private void SaveIcons()
        {
            BuildShortcutItemIcon(_shortcutItem.FullMediumIconPath, ShortcutConstantsAndEnums.MediumShortcutOutputSize,
                _shortcutItem.Properties.CurrentState.MediumImage,
                ShortcutConstantsAndEnums.MediumXyRatio);

            BuildShortcutItemIcon(_shortcutItem.FullSmallIconPath, ShortcutConstantsAndEnums.SmallShortcutOutputSize,
                _shortcutItem.Properties.CurrentState.SmallImage,
                ShortcutConstantsAndEnums.SmallXyRatio);
        }

        private static void BuildShortcutItemIcon(string fullIconPath, Size outputSize,
            ShortcutItemImage shortcutItemImage, XyRatio xyRatio)
        {
            BuildIcon(fullIconPath, outputSize.Width,
                outputSize.Height,
                shortcutItemImage.Bytes,
                (int)Math.Round(shortcutItemImage.Width * xyRatio.X, 0),
                (int)Math.Round(shortcutItemImage.Height * xyRatio.Y, 0),
                (int)Math.Round(shortcutItemImage.X * xyRatio.X, 0),
                (int)Math.Round(shortcutItemImage.Y * xyRatio.Y, 0));
        }

        private static void BuildIcon(string filePath, int width, int height, byte[] imageBytes, int imageWidth,
            int imageHeight, int imageX, int imageY)
        {
            IoUtils.ForceDelete(filePath);
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