using System;
using System.Reflection;
using System.Windows.Forms;
using TileIconifier.Controls;
using TileIconifier.Skinning;
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

        internal static BaseSkin SkinFromString(string skinString)
        {
            //attempt to load the type from the Skins assembly
            var type = Type.GetType("TileIconifier.Skinning.Skins." + skinString);
            if (type == null)
            {
                //unable to determine skin, pass the default
                return SkinHandler.DefaultSkin;
            }

            //pass the determined skin or the default on failure
            try
            {
                return (BaseSkin) Activator.CreateInstance(type);
            }
            catch
            {
                return SkinHandler.DefaultSkin;
            }
        }

        /// <summary>
        /// Returns the control from which the ToolStrip originated.
        /// </summary>
        /// <param name="sender">ToolStripItem that was clicked.</param>        
        public static Control GetToolStripSourceControl(object sender)
        {
            var menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                var owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    return owner.SourceControl;
                }
            }
            return null;
        }
    }
}
