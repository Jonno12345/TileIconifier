using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileIconifier.Controls
{
    class SkinnableTabControl : TabControl
    {
        private const ControlStyles OWNER_DRAWN_FLAGS =
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer |
            ControlStyles.ResizeRedraw |
            ControlStyles.UserPaint;

        private const TextFormatFlags TEXT_FLAGS =
            TextFormatFlags.HorizontalCenter |
            TextFormatFlags.VerticalCenter;

        private const string UNSUPPORTED_PROPERTY_ERROR =
            "This property is not currently supported by this control.";

        #region "Properties"
        /// <summary>
        /// Indicates whether or not we draw the control ourselves.
        /// </summary>
        private bool OwnerDrawInternal
        {
            get
            {
                return (FlatStyle == FlatStyle.Flat);
            }
        }

        /// <summary>
        /// Gets or sets whether or not the control has the OwnerDrawn flag
        /// at the Win32 API level.
        /// </summary>
        private bool HasOwnerDrawFlags
        {
            get
            {
                return GetStyle(OWNER_DRAWN_FLAGS);
            }
            set
            {
                SetStyle(OWNER_DRAWN_FLAGS, value);
            }
        }

        //The base class' ForeColor properties is totally hidden from the code editor 
        //and the designer, which is extremely inconvinient, so we need to reimplement it
        //to change its visibility attribute.
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        public override Color BackColor
        {
            get
            {
                //The base control does not support any other BackColor than 
                //the one the property returns, which means that we can't make 
                //it ambiant.
                if (OwnerDrawInternal && Parent != null)
                {
                    return Parent.BackColor;
                }
                return base.BackColor;
            }

            set
            {
                //We call the base class' setter just in case, but it
                //does nothing according to the Reference Source.
                base.BackColor = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete(UNSUPPORTED_PROPERTY_ERROR)]
        public new TabAlignment Alignment
        {
            get { return base.Alignment; }
            set { throw new NotSupportedException(UNSUPPORTED_PROPERTY_ERROR); }
        }

        private Appearance appearance = Appearance.Normal;
        [DefaultValue(Appearance.Normal)]
        public new Appearance Appearance
        {
            get { return appearance; }
            set
            {
                if (appearance != value)
                {
                    appearance = value;
                    SetBaseProperties();
                    Invalidate();
                }
            }
        }

        private FlatStyle flatStyle = FlatStyle.System;
        [DefaultValue(FlatStyle.System)]
        public FlatStyle FlatStyle
        {
            get { return flatStyle; }
            set
            {
                if (flatStyle != value)
                {
                    flatStyle = value;
                    SetBaseProperties();
                    Invalidate();
                    RefreshAllTabPagesColors();
                }
            }
        }

        private Color flatTabSelectedBackColor = SystemColors.Window;
        [DefaultValue(typeof(Color), nameof(SystemColors.Window))]
        public Color FlatTabSelectedBackColor
        {
            get
            {
                return flatTabSelectedBackColor;
            }
            set
            {
                if (flatTabSelectedBackColor != value)
                {
                    flatTabSelectedBackColor = value;
                    if (OwnerDrawInternal)
                    {
                        if (SelectedIndex >= 0 && SelectedIndex < TabPages.Count)
                        {
                            InvalidateTab(SelectedIndex);
                        }
                        RefreshAllTabPagesColors();
                    }
                }
            }
        }

        private Color flatTabSelectedForeColor = SystemColors.WindowText;
        [DefaultValue(typeof(Color), nameof(SystemColors.WindowText))]
        public Color FlatTabSelectedForeColor
        {
            get
            {
                return flatTabSelectedForeColor;
            }
            set
            {
                if (flatTabSelectedForeColor != value)
                {
                    flatTabSelectedForeColor = value;
                    if (OwnerDrawInternal)
                    {
                        if (SelectedIndex >= 0 && SelectedIndex < TabPages.Count)
                        {
                            InvalidateTab(SelectedIndex);
                        }
                        RefreshAllTabPagesColors();
                    }
                }
            }
        }

        private Color flatTabBorderColor = SystemColors.WindowFrame;
        [DefaultValue(typeof(Color), nameof(SystemColors.WindowFrame))]
        public Color FlatTabBorderColor
        {
            get { return flatTabBorderColor; }
            set
            {
                if (flatTabBorderColor != value)
                {
                    flatTabBorderColor = value;
                    if (OwnerDrawInternal)
                    {
                        Invalidate();
                    }
                }
            }
        }

        private int flatTabBorderWidth = 1;
        [DefaultValue(1)]
        public int FlatTabBorderWidth
        {
            get { return flatTabBorderWidth; }
            set
            {
                if (flatTabBorderWidth != value)
                {
                    flatTabBorderWidth = value;
                    if (OwnerDrawInternal)
                    {
                        Invalidate();
                    }
                }
            }
        }
        #endregion

        private void InvalidateTab(int index)
        {
            TabPage page = SelectedTab;
            if (SelectedTab != null)
            {
                Rectangle tabRect = GetTabRect(index);
                tabRect.Height += SelectedTab.Bounds.Top - tabRect.Bottom;
                Invalidate(tabRect);
            }
            else
            {
                Invalidate();
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);

            if (OwnerDrawInternal)
                Invalidate();
        }

        private void SetBaseProperties()
        {
            switch (FlatStyle)
            {
                case FlatStyle.Standard:
                case FlatStyle.System:
                    if (Appearance == Appearance.Button)
                    {
                        base.Appearance = TabAppearance.Buttons;
                    }
                    else
                    {
                        base.Appearance = TabAppearance.Normal;
                    }
                    HasOwnerDrawFlags = false;
                    break;

                case FlatStyle.Popup:
                    if (Appearance == Appearance.Button)
                    {
                        //Not a mistake, the "FlatButtons" Appearance really 
                        //better matches the "Popup" FlatStyle.
                        base.Appearance = TabAppearance.FlatButtons;
                    }
                    else
                    {
                        base.Appearance = TabAppearance.Normal;
                    }
                    HasOwnerDrawFlags = false;
                    break;

                case FlatStyle.Flat:
                    //We don't mind the base.Appearance property, since it
                    //has no value that matches a Flat FlatStyle.
                    HasOwnerDrawFlags = true;
                    break;
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            var tabPage = e.Control as TabPage;
            if (tabPage != null)
            {
                RefreshTabPageColors(tabPage);
            }
        }

        private void RefreshAllTabPagesColors()
        {
            foreach (TabPage p in TabPages)
            {
                RefreshTabPageColors(p);
            }
        }

        private void RefreshTabPageColors(TabPage tabPage)
        {
            //We don't touch to the tab pages in design mode to prevent
            //the designer setting the property value in the designer generated file, 
            //which causes weird things. As a side effect of this check, the designer
            //preview is not faithful to the actual result, but that's better than nothing.
            //To completely fix this issue, we would probably need to subclass the TabPage
            //control, which would be a nightmare because of the lack of designer support 
            //for custom TabPages.            
            if (DesignMode) return;

            if (FlatStyle == FlatStyle.Flat)
            {
                tabPage.UseVisualStyleBackColor = false;
                tabPage.BackColor = FlatTabSelectedBackColor;
                tabPage.ForeColor = FlatTabSelectedForeColor;
            }
            else
            {
                tabPage.ResetBackColor();
                tabPage.ResetForeColor();
                tabPage.UseVisualStyleBackColor = true;
            }
        }

        private void DrawRectangle(Graphics graphics, RectangleF bounds, Pen pen)
        {
            graphics.DrawRectangle(pen, bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (SelectedIndex < 0 || TabCount < 1)
                return;

            float borderWidth = FlatTabBorderWidth;
            Pen borderPen = new Pen(FlatTabBorderColor, borderWidth);
            Graphics g = e.Graphics;

            //Create a copy of all tap rectangles, since we will re-use and modify them
            //several times.
            RectangleF[] tabRects = new RectangleF[TabPages.Count];
            for (int i = 0; i < TabCount; i++)
            {
                tabRects[i] = GetTabRect(i);
            }


            //Adjust the size of tabs so that the left edge of the first one aligns with the page border.            
            //-2F is the substaction of the location of the first visible tab's left side and 
            //the page's left side. We hard-codethe value, because there is no way that I know of to get the
            //location of the first visible tab (the tab with an index of 0 is not necesserily the "leftiest" tab.)            
            //+borderWidth / 2F so that we draw the border outside of the tab, as we did with the page.
            float tabInflation = -2F + borderWidth / 2F;
            for (int i = 0; i < tabRects.Length; i++)
            {
                tabRects[i].Inflate(tabInflation, 0);
            }

            //Let's start drawing!

            e.Graphics.Clear(BackColor);

            //Draw a border just outside of the page
            RectangleF tabPageBorderBounds = SelectedTab.Bounds;
            tabPageBorderBounds.Inflate(borderWidth / 2F, borderWidth / 2F);
            if (Appearance == Appearance.Button)
            {
                DrawRectangle(g, tabPageBorderBounds, borderPen);
            }
            else
            {
                //If tabs have a tab appearance, there is no border under the selected tab
                RectangleF trect = tabRects[SelectedIndex];
                PointF[] pts =
                {
                    new PointF(trect.Left, tabPageBorderBounds.Top),
                    tabPageBorderBounds.Location,
                    new PointF(tabPageBorderBounds.Left, tabPageBorderBounds.Bottom),
                    new PointF(tabPageBorderBounds.Right, tabPageBorderBounds.Bottom),
                    new PointF(tabPageBorderBounds.Right, tabPageBorderBounds.Top),
                    new PointF(trect.Right, tabPageBorderBounds.Top)
                };
                g.DrawLines(borderPen, pts);
            }

            //Draw the border and background of each tab.
            if (Appearance == Appearance.Button)
            {
                for (int i = 0; i < TabCount; i++)
                {
                    DrawRectangle(g, tabRects[i], borderPen);

                    //Exclude the border that we have just drawn from the rectangle for when 
                    //we draw the background and text.
                    tabRects[i].Inflate(-borderWidth / 2F, -borderWidth / 2F);
                }
            }
            else
            {
                //Extend the tab so that it reaches the top of the page.
                //We must always use the rectangle of the selected tab to calculate the spacing to add, because
                //when Multiline is true, other tabs may appear on rows the are farther from the top of the page.
                float heightToAdd = tabPageBorderBounds.Top + borderWidth / 2F - tabRects[SelectedIndex].Bottom;
                for (int i = 0; i < TabCount; i++)
                {
                    tabRects[i].Height += heightToAdd;

                    if (i == SelectedIndex) //Tab is selected
                    {
                        PointF[] pts =
                        {
                            new PointF(tabRects[i].Right, tabRects[i].Bottom),
                            new PointF(tabRects[i].Right, tabRects[i].Top),
                            tabRects[i].Location,
                            new PointF(tabRects[i].Left, tabRects[i].Bottom)
                        };

                        g.DrawLines(borderPen, pts);
                    }

                    //Exclude the border that we may have just drawn from the rectangle for when 
                    //we draw the background and text. Don't forget that in this case, there 
                    //is no border at the bottom of the tab.
                    tabRects[i].X += borderWidth / 2F;
                    tabRects[i].Y += borderWidth / 2F;
                    tabRects[i].Width -= borderWidth;
                    tabRects[i].Height -= borderWidth / 2F;
                }
            }

            //Draw the text of each tab
            for (int i = 0; i < TabCount; i++)
            {
                Color textCol;
                if (i == SelectedIndex)
                {
                    using (var b = new SolidBrush(FlatTabSelectedBackColor))
                        g.FillRectangle(b, tabRects[i]);

                    textCol = FlatTabSelectedForeColor;
                }
                else
                {
                    textCol = ForeColor;
                }

                TextFormatFlags flags = TEXT_FLAGS;
                if (RightToLeft == RightToLeft.Yes)
                    flags |= TextFormatFlags.RightToLeft;

                TextRenderer.DrawText(g, TabPages[i].Text, Font, Rectangle.Round(tabRects[i]), textCol, flags);
            }

            borderPen.Dispose();
        }
    }
}
