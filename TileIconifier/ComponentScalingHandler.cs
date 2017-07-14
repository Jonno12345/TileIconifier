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
            if (!imageList.HandleCreated)
            {
                imageList.ImageSize = ScaleSize(imageList.ImageSize);
            }
            else
            {
                //If the handle has been created, we can't just change the ImageSize 
                //property because doing so clears all the images in the collection! 
                //Therefore, we clone each images and add them back after scaling the 
                //image list. This is far from ideal and images seem to progresively
                //become more blurry each time scaling is performed, but I guess there
                //is not much that we can do...
                var imgCopies = new Image[imageList.Images.Count];
                var imgKeys = new string[imageList.Images.Count];
                try
                {
                    //Backup the images & keys
                    for (var i = 0; i < imageList.Images.Count; i++)
                    {
                        imgCopies[i] = (Image)imageList.Images[i].Clone();
                        imgKeys[i] = imageList.Images.Keys[i];
                    }

                    //Perform scaling. Clearing the images is already done internally by 
                    //the image list component, but we do it here explicitely.
                    imageList.Images.Clear();
                    imageList.ImageSize = ScaleSize(imageList.ImageSize);

                    //Add the images back
                    for (var i = 0; i < imgCopies.Length; i++)
                    {
                        imageList.Images.Add(imgKeys[i], imgCopies[i]);
                    }
                    
                    //Ensure the handle was re-created at this point, which ensures 
                    //that the component has created its copies of the images, which
                    //allows us to dispose our copies immediately.
                    var ignored = imageList.Handle;
                }
                finally
                {
                    foreach (Image img in imgCopies)
                    {
                        if (img != null)
                        {
                            img.Dispose();
                        }                        
                    }
                }
            }
        }
    }
}
