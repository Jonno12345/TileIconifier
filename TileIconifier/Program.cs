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
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using TileIconifier.Core;
using TileIconifier.Core.Utilities;
using TileIconifier.Forms.Main;
using TileIconifier.Forms.Shared;
using TileIconifier.Localization;
using TileIconifier.Properties;
using TileIconifier.Skinning;
using TileIconifier.Skinning.Skins;
using TileIconifier.Utilities;

namespace TileIconifier
{
    internal static class Program
    {
        private static bool _doNotExit = true;
        private static FrmMain _fm;

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            SetUpLanguageFromConfig();
            ApplySkinFromConfig();

            try
            {
                if (!SystemUtils.IsAdministrator())
                {
                    MessageBox.Show(Strings.RunAsAdminFull,
                        @"TileIconifier - " + Strings.RunAsAdmin, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            catch (UnableToDetectAdministratorException)
            {
                MessageBox.Show(
                    Strings.VerifyAdminFull,
                    @"TileIconifier - " + Strings.VerifyAdmin, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            VerifyOs();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);

            while (_doNotExit)
            {
                _doNotExit = false;
                _fm = new FrmMain();
                _fm.LanguageChangedEvent += main_LanguageChangedEvent;
                Application.Run(_fm);
            }
        }

        private static void ApplySkinFromConfig()
        {
            var skin = Config.Instance.LastSkin;

            SkinHandler.SetCurrentSkin(ContainerUtils.SkinFromString(skin));
        }

        private static void SetUpLanguageFromConfig()
        {
            try
            {
                main_LanguageChangedEvent(null, new LocalizationEventArgs(Config.Instance.LocaleToUse));
            }
            catch
            {
                //ignore
            }
        }

        private static void VerifyOs()
        {
            //Test the machine is Windows 10 >= build 10586 or Windows 8.1
            var ver = Environment.OSVersion.Version;
            if (ver.Major < 6 || (ver.Major == 6 && ver.Minor < 3))
                MessageBox.Show(Strings.WindowsBefore81,
                    @"TileIconifier - " + Strings.WindowsTooEarly, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if (ver.Major == 10 && ver.Build < 10586)
                MessageBox.Show(
                    Strings.WrongBuildWindows10,
                    @"TileIconifier - " + Strings.BuildTooEarly, MessageBoxButtons.OK, MessageBoxIcon.Stop);


            ////Warning for Windows 8.1, but I think it's actually fine with all builds... Disabled it
            //else if (ver.Major == 6 && ver.Minor == 3)
            //    MessageBox.Show(
            //        @"You are running Windows 8.1, not all functionality has been confirmed working. Please report if an issue occurs, but be aware that not all functionality may be supported.",
            //        @"TileIconifier - Windows 8.1", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            //Test the process is running 32 bit on 32 bit, or 64 bit on 64 bit
            if (!Environment.Is64BitProcess && Environment.Is64BitOperatingSystem)
                MessageBox.Show(
                    Strings.IncorrectPlatformFull,
                    @"TileIconifier - " + Strings.IncorrectPlatform, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            FrmException.ShowExceptionHandler(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            FrmException.ShowExceptionHandler(e.ExceptionObject as Exception);
        }

        private static void main_LanguageChangedEvent(object sender, LocalizationEventArgs eventArgs)
        {
            var newCulture = eventArgs.Culture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(newCulture);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(newCulture);
            _doNotExit = true;
            _fm?.Close();
        }
    }
}