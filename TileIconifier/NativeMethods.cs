using System;
using System.Runtime.InteropServices;

namespace TileIconifier
{
    internal static class NativeMethods
    {
        public const int WM_PAINT = 0x000F;
        public const int WM_USER = 0x0400;

        public const int TBM_GETTICPOS = WM_USER + 15;
        public const int TBM_GETNUMTICS = WM_USER + 16;
        public const int TBM_GETTHUMBRECT = WM_USER + 25;
        public const int TBM_GETCHANNELRECT = WM_USER + 26;        

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]        
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, [In, Out] ref RECT lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;            
        }
    }
}
