using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;
using TileIconifier.Forms;
using System.Threading;

namespace TileIconifier
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!IsAdministrator())
            {
                MessageBox.Show("You must run this application as an Administrator!", "TileIconifier - Run as Administrator", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            VerifyOS();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            Application.Run(new frmMain());
        }

        private static void VerifyOS()
        {
            var ver = Environment.OSVersion.Version;
            if(ver.Major < 6 || (ver.Major == 6 && ver.Minor < 3))
                MessageBox.Show("You are on a version of Windows earlier than 8.1, changes might not take any effect.", "TileIconifier - Windows version too early", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if(ver.Major == 10 && ver.Build < 10586)
                MessageBox.Show("You are running Windows 10, but not on or above Build 10586! The program will run, but some changes may not take effect.", "TileIconifier - Build Too Early", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if(ver.Major == 6 && ver.Minor == 3)
                MessageBox.Show("You are running Windows 8.1, not all functionality has been confirmed working. Please report if an issue occurs, but be aware that not all functionality may be supported.", "TileIconifier - Windows 8.1", MessageBoxButtons.OK, MessageBoxIcon.Stop);

        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            frmUnhandledException.ShowExceptionHandler(e.Exception);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            frmUnhandledException.ShowExceptionHandler(e.ExceptionObject as Exception);
        }


        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }


    }

}
