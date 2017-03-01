using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileIconifier.Controls
{
    //Borrowed from there : http://stackoverflow.com/a/38405319

    class SkinnableTextBox : TextBox
    {
        const int WM_PAINT = 0xF;
        const uint RDW_INVALIDATE = 0x1;
        const uint RDW_IUPDATENOW = 0x100;
        const uint RDW_FRAME = 0x400;
        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);        

        Color borderColor = Color.Empty;
        [DefaultValue(typeof(Color), "")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                if (borderColor != value)
                {
                    borderColor = value;
                    Invalidate();
                }                
            }
        }

        Color borderFocusedColor = Color.Empty;
        [DefaultValue(typeof(Color), "")]
        public Color BorderFocusedColor
        {
            get { return borderFocusedColor; }
            set
            {
                if (borderFocusedColor != value)
                {
                    borderFocusedColor = value;
                    Invalidate();
                }
            }
        }

        Color borderDisabledColor = Color.Empty;
        [DefaultValue(typeof(Color), "")]
        public Color BorderDisabledColor
        {
            get { return borderDisabledColor; }
            set
            {
                if (borderDisabledColor != value)
                {
                    borderDisabledColor = value;
                    Invalidate();
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT && BorderStyle == BorderStyle.FixedSingle)
            {
                Color bColor;
                if (!Enabled && !BorderDisabledColor.IsEmpty)
                {
                    bColor = BorderDisabledColor;
                }
                else if (Focused && !BorderFocusedColor.IsEmpty)
                {
                    bColor = BorderFocusedColor;
                }
                else if (!BorderColor.IsEmpty)
                {
                    bColor = BorderColor;
                }
                else
                {
                    return;
                }
                
                var hdc = GetWindowDC(this.Handle);
                using (var g = Graphics.FromHdcInternal(hdc))
                using (var p = new Pen(bColor))
                    g.DrawRectangle(p, new Rectangle(0, 0, Width - 1, Height - 1));

                ReleaseDC(this.Handle, hdc);
            }
        }
    }
}
