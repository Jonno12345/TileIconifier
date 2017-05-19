using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TileIconifier.Skinning.Skins;

namespace TileIconifier.Controls
{
    class SkinnableListView : ListView, ISkinnableControl
    {
        private const string USE_FLATSTYLE_INSTEAD_ERROR = 
            "Use the FlatStyle property instead.";

        public SkinnableListView()
        {
            //Set the base class property to bypass the deprecated warning
            base.OwnerDraw = true;

            DoubleBuffered = true;
        }

        #region "Properties"
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Obsolete(USE_FLATSTYLE_INSTEAD_ERROR)]
        [DefaultValue(true)]
        public new bool OwnerDraw
        {
            get { return base.OwnerDraw; }
            set { throw new NotSupportedException(USE_FLATSTYLE_INSTEAD_ERROR); }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Obsolete(USE_FLATSTYLE_INSTEAD_ERROR)]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { throw new NotSupportedException(USE_FLATSTYLE_INSTEAD_ERROR); }
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
                    flatStyle = value;

                    switch (value)
                    {                        
                        case FlatStyle.Flat:
                        case FlatStyle.Popup:
                            //Popup effect not implemented, so FlatStyle.Popup behaves
                            //exactly like FlatStyle.Flat.
                            base.OwnerDraw = true;
                            base.BorderStyle = BorderStyle.FixedSingle;                            
                            break;

                        case FlatStyle.Standard:
                            //The appearance is still determined by the system, but at
                            //least the Paint events are raised.
                            base.OwnerDraw = true;
                            base.BorderStyle = BorderStyle.Fixed3D;
                            break;

                        default:
                            base.OwnerDraw = false;
                            base.BorderStyle = BorderStyle.Fixed3D;
                            break;
                    }                    
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
                        //This color is also used for the focused and the 
                        //disabled states if their value is empty.
                        if (FlatStyle == FlatStyle.Flat && 
                            (!Focused && Enabled || (Focused && FlatBorderFocusedColor.IsEmpty) ||
                            (!Enabled && FlatBorderDisabledColor.IsEmpty)))
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
                    if (FlatStyle == FlatStyle.Flat && Focused)
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
                    if (FlatStyle == FlatStyle.Flat && !Enabled)
                    {
                        Invalidate();
                    }
                }
            }
        }
        #endregion

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {    
            if (FlatStyle == FlatStyle.Flat || FlatStyle == FlatStyle.Popup)
            {
                using (var b = new SolidBrush(FlatHeaderBackColor))
                    e.Graphics.FillRectangle(b, e.Bounds);

                TextFormatFlags flags =                     
                    TextFormatFlags.VerticalCenter |
                    TextFormatFlags.EndEllipsis |
                    ConvertToTextFormatFlags(e.Header.TextAlign); //Header.TextAlign is already Rtl translated

                TextRenderer.DrawText(e.Graphics, e.Header.Text, Font, e.Bounds, FlatHeaderForeColor, flags);
            }
            else
            {
                e.DrawDefault = true;
            }

            base.OnDrawColumnHeader(e);
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            e.DrawDefault = DrawStandardItems;

            base.OnDrawItem(e);            
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = DrawStandardItems;

            base.OnDrawSubItem(e); 
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == NativeMethods.WM_PAINT && FlatStyle == FlatStyle.Flat)
            {
                DrawFlatBorder();
            }                
        }

        private void DrawFlatBorder()
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
            
            IntPtr hdc = NativeMethods.GetWindowDC(Handle);
            using (var g = Graphics.FromHdc(hdc))
            using (var p = new Pen(bColor))
                g.DrawRectangle(p, new Rectangle(0, 0, Width - 1, Height - 1));
            NativeMethods.ReleaseDC(Handle, hdc);
        }

        private TextFormatFlags ConvertToTextFormatFlags(HorizontalAlignment horiAlign)
        {     
            switch (horiAlign)
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

        public void ApplySkin(BaseSkin skin)
        {
            FlatStyle = skin.ListViewFlatStyle;
            FlatHeaderBackColor = skin.ListViewHeaderBackColor;
            FlatHeaderForeColor = skin.ListViewHeaderForeColor;
            BackColor = skin.ListViewBackColor;
            ForeColor = skin.ListViewForeColor;
            FlatBorderColor = skin.ListViewBorderColor;
            FlatBorderFocusedColor = skin.ListViewBorderFocusedColor;
            FlatBorderDisabledColor = skin.ListViewBorderDisabledColor;
        }
    }
}
