using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TileIconifier.Skinning.Skins;
using TileIconifier.Utilities;

namespace TileIconifier.Controls
{
    class SkinnableListView : ListView, ISkinnableControl
    {        
        public SkinnableListView()
        {            
            OwnerDraw = true;
            DoubleBuffered = true;
        }

        #region "Properties"
        [Browsable(false)]        
        [DefaultValue(true)]
        public new bool OwnerDraw
        {
            get { return base.OwnerDraw; }
            set { base.OwnerDraw = value; }
        }

        private bool _useExplorerStyle = true;
        [DefaultValue(true)]
        public bool UseExplorerStyle
        {
            get { return _useExplorerStyle; }
            set
            {
                if (_useExplorerStyle != value)
                {
                    _useExplorerStyle = value;
                    if (IsHandleCreated)
                    {
                        //The documentation for SetWindowTheme does not suggest that this is needed, 
                        //but some glitches occurs if we enable, then disable the explorer style
                        //with the same handle.
                        RecreateHandle();
                    }                    
                }
            }
        }

        private ListViewHeaderAppearance _headerAppearance = ListViewHeaderAppearance.Standard;
        [DefaultValue(ListViewHeaderAppearance.Standard)]
        public ListViewHeaderAppearance HeaderAppearance
        {
            get { return _headerAppearance; }
            set
            {
                //Note that this property has no effect at design time because the OwnerDraw 
                //property, which has to be true for this one to take effect, is ignored.
                if (_headerAppearance != value)
                {
                    _headerAppearance = value;
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
                    if (BorderStyle == BorderStyle.FixedSingle)
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
                    if (BorderStyle == BorderStyle.FixedSingle)
                    {
                        Invalidate();
                    }
                }
            }
        }

        private Color _borderColor = SystemColors.WindowFrame;
        [DefaultValue(typeof(Color), nameof(SystemColors.WindowFrame))]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                if (_borderColor != value)
                {
                    _borderColor = value;
                    {
                        //This color is also used for the focused and the 
                        //disabled states if their value is empty.
                        if (BorderStyle == BorderStyle.FixedSingle && 
                            (!Focused && Enabled || (Focused && BorderFocusedColor.IsEmpty) ||
                            (!Enabled && BorderDisabledColor.IsEmpty)))
                        {
                            InvalidateNonClient();
                        }
                    }
                }
            }
        }

        private Color _borderFocusedColor = Color.Empty;
        [DefaultValue(typeof(Color), "")]
        public Color BorderFocusedColor
        {
            get { return _borderFocusedColor; }
            set
            {
                if (_borderFocusedColor != value)
                {
                    _borderFocusedColor = value;
                    if (BorderStyle == BorderStyle.FixedSingle && Focused)
                    {
                        InvalidateNonClient();
                    }
                }
            }
        }

        private Color _borderDisabledColor = Color.Empty;
        [DefaultValue(typeof(Color), "")]
        public Color BorderDisabledColor
        {
            get { return _borderDisabledColor; }
            set
            {
                if (_borderDisabledColor != value)
                {
                    _borderDisabledColor = value;
                    if (BorderStyle == BorderStyle.FixedSingle && !Enabled)
                    {
                        InvalidateNonClient();
                    }
                }
            }
        }
        #endregion

        private void InvalidateNonClient()
        {
            LayoutAndPaintUtils.InvalidateNonClient(this);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            NativeMethods.SetWindowTheme(Handle, UseExplorerStyle ? "Explorer" : null, null);

            base.OnHandleCreated(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            if (BorderStyle == BorderStyle.FixedSingle)
            {
                InvalidateNonClient();
            }

            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            if (BorderStyle == BorderStyle.FixedSingle)
            {
                InvalidateNonClient();
            }

            base.OnLeave(e);
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            //Simply changes the default value. Users will have the oppurtunity to change 
            //it when the event is raised.
            e.DrawDefault = true;

            base.OnDrawColumnHeader(e);

            //Implements the flat header style
            if (e.DrawDefault && HeaderAppearance == ListViewHeaderAppearance.Flat)
            {
                using (var b = new SolidBrush(FlatHeaderBackColor))
                    e.Graphics.FillRectangle(b, e.Bounds);

                TextFormatFlags flags =
                    TextFormatFlags.VerticalCenter |
                    TextFormatFlags.EndEllipsis |
                    LayoutAndPaintUtils.ConvertToTextFormatFlags(e.Header.TextAlign); //Header.TextAlign is already Rtl translated

                TextRenderer.DrawText(e.Graphics, e.Header.Text, Font, e.Bounds, FlatHeaderForeColor, flags);

                //Drawing handled, so tell the system to draw nothing
                e.DrawDefault = false;
            }
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;

            base.OnDrawItem(e);            
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;

            base.OnDrawSubItem(e); 
        }        

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == NativeMethods.WM_NCPAINT && BorderStyle == BorderStyle.FixedSingle)
            {
                PaintCustomBorder(m.HWnd, m.WParam);
            }                
        }

        private void PaintCustomBorder(IntPtr hDC, IntPtr hRgn)
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

            using (var ncg = new NonClientGraphics(hDC, hRgn))
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
            UseExplorerStyle = skin.ListViewUseExplorerStyle;
            BorderStyle = skin.ListViewBorderStyle;
            HeaderAppearance = skin.ListViewHeaderStyle;
            FlatHeaderBackColor = skin.ListViewHeaderBackColor;
            FlatHeaderForeColor = skin.ListViewHeaderForeColor;
            BackColor = skin.ListViewBackColor;
            ForeColor = skin.ListViewForeColor;
            BorderColor = skin.ListViewBorderColor;
            BorderFocusedColor = skin.ListViewBorderFocusedColor;
            BorderDisabledColor = skin.ListViewBorderDisabledColor;
        }
    }

    public enum ListViewHeaderAppearance
    {
        Standard,
        Flat
    }
}
