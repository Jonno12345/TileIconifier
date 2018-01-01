/*  
    The original source for this control was created by jkristia @ http://www.codeproject.com/Articles/21965/Color-Picker-with-Color-Wheel-and-Eye-Dropper
    licenced under The Code Project Open License (CPOL).

    Minor modifications have been made to fit in with the requirements of this application:

    - Image for eyedropper altered
    - Border removed
    - Added support for DPI scaling

    - Changed some variable names and cleaned up to fit with coding practices throughout this solution
*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TileIconifier.Properties;
using TileIconifier.Skinning.Skins;
using TileIconifier.Utilities;

namespace TileIconifier.Controls.Eyedropper
{
    internal sealed class EyedropColorPicker : Control, ISkinnableControl
    {
        private readonly Bitmap _icon;
        private Bitmap _snapshot;
        private Color _selectedColor;
        private bool _isCapturing;
        private int _zoom = 6;
        private float _dpiScaleFactor;

        public EyedropColorPicker()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            _icon = Resources.Actions_color_picker_black_icon;
            CreateSnapshotImage();            
        }

        public event EventHandler SelectedColorChanged;

        [DefaultValue(6)]
        public int Zoom
        {
            get { return _zoom; }
            set
            {
                if (_zoom != value)
                {
                    if (value < 1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(Zoom), value, "Value must be greater than 0.");
                    }
                    _zoom = value;
                    CreateSnapshotImage();
                    if (_isCapturing)
                    {
                        Invalidate();
                    }
                }
            }
        }

        [Browsable(false)]
        public Color SelectedColor
        {
            get { return _selectedColor; }
            private set
            {
                if (_selectedColor != value)
                {
                    _selectedColor = value;
                    SelectedColorChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Returns the size of the screen region that is captured.
        /// </summary>        
        private Size GetSnapshotSize()
        {
            var width = ClientSize.Width / Zoom;
            var height = ClientSize.Height / Zoom;

            //When the zoom level is too high for the control size, 
            //fallback on the hightest zoom value that works.
            if (width < 1) width = 1;
            if (height < 1) height = 1;

            return new Size(width, height);
        }

        /// <summary>
        /// Creates or re-creates an empty bitmap with the appropriate size 
        /// that will hold the snapshot.
        /// </summary>
        private void CreateSnapshotImage()
        {
            _snapshot?.Dispose();

            var size = GetSnapshotSize();
            _snapshot = new Bitmap(size.Width, size.Height);
        }

        /// <summary>
        /// Returns a <see cref="Point"/> relative to the snapshot bounds 
        /// where the color will be obtained.
        /// </summary>        
        private Point GetSnapShotSelectedPixelLocation()
        {
            var snapBounds = new Rectangle(Point.Empty, _snapshot.Size);

            return LayoutAndPaintUtils.GetRectangleCenter(snapBounds);
        }

        /// <summary>
        /// Refreshes the snapshot and the selected color.
        /// </summary>
        private void RefreshSnapshotData()
        {
            var snapLoc = MousePosition;

            //Graphics.CopyFromScreen takes actual screen coordinate in pixel. However,
            //When running in dpi-virtualized mode (this is possible when the app is 
            //"system dpi aware" and the screen dpi changes while the app is running)
            //the MousePosition property returns a value that is virtualised to the
            //system dpi, not the actual value for the current monitor dpi. Therefore,
            //we need to correct it in order to obtain the real value. This won't be
            //needed when the app is per monitor dpi-aware...
            snapLoc.X = (int)Math.Round(snapLoc.X * _dpiScaleFactor);
            snapLoc.Y = (int)Math.Round(snapLoc.Y * _dpiScaleFactor);

            //Move the point to the upper-left corner of the image so that 
            //the cursor point is centered in the image.
            snapLoc.X -= _snapshot.Width / 2;
            snapLoc.Y -= _snapshot.Height / 2;

            //Get the screenshot
            using (var g = Graphics.FromImage(_snapshot))
            {
                g.CopyFromScreen(snapLoc, Point.Empty, _snapshot.Size);
            }

            //Get the color in the middle of the image
            var selectedPx = GetSnapShotSelectedPixelLocation();
            SelectedColor = _snapshot.GetPixel(selectedPx.X, selectedPx.Y);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            CreateSnapshotImage();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;

            Cursor = Cursors.Cross;
            //Remember that the monitor dpi setting can change while the app is running, 
            //so we really need to check this each time we start capturing.
            _dpiScaleFactor = Util.GetScalingFactor();
            _isCapturing = true;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;

            RefreshSnapshotData();
            //Important to use Refresh here to ensure there is no visible lag 
            //when the user moves the cursor. 
            Refresh();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;

            Cursor = Cursors.Arrow;
            _isCapturing = false;
            Invalidate();

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_isCapturing && _snapshot != null)
            {
                //Draw the screenshot scaled
                e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                var snapBounds = new Rectangle();
                snapBounds.Width = _snapshot.Width * Zoom;
                snapBounds.Height = _snapshot.Height * Zoom;
                e.Graphics.DrawImage(_snapshot, snapBounds);
                e.Graphics.InterpolationMode = InterpolationMode.Default;

                //Draw the square around the middle pixel
                var pt = GetSnapShotSelectedPixelLocation();
                LayoutAndPaintUtils.ScalePoint(ref pt, Zoom);
                var rectBounds = new Rectangle();
                rectBounds.X = pt.X - Zoom / 2;
                rectBounds.Y = pt.Y - Zoom / 2;
                rectBounds.Width = Zoom - 1; //In both cases, minus 1 is the typical GDI+ compensation
                rectBounds.Height = Zoom - 1;
                //Use a black and white dotted pattern to ensure the 
                //rectangle is visible with all background colors.
                using (var p = new Pen(Color.Black))
                {
                    p.DashStyle = DashStyle.Dot;
                    e.Graphics.DrawRectangle(p, rectBounds);
                    p.DashOffset = 1F;
                    p.Color = Color.White;
                    e.Graphics.DrawRectangle(p, rectBounds);
                }
            }
            else
            {
                //Draw the placeholder icon                
                var imgRect = ClientRectangle;
                imgRect.Inflate(-5, -5);
                e.Graphics.Clear(BackColor);
                e.Graphics.DrawImage(_icon, imgRect);
            }

            base.OnPaint(e);
        }

        protected override Size DefaultSize => new Size(36, 36);

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            if (!DesignMode)
            {
                Zoom = (int)Math.Round(Zoom * factor.Width);
            }

            base.ScaleControl(factor, specified);
        }

        public void ApplySkin(BaseSkin skin)
        {
            BackColor = skin.BackColor;
        }
    }
}