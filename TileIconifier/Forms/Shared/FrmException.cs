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
using System.Diagnostics;
using System.Windows.Forms;
using TileIconifier.Core.Utilities;

namespace TileIconifier.Forms.Shared
{
    public partial class FrmException : SkinnableForm
    {
        private readonly Exception _ex;

        public FrmException(Exception ex)
        {
            InitializeComponent();
            _ex = ex;
        }

        private string ExceptionString
            =>
                $@"TileIconifier Version: v{UpdateUtils.CurrentVersion} - {(Environment.Is64BitProcess ? @"x64" : "x86")
                    }
OS Version: {Environment.OSVersion.Version} - {
                    (Environment.Is64BitOperatingSystem ? @"x64" : "x86")}
Administrator?: {(SystemUtils.IsAdministrator() ? "Yes" : "No")}

{_ex}
";

        public static void ShowExceptionHandler(Exception ex)
        {
            using (var unhandedException = new FrmException(ex))
            {
                unhandedException.StartPosition = FormStartPosition.CenterScreen;
                unhandedException.ShowDialog();
            }
        }

        private void rtxtException_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            UrlUtils.OpenUrlInBrowser(e.LinkText);
        }

        private void FrmUnhandledExceptionLoad(object sender, EventArgs e)
        {
            rtxtUnhandledException.Text = rtxtUnhandledException.Text.Replace("[@@EXCEPTIONSTRING@@]",
                ExceptionString);
        }

        private void rtxtUnhandledException_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var contextMenu = new ContextMenu();
            var menuItem = new MenuItem("Copy Information For Github Issue");
            menuItem.Click += (o, ev) => Clipboard.SetData(DataFormats.Text, ExceptionString);
            contextMenu.MenuItems.Add(menuItem);

            rtxtUnhandledException.ContextMenu = contextMenu;
        }
    }
}