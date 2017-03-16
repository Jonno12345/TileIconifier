using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TileIconifier.Skinning.Skins;

namespace TileIconifier.Skinning
{
    public class ToolStripSystemRendererEx : ToolStripSystemRenderer
    {
        //myRenderer8        

        private const string stSTANDARD_VS_CLASS = "Menu";

        private string vsClass = stSTANDARD_VS_CLASS;
        private VisualStyleRenderer vsRenderer;
        private ToolStripSystemColorTable colorTable;        

        #region "Constructors"
        public ToolStripSystemRendererEx()
        {
            colorTable = new ToolStripSystemColorTable();
        }

        public ToolStripSystemRendererEx(ToolStripSystemColorScheme pScheme)
        {
            colorTable = new ToolStripSystemColorTable(pScheme);
        }
        #endregion

        private void PrepareVisualStyleRenderer(VisualStyleElement pElement)
        {
            if (vsRenderer == null)
            {
                vsRenderer = new VisualStyleRenderer(pElement);
            }
            else
            {
                vsRenderer.SetParameters(pElement);
            }
        }        

        #region "State helpers"
        /// <summary>
        /// Returns the Visual Style Element of a popup menu item with the appropriate state.
        /// </summary>
        /// <param name="ItemSelected"></param>
        /// <param name="ItemEnabled"></param>
        /// <returns></returns>
        private VisualStyleElement GetPopUpItemVSElement(bool ItemSelected, bool ItemEnabled)
        {
            int inState = 0;

            if (ItemSelected)
            {
                if (ItemEnabled)
                {
                    //Hot
                    inState = 2;
                }
                else
                {
                    //DisabledHot
                    inState = 4;
                }
            }
            else
            {
                if (ItemEnabled)
                {
                    //Normal
                    inState = 1;
                }
                else
                {
                    //Disabled
                    inState = 3;
                }
            }

            return VisualStyleElement.CreateElement(vsClass, 14, inState);
        }

        /// <summary>
        /// Vertically centers the text rectangle of a ToolstripMenuItem, which is otherwise aligned at the top.
        /// </summary>
        /// <param name="pPevent"></param>
        private void CenterItemTextRectangle(ref ToolStripItemTextRenderEventArgs pPevent)
        {
            Rectangle itemRect = new Rectangle(Point.Empty, pPevent.Item.Size);
            Rectangle textRectNew = pPevent.TextRectangle;

            textRectNew.Y = (itemRect.Height - textRectNew.Height) / 2;

            pPevent.TextRectangle = textRectNew;
        }

        private VisualStyleElement GetMenuBarItemVSElement(bool pItemSelected, bool pItemEnabled, bool pItemPushed)
        {
            int inState;

            if (pItemPushed)
            {
                if (pItemEnabled)
                {
                    //Pushed
                    inState = 3;
                }
                else
                {
                    //DisabledPushed
                    inState = 6;
                }
            }
            else if (pItemSelected)
            {
                if (pItemEnabled)
                {
                    //Hot
                    inState = 2;
                }
                else
                {
                    //DisabledHot
                    inState = 5;
                }
            }
            else
            {
                if (pItemEnabled)
                {
                    //Normal
                    inState = 1;
                }
                else
                {
                    //Disabled
                    inState = 4;
                }
            }

            return VisualStyleElement.CreateElement(vsClass, 8, inState);
        }

        private VisualStyleElement GetSubMenuArrowVSElement(bool pEnabled)
        {
            if (pEnabled)
            {
                return VisualStyleElement.CreateElement(vsClass, 16, 1);
            }
            else
            {
                return VisualStyleElement.CreateElement(vsClass, 16, 2);
            }
        }
        #endregion

        #region "Drawing helpers"
        private void DrawVisualStyle(Graphics pGrapics, VisualStyleElement pElement, Rectangle pBounds)
        {
            PrepareVisualStyleRenderer(pElement);
            vsRenderer.DrawBackground(pGrapics, pBounds);
        }

        private void FillRectangle(Graphics pGraphics, Color pBackColor, Rectangle pBounds)
        {
            using (SolidBrush b = new SolidBrush(pBackColor))
                pGraphics.FillRectangle(b, pBounds);
        }

        private void DrawRectangle(Graphics pGraphics, Color pColor, Rectangle pBounds)
        {
            //Compensation needed by GDI+
            pBounds.Width--;
            pBounds.Height--;

            using (Pen p = new Pen(pColor))
                pGraphics.DrawRectangle(p, pBounds);
        }

        private void DrawClassicSeparatorInternal(Graphics pGraphics, Color pSepColor, Rectangle pRect) //pRect.Size = size of the item.
        {
            //Spacing on each side of the separator.
            const int inLATTERAL_PADDING = 10;

            int inY = (pRect.Height / 2) - 1;
            Point sepBegins = new Point(inLATTERAL_PADDING, inY);
            Point sepEnds = new Point(pRect.Width - inLATTERAL_PADDING, inY);
            
            using (Pen p = new Pen(pSepColor))
                pGraphics.DrawLine(p, sepBegins, sepEnds);
        }
        #endregion

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        { 
            if (e.ToolStrip.IsDropDown) //Popup menu
            {
                //If the user color does not match the default color, it means that the user
                //has requested a specific color, so we draw a flat menu with it regardless of
                //system settings.
                Color sysColor = colorTable.PopupBackColor;
                if (sysColor != ToolStripSystemColorScheme.DefaultPopupBackColor)
                {
                    FillRectangle(e.Graphics, sysColor, e.AffectedBounds);
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
            else if (e.ToolStrip.GetType() == typeof(MenuStrip)) //Menu bar
            {
                Color sysColor = colorTable.MenuBarBackColor;
                if (sysColor != ToolStripSystemColorScheme.DefaultMenuBarBackColor)
                {
                    FillRectangle(e.Graphics, sysColor, e.AffectedBounds);
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
            Color sysColor;

            if (e.ToolStrip.IsDropDown)
            {
                //Popup menu

                sysColor = colorTable.PopupBorderColor;

                if (sysColor != ToolStripSystemColorScheme.DefaultPopupBorderColor)
                {
                    DrawRectangle(e.Graphics, sysColor, bounds);
                }
                else
                {
                    base.OnRenderToolStripBorder(e);
                }
            }
            else
            {
                //Menu bar

                sysColor = colorTable.MenuBarBorderColor;
                if (sysColor != ToolStripSystemColorScheme.DefaultMenuBarBorderColor)
                {
                    //Draws a border with the user-defined color. In this case, we just draw a plain line
                    //the the sake of simplicity.
                    Point borderBegins = new Point(e.AffectedBounds.X, e.AffectedBounds.Y + e.AffectedBounds.Height - 1);
                    Point borderEnds = new Point(e.AffectedBounds.X + e.AffectedBounds.Width, borderBegins.Y);

                    using (Pen p = new Pen(sysColor))
                        e.Graphics.DrawLine(p, borderBegins, borderEnds);
                }
                //We check the MenuBarBackColor, because if the user color does not match the default color,
                //the menu bar background is classic, so we draw a classic border, even then the border
                //color was not user-defined.
                else if (colorTable.MenuBarBackColor != ToolStripSystemColorScheme.DefaultMenuBarBackColor || !ToolStripManager.VisualStylesEnabled)
                {
                    //We let the base class handle the border drawing with the system colors. We only do this if 
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
                Color sysColor;
                Color defaultSysColor;

                if (item.IsOnDropDown)
                {
                    //Popup menu        

                    // VSWhidbey 518568: scoot in by 2 pixels when selected
                    fillRect.X += 2;
                    //its already 1 away from the right edge
                    fillRect.Width -= 3;

                    if (e.Item.Selected)
                    {
                        sysColor = colorTable.HighlightBackColor;
                        defaultSysColor = ToolStripSystemColorScheme.DefaultHighlightBackColor;
                    }
                    else
                    {
                        sysColor = colorTable.PopupBackColor;
                        defaultSysColor = ToolStripSystemColorScheme.DefaultPopupBackColor;
                    }

                    if (sysColor != defaultSysColor)
                    {
                        FillRectangle(e.Graphics, sysColor, fillRect);
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
                else
                {
                    //Menu bar item
                    
                    if (e.Item.Selected)
                    {
                        sysColor = colorTable.HighlightBackColor;
                        defaultSysColor = ToolStripSystemColorScheme.DefaultHighlightBackColor;
                    }
                    else
                    {
                        sysColor = colorTable.MenuBarBackColor;
                        defaultSysColor = ToolStripSystemColorScheme.DefaultMenuBarBackColor;
                    }

                    if (sysColor != defaultSysColor)
                    {
                        FillRectangle(e.Graphics, sysColor, fillRect);
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
            if ((e.ToolStrip.GetType() != typeof(MenuStrip) && !e.Item.IsOnDropDown) || e.TextDirection != ToolStripTextDirection.Horizontal)
            {
                base.OnRenderItemText(e);
                return;
            }

            //When the text is on a popup menu, It is vertically alligned at the top,
            //which is a problem if the item is tall, so we center the text rectangle manually.
            if (e.Item.IsOnDropDown)
                CenterItemTextRectangle(ref e);           
            
            VisualStyleElement vsElement;
            Color sysColor;
            Color defaultColor;
            Color textColor;

            if (e.Item.IsOnDropDown) //Popup menu item
            {
                
                if (e.Item.Selected)
                {
                    sysColor = colorTable.HighlightForeColor;
                    defaultColor = ToolStripSystemColorScheme.DefaultHighlightForeColor;
                }
                else
                {
                    sysColor = colorTable.PopupForeColor;
                    defaultColor = ToolStripSystemColorScheme.DefaultPopupForeColor;
                }

                if (sysColor != defaultColor)
                {
                    textColor = sysColor;
                }
                else if (ToolStripManager.VisualStylesEnabled)
                {
                    vsElement = GetPopUpItemVSElement(e.Item.Selected, e.Item.Enabled);
                    if (VisualStyleRenderer.IsElementDefined(vsElement))
                    {
                        PrepareVisualStyleRenderer(vsElement);
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
                if (e.Item.Selected)
                {
                    sysColor = colorTable.HighlightForeColor;
                    defaultColor = ToolStripSystemColorScheme.DefaultHighlightForeColor;
                }
                else
                {
                    sysColor = colorTable.MenuBarForeColor;
                    defaultColor = ToolStripSystemColorScheme.DefaultMenuBarForeColor;
                }

                if (sysColor != defaultColor)
                {
                    textColor = sysColor;
                }
                else if (ToolStripManager.VisualStylesEnabled)
                {
                    vsElement = GetMenuBarItemVSElement(e.Item.Selected, e.Item.Enabled, e.Item.Pressed);
                    if (VisualStyleRenderer.IsElementDefined(vsElement))
                    {
                        PrepareVisualStyleRenderer(vsElement);
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
            Color sysColor;
            Color defaultColor;            

            if (e.Item.Selected)
            {
                sysColor = colorTable.HighlightForeColor;
                defaultColor = ToolStripSystemColorScheme.DefaultHighlightForeColor;
            }
            else
            {
                sysColor = colorTable.HighlightForeColor;
                defaultColor = ToolStripSystemColorScheme.DefaultHighlightForeColor;
            }

            if (sysColor != defaultColor)
            {
                ControlPaint.DrawMenuGlyph(e.Graphics, e.ArrowRectangle, MenuGlyph.Arrow, sysColor, Color.Transparent);                
            }
            else if (ToolStripManager.VisualStylesEnabled)
            {
                VisualStyleElement vsElement = GetSubMenuArrowVSElement(e.Item.Enabled);
                if (VisualStyleRenderer.IsElementDefined(vsElement))
                {
                    //Creates a the appropriate rectangle for the arrow (the one that comes in the ToolStripArrowRenderEventArgs is too big!)
                    Rectangle arrowRect = new Rectangle(e.ArrowRectangle.Location, vsRenderer.GetPartSize(e.Graphics, ThemeSizeType.True));
                    //Centers the rectangle vertically
                    arrowRect.Y = e.ArrowRectangle.Y + (e.ArrowRectangle.Height - arrowRect.Height) / 2 + 1; //+1 is just a quick qualitative adjustement.
                    DrawVisualStyle(e.Graphics, vsElement, arrowRect);
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

            if (colorTable.PopupBackColor != ToolStripSystemColorScheme.DefaultPopupBackColor || !ToolStripManager.VisualStylesEnabled)
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

        //Rushed at this point...

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Rectangle bounds;
            Color sepCol = Core.Utilities.ColorUtils.BlendColors(colorTable.PopupForeColor, 1, colorTable.PopupBackColor, 3);

            if (colorTable.PopupForeColor != ToolStripSystemColorScheme.DefaultPopupForeColor || !ToolStripManager.VisualStylesEnabled)
            {
                bounds = new Rectangle(Point.Empty, e.Item.Size);                
                DrawClassicSeparatorInternal(e.Graphics, sepCol, bounds);
            }
            else
            {
                VisualStyleElement vsElement = VisualStyleElement.CreateElement(vsClass, 15, 0);
                if (VisualStyleRenderer.IsElementDefined(vsElement))
                {
                    int inPartHeight = vsRenderer.GetPartSize(e.Graphics, ThemeSizeType.Minimum).Height + 1;
                    int inY = (e.Item.Height - inPartHeight) / 2; //Vertical center
                    bounds = new Rectangle(0, inY, e.Item.Width, inPartHeight); //here, the rect is full width, and we shrink it when we check for RightToLeft.
                    ToolStripDropDownMenu dropDownMenu = (ToolStripDropDownMenu)e.Item.GetCurrentParent();

                    if (dropDownMenu != null)
                    {
                        if (dropDownMenu.RightToLeft == RightToLeft.No)
                        {
                            bounds.X += dropDownMenu.ImageScalingSize.Width + 2;
                            bounds.Width = dropDownMenu.Width - bounds.X;
                        }
                        else
                        {
                            bounds.X -= 2;
                            bounds.Width = dropDownMenu.Width - bounds.X - dropDownMenu.ImageScalingSize.Width;
                        }
                    }

                    DrawVisualStyle(e.Graphics, vsElement, bounds);
                }
                else
                {
                    base.OnRenderSeparator(e);
                }
            }
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            //base.OnRenderItemCheck(e);
            Rectangle bounds = new Rectangle(e.ImageRectangle.Location, e.ImageRectangle.Size);
            ControlPaint.DrawMenuGlyph(e.Graphics, bounds, MenuGlyph.Checkmark, colorTable.PopupForeColor, Color.Transparent);
        }
    }
}