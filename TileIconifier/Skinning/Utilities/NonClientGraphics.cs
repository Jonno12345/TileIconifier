using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileIconifier.Skinning.Utilities
{
    //A little convinience wrapper that creates a non client Graphics from handles 
    //and releases all the associated resources when it's disposed.
    class NonClientGraphics : IDisposable
    {
        private IntPtr _hWnd = IntPtr.Zero;
        private IntPtr _hRgnClip = IntPtr.Zero;
        private IntPtr _hDC = IntPtr.Zero;

        public Graphics Graphics { get; }

        public NonClientGraphics(IntPtr hWnd, IntPtr hRgnClip)
        {
            _hWnd = hWnd;

            var flagsDCX = NativeMethods.DCX_WINDOW | NativeMethods.DCX_CACHE | NativeMethods.DCX_CLIPSIBLINGS;
            if (hRgnClip != IntPtr.Zero && hRgnClip != (IntPtr)1)
            {
                //GetDCEx takes ownership of the region, so we give it a copy. This also means that we don't need to delete it afterward.
                _hRgnClip = LayoutAndPaintUtils.CopyHRgn(hRgnClip);
                //Only set this flag if we provide a valid region to GetDCEx. Otherwise, the latter fails and return null.
                flagsDCX |= NativeMethods.DCX_INTERSECTRGN;
            }

            _hDC = NativeMethods.GetDCEx(_hWnd, _hRgnClip, flagsDCX);

            if (_hDC != IntPtr.Zero)
            {
                Graphics = Graphics.FromHdc(_hDC);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Graphics != null)
                {
                    Graphics.Dispose();
                }
                if (_hDC != IntPtr.Zero)
                {
                    NativeMethods.ReleaseDC(_hWnd, _hDC);
                }
                else if (_hRgnClip != IntPtr.Zero)
                {
                    //hrgn not null, but hdc null, so the region was created, but GetDCEx probably failed,
                    //so it hasn't taken ownership of the region, so we need to delete it ourselves.
                    NativeMethods.DeleteObject(_hRgnClip);
                }
            }
        }
    }
}
