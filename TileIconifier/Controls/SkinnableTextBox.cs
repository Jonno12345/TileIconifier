using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TileIconifier.Skinning.Skins;
using TileIconifier.Utilities;

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
                //Don't check if the old value is the same as the new one!
                //Since the user can set base.BackColor by casting the
                //the control to an upper level type, it is entirely possible
                //for our "backColor" variable to be the same as "value" while
                //being different from base.BackColor even when those values
                //should be the same. Ultimately, the base class *already*
                //checks if the value is the same before doing expensive
                //operations anyway.
                backColor = value;
                if (!ReadOnly)
                {
                    base.BackColor = value;
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
                readOnlyBackColor = value;
                if (ReadOnly)
                {
                    base.BackColor = value;
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
                    if (BorderStyle == BorderStyle.FixedSingle && 
                        ((Enabled && !Focused) ||
                        (Focused && BorderFocusedColor.IsEmpty) ||
                        (!Enabled && BorderDisabledColor.IsEmpty)))
                    {
                        InvalidateBorder();
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
                    if (Focused && BorderStyle == BorderStyle.FixedSingle)
                    {
                        InvalidateBorder();
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
                    if (!Enabled && BorderStyle == BorderStyle.FixedSingle)
                    {
                        InvalidateBorder();
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

        protected override void OnEnter(EventArgs e)
        {
            if (BorderStyle == BorderStyle.FixedSingle)
            {
                InvalidateBorder();
            }

            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            if (BorderStyle == BorderStyle.FixedSingle)
            {
                InvalidateBorder();
            }

            base.OnLeave(e);
        }

        private void InvalidateBorder()
        {
            using (var reg = new Region(ClientRectangle))
            {
                var borderSize = SystemInformation.BorderSize;
                var rectContent = ClientRectangle;
                rectContent.Inflate(-borderSize.Width, -borderSize.Height);
                reg.Exclude(rectContent);
                Invalidate(reg);
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            //Strangely enough, the border of TextBox is a part of the client area when its style is FixedSingle.
            //I feel like this design decision was made to facilitate the customization of the border despite the 
            //fact that it's semantically wrong. However, since this app already has several helpers for drawing 
            //in the non client area, I have decided to paint the border as if it was part of the non client area so 
            //that if MS ever decides to move the border in the non client area, where it should probably be, 
            //we can just switch WM_PAINT with WM_NCPAINT. We would also need to update how we invalidate the border. 
            if (m.Msg == NativeMethods.WM_PAINT && BorderStyle == BorderStyle.FixedSingle)
            {                
                PaintCustomBorder(m.HWnd, m.WParam);
            }
        }

        private void PaintCustomBorder(IntPtr hWnd, IntPtr hRgn)
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
                //Regular border, which has already been drawn by the system at this point
                return;
            }

            using (var ncg = new NonClientGraphics(hWnd, hRgn))
            {
                if (ncg.Graphics == null)
                {
                    return;
                }

                ControlPaint.DrawBorder(ncg.Graphics, new Rectangle(new Point(0), Size), bColor, ButtonBorderStyle.Solid);
            }
        }

        public void ApplySkin(BaseSkin skin)
        {
            BorderStyle = skin.TextBoxBorderStyle;
            BackColor = skin.TextBoxBackColor;
            ReadOnlyBackColor = skin.TextBoxReadOnlyBackColor;
            BorderColor = skin.TextBoxBorderColor;
            BorderFocusedColor = skin.TextBoxBorderFocusedColor;
            BorderDisabledColor = skin.TextBoxBorderDisabledColor;
            ForeColor = skin.TextBoxForeColor;
        }
    }
}
