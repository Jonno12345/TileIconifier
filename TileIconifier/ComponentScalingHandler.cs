using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileIconifier
{
    /// <summary>
    ///     Provides methods to scale components that are not entirely scaled by Winform out of the box.
    /// </summary>
    internal class ComponentScalingHandler
    {
        private readonly SizeF _factor;
        private readonly BoundsSpecified _specified;

        public ComponentScalingHandler(SizeF factor, BoundsSpecified specified)
        {
            _factor = factor;
            _specified = specified;
        }

        private Size ScaleSize(Size size)
        {
            var sizeF = (SizeF)size;

            if (_specified.HasFlag(BoundsSpecified.Width))
            {
                sizeF.Width *= _factor.Width;
            }
            if (_specified.HasFlag(BoundsSpecified.Height))
            {
                sizeF.Height *= _factor.Height;
            }

            return Size.Round(sizeF);
        }

        public void Scale(ImageList imageList)
        {
            //If the handle has been created, we can't just change the ImageSize 
            //property because doing so clears all the images in the collection! 
            //Therefore, it should be treated as a special case. However, I am not
            //to sure how we could proceed while avoiding resource leak, so for now,
            //we just don't perform scaling in that scenario. That's is really no
            //big deal for now, since the app is not per-monitor aware, so scaling
            //is usually performed very early, before the form is even shown. In
            //the worst case scenario, the images will simply be too small!
            if (!imageList.HandleCreated)
            {
                imageList.ImageSize = ScaleSize(imageList.ImageSize);
            }            
        }
    }
}
