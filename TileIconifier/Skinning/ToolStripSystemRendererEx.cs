using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TileIconifier.Skinning.Skins;

namespace TileIconifier.Skinning
{
    public class ToolStripSystemRendererEx : ToolStripSystemRenderer
    {
        //myRenderer8        

        private const string STANDARD_VS_CLASS = "Menu";

        private readonly string _vsClass = STANDARD_VS_CLASS;        
        //Global instance of the visual style renderer. Don't use this variable in drawing code!
        //To simply draw a visual style, use the DrawVisualStyle method. To get a local reference
        //of the visual style renderer, use the GetVisualStyleRenderer method.
        private VisualStyleRenderer _visualStyleRenderer;
        private readonly ToolStripSystemColorTable _colorTable;        

        #region "Constructors"
        public ToolStripSystemRendererEx()
        {
            _colorTable = new ToolStripSystemColorTable(SkinHandler.DefaultSkin);
        }

        public ToolStripSystemRendererEx(BaseSkin skin)
        {
            _colorTable = new ToolStripSystemColorTable(skin);
        }
        #endregion

        private VisualStyleRenderer GetVisualStyleRenderer(VisualStyleElement element)
        {
            if (_visualStyleRenderer == null)
            {
                _visualStyleRenderer = new VisualStyleRenderer(element);
            }
            else
            {
                _visualStyleRenderer.SetParameters(element);
            }
            return _visualStyleRenderer;
        }

        /// <summary>
        /// Vertically centers the text rectangle of a ToolstripMenuItem. Fixes a bug 
        /// with ToolStripMenuItems where the text is not vertically centered if the item
        /// is taller than the default size.
        /// </summary>
        /// <param name="pevent"></param>
        private void CenterItemTextRectangle(ref ToolStripItemTextRenderEventArgs pevent)
        {
            Rectangle itemRect = new Rectangle(Point.Empty, pevent.Item.Size);
            Rectangle textRectNew = pevent.TextRectangle;

            textRectNew.Y = (itemRect.Height - textRectNew.Height) / 2;

            pevent.TextRectangle = textRectNew;
        }

        #region "State helpers"
        /// <summary>
        /// Returns the Visual Style Element of a popup menu item with the appropriate state.
        /// </summary>
        /// <param name="selected"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        private VisualStyleElement GetPopUpItemVsElement(bool selected, bool enabled)
        {
            int state;

            if (selected)
            {
                state = enabled ? 2 : 4;
            }
            else
            {
                state = enabled ? 1 : 3;
            }

            return VisualStyleElement.CreateElement(_vsClass, 14, state);
        }        

        private VisualStyleElement GetMenuBarItemVsElement(bool selected, bool enabled, bool pushed)
        {
            int state;

            if (pushed)
            {
                state = enabled ? 3 : 6;
            }
            else if (selected)
            {
                state = enabled ? 2 : 5;
            }
            else
            {
                state = enabled ? 1 : 4;
            }

            return VisualStyleElement.CreateElement(_vsClass, 8, state);
        }

        private VisualStyleElement GetMenuGlyphVsElement(MenuGlyph glyph, bool enabled)
        {
            switch(glyph)
            {
                case MenuGlyph.Arrow:
                    return VisualStyleElement.CreateElement(_vsClass, 16, enabled ? 1 : 2);

                case MenuGlyph.Bullet:
                    return VisualStyleElement.CreateElement(_vsClass, 11, enabled ? 3 : 4);

                case MenuGlyph.Checkmark:
                    return VisualStyleElement.CreateElement(_vsClass, 11, enabled ? 1 : 2);

                default:
                    throw new ArgumentException("Supported value.", nameof(glyph));
            }            
        }
       
        /// <param name="bitmap">Set to true if the associated foreground image is not a standard glyph. (I think)</param>        
        private VisualStyleElement GetMenuGlyphBackgroundVsElement(bool enabled, bool bitmap)
        {
            return !enabled ? 
                VisualStyleElement.CreateElement(_vsClass, 12, 1) : 
                VisualStyleElement.CreateElement(_vsClass, 12, bitmap ? 3 : 2);
        }

        #endregion

        #region "Drawing helpers"
        private void DrawVisualStyle(Graphics graphics, VisualStyleElement element, Rectangle bounds)
        {
            VisualStyleRenderer vsRenderer = GetVisualStyleRenderer(element);
            vsRenderer.DrawBackground(graphics, bounds);
        }

        private void FillRectangle(Graphics graphics, Color backColor, Rectangle bounds)
        {
            using (var b = new SolidBrush(backColor))
                graphics.FillRectangle(b, bounds);
        }

        private void DrawRectangle(Graphics graphics, Color color, Rectangle bounds)
        {
            //Compensation needed by GDI+
            bounds.Width--;
            bounds.Height--;

            using (var p = new Pen(color))
                graphics.DrawRectangle(p, bounds);
        }

        /// <param name="itemBounds">Rectangle of the *item* (in its full size) where the separator is contained.</param>
        private void DrawClassicSeparatorInternal(Graphics graphics, Color sepColor, Rectangle itemBounds)
        {
            //Spacing on each side of the separator.
            const int latteralPadding = 10;

            int y = (itemBounds.Height / 2) - 1;
            Point sepBegins = new Point(latteralPadding, y);
            Point sepEnds = new Point(itemBounds.Width - latteralPadding, y);
            
            using (var p = new Pen(sepColor))
                graphics.DrawLine(p, sepBegins, sepEnds);
        }
        #endregion

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        { 
            if (e.ToolStrip.IsDropDown) //Popup menu
            {
                //If the table color does not match the default color, it means that the user
                //has requested a specific color, so we draw a flat menu with it regardless of
                //system settings.
                Color tableColor = _colorTable.PopupBackColor;
                if (tableColor != ToolStripSystemColorTable.DefaultPopupBackColor)
                {
                    FillRectangle(e.Graphics, tableColor, e.AffectedBounds);
                }
                //If no user color set, we draw using system settings. The ToolStripSystemRenderer
                //was never updated to support visual styles, so we must implement them ourselves,
                //if they are enabled.
                else if (ToolStripManager.VisualStylesEnabled)
                {
                    VisualStyleElement vsElement = VisualStyleElement.CreateElement(_vsClass, 9, 0);
                    if (VisualStyleRenderer.IsElementDefined(vsElement))
                    {
                        DrawVisualStyle(e.Graphics, vsElement, e.AffectedBounds);
                    }
                    else
                    {
                        //If the visual style element is not defined, we fallback on the classic 
                        //menu, which the base class already implements.
                        base.OnRenderToolStripBackground(e);
                    }
                }
                else
                {
                    base.OnRenderToolStripBackground(e);
                }       
            }
            else if (e.ToolStrip is MenuStrip) //Menu bar
            {
                Color tableColor = _colorTable.MenuBarBackColor;
                if (tableColor != ToolStripSystemColorTable.DefaultMenuBarBackColor)
                {
                    FillRectangle(e.Graphics, tableColor, e.AffectedBounds);
                }
                else if (ToolStripManager.VisualStylesEnabled)
                {
                    VisualStyleElement vsElement = VisualStyleElement.CreateElement(_vsClass, 7, 0);
                    if (VisualStyleRenderer.IsElementDefined(vsElement))
                    {
                        DrawVisualStyle(e.Graphics, vsElement, e.AffectedBounds);
                    }
                    else
                    {
                        base.OnRenderToolStripBackground(e);
                    }
                }
                else
                {
                    base.OnRenderToolStripBackground(e);
                }                    
            }
            else
            {
                base.OnRenderToolStripBackground(e);
            }            
        }
        
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            Rectangle bounds = e.AffectedBounds;
            Color tableColor;

            if (e.ToolStrip.IsDropDown) //Popup menu
            {    
                tableColor = _colorTable.PopupBorderColor;

                if (tableColor != ToolStripSystemColorTable.DefaultPopupBorderColor)
                {
                    DrawRectangle(e.Graphics, tableColor, bounds);
                }
                else
                {
                    base.OnRenderToolStripBorder(e);
                }
            }
            else //Menu bar
            {
                tableColor = _colorTable.MenuBarBorderColor;
                if (tableColor != ToolStripSystemColorTable.DefaultMenuBarBorderColor)
                {
                    //Draws a border with the user-defined color. In this case, we just draw a plain line
                    //for the the sake of simplicity.
                    Point borderBegins = new Point(e.AffectedBounds.X, e.AffectedBounds.Y + e.AffectedBounds.Height - 1);
                    Point borderEnds = new Point(e.AffectedBounds.X + e.AffectedBounds.Width, borderBegins.Y);

                    using (var p = new Pen(tableColor))
                        e.Graphics.DrawLine(p, borderBegins, borderEnds);
                }
                //We check the MenuBarBackColor, because if the user color does not match the default color,
                //the menu bar background is classic, so we need to draw a classic border, even though the border
                //color was not user-defined.
                else if (_colorTable.MenuBarBackColor != ToolStripSystemColorTable.DefaultMenuBarBackColor || !ToolStripManager.VisualStylesEnabled)
                {
                    //We let the base class handle the border drawing with system colors. We only do this if 
                    //the menu bar background is not painted with visual styles, because the menu bar background 
                    //visual style image that is painted elsewhere already includes the border.
                    base.OnRenderToolStripBorder(e);
                }
            }
        }  

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            ToolStripMenuItem item = e.Item as ToolStripMenuItem;
            Graphics g = e.Graphics;  

            if (item != null)
            {                
                Rectangle fillRect = new Rectangle(Point.Empty, item.Size);
                Color tableColor;
                Color defaultColor;

                if (item.IsOnDropDown) //Popup menu  
                {    
                    // VSWhidbey 518568: scoot in by 2 pixels when selected
                    fillRect.X += 2;
                    //its already 1 away from the right edge
                    fillRect.Width -= 3;

                    //Determines the user color and the default color that needs to be compared.
                    //We really need to check if the item is enabled, because the Selected property 
                    //of ToolStrip menu items returns true whenever the mouse is over, even if 
                    //the item is disabled.
                    if (e.Item.Selected && e.Item.Enabled)
                    {
                        tableColor = _colorTable.HighlightBackColor;
                        defaultColor = ToolStripSystemColorTable.DefaultHighlightBackColor;
                    }
                    else
                    {
                        tableColor = _colorTable.PopupBackColor;
                        defaultColor = ToolStripSystemColorTable.DefaultPopupBackColor;
                    }

                    //Compares the user color and the default color and draw accordignly.
                    if (tableColor != defaultColor)
                    {
                        FillRectangle(e.Graphics, tableColor, fillRect);
                    }
                    else if (ToolStripManager.VisualStylesEnabled)
                    {
                        VisualStyleElement vsElement = GetPopUpItemVsElement(item.Selected, item.Enabled);
                        if (VisualStyleRenderer.IsElementDefined(vsElement))
                        {
                            DrawVisualStyle(g, vsElement, fillRect);
                        }
                        else
                        {
                            base.OnRenderMenuItemBackground(e);
                        }
                    }
                    else
                    {
                        base.OnRenderMenuItemBackground(e);
                    }            
                }
                else //Menu bar item
                {  
                    if (e.Item.Selected && e.Item.Enabled)
                    {
                        tableColor = _colorTable.HighlightBackColor;
                        defaultColor = ToolStripSystemColorTable.DefaultHighlightBackColor;
                    }
                    else
                    {
                        tableColor = _colorTable.MenuBarBackColor;
                        defaultColor = ToolStripSystemColorTable.DefaultMenuBarBackColor;
                    }

                    if (tableColor != defaultColor)
                    {
                        FillRectangle(e.Graphics, tableColor, fillRect);
                    }
                    else if (ToolStripManager.VisualStylesEnabled)
                    {
                        VisualStyleElement vsElement = GetMenuBarItemVsElement(e.Item.Selected, e.Item.Enabled, e.Item.Pressed);
                        if (VisualStyleRenderer.IsElementDefined(vsElement))
                        {
                            DrawVisualStyle(g, vsElement, fillRect);
                        }
                        else
                        {
                            base.OnRenderMenuItemBackground(e);
                        }
                    }
                    else
                    {
                        base.OnRenderMenuItemBackground(e);
                    }                    
                }
            }
        }                

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if ((!(e.ToolStrip is MenuStrip) && !e.Item.IsOnDropDown) || e.TextDirection != ToolStripTextDirection.Horizontal)
            {
                base.OnRenderItemText(e);
                return;
            }

            //When the text is on a popup menu, It is vertically alligned at the top,
            //which is a problem if the item is tall, so we center the text rectangle manually.
            if (e.Item.IsOnDropDown)
                CenterItemTextRectangle(ref e);           
            
            VisualStyleElement vsElement;
            Color tableColor;
            Color defaultColor;
            Color textColor;

            if (e.Item.IsOnDropDown) //Popup menu item
            {
                if (e.Item.Enabled)
                {
                    if (e.Item.Selected)
                    {
                        tableColor = _colorTable.HighlightForeColor;
                        defaultColor = ToolStripSystemColorTable.DefaultHighlightForeColor;
                    }
                    else
                    {
                        tableColor = _colorTable.PopupForeColor;
                        defaultColor = ToolStripSystemColorTable.DefaultPopupForeColor;
                    }
                }
                else
                {
                    tableColor = _colorTable.DisabledForeColor;
                    defaultColor = ToolStripSystemColorTable.DefaultDisabledForeColor;
                }

                if (tableColor != defaultColor)
                {
                    textColor = tableColor;
                }
                else if (ToolStripManager.VisualStylesEnabled)
                {
                    vsElement = GetPopUpItemVsElement(e.Item.Selected, e.Item.Enabled);
                    if (VisualStyleRenderer.IsElementDefined(vsElement))
                    {
                        VisualStyleRenderer vsRenderer = GetVisualStyleRenderer(vsElement);
                        textColor = vsRenderer.GetColor(ColorProperty.TextColor);
                    }
                    else
                    {
                        base.OnRenderItemText(e);
                        return;
                    }
                }
                else
                {
                    base.OnRenderItemText(e);
                    return;
                }                
            }
            else //Menu bar item
            {   
                if (e.Item.Enabled)
                {
                    if (e.Item.Selected)
                    {
                        tableColor = _colorTable.HighlightForeColor;
                        defaultColor = ToolStripSystemColorTable.DefaultHighlightForeColor;
                    }
                    else
                    {
                        tableColor = _colorTable.MenuBarForeColor;
                        defaultColor = ToolStripSystemColorTable.DefaultMenuBarForeColor;
                    }
                }
                else
                {
                    tableColor = _colorTable.DisabledForeColor;
                    defaultColor = ToolStripSystemColorTable.DefaultDisabledForeColor;
                }

                if (tableColor != defaultColor)
                {
                    textColor = tableColor;
                }
                else if (ToolStripManager.VisualStylesEnabled)
                {
                    vsElement = GetMenuBarItemVsElement(e.Item.Selected, e.Item.Enabled, e.Item.Pressed);
                    if (VisualStyleRenderer.IsElementDefined(vsElement))
                    {
                        VisualStyleRenderer vsRenderer = GetVisualStyleRenderer(vsElement);
                        textColor = vsRenderer.GetColor(ColorProperty.TextColor);
                    }
                    else
                    {
                        base.OnRenderItemText(e);
                        return;
                    }
                }  
                else
                {
                    base.OnRenderItemText(e);
                    return;
                }              
            }
            
            TextRenderer.DrawText(e.Graphics, e.Text, e.Item.Font, e.TextRectangle, textColor, e.TextFormat);
        }        

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            Color tableColor;
            Color defaultColor;            

            if (e.Item.Enabled)
            {
                if (e.Item.Selected)
                {
                    tableColor = _colorTable.HighlightForeColor;
                    defaultColor = ToolStripSystemColorTable.DefaultHighlightForeColor;
                }
                else
                {
                    tableColor = _colorTable.HighlightForeColor;
                    defaultColor = ToolStripSystemColorTable.DefaultHighlightForeColor;
                }
            }
            else
            {
                tableColor = _colorTable.DisabledForeColor;
                defaultColor = ToolStripSystemColorTable.DefaultDisabledForeColor;
            }

            if (tableColor != defaultColor)
            {
                ControlPaint.DrawMenuGlyph(e.Graphics, e.ArrowRectangle, MenuGlyph.Arrow, tableColor, Color.Transparent);                
            }
            else if (ToolStripManager.VisualStylesEnabled)
            {
                VisualStyleElement vsElement = GetMenuGlyphVsElement(MenuGlyph.Arrow, e.Item.Enabled);
                if (VisualStyleRenderer.IsElementDefined(vsElement))
                {
                    VisualStyleRenderer vsRenderer = GetVisualStyleRenderer(vsElement);
                    //Creates the appropriate rectangle for the arrow (e.ArrowRectangle is too big!)
                    Rectangle arrowRect = new Rectangle(e.ArrowRectangle.Location, vsRenderer.GetPartSize(e.Graphics, ThemeSizeType.True));
                    //Centers the rectangle vertically
                    arrowRect.Y = e.ArrowRectangle.Y + (e.ArrowRectangle.Height - arrowRect.Height) / 2 + 1; //+1 is just a quick empirical adjustement.
                    vsRenderer.DrawBackground(e.Graphics, arrowRect);
                }
                else
                {
                    base.OnRenderArrow(e);
                }                
            }
            else
            {
                base.OnRenderArrow(e);
            }
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            //We only need to draw the gutter background for the visually styled menu item.

            if (_colorTable.PopupBackColor != ToolStripSystemColorTable.DefaultPopupBackColor || !ToolStripManager.VisualStylesEnabled)
            {
                base.OnRenderImageMargin(e);
                return;
            }

            VisualStyleElement vsElement = VisualStyleElement.CreateElement(_vsClass, 13, 0);
            if (!VisualStyleRenderer.IsElementDefined(vsElement))
            {
                base.OnRenderImageMargin(e);
                return;
            }

            DrawVisualStyle(e.Graphics, vsElement, e.AffectedBounds);
        }
        
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Rectangle bounds;            

            if (_colorTable.PopupForeColor != ToolStripSystemColorTable.DefaultPopupForeColor)
            {
                bounds = new Rectangle(Point.Empty, e.Item.Size);
                //We should probably use the PopupBorderColor for the separator, but in the specific
                //context of the dark skin, that color is not contrasty enough, so to keep things simple,
                //we just blend the fore color and the back color.
                DrawClassicSeparatorInternal(
                    e.Graphics,
                    Core.Utilities.ColorUtils.BlendColors(_colorTable.PopupForeColor, 1, _colorTable.PopupBackColor, 3),
                    bounds);
            }
            else if (ToolStripManager.VisualStylesEnabled)
            {
                VisualStyleElement vsElement = VisualStyleElement.CreateElement(_vsClass, 15, 0);
                if (VisualStyleRenderer.IsElementDefined(vsElement))
                {
                    VisualStyleRenderer vsRenderer = GetVisualStyleRenderer(vsElement);
                    int partHeight = vsRenderer.GetPartSize(e.Graphics, ThemeSizeType.True).Height;
                    int y = (e.Item.Height - partHeight) / 2; //Vertical center
                    bounds = new Rectangle(0, y, e.Item.Width, partHeight); //here, the rect is full width, and we shrink it when we check for RightToLeft.
                    ToolStripDropDownMenu dropDownMenu = e.Item.GetCurrentParent() as ToolStripDropDownMenu;

                    if (dropDownMenu != null)
                    {
                        if (dropDownMenu.RightToLeft == RightToLeft.No)
                        {
                            bounds.X += dropDownMenu.ImageScalingSize.Width + 6;
                            bounds.Width = dropDownMenu.Width - bounds.X;
                        }
                        else
                        {
                            bounds.X -= 6;
                            bounds.Width = dropDownMenu.Width - bounds.X - dropDownMenu.ImageScalingSize.Width;
                        }
                    }

                    vsRenderer.DrawBackground(e.Graphics, bounds);
                }
                else
                {
                    base.OnRenderSeparator(e);
                }
            }
            else
            {
                base.OnRenderSeparator(e);
            }
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            Rectangle glyphBounds = e.ImageRectangle;
            Color tableColor;
            Color defaultColor;

            if (e.Item.Enabled)
            {
                if (e.Item.Selected)
                {
                    tableColor = _colorTable.HighlightForeColor;
                    defaultColor = ToolStripSystemColorTable.DefaultHighlightForeColor;
                }
                else
                {
                    tableColor = _colorTable.HighlightForeColor;
                    defaultColor = ToolStripSystemColorTable.DefaultHighlightForeColor;
                }
            }
            else
            {
                tableColor = _colorTable.DisabledForeColor;
                defaultColor = ToolStripSystemColorTable.DefaultDisabledForeColor;
            }

            if (tableColor != defaultColor)
            {
                ControlPaint.DrawMenuGlyph(e.Graphics, glyphBounds, MenuGlyph.Checkmark, tableColor, Color.Transparent);
            }
            else if (ToolStripManager.VisualStylesEnabled)
            {
                VisualStyleElement backVsElement = GetMenuGlyphBackgroundVsElement(e.Item.Enabled, false);
                VisualStyleElement glyphVsElement = GetMenuGlyphVsElement(MenuGlyph.Checkmark, e.Item.Enabled);
                if (VisualStyleRenderer.IsElementDefined(backVsElement) && VisualStyleRenderer.IsElementDefined(glyphVsElement))
                {
                    //We use Inflate in order to keep the rectangle centered with the glyph.
                    int inflation = ((e.Item.Height - e.ImageRectangle.Height) / 2);
                    Rectangle backBounds = glyphBounds;
                    backBounds.Inflate(inflation, inflation);
                    DrawVisualStyle(e.Graphics, backVsElement, backBounds);
                    DrawVisualStyle(e.Graphics, glyphVsElement, glyphBounds);
                }
                else
                {
                    base.OnRenderItemCheck(e);
                }
            }
            else
            {
                base.OnRenderItemCheck(e);
            }
        }
    }
}