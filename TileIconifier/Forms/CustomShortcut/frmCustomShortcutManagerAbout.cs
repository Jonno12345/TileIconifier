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
using TileIconifier.Custom;

namespace TileIconifier.Forms
{
    public partial class frmCustomShortcutManagerHelp : Form
    {
        public frmCustomShortcutManagerHelp()
        {
            InitializeComponent();
        }

        private void rtxtHelp_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            var p = Process.Start(e.LinkText);
        }

        private void frmHelp_Load(object sender, EventArgs e)
        {
            rtxtHelp.Text = rtxtHelp.Text.Replace("[@@PROGRAMOUTPUTPATH@@]", CustomShortcutConstants.CUSTOM_SHORTCUT_VBS_PATH);
            rtxtHelp.Text = rtxtHelp.Text.Replace("[@@PROGRAMCURRENTUSERSHORTCUTPATH@@]", CustomShortcutConstants.CUSTOM_SHORTCUT_CURRENT_USER_PATH);
            rtxtHelp.Text = rtxtHelp.Text.Replace("[@@PROGRAMALLUSERSHORTCUTPATH@@]", CustomShortcutConstants.CUSTOM_SHORTCUT_ALL_USERS_PATH);

        }
    }
}
