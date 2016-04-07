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
        private Point _movingPoint = Point.Empty;
        private bool _panning;

        private Point _startingPoint = Point.Empty;
        internal int MaxHeight;
        internal int MaxWidth;
        internal int MinHeight;
        internal int MinWidth;

        public PannablePictureBoxImage PannablePictureBoxImage;

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
            //if (PannablePictureBoxImage != null)
            //{
            //    if (_movingPoint.X + PannablePictureBoxImage.Width > pctBox.Width * 2)
            //        _movingPoint.X = pctBox.Width * 2 - PannablePictureBoxImage.Width;
            //    if (_movingPoint.X < -2 * pctBox.Width)
            //        _movingPoint.X = -2 * pctBox.Width;
            //    if (_movingPoint.Y + PannablePictureBoxImage.Height > pctBox.Height * 2)
            //        _movingPoint.Y = pctBox.Height * 2 - PannablePictureBoxImage.Height;
            //    if (_movingPoint.Y < -2 * pctBox.Height)
            //        _movingPoint.Y = -2 * pctBox.Height;
            //}
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

            //////enforce the width and height as a binding box
            ////if (_movingPoint.X + PannablePictureBoxImage.Width > pctBox.Width)
            ////    _movingPoint.X = pctBox.Width - PannablePictureBoxImage.Width;
            ////if (_movingPoint.Y + PannablePictureBoxImage.Height > pctBox.Height)
            ////    _movingPoint.Y = pctBox.Height - PannablePictureBoxImage.Height;
            ////if (_movingPoint.X < 0)
            ////    _movingPoint.X = 0;
            ////if (_movingPoint.Y < 0)
            ////    _movingPoint.Y = 0;
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
            if (PannablePictureBoxImage.Image == null || PannablePictureBoxImage.Width < 1 ||
                PannablePictureBoxImage.Height < 1)
                return;

            e.Graphics.DrawImage(
                SetResolution(PannablePictureBoxImage.Image, PannablePictureBoxImage.Width,
                    PannablePictureBoxImage.Height), PannablePictureBoxImage.X, PannablePictureBoxImage.Y);

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
            DoubleClick?.Invoke(sender, e);
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
    }
}