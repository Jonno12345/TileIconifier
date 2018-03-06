using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace TileIconifier
{
    internal static class NativeMethods
    {
        //Keep the prefix of constants in alphabetical order
        public const int DCX_WINDOW = 0x00000001;
        public const int DCX_CACHE = 0x00000002;
        public const int DCX_CLIPSIBLINGS = 16;
        public const int DCX_INTERSECTRGN = 0x00000080;

        public const int RDW_INVALIDATE = 0x0001;
        public const int RDW_ERASE = 0x0004;
        public const int RDW_ERASENOW = 0x0200;
        public const int RDW_UPDATENOW = 0x0100;
        public const int RDW_FRAME = 0x0400;

        public const int RGN_XOR = 3;
        public const int RGN_COPY = 5;        

        public const int TBM_GETTICPOS = WM_USER + 15;
        public const int TBM_GETNUMTICS = WM_USER + 16;
        public const int TBM_GETTHUMBRECT = WM_USER + 25;
        public const int TBM_GETCHANNELRECT = WM_USER + 26;

        public const int WM_PAINT = 0x000F;
        public const int WM_NCPAINT = 0x0085;
        public const int WM_USER = 0x0400;

        public const int WS_EX_CLIENTEDGE = 0x00000200;
        public const int WS_BORDER = 0x00800000;

        //Keep the library name of functions in alphabetical order
        //GDI32
        [DllImport("gdi32.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateRectRgn(int x1, int y1, int x2, int y2);

        [DllImport("gdi32.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int CombineRgn(IntPtr hRgn, IntPtr hRgn1, IntPtr hRgn2, int nCombineMode);

        [DllImport("gdi32.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool DeleteObject(IntPtr hObject);

        //User32
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]        
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, int flags);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]        
        public static extern bool RedrawWindow(IntPtr hwnd, ref RECT rcUpdate, IntPtr hrgnUpdate, int flags);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]        
        public static extern bool GetWindowRect(IntPtr hWnd, [In, Out] ref RECT rect);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, [In, Out] ref RECT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        //UXTheme
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public RECT(Rectangle r)
            {
                left = r.Left;
                top = r.Top;
                right = r.Right;
                bottom = r.Bottom;
            }

            public int left;
            public int top;
            public int right;
            public int bottom;            
        }
    }
}
