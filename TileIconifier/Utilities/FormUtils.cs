using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileIconifier.Utilities
{
    class FormUtils
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
