using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TileIconifier.Utilities
{
    class LayoutAndPaintUtils
    {
        /// <summary>
        ///     Scale a point using the specified scaling factor.
        /// </summary>
        /// <param name="pt"><see cref="Point"/> to scale</param>
        /// <param name="scale"><see cref="SizeF"/> specifiying the scaling factor for each axis of the point.</param>
        public static void ScalePoint(ref Point pt, SizeF scale)
        {
            var ptF = (PointF)pt;

            ptF.X *= scale.Width;
            ptF.Y *= scale.Height;

            pt = Point.Round(ptF);
        }

        /// <summary>
        /// Scales the specified point by the specified factor.
        /// </summary>        
        public static void ScalePoint(ref Point point, int factor)
        {
            point.X *= factor;
            point.Y *= factor;
        }

        /// <summary>
        /// Returns a <see cref="Point"/> located at the horizontal and 
        /// vertical center of the specified rectangle.
        /// </summary>        
        public static Point GetRectangleCenter(Rectangle rect)
        {
            var center = rect.Location;
            center.X += rect.Width / 2;
            center.Y += rect.Height / 2;

            return center;
        }

        /// <summary>
        ///     Returns a <see cref="Rectangle"/> whose size was increased or decreased from the specified 
        ///     <see cref="Rectangle"/> based on the specified <see cref="Padding"/>.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="pad"></param>
        /// <returns></returns>
        public static Rectangle InflateRectangle(Rectangle rect, Padding pad)
        {
            Rectangle r = Rectangle.FromLTRB(
                    rect.Left + pad.Left,
                    rect.Top + pad.Top,
                    rect.Right - pad.Right,
                    rect.Bottom - pad.Bottom);
            return r;
        }

        /// <summary>
        ///     Returns a <see cref="TextFormatFlags"/> value equivalent to the specified <see cref="ContentAlignment"/>.
        /// </summary>
        /// <param name="contentAlign"></param>        
        public static TextFormatFlags ConvertToTextFormatFlags(ContentAlignment contentAlign)
        {
            TextFormatFlags flags = new TextFormatFlags();

            //Top
            if (contentAlign == ContentAlignment.TopLeft || contentAlign == ContentAlignment.TopCenter || contentAlign == ContentAlignment.TopRight)
                flags |= TextFormatFlags.Top;

            //Middle
            if (contentAlign == ContentAlignment.MiddleLeft || contentAlign == ContentAlignment.MiddleCenter || contentAlign == ContentAlignment.MiddleRight)
                flags |= TextFormatFlags.VerticalCenter;

            //Bottom
            if (contentAlign == ContentAlignment.BottomLeft || contentAlign == ContentAlignment.BottomCenter || contentAlign == ContentAlignment.BottomRight)
                flags |= TextFormatFlags.Bottom;

            //Left
            if (contentAlign == ContentAlignment.BottomLeft || contentAlign == ContentAlignment.MiddleLeft || contentAlign == ContentAlignment.TopLeft)
                flags |= TextFormatFlags.Left;

            //Center
            if (contentAlign == ContentAlignment.BottomCenter || contentAlign == ContentAlignment.MiddleCenter || contentAlign == ContentAlignment.TopCenter)
                flags |= TextFormatFlags.HorizontalCenter;

            //Right
            if (contentAlign == ContentAlignment.BottomRight || contentAlign == ContentAlignment.MiddleRight || contentAlign == ContentAlignment.TopRight)
                flags |= TextFormatFlags.Right;

            return flags;
        }

        /// <summary>
        ///     Returns a <see cref="TextFormatFlags"/> value equivalent to the specified <see cref="HorizontalAlignment"/>.
        /// </summary>
        public static TextFormatFlags ConvertToTextFormatFlags(HorizontalAlignment horiAlign)
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

        /// <summary>
        ///     Invalidate the region of the specified control defined by the specified Rectangles.
        /// </summary>
        /// <param name="control">Control to invalidate.</param>
        /// <param name="rects">Parts of the control to invalidate.</param>
        /// <remarks>Empty rectangles are ignored.</remarks>
        public static void InvalidateRectangles(Control control, params Rectangle[] rects)
        {
            if (control == null)
            {
                throw new ArgumentNullException(nameof(control));
            }
            if (rects == null)
            {
                throw new ArgumentNullException(nameof(rects));
            }

            //Calling Invalidate with an empty rectangle causes the whole control to be 
            //invalidated, which is obviously not what we want to do here, so we really 
            //need to ensure that at least one rectangle is not empty. (Btw, this does
            //not apply to Invalidate(Region). In that case, an empty region causes the
            //control not to be invalidated at all.)
            if (rects.Length < 1 || Array.TrueForAll(rects, r => r.IsEmpty))
            {
                return;
            }

            if (rects.Length == 1)
            {
                control.Invalidate(rects[0]);
            }
            else
            {
                using (var reg = new Region(rects[0]))
                {
                    for (var i = 1; i < rects.Length; i++)
                    {
                        reg.Union(rects[i]);
                    }
                    control.Invalidate(reg);
                }
            }
        }

        /// <summary>
        ///     Returns the client rectangle of the specified control with a location relative
        ///     to the control's location.
        /// </summary>       
        public static Rectangle GetAbsoluteClientRectangle(Control control)
        {
            //Get the whole control rect relative to the screen.
            var nonClientToScreen = new NativeMethods.RECT();
            NativeMethods.GetWindowRect(control.Handle, ref nonClientToScreen);
            //Get the client rectangle relative to the screen.
            var clientToScreen = control.RectangleToScreen(control.ClientRectangle);

            var clientRect = new Rectangle();
            clientRect.X = clientToScreen.Left - nonClientToScreen.left;
            clientRect.Y = clientToScreen.Top - nonClientToScreen.top;
            clientRect.Size = clientToScreen.Size;

            return clientRect;
        }

        //Adapted from https://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/ToolStripTextBox.cs
        /// <summary>
        ///     Invalidates the entire non-client area of the specified control.
        /// </summary>        
        public static void InvalidateNonClient(Control control)
        {
            //Very important check. Not only it avoids useless processing, but otherwise, 
            //the Handle property getter may force the creation of the handle, which can 
            //cause problems if it happens too early (e.g. The background color of the 
            //RichTextBox is ignored).
            if (!control.IsHandleCreated)
            {
                return;
            }

            var absoluteClientRectangle = GetAbsoluteClientRectangle(control);
            var clientRect = new NativeMethods.RECT(control.ClientRectangle);
            var hNonClientRegion = IntPtr.Zero;
            var hClientRegion = IntPtr.Zero;
            var hTotalRegion = IntPtr.Zero;

            try
            {
                // get the total client area, then exclude the client by using XOR

                //Note that even with the RDW_FRAME flag, RedrawWindow takes a region 
                //relative to the client area. Therefore, the top left corner of the 
                //non client area possibly has negative coordonates.
                hTotalRegion = NativeMethods.CreateRectRgn(-absoluteClientRectangle.X, -absoluteClientRectangle.Y, control.Width, control.Height);
                hClientRegion = NativeMethods.CreateRectRgn(
                    clientRect.left,
                    clientRect.top,
                    clientRect.right,
                    clientRect.bottom);
                hNonClientRegion = NativeMethods.CreateRectRgn(0, 0, 0, 0);

                NativeMethods.CombineRgn(hNonClientRegion, hTotalRegion, hClientRegion, NativeMethods.RGN_XOR);

                // Call RedrawWindow with the region.
                NativeMethods.RECT ignored = new NativeMethods.RECT();
                NativeMethods.RedrawWindow(control.Handle, ref ignored, hNonClientRegion,
                                               NativeMethods.RDW_INVALIDATE | //NativeMethods.RDW_ERASE |
                                               //NativeMethods.RDW_UPDATENOW | NativeMethods.RDW_ERASENOW |
                                               NativeMethods.RDW_FRAME);
            }
            finally
            {
                // clean up our regions.
                try
                {
                    if (hNonClientRegion != IntPtr.Zero)
                    {
                        NativeMethods.DeleteObject(hNonClientRegion);
                    }
                }
                finally
                {
                    try
                    {
                        if (hClientRegion != IntPtr.Zero)
                        {
                            NativeMethods.DeleteObject(hClientRegion);
                        }
                    }
                    finally
                    {
                        if (hTotalRegion != IntPtr.Zero)
                        {
                            NativeMethods.DeleteObject(hTotalRegion);
                        }
                    }
                }

            }
        }

        /// <summary>
        ///     Returns the handle of a new region identical to the one specified by the provided handle.
        /// </summary>
        /// <param name="hRgnToCopy">Handle to the region to copy.</param>        
        internal static IntPtr CopyHRgn(IntPtr hRgnToCopy)
        {
            var hRgnCopy = NativeMethods.CreateRectRgn(0, 0, 0, 0);
            var result = NativeMethods.CombineRgn(hRgnCopy, hRgnToCopy, IntPtr.Zero, NativeMethods.RGN_COPY);

            if (result == 0)
            {
                //An error occured. Cleanup the region that we created before throwing an exception.
                NativeMethods.DeleteObject(hRgnCopy);
                throw new Win32Exception();
            }

            return hRgnCopy;
        }
    }
}
