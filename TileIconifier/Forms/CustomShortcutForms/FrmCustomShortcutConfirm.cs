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
using System.Windows.Forms;
using TileIconifier.Core.Custom;

namespace TileIconifier.Forms.CustomShortcutForms
{
    public partial class FrmCustomShortcutConfirm : Form
    {
        public string ShortcutName;

        public FrmCustomShortcutConfirm()
        {
            InitializeComponent();
        }

        private void FrmCustomShortcutConfirm_Load(object sender, EventArgs e)
        {
            txtCustomShortcutName.Text = ShortcutName;
            lblCaption.Text =
                $@"This will create a custom shortcut with the same parameters as {ShortcutName.QuoteWrap()
                    }. This is useful for tiles that don't work by default (Such as Microsoft Office and Mozilla Firefox). Please confirm the name for the new shortcut (The same name as the original is not a problem)";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ShortcutName = txtCustomShortcutName.Text.CleanInvalidFilenameChars();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtCustomShortcutName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            btnOk_Click(this, null);
        }
    }
}