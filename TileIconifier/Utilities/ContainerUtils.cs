using System.Windows.Forms;
using TileIconifier.Controls;
using TileIconifier.Skinning.Skins;

namespace TileIconifier.Utilities
{
    internal static class ContainerUtils
    {    
        /// <summary>
        ///     Applies the specified skin on the specified control and all of its childs.
        /// </summary>        
        internal static void ApplySkinToControl(BaseSkin skin, Control control)
        {
            var skinnableControl = control as ISkinnableControl;
            if (skinnableControl != null)
            {
                skinnableControl.ApplySkin(skin);
                return;
            }
            
            //Recursive loop that applies the skin to controls inside controls. At this
            //point, the control is not a handled skinnable control so it is likely to be just
            //a container that contains more controls.            
            foreach (Control c in control.Controls)
            {
                ApplySkinToControl(skin, c);
            }
        }
    }
}
