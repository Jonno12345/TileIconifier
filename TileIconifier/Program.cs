#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2016 Johnathon M
// 
//         Permission is hereby granted, free of charge, to any person obtaining a copy
//         of this software and associated documentation files (the "Software"), to deal
//         in the Software without restriction, including without limitation the rights
//         to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//         copies of the Software, and to permit persons to whom the Software is
//         furnished to do so, subject to the following conditions:
// 
//         The above copyright notice and this permission notice shall be included in
//         all copies or substantial portions of the Software.
// 
//         THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//         IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//         FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//         AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//         LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//         OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//         THE SOFTWARE.
// 
// */

#endregion

using System;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using TileIconifier.Forms;
using TileIconifier.Forms.Shared;

namespace TileIconifier
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                if (!IsAdministrator())
                {
                    MessageBox.Show(@"You must run this application as an Administrator!",
                        @"TileIconifier - Run as Administrator", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            catch (UnableToDetectAdministratorException)
            {
                MessageBox.Show(
                    @"An error occurred detecting administrator state. Please verify you are running as an administrator, or issues will occur!",
                    @"TileIconifier - Verify Administrator", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            VerifyOs();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            Application.Run(new FrmMain());
        }

        private static void VerifyOs()
        {
            //Test the machine is Windows 10 >= build 10586 or Windows 8.1
            var ver = Environment.OSVersion.Version;
            if (ver.Major < 6 || (ver.Major == 6 && ver.Minor < 3))
                MessageBox.Show(@"You are on a version of Windows earlier than 8.1, changes might not take any effect.",
                    @"TileIconifier - Windows version too early", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if (ver.Major == 10 && ver.Build < 10586)
                MessageBox.Show(
                    @"You are running Windows 10, but not on or above Build 10586! The program will run, but some changes may not take effect.",
                    @"TileIconifier - Build Too Early", MessageBoxButtons.OK, MessageBoxIcon.Stop);


            ////Warning for Windows 8.1, but I think it's actually fine with all builds... Disabled it
            //else if (ver.Major == 6 && ver.Minor == 3)
            //    MessageBox.Show(
            //        @"You are running Windows 8.1, not all functionality has been confirmed working. Please report if an issue occurs, but be aware that not all functionality may be supported.",
            //        @"TileIconifier - Windows 8.1", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            //Test the process is running 32 bit on 32 bit, or 64 bit on 64 bit
            if (!Environment.Is64BitProcess && Environment.Is64BitOperatingSystem)
                MessageBox.Show(
                    @"You are running as a 32-bit process within a 64-bit operating system. Certain functionality, such as powershell, may not work correctly.",
                    @"TileIconifier - Incorrect platform", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            FrmException.ShowExceptionHandler(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            FrmException.ShowExceptionHandler(e.ExceptionObject as Exception);
        }

        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            if (identity != null)
            {
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            throw new UnableToDetectAdministratorException();
        }
    }
}