using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace TileIconifier.Forms
{
    public partial class FrmException : Form
    {
        private readonly Exception _ex;

        public FrmException(Exception ex)
        {
            InitializeComponent();
            _ex = ex;
        }

        private void rtxtException_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            using (Process.Start(e.LinkText)) { }
        }

        private void FrmUnhandledExceptionLoad(object sender, EventArgs e)
        {
            rtxtUnhandledException.Text = rtxtUnhandledException.Text.Replace("[@@EXCEPTIONSTACKTRACE@@]",
                _ex.ToString());
        }

        public static void ShowExceptionHandler(Exception ex)
        {
            using (var unhandedException = new FrmException(ex))
            {
                unhandedException.StartPosition = FormStartPosition.CenterScreen;
                unhandedException.ShowDialog();
            }
        }

        private void rtxtUnhandledException_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var contextMenu = new ContextMenu();
                var menuItem = new MenuItem("Copy Information For Github Issue");
                menuItem.Click += (o, ev) => Clipboard.SetData(DataFormats.Text, _ex.ToString());
                contextMenu.MenuItems.Add(menuItem);

                rtxtUnhandledException.ContextMenu = contextMenu;
            }
        }
    }
}