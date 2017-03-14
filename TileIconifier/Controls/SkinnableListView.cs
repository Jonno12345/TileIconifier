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
        public SkinnableListView()
        {
            base.OwnerDraw = true;
        }

        #region "Properties"
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use the FlatStyle property instead.")]
        [DefaultValue(true)]
        public new bool OwnerDraw
        {
            get { return base.OwnerDraw; }
            set { base.OwnerDraw = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use the FlatStyle property instead.")]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; }
        }
        
        private FlatStyle flatStyle = FlatStyle.Standard;
        [DefaultValue(FlatStyle.Standard)]
        public FlatStyle FlatStyle
        {
            get { return flatStyle; }
            set
            {
                if (flatStyle != value)
                {                    
                    switch (value)
                    {
                        case FlatStyle.Standard:                            
                            base.BorderStyle = BorderStyle.Fixed3D;                            
                            break;
                        case FlatStyle.Flat:                            
                            base.BorderStyle = BorderStyle.FixedSingle;
                            break;
                        default:
                            throw new InvalidEnumArgumentException("Only the Standard and Flat FlatStyle are supported by this control.");
                    }
                    flatStyle = value;
                }
            }
        }
                
        private bool drawStandardItems = true;
        [DefaultValue(true)]
        public bool DrawStandardItems
        {
            get { return drawStandardItems; }
            set
            {
                if (drawStandardItems != value)
                {
                    drawStandardItems = value;
                    Invalidate();
                }
            }
        }

        private Color flatHeaderBackColor = SystemColors.Control;
        [DefaultValue(typeof(Color), nameof(SystemColors.Control))]
        public Color FlatHeaderBackColor
        {
            get { return flatHeaderBackColor; }
            set
            {
                if (flatHeaderBackColor != value)
                {
                    flatHeaderBackColor = value;
                    if (FlatStyle == FlatStyle.Flat)
                    {
                        Invalidate();
                    }
                }                
            }
        }

        private Color flatHeaderForeColor = SystemColors.ControlText;
        [DefaultValue(typeof(Color), nameof(SystemColors.ControlText))]
        public Color FlatHeaderForeColor
        {
            get { return flatHeaderForeColor; }
            set
            {
                if (flatHeaderForeColor != value)
                {
                    flatHeaderForeColor = value;
                    if (FlatStyle == FlatStyle.Flat)
                    {
                        Invalidate();
                    }
                }
            }
        }

        private Color flatBorderColor = SystemColors.WindowFrame;
        [DefaultValue(typeof(Color), nameof(SystemColors.WindowFrame))]
        public Color FlatBorderColor
        {
            get { return flatBorderColor; }
            set
            {
                if (flatBorderColor != value)
                {
                    flatBorderColor = value;
                    {
                        if (FlatStyle == FlatStyle.Flat)
                        {
                            Invalidate();
                        }
                    }
                }
            }
        }

        private Color flatBorderFocusedColor = Color.Empty;
        [DefaultValue(typeof(Color), "")]
        public Color FlatBorderFocusedColor
        {
            get { return flatBorderFocusedColor; }
            set
            {
                if (flatBorderFocusedColor != value)
                {
                    flatBorderFocusedColor = value;
                    if (FlatStyle == FlatStyle.Flat)
                    {
                        Invalidate();
                    }
                }
            }
        }

        private Color flatBorderDisabledColor = Color.Empty;
        [DefaultValue(typeof(Color), "")]
        public Color FlatBorderDisabledColor
        {
            get { return flatBorderDisabledColor; }
            set
            {
                if (flatBorderDisabledColor != value)
                {
                    flatBorderDisabledColor = value;
                    if (FlatStyle == FlatStyle.Flat)
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
            
            if (FlatStyle == FlatStyle.Flat)
            {
                using (SolidBrush b = new SolidBrush(FlatHeaderBackColor))
                    e.Graphics.FillRectangle(b, e.Bounds);

                TextFormatFlags flags =                     
                    TextFormatFlags.VerticalCenter |
                    TextFormatFlags.EndEllipsis |
                    ConvertToTextFormatFlags(e.Header.TextAlign);

                TextRenderer.DrawText(e.Graphics, e.Header.Text, Font, e.Bounds, FlatHeaderForeColor, flags);
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            base.OnDrawItem(e);

            e.DrawDefault = DrawStandardItems;
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            base.OnDrawSubItem(e);
                        
            e.DrawDefault = DrawStandardItems;           
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == NativeMethods.WM_PAINT && FlatStyle == FlatStyle.Flat)
            {
                DrawUserBorder();
            }                
        }

        private void DrawUserBorder()
        {
            Color bColor;
            if (!Enabled && !FlatBorderDisabledColor.IsEmpty)
            {
                bColor = FlatBorderDisabledColor;
            }
            else if (Focused && !FlatBorderFocusedColor.IsEmpty)
            {
                bColor = FlatBorderFocusedColor;
            }            
            else
            {
                //Unlike with the SkinnableTextBox, we need to draw the standard
                //frame even if no color is specified for the standard state, because
                //otherwise, the previous color gets stuck. 
                bColor = FlatBorderColor;
            }
            
            var hdc = NativeMethods.GetWindowDC(this.Handle);
            using (var g = Graphics.FromHdcInternal(hdc))
            using (var p = new Pen(bColor))
                g.DrawRectangle(p, new Rectangle(0, 0, Width - 1, Height - 1));
            NativeMethods.ReleaseDC(Handle, hdc);
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
