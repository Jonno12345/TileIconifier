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

        private string vsClass = STANDARD_VS_CLASS;        
        //Global instance of the visual style renderer. Don't use this variable in drawing code!
        //To simply draw a visual style, use the DrawVisualStyle method. To get a local reference
        //of the visual style renderer, use the GetVisualStyleRenderer method.
        private VisualStyleRenderer visualStyleRenderer;
        private ToolStripSystemColorTable colorTable;        

        #region "Constructors"
        public ToolStripSystemRendererEx()
        {
            colorTable = new ToolStripSystemColorTable(SkinHandler.DefaultSkin);
        }

        public ToolStripSystemRendererEx(BaseSkin skin)
        {
            colorTable = new ToolStripSystemColorTable(skin);
        }
        #endregion

        private VisualStyleRenderer GetVisualStyleRenderer(VisualStyleElement element)
        {
            if (visualStyleRenderer == null)
            {
                visualStyleRenderer = new VisualStyleRenderer(element);
            }
            else
            {
                visualStyleRenderer.SetParameters(element);
            }
            return visualStyleRenderer;
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
        private VisualStyleElement GetPopUpItemVSElement(bool selected, bool enabled)
        {
            int state;

            if (selected)
            {
                if (enabled)
                {
                    //Hot
                    state = 2;
                }
                else
                {
                    //DisabledHot
                    state = 4;
                }
            }
            else
            {
                if (enabled)
                {
                    //Normal
                    state = 1;
                }
                else
                {
                    //Disabled
                    state = 3;
                }
            }

            return VisualStyleElement.CreateElement(vsClass, 14, state);
        }        

        private VisualStyleElement GetMenuBarItemVSElement(bool selected, bool enabled, bool pushed)
        {
            int state;

            if (pushed)
            {
                if (enabled)
                {
                    //Pushed
                    state = 3;
                }
                else
                {
                    //DisabledPushed
                    state = 6;
                }
            }
            else if (selected)
            {
                if (enabled)
                {
                    //Hot
                    state = 2;
                }
                else
                {
                    //DisabledHot
                    state = 5;
                }
            }
            else
            {
                if (enabled)
                {
                    //Normal
                    state = 1;
                }
                else
                {
                    //Disabled
                    state = 4;
                }
            }

            return VisualStyleElement.CreateElement(vsClass, 8, state);
        }

        private VisualStyleElement GetMenuGlyphVSElement(MenuGlyph glyph, bool enabled)
        {
            switch(glyph)
            {
                case MenuGlyph.Arrow:
                    if (enabled)                    
                        return VisualStyleElement.CreateElement(vsClass, 16, 1);                    
                    else                    
                        return VisualStyleElement.CreateElement(vsClass, 16, 2);

                case MenuGlyph.Bullet:
                    if (enabled)
                        return VisualStyleElement.CreateElement(vsClass, 11, 3);
                    else
                        return VisualStyleElement.CreateElement(vsClass, 11, 4);

                case MenuGlyph.Checkmark:
                    if (enabled)
                        return VisualStyleElement.CreateElement(vsClass, 11, 1);
                    else
                        return VisualStyleElement.CreateElement(vsClass, 11, 2);

                default:
                    throw new ArgumentException("Supported value.", nameof(glyph));
            }            
        }
       
        /// <param name="bitmap">Set to true if the associated foreground image is not a standard glyph. (I think)</param>        
        private VisualStyleElement GetMenuGlyphBackgroundVSElement(bool enabled, bool bitmap)
        {
            if (!enabled)
                return VisualStyleElement.CreateElement(vsClass, 12, 1);
            else if (bitmap)
                return VisualStyleElement.CreateElement(vsClass, 12, 3);
            else
                return VisualStyleElement.CreateElement(vsClass, 12, 2);
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
            const int LATTERAL_PADDING = 10;

            int y = (itemBounds.Height / 2) - 1;
            Point sepBegins = new Point(LATTERAL_PADDING, y);
            Point sepEnds = new Point(itemBounds.Width - LATTERAL_PADDING, y);
            
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
                Color tableColor = colorTable.PopupBackColor;
                if (tableColor != ToolStripSystemColorTable.DefaultPopupBackColor)
                {
                    FillRectangle(e.Graphics, tableColor, e.AffectedBounds);
                }
                //If no user color set, we draw using system settings. The ToolStripSystemRenderer
                //was never updated to support visual styles, so we must implement them ourselves,
                //if they are enabled.
                else if (ToolStripManager.VisualStylesEnabled)
                {
                    VisualStyleElement vsElement = VisualStyleElement.CreateElement(vsClass, 9, 0);
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
                Color tableColor = colorTable.MenuBarBackColor;
                if (tableColor != ToolStripSystemColorTable.DefaultMenuBarBackColor)
                {
                    FillRectangle(e.Graphics, tableColor, e.AffectedBounds);
                }
                else if (ToolStripManager.VisualStylesEnabled)
                {
                    VisualStyleElement vsElement = VisualStyleElement.CreateElement(vsClass, 7, 0);
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
                tableColor = colorTable.PopupBorderColor;

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
                tableColor = colorTable.MenuBarBorderColor;
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
                else if (colorTable.MenuBarBackColor != ToolStripSystemColorTable.DefaultMenuBarBackColor || !ToolStripManager.VisualStylesEnabled)
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
                        tableColor = colorTable.HighlightBackColor;
                        defaultColor = ToolStripSystemColorTable.DefaultHighlightBackColor;
                    }
                    else
                    {
                        tableColor = colorTable.PopupBackColor;
                        defaultColor = ToolStripSystemColorTable.DefaultPopupBackColor;
                    }

                    //Compares the user color and the default color and draw accordignly.
                    if (tableColor != defaultColor)
                    {
                        FillRectangle(e.Graphics, tableColor, fillRect);
                    }
                    else if (ToolStripManager.VisualStylesEnabled)
                    {
                        VisualStyleElement vsElement = GetPopUpItemVSElement(item.Selected, item.Enabled);
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
                        tableColor = colorTable.HighlightBackColor;
                        defaultColor = ToolStripSystemColorTable.DefaultHighlightBackColor;
                    }
                    else
                    {
                        tableColor = colorTable.MenuBarBackColor;
                        defaultColor = ToolStripSystemColorTable.DefaultMenuBarBackColor;
                    }

                    if (tableColor != defaultColor)
                    {
                        FillRectangle(e.Graphics, tableColor, fillRect);
                    }
                    else if (ToolStripManager.VisualStylesEnabled)
                    {
                        VisualStyleElement vsElement = GetMenuBarItemVSElement(e.Item.Selected, e.Item.Enabled, e.Item.Pressed);
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
                        tableColor = colorTable.HighlightForeColor;
                        defaultColor = ToolStripSystemColorTable.DefaultHighlightForeColor;
                    }
                    else
                    {
                        tableColor = colorTable.PopupForeColor;
                        defaultColor = ToolStripSystemColorTable.DefaultPopupForeColor;
                    }
                }
                else
                {
                    tableColor = colorTable.DisabledForeColor;
                    defaultColor = ToolStripSystemColorTable.DefaultDisabledForeColor;
                }

                if (tableColor != defaultColor)
                {
                    textColor = tableColor;
                }
                else if (ToolStripManager.VisualStylesEnabled)
                {
                    vsElement = GetPopUpItemVSElement(e.Item.Selected, e.Item.Enabled);
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
                        tableColor = colorTable.HighlightForeColor;
                        defaultColor = ToolStripSystemColorTable.DefaultHighlightForeColor;
                    }
                    else
                    {
                        tableColor = colorTable.MenuBarForeColor;
                        defaultColor = ToolStripSystemColorTable.DefaultMenuBarForeColor;
                    }
                }
                else
                {
                    tableColor = colorTable.DisabledForeColor;
                    defaultColor = ToolStripSystemColorTable.DefaultDisabledForeColor;
                }

                if (tableColor != defaultColor)
                {
                    textColor = tableColor;
                }
                else if (ToolStripManager.VisualStylesEnabled)
                {
                    vsElement = GetMenuBarItemVSElement(e.Item.Selected, e.Item.Enabled, e.Item.Pressed);
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
                    tableColor = colorTable.HighlightForeColor;
                    defaultColor = ToolStripSystemColorTable.DefaultHighlightForeColor;
                }
                else
                {
                    tableColor = colorTable.HighlightForeColor;
                    defaultColor = ToolStripSystemColorTable.DefaultHighlightForeColor;
                }
            }
            else
            {
                tableColor = colorTable.DisabledForeColor;
                defaultColor = ToolStripSystemColorTable.DefaultDisabledForeColor;
            }

            if (tableColor != defaultColor)
            {
                ControlPaint.DrawMenuGlyph(e.Graphics, e.ArrowRectangle, MenuGlyph.Arrow, tableColor, Color.Transparent);                
            }
            else if (ToolStripManager.VisualStylesEnabled)
            {
                VisualStyleElement vsElement = GetMenuGlyphVSElement(MenuGlyph.Arrow, e.Item.Enabled);
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

            if (colorTable.PopupBackColor != ToolStripSystemColorTable.DefaultPopupBackColor || !ToolStripManager.VisualStylesEnabled)
            {
                base.OnRenderImageMargin(e);
                return;
            }

            VisualStyleElement vsElement = VisualStyleElement.CreateElement(vsClass, 13, 0);
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

            if (colorTable.PopupForeColor != ToolStripSystemColorTable.DefaultPopupForeColor)
            {
                bounds = new Rectangle(Point.Empty, e.Item.Size);
                //We should probably use the PopupBorderColor for the separator, but in the specific
                //context of the dark skin, that color is not contrasty enough, so to keep things simple,
                //we just blend the fore color and the back color.
                DrawClassicSeparatorInternal(
                    e.Graphics,
                    Core.Utilities.ColorUtils.BlendColors(colorTable.PopupForeColor, 1, colorTable.PopupBackColor, 3),
                    bounds);
            }
            else if (ToolStripManager.VisualStylesEnabled)
            {
                VisualStyleElement vsElement = VisualStyleElement.CreateElement(vsClass, 15, 0);
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
                    tableColor = colorTable.HighlightForeColor;
                    defaultColor = ToolStripSystemColorTable.DefaultHighlightForeColor;
                }
                else
                {
                    tableColor = colorTable.HighlightForeColor;
                    defaultColor = ToolStripSystemColorTable.DefaultHighlightForeColor;
                }
            }
            else
            {
                tableColor = colorTable.DisabledForeColor;
                defaultColor = ToolStripSystemColorTable.DefaultDisabledForeColor;
            }

            if (tableColor != defaultColor)
            {
                ControlPaint.DrawMenuGlyph(e.Graphics, glyphBounds, MenuGlyph.Checkmark, tableColor, Color.Transparent);
            }
            else if (ToolStripManager.VisualStylesEnabled)
            {
                VisualStyleElement backVSElement = GetMenuGlyphBackgroundVSElement(e.Item.Enabled, false);
                VisualStyleElement glyphVSElement = GetMenuGlyphVSElement(MenuGlyph.Checkmark, e.Item.Enabled);
                if (VisualStyleRenderer.IsElementDefined(backVSElement) && VisualStyleRenderer.IsElementDefined(glyphVSElement))
                {
                    //We use Inflate in order to keep the rectangle centered with the glyph.
                    int inflation = ((e.Item.Height - e.ImageRectangle.Height) / 2);
                    Rectangle backBounds = glyphBounds;
                    backBounds.Inflate(inflation, inflation);
                    DrawVisualStyle(e.Graphics, backVSElement, backBounds);
                    DrawVisualStyle(e.Graphics, glyphVSElement, glyphBounds);
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