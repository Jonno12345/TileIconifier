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

    class SkinnableTextBox : TextBox, ISkinnableTextBox
    {   
        #region "Properties"
        private Color backColor = SystemColors.Window;
        [DefaultValue(typeof(Color), nameof(SystemColors.Window))]
        public new Color BackColor
        {
            get { return backColor; }
            set
            {
                if (backColor != value)
                {
                    backColor = value;
                    if (!ReadOnly)
                    {
                        base.BackColor = value;
                    }
                }                
            }
        }

        private Color readOnlyBackColor = SystemColors.Control;
        [DefaultValue(typeof(Color), nameof(SystemColors.Control))]
        public Color ReadOnlyBackColor
        {
            get { return readOnlyBackColor; }
            set
            {
                if (readOnlyBackColor != value)
                {
                    readOnlyBackColor = value;  
                    if (ReadOnly)
                    {
                        base.BackColor = value;
                    }
                }
            }
        }

        private Color borderColor = SystemColors.WindowFrame;
        [DefaultValue(typeof(Color), nameof(SystemColors.WindowFrame))]
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

        private Color borderFocusedColor = Color.Empty;
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

        private Color borderDisabledColor = Color.Empty;
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
        #endregion

        protected override void OnReadOnlyChanged(EventArgs e)
        {
            base.OnReadOnlyChanged(e);

            //We use the base class property to change the actual color. 
            //This classe's BackColor property stores the not-read-only-BackColor 
            //value independently from the actual (current) Background color.
            if (ReadOnly)
            {
                base.BackColor = ReadOnlyBackColor;
            }
            else
            {
                base.BackColor = BackColor;
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            //The paint event is not fired, so we must listen for the paint Windows message ourselves.
            if (m.Msg == NativeMethods.WM_PAINT && BorderStyle == BorderStyle.FixedSingle)
            {                
                PaintBorder();
            }
        }

        private void PaintBorder()
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
            else if (BorderColor != SystemColors.WindowFrame)
            {
                bColor = BorderColor;
            }
            else
            {
                return;
            }

            IntPtr hdc = NativeMethods.GetWindowDC(Handle);
            using (var g = Graphics.FromHdc(hdc))
            using (var p = new Pen(bColor))
                g.DrawRectangle(p, new Rectangle(0, 0, Width - 1, Height - 1));
            NativeMethods.ReleaseDC(Handle, hdc);
        }
    }
}
