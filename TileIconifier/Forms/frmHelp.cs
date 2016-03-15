using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileIconifier.Forms
{
    public partial class frmHelp : Form
    {
        public frmHelp()
        {
            InitializeComponent();
        }

        private void rtxtAbout_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            using (var p = Process.Start(e.LinkText)) { }
        }
    }
}
