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
            var owner = menuItem?.Owner as ContextMenuStrip;
            return owner?.SourceControl;
        }
    }
}
