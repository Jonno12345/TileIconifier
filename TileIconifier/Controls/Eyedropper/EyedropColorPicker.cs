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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TileIconifier.Core.Utilities;
using TileIconifier.Properties;
using TileIconifier.Skinning.Skins;

namespace TileIconifier.Controls.Eyedropper
{
    internal sealed class EyedropColorPicker : Control, ISkinnableControl
    {
        private readonly Bitmap _mIcon;

        private bool _iscapturing;

        private Bitmap _mSnapshot;
        private float _mZoom = 6;

        public EyedropColorPicker()
        {
            DoubleBuffered = true;
            var eyedropperIcon = new Bitmap(Resources.Actions_color_picker_black_icon);
            _mIcon = ImageUtils.ResizeImage(eyedropperIcon, 20, 20);
            eyedropperIcon.Dispose();

        }
        
        public int Zoom
        {
            get { return (int) _mZoom; }
            set
            {
                _mZoom = value;
                RecalcSnapshotSize();
            }
        }

        public Color SelectedColor { get; set; }

        private RectangleF ImageRect => Util.Rect(ClientRectangle);
        public event EventHandler SelectedColorChanged;

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RecalcSnapshotSize();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_mSnapshot == null) return;
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            var r = RectangleF.Empty;
            r.Width = _mSnapshot.Size.Width*Zoom;
            r.Height = _mSnapshot.Size.Height*Zoom;
            r.X = 0;
            r.Y = 0;
            e.Graphics.DrawImage(_mSnapshot, r);

            if (_iscapturing)
            {
                var center = Util.Center(r);
                var centerrect = new Rectangle(Util.Point(center), new Size(0, 0));
                centerrect.X -= Zoom/2 - 2;
                centerrect.Y -= Zoom/2 - 2 ;
                centerrect.Width = Zoom;
                centerrect.Height = Zoom;
                e.Graphics.DrawRectangle(Pens.Black, centerrect);
            }
            else
            {
                int offset = (int)((r.Width - _mIcon.Width) / 2);

                e.Graphics.FillRectangle(new SolidBrush(BackColor), new Rectangle(new Point(0, 0), Size));
                e.Graphics.DrawImage(_mIcon, offset, offset);
            }
            //
            ////draws a border - removed for now.

            //var rr = ClientRectangle;
            //Pen pen = new Pen(BackColor, 3);
            //rr.Inflate(-1, -1);
            //e.Graphics.DrawRectangle(pen, rr);
            //Util.DrawFrame(e.Graphics, rr, 6, Color.CadetBlue);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
            Cursor = Cursors.Cross;
            _iscapturing = true;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                GetSnapshot();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            {
                Cursor = Cursors.Arrow;
                _iscapturing = false;
                Invalidate();
            }
        }

        private void RecalcSnapshotSize()
        {
            _mSnapshot?.Dispose();
            var r = ImageRect;

            var w = (int) Math.Floor(r.Width/Zoom);
            var h = (int) Math.Floor(r.Height/Zoom);
            _mSnapshot = new Bitmap(w, h);
        }

        private void GetSnapshot()
        {
            var p = MousePosition;

            var scaling = Util.GetScalingFactor();
            p.X = (int)(p.X * scaling);
            p.Y = (int)(p.Y * scaling);
            p.X -= (int)(_mSnapshot.Width * scaling)/2;
            p.Y -= (int)(_mSnapshot.Height * scaling)/2;
            
            using (var dc = Graphics.FromImage(_mSnapshot))
            {
                dc.CopyFromScreen(p, new Point(0, 0), _mSnapshot.Size);
                Refresh(); //Invalidate();

                var center = Util.Center(new RectangleF(0, 0, _mSnapshot.Size.Width, _mSnapshot.Size.Height));
                var c = _mSnapshot.GetPixel((int) Math.Ceiling(center.X), (int) Math.Ceiling(center.Y));
                if (c == SelectedColor) return;
                SelectedColor = c;
                SelectedColorChanged?.Invoke(this, null);
            }
        }

        public void ApplySkin(BaseSkin skin)
        {
            BackColor = skin.BackColor;
        }
    }
}