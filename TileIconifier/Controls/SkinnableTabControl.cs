using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using TileIconifier.Skinning;
using TileIconifier.Skinning.Skins;

namespace TileIconifier.Controls
{
    class SkinnableTabControl : TabControl, ISkinnableControl
    {
        private const string UNSUPPORTED_PROPERTY_ERROR =
            "This property is not currently supported by this control.";

        private const TextFormatFlags TEXT_FLAGS =
            TextFormatFlags.HorizontalCenter |
            TextFormatFlags.VerticalCenter;
        
        /// <summary>
        ///     Collection of <see cref="ControlStyles"/> that should be set to true 
        ///     when we want to draw ourselves. 
        /// </summary>
        private static readonly ReadOnlyCollection<ControlStyles> customPaintingFlags = new List<ControlStyles>
        {
            ControlStyles.UserPaint,
            ControlStyles.AllPaintingInWmPaint,
            ControlStyles.OptimizedDoubleBuffer,
            ControlStyles.ResizeRedraw            
        }.AsReadOnly();

        /// <summary>
        ///     Dictionary of the default values for the <see cref="ControlStyles"/>
        ///     that we change when we are drawing ourselves. 
        /// </summary>
        private readonly ReadOnlyDictionary<ControlStyles, bool> defaultPaintingFlags;                

        public SkinnableTabControl()
        {
            //Backup the initial ControlStyles in case we need to reset them.
            var defaultsFlags = new Dictionary<ControlStyles, bool>();
            foreach (var f in customPaintingFlags)
            {
                defaultsFlags.Add(f,GetStyle(f));
            }
            defaultPaintingFlags = new ReadOnlyDictionary<ControlStyles, bool>(defaultsFlags);
        }

        #region "Properties"
        /// <summary>
        ///     Indicates whether or not we should draw the control ourselves.
        /// </summary>
        private bool HandleDrawing
        {
            get { return (FlatStyle == FlatStyle.Flat); }
        }

        public override Color BackColor
        {
            get
            {
                //We make the property ambiant, but only if we draw the control 
                //ourselves because the base control does not support any other 
                //BackColor than the one that the property returns.
                if (HandleDrawing && Parent != null)
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Obsolete(UNSUPPORTED_PROPERTY_ERROR)]
        public new TabAlignment Alignment
        {
            get { return base.Alignment; }
            set { base.Alignment = value; }
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

        private FlatStyle flatStyle = FlatStyle.Standard;
        [DefaultValue(FlatStyle.Standard)]
        public FlatStyle FlatStyle
        {
            get { return flatStyle; }
            set
            {
                //Don't check if the old value is the same as the new one,
                //because this property sets other properties that are
                //shadowed and therefore, they could be out-of-sync with
                //this one.
                flatStyle = value;
                SetBaseProperties();
                Invalidate();
                RefreshAllTabPagesColors();
            }
        }

        private Color flatTabSelectedBackColor = SystemColors.Window;
        [DefaultValue(typeof(Color), nameof(SystemColors.Window))]
        public Color FlatTabSelectedBackColor
        {
            get { return flatTabSelectedBackColor; }
            set
            {
                if (flatTabSelectedBackColor != value)
                {
                    flatTabSelectedBackColor = value;
                    if (HandleDrawing)
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
            get { return flatTabSelectedForeColor; }
            set
            {
                if (flatTabSelectedForeColor != value)
                {
                    flatTabSelectedForeColor = value;
                    if (HandleDrawing)
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
                    if (HandleDrawing)
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
                    if (HandleDrawing)
                    {
                        Invalidate();
                    }
                }
            }
        }
        #endregion

        /// <summary>
        ///     Invalidate the region of the tab at the given index.
        /// </summary>        
        private void InvalidateTab(int index)
        {
            //We use the selected tab to calculate the spacing between the bottom
            //of the rectangle returned by GetTabRect() and the top of the page
            //so that we can invalidate it too (remember that we draw there so that
            //the tab blends with the page). If we can't obtain the selected page, we
            //just invalidate the whole control.
            var page = SelectedTab;
            if (page != null)
            {
                var tabRect = GetTabRect(index);
                tabRect.Height += page.Bounds.Top - tabRect.Bottom;
                Invalidate(tabRect);
            }
            else
            {
                Invalidate();
            }
        }

        /// <summary>
        ///     Sets the appropriate flags to the control so that it can be owner drawn.
        /// </summary>
        private void EnableOwnerDrawing()
        {
            //When the control is owner drawn, the tabs' auto size is not scaled to the screen DPI,
            //even though the font is, which causes the text to be cropped. As 
            //a workaround, we force a fixed size for all tabs.
            if (SizeMode == TabSizeMode.Normal)
            {
                SizeMode = TabSizeMode.Fixed;
            }
            var flags = new ControlStyles();
            foreach (ControlStyles s in customPaintingFlags)
            {
                flags |= s;
            }

            SetStyle(flags, true);
        }

        /// <summary>
        ///     Reset the owner drawing flags.
        /// </summary>
        private void ResetOwnerDrawing()
        {
            foreach (var k in defaultPaintingFlags.Keys)
            {
                SetStyle(k, defaultPaintingFlags[k]);
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);

            if (HandleDrawing)
                Invalidate();
        }

        /// <summary>
        ///     Applies the most appropriate values to the base control 
        ///     depending of the FlatStyle and the Appearance properties 
        ///     that we have implemented ourselves.
        /// </summary>
        private void SetBaseProperties()
        {
            switch (FlatStyle)
            {
                case FlatStyle.Standard:
                    //Not supported. Fallback on FlatStyle.System
                case FlatStyle.System:
                    if (Appearance == Appearance.Button)
                    {
                        base.Appearance = TabAppearance.Buttons;
                    }
                    else
                    {
                        base.Appearance = TabAppearance.Normal;
                    }
                    ResetOwnerDrawing();
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
                    ResetOwnerDrawing();
                    break;

                case FlatStyle.Flat:
                    //We don't mind the base.Appearance property, since it
                    //has no value that matches a Flat FlatStyle and we draw
                    //everything ourslves anyway.
                    EnableOwnerDrawing();
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

        /// <summary>
        ///     Sets the appropriate colors to the given <see cref="TabPage"/>.
        /// </summary>        
        private void RefreshTabPageColors(TabPage tabPage)
        {
            //We don't touch to the tab pages in design mode to prevent
            //the designer from setting the property value in the designer 
            //generated file. As a side effect of this check, the designer 
            //preview is not faithful to the actual result.                  
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

        private static void DrawRectangle(Graphics graphics, RectangleF bounds, Pen pen)
        {
            graphics.DrawRectangle(pen, bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {   
            if (HandleDrawing)
            {
                if (SelectedIndex < 0 || TabCount < 1)
                    return;

                var borderWidth = (float)FlatTabBorderWidth;
                var borderPen = new Pen(FlatTabBorderColor, borderWidth);
                var g = e.Graphics;
                var scaleX = g.DpiX / 96F;

                //Create a copy of all tab rectangles, since we will re-use and modify them
                //several times.
                RectangleF[] tabRects = new RectangleF[TabPages.Count];
                for (int i = 0; i < TabCount; i++)
                {
                    tabRects[i] = GetTabRect(i);
                }


                //Adjust the size of tabs so that the left edge of the first one aligns with the page border.            
                //-2F is an empirical value that is the space between the left side of the first visible tab and the 
                //left side of the page. We hard-code the value, because there is no way that I know of to get the
                //location of the first visible tab (the tab with an index of 0 is not necesserily the "leftiest" tab.)            
                //+borderWidth / 2F ensures that we draw the border outside of the tab, as we do with the page.
                var tabInflation = -2F * (float)Math.Floor(scaleX) + borderWidth / 2F;
                for (int i = 0; i < tabRects.Length; i++)
                {
                    tabRects[i].Inflate(tabInflation, 0);
                }

                //Let's start drawing!

                g.Clear(BackColor);

                //Draw a border just outside of the page
                var tabPageBorderBounds = (RectangleF)SelectedTab.Bounds;
                tabPageBorderBounds.Inflate(borderWidth / 2F, borderWidth / 2F);
                if (Appearance == Appearance.Button)
                {
                    DrawRectangle(g, tabPageBorderBounds, borderPen);
                }
                else
                {
                    //If tabs have a tab appearance, there is no border under the selected tab, so 
                    //we draw a partial rectangle with DrawLines().
                    var trect = tabRects[SelectedIndex];
                    var pts = new[]
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
                    var heightToAdd = tabPageBorderBounds.Top + borderWidth / 2F - tabRects[SelectedIndex].Bottom;
                    for (int i = 0; i < TabCount; i++)
                    {
                        tabRects[i].Height += heightToAdd;

                        if (i == SelectedIndex) //Tab is selected
                        {
                            var pts = new[]
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

                //Draw the background, focus rectangle and text of each tab
                for (int i = 0; i < TabCount; i++)
                {
                    Color textCol;
                    if (i == SelectedIndex)
                    {
                        using (var b = new SolidBrush(FlatTabSelectedBackColor))
                            g.FillRectangle(b, tabRects[i]);

                        if (Focused && ShowFocusCues)
                        {
                            var focusRect = Rectangle.Round(tabRects[i]);
                            focusRect.Inflate(-3, -3);
                            ControlPaint.DrawFocusRectangle(g, focusRect, flatTabSelectedForeColor, FlatTabSelectedBackColor);
                        }

                        textCol = FlatTabSelectedForeColor;
                    }
                    else
                    {
                        textCol = ForeColor;
                    }

                    var flags = TEXT_FLAGS;
                    if (RightToLeft == RightToLeft.Yes)
                        flags |= TextFormatFlags.RightToLeft;

                    TextRenderer.DrawText(g, TabPages[i].Text, Font, Rectangle.Round(tabRects[i]), textCol, flags);
                }

                borderPen.Dispose();
            }

            base.OnPaint(e);
        }

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            base.ScaleControl(factor, specified);

            //Implements scaling of the ItemSize property.
            //We must ONLY scale the value of ItemSize if it has been set by the user, because the default value
            //is already scaled by the system. Unfortunately, the ShouldSerializeItemSize method that allows us 
            //to determine if the value is user-set is private, so we must use reflection to call it.
            var method = typeof(TabControl).GetMethod("ShouldSerialize" + nameof(ItemSize), BindingFlags.NonPublic | BindingFlags.Instance);
            if (method != null && method.ReturnType == typeof(bool))
            {
                var result = (bool)method.Invoke(this, null);
                if (result)
                {
                    //Perform scaling
                    var tabSizeF = (SizeF)ItemSize;
                    if (specified.HasFlag(BoundsSpecified.Width))
                    {
                        tabSizeF.Width *= factor.Width;
                    }
                    if (specified.HasFlag(BoundsSpecified.Height))
                    {
                        tabSizeF.Height *= factor.Height;
                    }
                    ItemSize = Size.Round(tabSizeF);
                }
            }
        }

        public void ApplySkin(BaseSkin skin)
        {
            FlatStyle = skin.TabControlFlatStyle;
            FlatTabSelectedBackColor = skin.TabControlSelectedTabBackColor;
            FlatTabSelectedForeColor = skin.TabControlSelectedTabForeColor;
            FlatTabBorderColor = skin.TabControlTabBorderColor;
            
            foreach (Control c in Controls)
            {
                SkinUtils.ApplySkinToControl(skin, c);
            }          
        }        
    }
}
