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
        private delegate void drawClassicElement(Graphics pGraphics, Color pColor, Rectangle pRect);

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

        #region "Misc helpers"
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
        private void DrawVisualStyle(Graphics pGraphics, Rectangle pRect, VisualStyleElement pVSElement, Color pFallbackColor, ref drawClassicElement pFallbackMethod)
        {
            if (VisualStyleRenderer.IsElementDefined(pVSElement))
            {
                PrepareVisualStyleRenderer(pVSElement);
                vsRenderer.DrawBackground(pGraphics, pRect);
            }
            else
                pFallbackMethod(pGraphics, pFallbackColor, pRect);
        }        

        private void DrawClassicMenuBarBackInternal(Graphics pGraphics, Color pBackColor, Rectangle pRect)
        {
            using (SolidBrush b = new SolidBrush(pBackColor))
                pGraphics.FillRectangle(b, pRect);
        }   

        private void DrawClassicPopupMenuBackInternal(Graphics pGraphics, Color pBackColor, Rectangle pRect)
        {
            using (SolidBrush b = new SolidBrush(pBackColor))
                pGraphics.FillRectangle(b, pRect);
        }
        
        private void DrawBorderInternal(ToolStripRenderEventArgs e)
        {
            Rectangle bounds = e.AffectedBounds;
            
            if (e.ToolStrip.IsDropDown)
            {
                //Popup menu
                if (colorTable.PopupBorderColor != ToolStripSystemColorScheme.DefaultPopupBorderColor || SystemInformation.IsFlatMenuEnabled)
                {
                    bounds.Width -= 1;
                    bounds.Height -= 1;                    
                    using (Pen p = new Pen(colorTable.PopupBorderColor))
                        e.Graphics.DrawRectangle(p, bounds);
                }
                else
                {
                    ControlPaint.DrawBorder3D(e.Graphics, bounds, Border3DStyle.Raised);
                }
            }
            else
            {
                //Menu bar
                if (colorTable.MenuBarBorderColor != ToolStripSystemColorScheme.DefaultMenuBarBorderColor)
                {
                    //Draws a border with the suer-defined color. In this case, we just draw a plain line
                    //the the sake of simplicity.
                    Point borderBegins = new Point(e.AffectedBounds.X, e.AffectedBounds.Y + e.AffectedBounds.Height - 1);
                    Point borderEnds = new Point(e.AffectedBounds.X + e.AffectedBounds.Width, borderBegins.Y);
                                        
                    using (Pen p = new Pen(colorTable.MenuBarBorderColor))
                        e.Graphics.DrawLine(p, borderBegins, borderEnds);                        
                }
                else if (colorTable.MenuBarBackColor != ToolStripSystemColorScheme.DefaultMenuBarBackColor || !ToolStripManager.VisualStylesEnabled)
                {
                    //We let the base class handle the border drawing with the system colors. We only do this if 
                    //the menu bar background is not painted with visual styles, because the menu bar background 
                    //visual style image that is painted elsewhere already includes the border.
                    base.OnRenderToolStripBorder(e);
                }                    
            }
        }        

        private void DrawClassicMenuBarItemBackInternal(Graphics pGraphics, Color pBackColor, Rectangle pRect)
        {
            using (SolidBrush b = new SolidBrush(pBackColor))
            {
                pGraphics.FillRectangle(b, pRect);
            }
        }
        
        private void DrawClassicPopupMenuItemBackInternal(Graphics pGraphics, Color pBackColor, Rectangle pRect)
        {
            using (SolidBrush b = new SolidBrush(pBackColor))
            {
                pGraphics.FillRectangle(b, pRect);
            }
        }

        private void DrawClassicPopupMenuArrowInternal(Graphics pGraphics, Color pGlyphColor, Rectangle pRect)
        {
            ControlPaint.DrawMenuGlyph(pGraphics, pRect, MenuGlyph.Arrow, pGlyphColor, Color.Transparent);
        }

        private void DrawClassicSeparatorInternal(Graphics pGraphics, Color pSepColor, Rectangle pRect) //pRect.Size = size of the item.
        {
            //Spacing on each side of the separator.
            const int inLATTERAL_PADDING = 10;

            int inY = (pRect.Height / 2) - 1;
            Point sepBegins = new Point(inLATTERAL_PADDING, inY);
            Point sepEnds = new Point(pRect.Width - inLATTERAL_PADDING, inY);
            
            using (Pen p = new Pen(colorTable.PopupForeColor))
                pGraphics.DrawLine(p, sepBegins, sepEnds);
        }
        #endregion

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip.GetType() != typeof(MenuStrip) && !e.ToolStrip.IsDropDown)
            {
                base.OnRenderToolStripBackground(e);
                return;
            }            
            
            if (e.ToolStrip.IsDropDown) //Popup menu
            {
                Color sysColor = colorTable.PopupBackColor;
                if (sysColor != ToolStripSystemColorScheme.DefaultPopupBackColor || !ToolStripManager.VisualStylesEnabled)
                    DrawClassicPopupMenuBackInternal(e.Graphics, sysColor, e.AffectedBounds);
                else
                {
                    VisualStyleElement vsElement = VisualStyleElement.CreateElement(vsClass, 9, 0);
                    drawClassicElement fallBackHandler = DrawClassicPopupMenuBackInternal;
                    DrawVisualStyle(e.Graphics, e.AffectedBounds, vsElement, sysColor, ref fallBackHandler);
                }       
            }
            else //Menu bar
            {
                Color sysColor = colorTable.MenuBarBackColor;
                if (sysColor != ToolStripSystemColorScheme.DefaultMenuBarBackColor || !ToolStripManager.VisualStylesEnabled)
                    DrawClassicMenuBarBackInternal(e.Graphics, sysColor, e.AffectedBounds);
                else
                {
                    VisualStyleElement vsElement = VisualStyleElement.CreateElement(vsClass, 7, 0);
                    drawClassicElement fallbackHandler = DrawClassicMenuBarBackInternal;
                    DrawVisualStyle(e.Graphics, e.AffectedBounds, vsElement, sysColor, ref fallbackHandler);
                }
                    
            }            
        }
        
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            DrawBorderInternal(e);
        }  

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            ToolStripMenuItem item = e.Item as ToolStripMenuItem;
            Graphics g = e.Graphics;  

            if (item != null)
            {                
                Rectangle fillRect = new Rectangle(Point.Empty, item.Size);
                if (item.IsOnDropDown)
                {
                    //Popup menu
                    
                    Color sysColor;
                    Color defaultSysColor;                    

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


                    if (sysColor != defaultSysColor || !ToolStripManager.VisualStylesEnabled)
                        DrawClassicPopupMenuItemBackInternal(e.Graphics, sysColor, fillRect);
                    else
                    {
                        VisualStyleElement vsElement = GetPopUpItemVSElement(e.Item.Selected, e.Item.Enabled);
                        drawClassicElement fallbackHandler = DrawClassicPopupMenuBackInternal;
                        DrawVisualStyle(e.Graphics, fillRect, vsElement, sysColor, ref fallbackHandler);
                    }            
                }
                else
                {
                    //Menu bar item

                    Color sysColor;
                    Color defaultSysColor;
                    
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

                    if (sysColor != defaultSysColor || !ToolStripManager.VisualStylesEnabled)
                        DrawClassicMenuBarItemBackInternal(e.Graphics, sysColor, fillRect);
                    else
                    {
                        VisualStyleElement vsElement = GetMenuBarItemVSElement(e.Item.Selected, e.Item.Enabled, e.Item.Pressed);
                        drawClassicElement fallbackHandler = DrawClassicMenuBarItemBackInternal;
                        DrawVisualStyle(e.Graphics, fillRect, vsElement, sysColor, ref fallbackHandler);
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
            Color textColor;

            //Here, we don't use the DrawVisualStyle method like the others because we render text
            //using the TextRenderer wether or not visual style are used.

            if (e.Item.IsOnDropDown) //Popup menu item
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
                    sysColor = colorTable.PopupForeColor;
                    defaultColor = ToolStripSystemColorScheme.DefaultPopupForeColor;
                }

                if (sysColor != defaultColor || !ToolStripManager.VisualStylesEnabled)
                {
                    textColor = sysColor;
                }
                else
                {
                    vsElement = GetPopUpItemVSElement(e.Item.Selected, e.Item.Enabled);
                    if (VisualStyleRenderer.IsElementDefined(vsElement))
                    {
                        PrepareVisualStyleRenderer(vsElement);
                        textColor = vsRenderer.GetColor(ColorProperty.TextColor);
                    }
                    else
                    {
                        textColor = sysColor;
                    }
                }
                
            }
            else //Menu bar item
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
                    sysColor = colorTable.MenuBarForeColor;
                    defaultColor = ToolStripSystemColorScheme.DefaultMenuBarForeColor;
                }

                if (sysColor != defaultColor || !ToolStripManager.VisualStylesEnabled)
                {
                    textColor = sysColor;
                }
                else
                {
                    vsElement = GetMenuBarItemVSElement(e.Item.Selected, e.Item.Enabled, e.Item.Pressed);
                    if (VisualStyleRenderer.IsElementDefined(vsElement))
                    {
                        PrepareVisualStyleRenderer(vsElement);
                        textColor = vsRenderer.GetColor(ColorProperty.TextColor);
                    }
                    else
                    {
                        textColor = sysColor;
                    }
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

            if (sysColor != defaultColor || !ToolStripManager.VisualStylesEnabled)
            {
                DrawClassicPopupMenuArrowInternal(e.Graphics, sysColor, e.ArrowRectangle);
            }
            else
            {
                VisualStyleElement vsElement = GetSubMenuArrowVSElement(e.Item.Enabled);
                //Creates a the appropriate rectangle for the arrow (the one that comes in the ToolStripArrowRenderEventArgs is too big!)
                Rectangle arrowRect = new Rectangle(e.ArrowRectangle.Location, vsRenderer.GetPartSize(e.Graphics, ThemeSizeType.True));
                //Centers the rectangle vertically
                arrowRect.Y = e.ArrowRectangle.Y + (e.ArrowRectangle.Height - arrowRect.Height) / 2 + 1; //+1 is just a quick qualitative adjustement.
                drawClassicElement fallbackHandler = DrawClassicPopupMenuArrowInternal;
                DrawVisualStyle(e.Graphics, arrowRect, vsElement, sysColor, ref fallbackHandler);
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
            
            PrepareVisualStyleRenderer(vsElement);            
            vsRenderer.DrawBackground(e.Graphics, e.AffectedBounds);
        }

        //Rushed at this point...

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Rectangle bounds;

            if (colorTable.PopupForeColor != ToolStripSystemColorScheme.DefaultPopupForeColor || !ToolStripManager.VisualStylesEnabled)
            {
                bounds = new Rectangle(Point.Empty, e.Item.Size);
                DrawClassicSeparatorInternal(e.Graphics, colorTable.PopupForeColor, bounds);
            }
            else
            {
                VisualStyleElement vsElement = VisualStyleElement.CreateElement(vsClass, 15, 0);
                int inPartHeight = vsRenderer.GetPartSize(e.Graphics, ThemeSizeType.Minimum).Height + 1;
                int inY = (e.Item.Height - inPartHeight) / 2; //Vertical center
                bounds = new Rectangle(0, inY, e.Item.Width, inPartHeight); //here, the rect is full width, and we shrink it below.
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

                drawClassicElement fallbackHandler = DrawClassicSeparatorInternal;
                DrawVisualStyle(e.Graphics, bounds, vsElement, colorTable.PopupForeColor, ref fallbackHandler); //Fallback does not work correctly with this rect.
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