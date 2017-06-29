using System.Windows.Forms;

namespace TileIconifier.Utilities
{
    static class ControlUtils
    {
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
