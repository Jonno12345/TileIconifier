using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;
using TileIconifier.Forms;

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
            if(!IsAdministrator())
            {
                MessageBox.Show("You must run this application as an Administrator!", "TileIconifier - Run as Administrator", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            } else if(!IsAboveWindows10586())
            {
                MessageBox.Show("You are not on or above Build 10586! The program will run, but changes may not take effect.", "TileIconifier - Build Too Early", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static bool IsAboveWindows10586()
        {
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            int buildNo = int.Parse(reg.GetValue("CurrentBuildNumber").ToString());
            return buildNo >= 10586;
        }
    }

}
