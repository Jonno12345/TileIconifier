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

using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using TileIconifier.Forms.Shared;
using TileIconifier.Skinning;

namespace TileIconifier.Utilities
{
    internal class FormUtils
    {
        public static void ShowCenteredDialogForm<T>(IWin32Window sender) where T : Form, new()
        {
            using (var oneShotForm = new T())
            {
                oneShotForm.StartPosition = FormStartPosition.CenterScreen;
                oneShotForm.ShowDialog(sender);
            }
        }

        public static void DoBackgroundWorkWithSplash(IWin32Window sender, DoWorkEventHandler workToDo,
            string splashText, bool singleThreadApartment = false)
        {
            var loadingSplash = new FrmLoadingSplash {StartPosition = FormStartPosition.CenterParent};
            loadingSplash.SetTitle(splashText);

            var thread = new Thread(() =>
            {
                workToDo(null, null);
                loadingSplash.WorkCompleted();
            });
            if (singleThreadApartment)
                thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            loadingSplash.ShowDialog(sender);
        }

        public static DialogResult ShowMessage(
            IWin32Window owner,
            string text,
            string caption = null,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None,
            MessageBoxDefaultButton? defaultButton = null)
        {
            if (SkinHandler.GetCurrentSkin().EnforceOnMessageBox)
            {
                return FrmMessageBox.Show(owner, text, caption, buttons, icon, defaultButton);
            }
            else if(defaultButton != null)
            {
                return MessageBox.Show(owner, text, caption, buttons, icon, (MessageBoxDefaultButton)defaultButton);
            }
            else
            {
                return MessageBox.Show(owner, text, caption, buttons, icon);
            }
        }
    }
}