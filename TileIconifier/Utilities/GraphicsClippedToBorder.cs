using System;
using System.Drawing;
using System.Windows.Forms;

namespace TileIconifier.Utilities
{
    //A little helper object that temporarily modifies the clipping region of a Graphics to exclude everything except the borders.
    sealed class GraphicsClippedToBorder : IDisposable
    {
        private bool _disposed;
        private readonly Graphics _graphics;
        private readonly Region _oldClip;
        private readonly Region _tmpClip;

        private static Size GetBorderSize(BorderStyle borderStyle)
        {
            switch (borderStyle)
            {
                case BorderStyle.None:
                    return new Size();

                case BorderStyle.FixedSingle:
                    return SystemInformation.BorderSize;

                case BorderStyle.Fixed3D:
                    return SystemInformation.Border3DSize;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Clips the region of a <see cref="Graphics"/> to the borders of the control.
        /// </summary>
        /// <param name="g">A <see cref="Graphics"/> for the non-client area of the control.</param>
        /// <param name="ctrl"></param>
        /// <param name="borderStyle"></param>
        public GraphicsClippedToBorder(Graphics g, Control ctrl, BorderStyle borderStyle)
        {
            var sizeBorder = GetBorderSize(borderStyle);
            var rectExclude = new Rectangle
            {
                Width = ctrl.Width - 2*sizeBorder.Width,
                Height = ctrl.Height - 2*sizeBorder.Height,
                X = sizeBorder.Width,
                Y = sizeBorder.Height
            };

            _graphics = g;
            _oldClip = _graphics.Clip;
            _tmpClip = _oldClip.Clone();
            _tmpClip.Exclude(rectExclude);
            _graphics.Clip = _tmpClip;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            if (_oldClip != null)
            {
                _graphics.Clip = _oldClip;
            }
            _tmpClip?.Dispose();

            _disposed = true;
        }
    }
}
