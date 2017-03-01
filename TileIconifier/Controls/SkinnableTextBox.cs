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
    //Inspired from there : http://stackoverflow.com/a/38405319

    class SkinnableTextBox : TextBox
    {
        const int WM_PAINT = 0xF;  //Find a better place for this constant.          

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
                    if (BorderStyle == BorderStyle.FixedSingle)
                    {
                        Invalidate();
                    }                        
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
                    if (BorderStyle == BorderStyle.FixedSingle)
                    {
                        Invalidate();
                    }
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
                    if (BorderStyle == BorderStyle.FixedSingle)
                    {
                        Invalidate();
                    }
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            //The paint event is not fired, so we must listen for the paint Windows message ourselves.
            if (m.Msg == WM_PAINT && BorderStyle == BorderStyle.FixedSingle)
            {                
                PaintUserBorder();
            }
        }

        private void PaintUserBorder()
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

            using (Graphics g = CreateGraphics())
            using (Pen p = new Pen(bColor))
                g.DrawRectangle(p, new Rectangle(0, 0, Width - 1, Height - 1));
        }
    }
}
