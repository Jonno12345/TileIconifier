using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TileIconifier.Skinning.Skins;
using TileIconifier.Utilities;

namespace TileIconifier.Controls.IconListView
{
    //Prevent child controls from being added at design time.
    [Designer("System.Windows.Forms.Design.ControlDesigner, System.Design", typeof(IDesigner))]
    class IconListView : ScrollableControl, ISkinnableControl
    {
        private const string VSCLASS_LISTVIEW = "ListView";
        private const string VSCLASS_EXPLORER_LISTVIEW_ITEM = "Explorer::ListView";
        private const int VSPART_LISTVIEW_ITEM = 1;
        //Item states
        private const int
            VSSTATE_LISTVIEW_ITEM_HOT = 2,
            VSSTATE_LISTVIEW_ITEM_SELECTED = 3,
            VSSTATE_LISTVIEW_ITEM_SELECTEDNOTFOCUS = 5,
            VSSTATE_LISTVIEW_ITEM_HOTSELECTED = 6;

        //Spacing at the top and left of the item. This value is not user-customizable, but we 
        //can't make it read-only, because it needs to be scaled in ScaleControl.
        private int _itemSpacing = 5;

        //Dimensions of the grid, measured in items.
        private int _rows;
        private int _columns;

        //This value may be fetched dozens of times per seconds in OnMouseMove, so we cache it. 
        //Ideally, we would also update it's value when the system setting is changed, probably 
        //using SystemEvents.UserPreferenceChanged.
        private static bool _systemHotTrackingEnabled = SystemInformation.IsHotTrackingEnabled;

        //Lazy-loaded instance of the VisualStyleRenderer used to draw this control.
        //When it's needed, use the GetVisualStyleRenderer method instead of this field!
        private VisualStyleRenderer _vsRenderer;

        private Size ItemsSpaceNeeded => ItemSize + new Size(_itemSpacing, _itemSpacing);

        //We track the hot item even if visual styles are disabled in case they are 
        //suddently enabled without the mouse moving. We do not need to track it if UseExplorerStyle
        //is false because we do it in that property's setter. As for _systemHotTrackingEnabled,
        //we just assume that it never changes (if it does, wihch is extremelly unlikely, 
        //the change simply won't take effect until the app is restarted since the value is not
        //kept in sync with system events).
        private bool IsHotTrackingItems => _systemHotTrackingEnabled && UseExplorerStyle;

        //Don't use this property when a invalidating for a state change, because even if the new 
        //state does not use a custom drawn border, the previous state might have, so we need to erase 
        //the old border.
        private bool BorderIsCustomDrawn =>
            (BorderStyle == BorderStyle.Fixed3D && VisualStyleRenderer.IsSupported) ||
            (BorderStyle == BorderStyle.FixedSingle && (
                (!Enabled && !BorderDisabledColor.IsEmpty) || (!Enabled && BorderDisabledColor.IsEmpty && !BorderColor.IsEmpty) ||
                (Focused && !BorderFocusedColor.IsEmpty) || (Focused && BorderFocusedColor.IsEmpty && !BorderColor.IsEmpty) ||
                (!Focused && BorderColor != SystemColors.WindowFrame))
            );
        
        /// <summary>
        ///     Gets the bounding rectangle of the complete control, including its non client area.
        /// </summary>
        private Rectangle NonClientRectangle => new Rectangle(Point.Empty, Size);

        /// <summary>
        ///     Gets the index of the first partially or entirely visible item.
        /// </summary>
        private int FirstVisibleItemIndex
        {
            get
            {
                if (Items.Count < 1)
                {
                    return -1;
                }
                var value = ((VerticalScroll.Value - Padding.Top) / ItemsSpaceNeeded.Height) * _columns;
                if (value < 0) value = 0;
                return value;
            }
        }

        /// <summary>
        ///     Gets the index of the last partially or entirely visible item.
        /// </summary>
        private int LastVisibleItemIndex
        {
            get
            {
                if (Items.Count < 1)
                {
                    return -1;
                }
                var value = ((int)Math.Ceiling((VerticalScroll.Value - Padding.Top + ClientSize.Height) / (double)ItemsSpaceNeeded.Height)) * _columns - 1;
                if (value < 0) value = 0;
                else if (value >= Items.Count) value = Items.Count - 1;
                return value;
            }
        }

        /// <summary>
        ///     Gets the bounds of the items relative to the display rectangle.
        /// </summary>
        internal Rectangle[] ItemDisplayBounds { get; private set; }

        private int _hotItemIndex = -1;
        /// <summary>
        ///     Gets the index of the item on which the mouse currently is.
        /// </summary>
        internal int HotItemIndex
        {
            get
            {
                if (IsHotTrackingItems)
                {
                    return _hotItemIndex;
                }
                return -1;
            }
            private set
            {
                if (_hotItemIndex != value)
                {
                    if (value >= Items.Count || value < -1)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    if (IsHotTrackingItems)
                    {
                        using (var reg = new Region(Rectangle.Empty))
                        {
                            //Old value
                            if (_hotItemIndex != -1)
                            {
                                reg.Xor(Items[_hotItemIndex].Bounds);
                            }
                            //New value
                            if (value != -1)
                            {
                                reg.Xor(Items[value].Bounds);
                            }
                            //Important: Set the value before we invalidate!
                            _hotItemIndex = value;
                            Invalidate(reg);
                        }
                    }
                    else
                    {
                        _hotItemIndex = value;
                    }
                }
            }
        }

        public IconListView()
        {
            DoubleBuffered = true;
            AutoScroll = true;
            Items = new IconListVewItemCollection(this);
            BackColor = SystemColors.Window;
            ForeColor = SystemColors.WindowText;
            //Note that for now, we don't really need to call CalculateLayout at initialization. 
            //The consequence of not calling it here would be that ItemDisplayBounds stays null until
            //the control is resized or an item is added, which is not a problem since the only 
            //public member that uses that property is in the IconListViewItem class. However,
            //since CalculateLayout does not do much when no items are present, we call it anyway
            //to prevent problems in the future.
            CalculateLayout();
        }

        [DefaultValue(typeof(Color), nameof(SystemColors.Window))]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        [DefaultValue(typeof(Color), nameof(SystemColors.WindowText))]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        [DefaultValue(true)]
        public override bool AutoScroll
        {
            get { return base.AutoScroll; }
            set { base.AutoScroll = value; }
        }

        private BorderStyle _borderStyle = BorderStyle.Fixed3D;
        [DefaultValue(BorderStyle.Fixed3D)]
        public BorderStyle BorderStyle
        {
            get { return _borderStyle; }
            set
            {
                if (_borderStyle != value)
                {
                    _borderStyle = value;
                    UpdateStyles();
                }
            }
        }

        private Size _itemSize = new Size(24, 24);
        [DefaultValue(typeof(Size), "24, 24")]
        public Size ItemSize
        {
            get { return _itemSize; }
            set
            {
                if (_itemSize != value)
                {
                    _itemSize = value;
                    CalculateLayout();
                }
            }
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
                    if (SelectedIndex != -1)
                    {
                        Invalidate(Items[SelectedIndex].Bounds);
                    }
                    TrackHotItem();
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
                            Invalidate();
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
                        Invalidate();
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
                        Invalidate();
                    }
                }
            }
        }

        private int _selectedIndex = -1;
        /// <summary>
        ///     Gets the index of the selected item. To set the selected item, use the Select property of the item.
        /// </summary>
        [DefaultValue(-1)]
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            internal set
            {
                if (_selectedIndex != value)
                {
                    var nextItemBounds = Rectangle.Empty;
                    if (value >= 0 && value < Items.Count)
                    {
                        nextItemBounds = Items[value].Bounds;
                    }
                    else if (value != -1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(SelectedIndex), value, null);
                    }

                    //Add the rectangle of the previously selected item, 
                    //if relevant (if there was previously no selected item, 
                    //the selected index was -1.
                    var oldItemBounds = Rectangle.Empty;
                    if (_selectedIndex >= 0 && _selectedIndex < Items.Count)
                    {
                        oldItemBounds = Items[_selectedIndex].Bounds;
                    }

                    //Obviously, we must set the new value before scrolling to the new item and invalidating
                    _selectedIndex = value;

                    //Scroll to the newly selected item. If no scrolling was required, 
                    //we simply invalidate the affected items to refresh their visual state.
                    if (_selectedIndex == -1 || !ScrollToItem(value))
                    {
                        LayoutAndPaintUtils.InvalidateRectangles(this, nextItemBounds, oldItemBounds);
                    }

                    OnSelectedIndexChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     Returns the item that is currently selected
        /// </summary>
        [Browsable(false)]
        public IconListViewItem SelectedItem
        {
            get
            {
                if (SelectedIndex != -1)
                {
                    return Items[SelectedIndex];
                }
                return null;
            }
        }

        /// <summary>
        ///     Gets a collection containing all items in the control.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public IconListVewItemCollection Items { get; }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;

                cp.ExStyle &= (~NativeMethods.WS_EX_CLIENTEDGE);
                cp.Style &= (~NativeMethods.WS_BORDER);

                switch (BorderStyle)
                {
                    case BorderStyle.Fixed3D:
                        cp.ExStyle |= NativeMethods.WS_EX_CLIENTEDGE;
                        break;
                    case BorderStyle.FixedSingle:
                        cp.Style |= NativeMethods.WS_BORDER;
                        break;
                }
                return cp;
            }
        }

        #region "Additionnal events"
        public event EventHandler SelectedIndexChanged;
        public event EventHandler ItemActivate;

        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            SelectedIndexChanged?.Invoke(this, e);
        }

        protected virtual void OnItemActivate(EventArgs e)
        {
            ItemActivate?.Invoke(this, e);
        }
        #endregion

        private VisualStyleRenderer GetVisualStyleRenderer(VisualStyleElement vsElement)
        {
            if (_vsRenderer == null)
            {
                _vsRenderer = new VisualStyleRenderer(vsElement);
            }
            else
            {
                _vsRenderer.SetParameters(vsElement);
            }
            return _vsRenderer;
        }

        //This should not be called before the item collection is initialized
        private void CalculateLayout()
        {
            if (ClientSize.Width < 1 || ClientSize.Height < 1)
            {
                _rows = 0;
                _columns = 0;
                ItemDisplayBounds = new Rectangle[Items.Count]; //All equal to Rectangle.Empty by default
                return;
            }

            var cols = (int)Math.Floor((double)DisplayRectangle.Width / (ItemsSpaceNeeded.Width));
            //When there is not enough place for a single column, cols is equals to 0 and 
            //prevents us to calculate the number of rows. Therefore, if the width is insuficient, 
            //we just act as if there was enought place for the column and let it be cropped.
            _columns = (cols > 0) ? cols : 1;
            _rows = (int)Math.Ceiling((double)Items.Count / _columns);
            //The value of the scrollbar indicates the vertical offset of all the items.
            //When the value is, say, 23, all the items are 23px higher than if the
            //scollbar value was 0;
            var displayHeight = (ItemsSpaceNeeded.Height) * _rows;
            
            AutoScrollMinSize = new Size(ItemsSpaceNeeded.Width + Padding.Horizontal, displayHeight + Padding.Vertical);
            //Don't use the DisplayRectangle before this line, because the scroll properties set above are used for its calculation.

            //Calculate item bounds
            ItemDisplayBounds = new Rectangle[Items.Count];
            if (Items.Count > 0)
            {
                var index = 0;
                for (var y = 0; y < _rows; y++)
                {
                    for (var x = 0; x < _columns; x++)
                    {
                        var r = new Rectangle();
                        r.Size = ItemSize;

                        r.X = _itemSpacing; //Place the item in the first column
                        r.X += ItemsSpaceNeeded.Width * x; //Adjust depending of the position

                        r.Y = _itemSpacing;
                        r.Y += ItemsSpaceNeeded.Height * y;

                        ItemDisplayBounds[index] = r;

                        index++;

                        if (index >= Items.Count) break;
                    }
                    if (index >= Items.Count) break;
                }
            }

            Invalidate();
        }

        //Call this method to inform the control that items have been added
        internal void OnItemAddedInternal(bool lastItemFromBatch)
        {
            if (lastItemFromBatch)
            {
                CalculateLayout();
            }
        }

        //Call this method to inform the control that items have been removed
        internal void OnItemRemovedInternal(bool lastItemFromBatch)
        {
            if (lastItemFromBatch)
            {
                CalculateLayout();
            }
        }

        //Call this method whenever an item is about to be removed. This ensure 
        //that this control does not hold any values that refer to an item that 
        //it no longer holds.
        internal void OnItemRemovingInternal(int index)
        {
            SelectedIndex = -1;
            //Purposely use the field instead of the property in order to avoid invalidation. 
            //We can't do the same with SelectedIndex since it has a public event that may
            //need to be raised.
            _hotItemIndex = -1;
        }

        /// <summary>
        ///     Scrolls the view to make the item at the specified index visible, if it is not already.
        /// </summary>        
        private bool ScrollToItem(int itemIndex)
        {
            var itemBounds = Items[itemIndex].Bounds;
            var scrollPerformed = false;
            //Check if (ClientSize.Height < ItemSize.Height) because in that condition, aligning the item at the
            //top crops the bottom, which causes the item to be realigned at the bottom when clicked a second time.
            if (itemBounds.Top - _itemSpacing < ClientRectangle.Top || ClientSize.Height < ItemSize.Height)
            {
                AutoScrollPosition = new Point(AutoScrollPosition.X, ItemDisplayBounds[itemIndex].Top - _itemSpacing + Padding.Top);
                scrollPerformed = true;
            }
            else if (itemBounds.Bottom > ClientRectangle.Bottom)
            {
                AutoScrollPosition = new Point(AutoScrollPosition.X, ItemDisplayBounds[itemIndex].Bottom - ClientRectangle.Height + Padding.Top);
                scrollPerformed = true;
            }
            //Setting the AutoScrollPosition property does not invalidate the control, 
            //so we need to do it ourselves
            if (scrollPerformed)
            {
                Invalidate();
            }
            return scrollPerformed;
        }

        /// <summary>
        ///     Returns the <see cref="IconListViewItem"/> whose bounds relative the client area
        ///     of this <see cref="IconListView"/> contains the specified point.
        /// </summary>        
        public IconListViewItem GetItemAt(Point pt)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Bounds.Contains(pt))
                {
                    return Items[i];
                }
            }

            return null;
        }

        #region "Input processing"
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();

            base.OnMouseDown(e);

            var clickedItem = GetItemAt(e.Location);
            SelectedIndex = (clickedItem != null) ? clickedItem.Index : -1;
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (SelectedIndex != -1 && Items[SelectedIndex].Bounds.Contains(e.Location))
            {
                OnItemActivate(EventArgs.Empty);
            }
            base.OnMouseDoubleClick(e);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            //From the ListView control code in the Reference Source.
            if ((keyData & Keys.Alt) == Keys.Alt) return false;
            switch (keyData & Keys.KeyCode)
            {
                case Keys.PageUp:
                case Keys.PageDown:
                case Keys.Home:
                case Keys.End:
                //Ours
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.Enter:
                    return true;
            }

            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            int? newIndex = null;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    newIndex = SelectedIndex - _columns;
                    break;

                case Keys.Down:
                    newIndex = SelectedIndex + _columns;
                    break;

                case Keys.Left:
                    newIndex = SelectedIndex - 1;
                    break;

                case Keys.Right:
                    newIndex = SelectedIndex + 1;
                    break;

                case Keys.Home:
                    newIndex = 0;
                    break;

                case Keys.End:
                    newIndex = Items.Count - 1;
                    break;

                case Keys.PageUp:
                    var visibleRows = ClientSize.Height / ItemsSpaceNeeded.Height;
                    newIndex = SelectedIndex - _columns * visibleRows; //meh
                    break;

                case Keys.PageDown:
                    var visibleRows2 = ClientSize.Height / ItemsSpaceNeeded.Height;
                    newIndex = SelectedIndex + _columns * visibleRows2; //meh
                    break;

                case Keys.Enter:
                    if (SelectedIndex != -1)
                        OnItemActivate(EventArgs.Empty);
                    break;
            }

            if (newIndex != null && newIndex >= 0)
            {
                if (newIndex >= Items.Count)
                {
                    SelectedIndex = Items.Count - 1;
                }
                else
                {
                    SelectedIndex = (int)newIndex;
                }
            }

            base.OnKeyDown(e);
        }

        /// <summary>
        ///     Refreshes the value of <see cref="HotItemIndex"/>.
        /// </summary>
        /// <param name="mouseLocation">Mouse location relative to the client rectangle.</param>
        private void TrackHotItem(Point? mouseLocation = null)
        {
            if (!IsHotTrackingItems || Items.Count < 1)
            {
                HotItemIndex = -1;
                return;
            }

            var mouseLoc = default(Point);

            if (mouseLocation == null)
            {
                mouseLoc = PointToClient(MousePosition);
            }
            else
            {
                mouseLoc = (Point)mouseLocation;
            }

            //PERF: A likely scenario is that the mouse is still over the same item, so
            //check that condition first before lopping for each items.
            if (HotItemIndex != -1 && Items[HotItemIndex].Bounds.Contains(mouseLoc))
            {
                return;
            }

            var last = LastVisibleItemIndex;
            for (var i = FirstVisibleItemIndex; i <= last; i++)
            {
                if (Items[i].Bounds.Contains(mouseLoc))
                {
                    HotItemIndex = i;
                    return;
                }
            }

            HotItemIndex = -1;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            TrackHotItem(e.Location);

            base.OnMouseMove(e);
        }

        //Only called when scrolling with the scroll bar
        protected override void OnScroll(ScrollEventArgs se)
        {
            //I have not been able to identify the exact cause, but glitches occur on 
            //*some* machines without the line below, while others work just fine...
            Invalidate();

            base.OnScroll(se);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            //Must be after the base call.
            TrackHotItem();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            //This is needed because if the mouse leaves the control fast enough, 
            //the last OnMouseMove call occurs when the mouse is still over an item.
            HotItemIndex = -1;

            base.OnMouseLeave(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            //Invalidate client area, but only the selected items since it's the only 
            //part that change depending of the state.
            if (SelectedIndex >= 0)
            {
                Invalidate(Items[SelectedIndex].Bounds);
            }

            //Invalidate non-client area, but only with a single border. Other border styles
            //don't change depending of the state.
            if (BorderStyle == BorderStyle.FixedSingle)
            {
                InvalidateNonClient();
            }

            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            //Invalidate client area, but only the selected items since it's the only 
            //part that change depending of the state.
            if (SelectedIndex >= 0)
            {
                Invalidate(Items[SelectedIndex].Bounds);
            }

            //Invalidate non-client area, but only with a single border. Other border styles
            //don't change depending of the state.
            if (BorderStyle == BorderStyle.FixedSingle)
            {
                InvalidateNonClient();
            }

            base.OnLeave(e);
        }
        #endregion

        protected override void OnEnabledChanged(EventArgs e)
        {
            //Invalidate non-client area, but only with a single border. Other border styles
            //don't change depending of the state.
            if (BorderStyle == BorderStyle.FixedSingle)
            {
                InvalidateNonClient();
            }

            base.OnEnabledChanged(e);
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            CalculateLayout();

            base.OnPaddingChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            CalculateLayout();

            //The border is already invalidated by the system when the control is resized,
            //but there seem to have a problem with the region when the scrollbar is visible,
            //that causes the right border to be jaggy. Invalidating the non client a second
            //time fixes the issue.
            if (BorderIsCustomDrawn)
            {
                InvalidateNonClient();
            }

            base.OnSizeChanged(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_NCPAINT)
            {
                WmNCPaint(ref m);
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void InvalidateNonClient()
        {
            LayoutAndPaintUtils.InvalidateNonClient(this);
        }

        private void WmNCPaint(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == NativeMethods.WM_NCPAINT)
            {
                if (BorderStyle == BorderStyle.FixedSingle)
                {
                    PaintCustomBorder(m.HWnd, m.WParam);
                }
                else if (BorderStyle == BorderStyle.Fixed3D && VisualStyleRenderer.IsSupported)
                {
                    PaintVisuallyStyledBorder(m.HWnd, m.WParam);
                }
            }
        }

        private void PaintCustomBorder(IntPtr hWnd, IntPtr hRgn)
        {
            var bColor = Color.Empty;
            //If a state color is empty, it means that the border color does not change with that state.
            if (!Enabled && !BorderDisabledColor.IsEmpty)
            {
                bColor = BorderDisabledColor;
            }
            else if (Focused && !BorderFocusedColor.IsEmpty)
            {
                bColor = BorderFocusedColor;
            }
            //Only draw the regular border if it is not the same color as the one already drawn by the system.
            else if (BorderColor != SystemColors.WindowFrame)
            {
                bColor = BorderColor;
            }

            if (!bColor.IsEmpty)
            {
                using (var ncg = new NonClientGraphics(hWnd, hRgn))
                {
                    var g = ncg.Graphics;
                    if (g == null)
                    {
                        return;
                    }
                    ControlPaint.DrawBorder(g, NonClientRectangle, bColor, ButtonBorderStyle.Solid);
                }
            }
        }

        private void PaintVisuallyStyledBorder(IntPtr hWnd, IntPtr hRgn)
        {
            var vsElement = VisualStyleElement.CreateElement(VSCLASS_LISTVIEW, 0, 0);
            if (VisualStyleRenderer.IsElementDefined(vsElement))
            {
                using (var ncg = new NonClientGraphics(hWnd, hRgn))
                {
                    var g = ncg.Graphics;
                    if (g == null)
                    {
                        return;
                    }
                    var vsRenderer = GetVisualStyleRenderer(vsElement);
                    using (new GraphicsClippedToBorder(g, this, BorderStyle))
                    {
                        vsRenderer.DrawBackground(g, NonClientRectangle);
                    }
                }
            }
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            //Paint the items
            if (Items.Count > 0)
            {
                var last = LastVisibleItemIndex;
                for (var i = FirstVisibleItemIndex; i <= last; i++)
                {
                    //Only call OnPaintItem for items that are part of the region that is being drawn.
                    if (e.ClipRectangle.IntersectsWith(Items[i].Bounds))
                    {
                        OnPaintItem(new IconListViewPaintItemEventArgs(e.Graphics, e.ClipRectangle, Items[i]));
                    }
                }
            }
            
            base.OnPaint(e);
        }

        protected virtual void OnPaintItem(IconListViewPaintItemEventArgs e)
        {
            var vsWorked = false;
            var textColor = Color.Empty;

            //If we are not hot tracking items, Item.MouseIsHover is always false
            if (e.Item.MouseIsOver && VisualStyleRenderer.IsSupported)
            {
                var vsElement = VisualStyleElement.CreateElement(
                    VSCLASS_EXPLORER_LISTVIEW_ITEM,
                    VSPART_LISTVIEW_ITEM,
                    e.Item.Selected ? VSSTATE_LISTVIEW_ITEM_HOTSELECTED : VSSTATE_LISTVIEW_ITEM_HOT);
                if (VisualStyleRenderer.IsElementDefined(vsElement))
                {
                    var vsRenderer = GetVisualStyleRenderer(vsElement);
                    textColor = vsRenderer.GetColor(ColorProperty.TextColor);
                    vsRenderer.DrawBackground(e.Graphics, e.Item.Bounds);
                    vsWorked = true;
                }
            }
            else if (e.Item.Selected)
            {
                if (UseExplorerStyle && VisualStyleRenderer.IsSupported)
                {
                    //The ListView visual style elements provided by Winform are not defined, 
                    //so we create our owns using the Explorer style.
                    var vsElement = VisualStyleElement.CreateElement(
                        VSCLASS_EXPLORER_LISTVIEW_ITEM,
                        VSPART_LISTVIEW_ITEM,
                        Focused ? VSSTATE_LISTVIEW_ITEM_SELECTED : VSSTATE_LISTVIEW_ITEM_SELECTEDNOTFOCUS);
                    if (VisualStyleRenderer.IsElementDefined(vsElement))
                    {
                        var vsRenderer = GetVisualStyleRenderer(vsElement);
                        textColor = vsRenderer.GetColor(ColorProperty.TextColor);
                        vsRenderer.DrawBackground(e.Graphics, e.Item.Bounds);
                        vsWorked = true;
                    }
                }

                if (!vsWorked)
                {
                    Brush backBrush;
                    if (Focused)
                    {
                        textColor = SystemColors.HighlightText;
                        backBrush = SystemBrushes.Highlight;
                    }
                    else
                    {
                        textColor = SystemColors.ControlText;
                        backBrush = SystemBrushes.Control;
                    }

                    e.Graphics.FillRectangle(backBrush, e.Item.Bounds);
                }
            }
            else
            {
                textColor = ForeColor;
                if (!SystemInformation.HighContrast)
                {
                    var c = Core.Utilities.ColorUtils.BlendColors(ForeColor, 1, BackColor, 7);
                    ControlPaint.DrawBorder(e.Graphics, e.Item.Bounds, c, ButtonBorderStyle.Solid);
                }
            }
            if (e.Item.Image != null)
            {
                e.Graphics.DrawImage(e.Item.Image, e.Item.ImageBounds);
            }

            if (!string.IsNullOrEmpty(e.Item.Text))
            {
                TextRenderer.DrawText(e.Graphics, e.Item.Text, Font, e.Item.Bounds, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.Bottom);
            }
        }

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            base.ScaleControl(factor, specified);

            //Scale spacing. Ideally, we should only perform this according the screen DPI, 
            //not the font size. To revisit when we use .net 4.7.
            if (specified == BoundsSpecified.Size)
            {
                _itemSpacing *= (int)Math.Round((factor.Width + factor.Height) / 2);
            }

            //Scale item size
            var scaledItemSize = new SizeF();

            if (specified.HasFlag(BoundsSpecified.Width))
            {
                scaledItemSize.Width = ItemSize.Width * factor.Width;
            }

            if (specified.HasFlag(BoundsSpecified.Height))
            {
                scaledItemSize.Height = ItemSize.Height * factor.Height;
            }

            ItemSize = Size.Round(scaledItemSize);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (IconListViewItem i in Items)
                {
                    if (i != null)
                    {
                        i.Dispose();
                    }
                }
            }

            base.Dispose(disposing);
        }

        public void ApplySkin(BaseSkin skin)
        {
            UseExplorerStyle = skin.ListViewUseExplorerStyle;
            BorderStyle = skin.ListViewBorderStyle;
            BackColor = skin.ListViewBackColor;
            ForeColor = skin.ListViewForeColor;
            BorderColor = skin.ListViewBorderColor;
            BorderFocusedColor = skin.ListViewBorderFocusedColor;
            BorderDisabledColor = skin.ListViewBorderDisabledColor;
        }
    }
}
