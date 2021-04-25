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
using TileIconifier.Core.Custom;

namespace TileIconifier.Controls
{
    public partial class AllOrCurrentUserRadios : UserControl
    {
        public AllOrCurrentUserRadios()
        {
            InitializeComponent();
        }

        public string PathSelection()
        {
            if (radAllUsers.Checked)
                return CustomShortcutGetters.CustomShortcutAllUsersPath;
            if (radCurrentUser.Checked)
                return CustomShortcutGetters.CustomShortcutCurrentUserPath;

            throw new Exception("Unknown radio button checked?!");
        }

        public ShortcutUser GetCheckedRadio()
        {
            if (radAllUsers.Checked)
                return ShortcutUser.AllUsers;
            if (radCurrentUser.Checked)
                return ShortcutUser.CurrentUser;

            throw new Exception("Unknown radio button checked?!");
        }

        public void SetCheckedRadio(ShortcutUser radioOption)
        {
            switch (radioOption)
            {
                case ShortcutUser.AllUsers:
                    radAllUsers.Checked = true;
                    break;
                case ShortcutUser.CurrentUser:
                    radCurrentUser.Checked = true;
                    break;
            }
        }
    }
}