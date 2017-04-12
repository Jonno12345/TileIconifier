using System;
using System.Runtime.InteropServices;

namespace TileIconifier
{
    internal static class NativeMethods
    {
        internal const int WM_PAINT = 0xF;
        internal const int WM_USER = 0x0400;

        internal const int TBM_GETTHUMBRECT = WM_USER + 25;
        internal const int TBM_GETCHANNELRECT = WM_USER + 26;        

        [DllImport("user32.dll")]
        internal static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]        
        internal extern static IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, [In, Out] ref RECT lParam);

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;            
        }
    }
}
