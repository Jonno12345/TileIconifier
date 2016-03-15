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
    public partial class frmUnhandledException : Form
    {
        private Exception _ex;
        public frmUnhandledException(Exception ex)
        {
            InitializeComponent();
            _ex = ex;
        }

        private void rtxtUnhandledException_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            var p = Process.Start(e.LinkText);
        }

        private void frmUnhandledException_Load(object sender, EventArgs e)
        {
            rtxtUnhandledException.Text = rtxtUnhandledException.Text.Replace("[@@EXCEPTIONSTACKTRACE@@]", _ex.ToString());
        }

        public static void ShowExceptionHandler(Exception ex)
        {
            using (var unhandedException = new frmUnhandledException(ex))
            {
                unhandedException.StartPosition = FormStartPosition.CenterScreen;
                unhandedException.ShowDialog();
            }
        }

        private void rtxtUnhandledException_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {   
                ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
                var menuItem = new MenuItem("Copy Information For Github Issue");
                menuItem.Click += new EventHandler((o,ev) => Clipboard.SetData(DataFormats.Text, _ex.ToString()));
                contextMenu.MenuItems.Add(menuItem);

                rtxtUnhandledException.ContextMenu = contextMenu;
            }
        }
            }
}
