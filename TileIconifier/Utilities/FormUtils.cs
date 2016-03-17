using System.Windows.Forms;

namespace TileIconifier.Utilities
{
    internal class FormUtils
    {
        public static void ShowCenteredDialogForm<T>(IWin32Window sender) where T : Form, new()
        {
            using (var oneShotForm = new T())
            {
                oneShotForm.StartPosition = FormStartPosition.CenterScreen;
                oneShotForm.ShowDialog(sender);
            }
        }
    }
}