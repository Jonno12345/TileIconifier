using System;
using System.Runtime.InteropServices;

namespace TileIconifier
{
    internal static class NativeMethods
    {
        internal const int WM_PAINT = 0xF;

        [DllImport("user32.dll")]
        internal static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
    }
}
