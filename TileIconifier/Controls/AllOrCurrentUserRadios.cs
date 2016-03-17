using System;
using System.Windows.Forms;
using TileIconifier.Custom;

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