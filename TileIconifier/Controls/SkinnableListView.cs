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
    class SkinnableListView : ListView
    {
        const int WM_PAINT = 0xF;  //Find a better place for this constant.  

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        #region "Properties"
        [DefaultValue(true)]
        private bool headersUseVisualStyleColors = true;
        public bool HeadersUseVisualStyleColors
        {
            get
            {
                return headersUseVisualStyleColors;
            }
            set
            {
                headersUseVisualStyleColors = value;
                OwnerDraw = !value;
            }
        }

        private Color headerBackColor = SystemColors.Control;
        [DefaultValue(typeof(Color), "Control")]
        public Color HeaderBackColor
        {
            get
            {
                return headerBackColor;
            }
            set
            {
                if (headerBackColor != value)
                {
                    headerBackColor = value;
                    if (HeadersUseVisualStyleColors)
                    {
                        Invalidate();
                    }
                }                
            }
        }

        private Color headerForeColor = SystemColors.ControlText;
        [DefaultValue(typeof(Color), "ControlText")]
        public Color HeaderForeColor
        {
            get
            {
                return headerForeColor;
            }
            set
            {
                if (headerForeColor != value)
                {
                    headerForeColor = value;
                    if (HeadersUseVisualStyleColors)
                    {
                        Invalidate();
                    }
                }
            }
        }

        private Color borderColor;
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                if (borderColor != value)
                {
                    borderColor = value;
                    {
                        if (BorderStyle == BorderStyle.FixedSingle)
                        {
                            Invalidate();
                        }
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
        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            base.OnDrawColumnHeader(e);
            
            if (!HeadersUseVisualStyleColors)
            {
                using (SolidBrush b = new SolidBrush(HeaderBackColor))
                    e.Graphics.FillRectangle(b, e.Bounds);

                TextFormatFlags flags = 
                    TextFormatFlags.Default |
                    TextFormatFlags.VerticalCenter |
                    TextFormatFlags.EndEllipsis |
                    ConvertToTextFormatFlags(e.Header.TextAlign);

                TextRenderer.DrawText(e.Graphics, e.Header.Text, Font, e.Bounds, HeaderForeColor, flags);
            }
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            base.OnDrawItem(e);

            e.DrawDefault = true;
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            base.OnDrawSubItem(e);

            //Maybe not needed since we already set DrawDefault to true in OnDrawItem. To verify one day...
            e.DrawDefault = true;           
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT && BorderStyle == BorderStyle.FixedSingle)
            {
                DrawUserBorder();
            }                
        }

        private void DrawUserBorder()
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
                //Unlike with the SkinnableTextBox, we need to draw the standard
                //frame even if no color is specified for the standard state, because
                //otherwise, the previous color gets stuck. 
                bColor = SystemColors.WindowFrame;
            }

            //The graphics obtained with this.CreateGraphics only works for 
            //the content area (drawings on it end up under the header, scrollbars, etc)
            //and drawing on it gets buggy when scrolling so we must use a native function.
            var hdc = GetWindowDC(this.Handle);
            using (var g = Graphics.FromHdcInternal(hdc))
            using (var p = new Pen(bColor))
                g.DrawRectangle(p, new Rectangle(0, 0, Width - 1, Height - 1));
            ReleaseDC(this.Handle, hdc);
        }

        private TextFormatFlags ConvertToTextFormatFlags(HorizontalAlignment pHoriAlign)
        {     
            switch (pHoriAlign)
            {
                case HorizontalAlignment.Left:
                    return TextFormatFlags.Left;
                case HorizontalAlignment.Center:
                    return TextFormatFlags.HorizontalCenter;
                case HorizontalAlignment.Right:
                    return TextFormatFlags.Right;
                default:
                    throw new ArgumentException("Unsupported horizontal alignement.");
            }                
        }
    }
}
