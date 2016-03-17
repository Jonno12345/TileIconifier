using System;
using System.Diagnostics;
using System.Windows.Forms;
using TileIconifier.Custom;

namespace TileIconifier.Forms.CustomShortcutForms
{
    public partial class FrmCustomShortcutManagerHelp : Form
    {
        public FrmCustomShortcutManagerHelp()
        {
            InitializeComponent();
        }

        private void rtxtHelp_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            using (Process.Start(e.LinkText))
            {
            }
        }

        private void frmHelp_Load(object sender, EventArgs e)
        {
            rtxtHelp.Text = rtxtHelp.Text.Replace("[@@PROGRAMOUTPUTPATH@@]", CustomShortcutGetters.CustomShortcutVbsPath);
            rtxtHelp.Text = rtxtHelp.Text.Replace("[@@PROGRAMCURRENTUSERSHORTCUTPATH@@]",
                CustomShortcutGetters.CustomShortcutCurrentUserPath);
            rtxtHelp.Text = rtxtHelp.Text.Replace("[@@PROGRAMALLUSERSHORTCUTPATH@@]",
                CustomShortcutGetters.CustomShortcutAllUsersPath);
        }
    }
}