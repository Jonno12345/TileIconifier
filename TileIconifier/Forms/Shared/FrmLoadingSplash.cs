#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2021 Johnathon M
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
using System.Windows.Forms;

namespace TileIconifier.Forms.Shared
{
    public partial class FrmLoadingSplash : SkinnableForm
    {
        private string _title = "Loading";

        public FrmLoadingSplash()
        {
            InitializeComponent();
        }

        public void SetTitle(string title)
        {
            _title = title;
        }

        public void WorkCompleted()
        {
            Invoke(new Action(Close));
        }

        /// <summary>
        ///     Stops user from moving this window
        /// </summary>
        /// <param name="message"></param>
        protected override void WndProc(ref Message message)
        {
            const int wmSyscommand = 0x0112;
            const int scMove = 0xF010;

            switch (message.Msg)
            {
                case wmSyscommand:
                    var command = message.WParam.ToInt32() & 0xfff0;
                    if (command == scMove)
                        return;
                    break;
            }

            base.WndProc(ref message);
        }

        private void tmrIncrement_Tick(object sender, EventArgs e)
        {
            const int maxPeriods = 5;

            if (lblLoading.Text.Split('.').Length > maxPeriods)
                lblLoading.Text = _title;

            if (Text.Split('.').Length > maxPeriods)
                Text = _title;

            lblLoading.Text += @".";
            Text += @".";
        }

        private void frmLoadingSplash_Load(object sender, EventArgs e)
        {
            tmrIncrement_Tick(null, null);
        }
    }
}