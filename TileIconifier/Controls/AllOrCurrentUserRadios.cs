using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TileIconifier.Custom;

namespace TileIconifier.Controls
{
    public partial class AllOrCurrentUserRadios : UserControl
    {
        public string PathSelection()
        {

            if (radAllUsers.Checked)
                return CustomShortcutConstants.CUSTOM_SHORTCUT_ALL_USERS_PATH;
            else if (radCurrentUser.Checked)
                return CustomShortcutConstants.CUSTOM_SHORTCUT_CURRENT_USER_PATH;

            throw new Exception("Unknown radio button checked?!");

        }

        public ShortcutUser GetCheckedRadio()
        {
            if (radAllUsers.Checked)
                return ShortcutUser.ALL_USERS;
            else if (radCurrentUser.Checked)
                return ShortcutUser.CURRENT_USER;

            throw new Exception("Unknown radio button checked?!");

        }

        public void SetCheckedRadio(ShortcutUser radioOption)
        {
            switch (radioOption)
            {
                case ShortcutUser.ALL_USERS:
                    radAllUsers.Checked = true;
                    break;
                case ShortcutUser.CURRENT_USER:
                    radCurrentUser.Checked = true;
                    break;
            }
        }

        public AllOrCurrentUserRadios()
        {
            InitializeComponent();
        }
    }
}
