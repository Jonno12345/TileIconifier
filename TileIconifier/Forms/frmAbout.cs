using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using TileIconifier.Utilities;

namespace TileIconifier.Forms
{
    public partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();
        }

        private void rtxtAbout_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            using (Process.Start(e.LinkText))
            {
            }
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            rtxtAbout.Text = rtxtAbout.Text.Replace("[@@CURVER@@]", UpdateUtils.CurrentVersion);
        }
    }
}