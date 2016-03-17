using System.Diagnostics;
using System.Windows.Forms;

namespace TileIconifier.Forms
{
    public partial class FrmHelp : Form
    {
        public FrmHelp()
        {
            InitializeComponent();
        }

        private void rtxtAbout_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            using (Process.Start(e.LinkText))
            {
            }
        }
    }
}