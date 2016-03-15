using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TileIconifier.Custom;
using TileIconifier.Properties;
using TileIconifier.Utilities;

namespace TileIconifier.Forms
{
    public partial class frmCustomShortcutManagerMain : Form
    {
        private List<CustomShortcut> _customShortcutsList;

        public frmCustomShortcutManagerMain()
        {
            InitializeComponent();
            RefreshCustomShortcuts();
            
        }

        private void RefreshCustomShortcuts()
        {
            LoadCustomShortcuts();
            lstCustomShortcuts.Clear();
            lstCustomShortcuts.Columns.Clear();

            lstCustomShortcuts.Columns.Add("Shortcut Name", (lstCustomShortcuts.Width / 4) * 2 -2 , HorizontalAlignment.Left);
            lstCustomShortcuts.Columns.Add("Shortcut Type", (lstCustomShortcuts.Width / 4) -1, HorizontalAlignment.Left);
            lstCustomShortcuts.Columns.Add("Shortcut User", (lstCustomShortcuts.Width / 4) - 1, HorizontalAlignment.Left);

            var smallImageList = new ImageList();
            for(var i = 0; i < _customShortcutsList.Count; i++)
            {
                smallImageList.Images.Add(_customShortcutsList[i].ShortcutItem.MediumImage ?? (_customShortcutsList[i].ShortcutItem.StandardIcon ?? Resources.QuestionMark));
                _customShortcutsList[i].ImageIndex = i;
                lstCustomShortcuts.Items.Add(_customShortcutsList[i]);
            }
            lstCustomShortcuts.SmallImageList = smallImageList;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUtils.ShowCenteredDialogForm<frmCustomShortcutManagerHelp>(this);
        }

        private void frmCustomShortcutManagerMain_Load(object sender, EventArgs e)
        {

        }

        private void LoadCustomShortcuts()
        {
            _customShortcutsList = new List<CustomShortcut>();

            if (!Directory.Exists(CustomShortcutConstants.CUSTOM_SHORTCUT_VBS_PATH))
                return;

            //get all VBS files built by TileIconifier
            foreach (var vbsFile in new DirectoryInfo(CustomShortcutConstants.CUSTOM_SHORTCUT_VBS_PATH).GetFiles("*.vbs", SearchOption.AllDirectories))
            {
                try
                {
                    //Only add it to our list if it's relevant to this user account
                    var customShortcut = CustomShortcut.Load(vbsFile.FullName);
                    if(customShortcut.ShortcutItem.ShortcutUser != ShortcutUser.UNKNOWN)
                        _customShortcutsList.Add(customShortcut);
                }
                catch { }
            }
        }

        private void btnCreateNewShortcut_Click(object sender, EventArgs e)
        {
            using (var newShortcutForm = new frmCustomShortcutManagerNew())
            {
                newShortcutForm.ShowDialog(this);
            }
            RefreshCustomShortcuts();
        }

        private void lstCustomShortcuts_SelectedIndexChanged(object sender, EventArgs e)
        {
            // var x = CustomShortcutVbs.Load(((ShortcutItem)lstCustomShortcuts.SelectedItem).TargetFilePath);
        }

        private void btnDeleteCustomShortcut_Click(object sender, EventArgs e)
        {
            if (lstCustomShortcuts.SelectedItems.Count == 0)
                return;

            var customShortcut = (CustomShortcut)lstCustomShortcuts.SelectedItems[0];

            if (MessageBox.Show(string.Format("Are you sure you wish to delete the custom shortcut for {0}?", customShortcut.Text.QuoteWrap()), "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            
            try
            {
                customShortcut.Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to clear up shortcuts." + ex.ToString());
            }

            //update our lists and refresh
            RefreshCustomShortcuts();
        }
    }
}
