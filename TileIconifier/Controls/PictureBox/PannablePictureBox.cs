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
using System.Windows.Forms;
using TileIconifier.Core.Utilities;
using TileIconifier.Skinning;

namespace TileIconifier.Controls.PictureBox
{
    [SkinIgnore]
    public partial class PannablePictureBox : UserControl
    {
        private readonly Font _overlayFont = new Font(new FontFamily("Segoe UI"), 9f, FontStyle.Regular);
        private Point _movingPoint = Point.Empty;
        private bool _panning;

        private Point _startingPoint = Point.Empty;
        internal int MaxHeight;
        internal int MaxWidth;
        internal int MinHeight;
        internal int MinWidth;
        public Color OverlayColor = Color.White;

        public PannablePictureBoxImage PannablePictureBoxImage;

        public bool ShowTextOverlay = false;
        public string TextOverlay;

        public Point TextOverlayPoint;

        public PannablePictureBox()
        {
            InitializeComponent();
            PannablePictureBoxImage = new PannablePictureBoxImage();
            PannablePictureBoxImage.OnPannablePictureNewImageSet += Image_OnPannablePictureNewImageSet;
            pctBox.MouseWheel += PctBox_MouseWheel;
        }

        public Size AssociatedSize { get; set; }

        public void PannablePictureBoxImage_OnPannablePictureImagePropertyChange(object sender, EventArgs e)
        {
            TriggerUpdate();
        }

        public new event EventHandler Click;
        public new event EventHandler DoubleClick;
        public event PannablePictureBoxImage.PannablePictureImagePropertyChanges OnPannablePictureImagePropertyChange;

        public void ShrinkImage()
        {
            SetZoom(GetZoomPercentage() - 0.5m);
        }

        public void EnlargeImage()
        {
            SetZoom(GetZoomPercentage() + 0.5m);
        }

        public void ResetImage()
        {
            ResetZoom(false);
            CenterImage(false);
            pctBox.Invalidate();
        }

        public void ResetZoom(bool invalidateImage = true)
        {
            PannablePictureBoxImage.Width = pctBox.Width;
            PannablePictureBoxImage.Height = (int) (PannablePictureBoxImage.Width/PannablePictureBoxImage.AspectRatio);
            TriggerUpdate(invalidateImage);
        }

        public void CenterImage(bool invalidateImage = true)
        {
            if (PannablePictureBoxImage.Image == null) return;
            PannablePictureBoxImage.X = (pctBox.Width - PannablePictureBoxImage.Width)/2;
            PannablePictureBoxImage.Y = (pctBox.Height - PannablePictureBoxImage.Height)/2;
            TriggerUpdate(invalidateImage);
        }

        public void SetImage(Image image, int width, int height, int x, int y)
        {
            PannablePictureBoxImage.SetImage(image, width, height, x, y);
            _movingPoint = new Point(x, y);
        }

        public void SetZoom(decimal value)
        {
            if (value < 1) value = 1;
            if (value > 100) value = 100;
            var previousWidth = PannablePictureBoxImage.Width;
            var previousHeight = PannablePictureBoxImage.Height;

            PannablePictureBoxImage.Width = (int) ((decimal) MaxWidth/100*value);
            PannablePictureBoxImage.Height = (int) (PannablePictureBoxImage.Width/PannablePictureBoxImage.AspectRatio);

            PannablePictureBoxImage.X += (previousWidth - PannablePictureBoxImage.Width)/2;
            PannablePictureBoxImage.Y += (previousHeight - PannablePictureBoxImage.Height)/2;

            TriggerUpdate();
        }

        public void AlignTop()
        {
            Align(y: 0);
        }

        public void AlignLeft()
        {
            Align(0);
        }

        public void AlignRight()
        {
            Align(pctBox.Width - PannablePictureBoxImage.Width);
        }

        public void AlignBottom()
        {
            Align(y: pctBox.Height - PannablePictureBoxImage.Height);
        }

        public void AlignXMiddle()
        {
            Align((pctBox.Width - PannablePictureBoxImage.Width)/2);
        }

        public void AlignYMiddle()
        {
            Align(y: (pctBox.Height - PannablePictureBoxImage.Height)/2);
        }

        public void Nudge(int? x = null, int? y = null)
        {
            if (x != null)
                PannablePictureBoxImage.X += (int) x;
            if (y != null)
                PannablePictureBoxImage.Y += (int) y;
            TriggerUpdate();
        }


        internal decimal GetZoomPercentage()
        {
            var zoomPercentage = (decimal) PannablePictureBoxImage.Width/MaxWidth*
                                 100;
            return zoomPercentage >= 1 ? zoomPercentage : 1;
        }

        private void PctBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                ShrinkImage();
            }
            else
            {
                EnlargeImage();
            }
        }

        private void Image_OnPannablePictureNewImageSet(object sender, EventArgs e)
        {
            pctBox.Invalidate();
        }

        private void pctBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            _panning = true;

            pctBox.Invalidate();
            _startingPoint = new Point(e.Location.X - _movingPoint.X,
                e.Location.Y - _movingPoint.Y);
        }

        private void pctBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            _panning = false;
            TriggerUpdate();
        }

        private void pctBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_panning || PannablePictureBoxImage.Image == null || e.Button != MouseButtons.Left) return;
            _movingPoint = new Point(e.Location.X - _startingPoint.X,
                e.Location.Y - _startingPoint.Y);

            if (_movingPoint.X + PannablePictureBoxImage.Width > MaxWidth*2)
                _movingPoint.X = MaxWidth*2 - PannablePictureBoxImage.Width;
            if (_movingPoint.Y + PannablePictureBoxImage.Height > MaxHeight*2)
                _movingPoint.Y = MaxHeight*2 - PannablePictureBoxImage.Height;
            if (_movingPoint.X < MinWidth*2)
                _movingPoint.X = MinWidth*2;
            if (_movingPoint.Y < MinHeight*2)
                _movingPoint.Y = MinHeight*2;

            PannablePictureBoxImage.X = _movingPoint.X;
            PannablePictureBoxImage.Y = _movingPoint.Y;

            pctBox.Invalidate();
        }

        private void pctBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            if (PannablePictureBoxImage.Width < 1 ||
                PannablePictureBoxImage.Height < 1)
                return;

            if (PannablePictureBoxImage.Image != null)
            {
                e.Graphics.DrawImage(
                    SetResolution(PannablePictureBoxImage.Image, PannablePictureBoxImage.Width,
                        PannablePictureBoxImage.Height), PannablePictureBoxImage.X, PannablePictureBoxImage.Y);

                if (ShowTextOverlay)
                {
                    DrawTextOverlay(e);
                }

                //debug mode overlay
#if DEBUG
            e.Graphics.DrawString(PannablePictureBoxImage.Width + ", " + PannablePictureBoxImage.Height, DefaultFont,
                new SolidBrush(Color.Red), 0, 0);
            e.Graphics.DrawString(PannablePictureBoxImage.X + ", " + PannablePictureBoxImage.Y, DefaultFont,
                new SolidBrush(Color.Red), 0, 20);
            e.Graphics.DrawString(pctBox.Width + ", " + pctBox.Height, DefaultFont, new SolidBrush(Color.Red), 0, 40);
            e.Graphics.DrawString(MinWidth + "_" + MaxWidth + ", " + MinHeight + "_" + MaxHeight, DefaultFont,
                new SolidBrush(Color.Red), 0, 60);
#endif
            }
            else
            {
                e.Graphics.DrawString("Double", DefaultFont,
                    new SolidBrush(Color.Red), 3, 3);
                e.Graphics.DrawString("Click", DefaultFont,
                    new SolidBrush(Color.Red), 3, 19);
                e.Graphics.DrawString("Me!", DefaultFont,
                    new SolidBrush(Color.Red), 3, 35);
            }
        }

        //not a fun mass of parsing. Attempts to closely match the label behaviour of the Windows 10 Start Menu. Haven't tested against Windows 8.1
        //probably fails in some instances...
        private void DrawTextOverlay(PaintEventArgs e)
        {
            try
            {
                //get the width of the text before any manipulation
                var textWidth = e.Graphics.MeasureString(TextOverlay, _overlayFont).Width;
                //maximum length for a string, without spaces, to fit on the initial line - TODO: Pass these values in if there is ever a new tile type available
                const int maxSingleLineLength = 90;
                //maximum length of a line before truncating with ellipsis
                const int ellipsisLength = 91;

                //function to loop through a line, removing a char at a time until the length satisfies the requirement
                Func<string, int, string> getCharsToMaxLength = (inputString, length) =>
                {
                    var textChunk = inputString;
                    do
                    {
                        textChunk = textChunk.Substring(0, textChunk.Length - 1);
                    } while (e.Graphics.MeasureString(textChunk, _overlayFont).Width > length);
                    return textChunk;
                };

                //if we have a space somewhere in the string and it's over the max single line length it will be split over two lines
                if (textWidth >= maxSingleLineLength && TextOverlay.Contains(" "))
                {
                    //get the first chunk of data to process
                    var firstChunk = getCharsToMaxLength(TextOverlay, maxSingleLineLength);

                    string firstLine;
                    string secondLine;

                    //different handling whether there is a space in the first chunk or not. If there *is* a space, we will grab up until the last occurrance of a space character
                    //anything after that space character is put on the second line
                    //
                    //if there isn't a space in the first line, the entire chunk can be made the first line. The second line will start from where the first space character after this chunk is
                    //EXAMPLES
                    //1- XXXXX XXX XXXXXXXXXXXXXXXXXXXXXXXXXX
                    //This should place XXXXX XXX on line one, and XXXXXXXXXXXXXXXXXXXXXXXXXX on line two
                    //
                    //2- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX X
                    //This should place XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX on line one, and X on line two
                    if (firstChunk.Contains(" "))
                    {
                        var lastSpaceIndex = firstChunk.LastIndexOf(" ", StringComparison.Ordinal);
                        firstLine = firstChunk.Substring(0, lastSpaceIndex).Trim();
                        secondLine = TextOverlay.Substring(lastSpaceIndex, TextOverlay.Length - lastSpaceIndex).Trim();
                    }
                    else
                    {
                        firstLine = firstChunk;
                        var tempSecondLine =
                            TextOverlay.Substring(firstChunk.Length, TextOverlay.Length - firstChunk.Length).Trim();
                        var firstSpaceIndex = tempSecondLine.IndexOf(" ", StringComparison.Ordinal);
                        secondLine =
                            tempSecondLine.Substring(firstSpaceIndex, tempSecondLine.Length - firstSpaceIndex).Trim();
                    }

                    //compare each line to the ellipsis length - if exceeds, truncate with ...
                    //from previous examples above, we should now have:
                    //1. XXXXX XXX
                    //   XXXXXXXXXX...
                    //
                    //2. XXXXXXXXXX...
                    //   X
                    if (e.Graphics.MeasureString(firstLine, _overlayFont).Width > ellipsisLength)
                        firstLine = getCharsToMaxLength(firstLine, ellipsisLength) + "...";
                    if (e.Graphics.MeasureString(secondLine, _overlayFont).Width > ellipsisLength)
                    {
                        var tempSecondLine = getCharsToMaxLength(secondLine, ellipsisLength);

                        if (tempSecondLine.Contains(" "))
                        {
                            //Another fun quirk - we only keep the parts of the second line that can be fully displayed?
                            //Example:
                            //XXXXXXXX XXXXXXXXXXX
                            //becomes
                            //XXXXXXXX...
                            //NOT
                            //XXXXXXXX X...
                            var lastSpaceIndex = tempSecondLine.LastIndexOf(" ", StringComparison.Ordinal);
                            secondLine = tempSecondLine.Substring(0, lastSpaceIndex).Trim() + "...";
                        }
                        else
                        {
                            secondLine = tempSecondLine + "...";
                        }
                    }

                    //draw our lines, first line 16px higher than the default (?) TODO: 16 should really be passed in if new tile types ever become available
                    e.Graphics.DrawString(firstLine, _overlayFont, new SolidBrush(OverlayColor),
                        new PointF(TextOverlayPoint.X, TextOverlayPoint.Y - 16));
                    e.Graphics.DrawString(secondLine, _overlayFont, new SolidBrush(OverlayColor), TextOverlayPoint);
                }
                else
                {
                    //if we have no spaces, it'll always be on a single line. Check if the line needs truncating and display in default position
                    var renderLine = TextOverlay;
                    if (e.Graphics.MeasureString(renderLine, _overlayFont).Width > ellipsisLength)
                        renderLine = getCharsToMaxLength(renderLine, ellipsisLength) + "...";
                    e.Graphics.DrawString(renderLine, _overlayFont, new SolidBrush(OverlayColor), TextOverlayPoint);
                }
            }
            catch
            {
                //ignore a failure
            }
        }

        private Image SetResolution(Image image, int width, int height)
        {
            if (image == null) return null;
            image = ImageUtils.ScaleImage(image, width, height) ?? image;
            return image;
        }

        private void pctBox_Click(object sender, EventArgs e)
        {
            Click?.Invoke(sender, e);
        }

        private void pctBox_DoubleClick(object sender, EventArgs e)
        {
            DoubleClick?.Invoke(this, e);
        }

        private void PannablePictureBox_Load(object sender, EventArgs e)
        {
            MinHeight = -200; //-2*pctBox.Height;
            MaxHeight = 400; //4*pctBox.Height;
            MinWidth = -200; //-2*pctBox.Width;
            MaxWidth = 400; //4*pctBox.Width;
        }

        private void TriggerUpdate(bool invalidate = true)
        {
            OnPannablePictureImagePropertyChange?.Invoke(PannablePictureBoxImage, null);
            if (invalidate) pctBox.Invalidate();
        }

        private void Align(int? x = null, int? y = null)
        {
            if (PannablePictureBoxImage.Image == null) return;
            if (x != null)
                PannablePictureBoxImage.X = (int) x;
            if (y != null)
                PannablePictureBoxImage.Y = (int) y;
            TriggerUpdate();
        }
    }
}