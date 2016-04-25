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

using System.IO;
using System.Linq;
using System.Xml.Linq;
using TileIconifier.Core.Utilities;

namespace TileIconifier.Core.Shortcut.State
{
    public class ShortcutItemStateController
    {
        private ShortcutIconState OldState { get; set; }
        public ShortcutIconState CurrentState { get; set; }

        public bool HasUnsavedChanges => !CurrentState.Equals(OldState);
        public bool ForegroundTextColourChanged => CurrentState.ForegroundText != OldState.ForegroundText;

        public void SaveMediumIconMetadata(string filePath)
        {
            CurrentState.MediumImage.Save(filePath);
        }

        public void SaveSmallIconMetadata(string filePath)
        {
            CurrentState.SmallImage.Save(filePath);
        }

        public void UndoChanges()
        {
            CurrentState = OldState.Clone();
        }

        public void CommitChanges()
        {
            OldState = CurrentState.Clone();
        }

        public void ResetParameters()
        {
            //defaults
            OldState = new ShortcutIconState
            {
                BackgroundColor = ShortcutConstantsAndEnums.DefaultAccentColor ?? "black",
                ForegroundText = "light",
                ShowNameOnSquare150X150Logo = true,
                MediumImage = new ShortcutItemImage(ShortcutConstantsAndEnums.MediumShortcutOutputSize),
                SmallImage = new ShortcutItemImage(ShortcutConstantsAndEnums.SmallShortcutOutputSize)
            };
            CurrentState = OldState.Clone();
        }

        internal void LoadParameters(bool loadFromFile, string visualElementsManifestPath,
            string mediumImageResizeMetadataPath, string smallImageResizeMetadataPath, string targetFolderPath)
        {
            if (loadFromFile)
            {
                var xmlDoc = XDocument.Load(visualElementsManifestPath);

                try
                {
                    ShortcutItemImage mediumImage = null;
                    ShortcutItemImage smallImage = null;
                    if (File.Exists(mediumImageResizeMetadataPath))
                    {
                        mediumImage = ShortcutItemImage.Load(mediumImageResizeMetadataPath);
                    }
                    if (File.Exists(smallImageResizeMetadataPath))
                    {
                        smallImage = ShortcutItemImage.Load(smallImageResizeMetadataPath);
                    }

                    var parameters = from b in xmlDoc.Descendants("VisualElements")
                        select new ShortcutIconState
                        {
                            BackgroundColor = b.Attribute("BackgroundColor").Value,
                            ForegroundText = b.Attribute("ForegroundText").Value,
                            ShowNameOnSquare150X150Logo = b.Attribute("ShowNameOnSquare150x150Logo").Value == "on",
                            MediumImage =
                                mediumImage ?? new ShortcutItemImage(ShortcutConstantsAndEnums.MediumShortcutOutputSize)
                                {
                                    Bytes =
                                        ImageUtils.LoadFileToByteArray(targetFolderPath +
                                                                       b.Attribute("Square150x150Logo").Value),
                                    X = 0,
                                    Y = 0
                                },
                            SmallImage =
                                smallImage ?? new ShortcutItemImage(ShortcutConstantsAndEnums.SmallShortcutOutputSize)
                                {
                                    Bytes =
                                        ImageUtils.LoadFileToByteArray(targetFolderPath +
                                                                       b.Attribute("Square70x70Logo").Value),
                                    X = 0,
                                    Y = 0
                                }
                        };

                    OldState = parameters.Single();
                    CurrentState = OldState.Clone();
                }
                catch
                {
                    ResetParameters();
                }
            }
            else
            {
                ResetParameters();
            }
        }
    }
}