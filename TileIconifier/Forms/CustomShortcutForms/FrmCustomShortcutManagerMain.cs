using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TileIconifier.Custom;
using TileIconifier.Properties;
using TileIconifier.Utilities;

namespace TileIconifier.Forms.CustomShortcutForms
{
    public partial class FrmCustomShortcutManagerMain : Form
    {
        private List<CustomShortcut> _customShortcutsList;

        public FrmCustomShortcutManagerMain()
        {
            InitializeComponent();
            RefreshCustomShortcuts();
        }

        private void RefreshCustomShortcuts()
        {
            LoadCustomShortcuts();
            lstCustomShortcuts.Clear();
            lstCustomShortcuts.Columns.Clear();

            lstCustomShortcuts.Columns.Add("Shortcut Name", lstCustomShortcuts.Width/4 *2 - 2, HorizontalAlignment.Left);
            lstCustomShortcuts.Columns.Add("Shortcut Type", lstCustomShortcuts.Width/4 - 1, HorizontalAlignment.Left);
            lstCustomShortcuts.Columns.Add("Shortcut User", lstCustomShortcuts.Width/4 - 1, HorizontalAlignment.Left);

            var smallImageList = new ImageList();
            for (var i = 0; i < _customShortcutsList.Count; i++)
            {
                smallImageList.Images.Add(_customShortcutsList[i].ShortcutItem.MediumImage ??
                                          (_customShortcutsList[i].ShortcutItem.StandardIcon ?? Resources.QuestionMark));
                _customShortcutsList[i].ImageIndex = i;
                lstCustomShortcuts.Items.Add(_customShortcutsList[i]);
            }
            lstCustomShortcuts.SmallImageList = smallImageList;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUtils.ShowCenteredDialogForm<FrmCustomShortcutManagerHelp>(this);
        }

        private void frmCustomShortcutManagerMain_Load(object sender, EventArgs e)
        {
        }

        private void LoadCustomShortcuts()
        {
            _customShortcutsList = new List<CustomShortcut>();

            if (!Directory.Exists(CustomShortcutGetters.CustomShortcutVbsPath))
                return;

            //get all VBS files built by TileIconifier
            foreach (var customShortcut in 
                new DirectoryInfo(CustomShortcutGetters.CustomShortcutVbsPath).GetFiles("*.vbs", SearchOption.AllDirectories)
                    .Select(vbsFile => CustomShortcut.Load(vbsFile.FullName))
                        .Where(customShortcut => customShortcut.ShortcutItem.ShortcutUser != ShortcutUser.Unknown))
            {
                _customShortcutsList.Add(customShortcut);
            }
        }

        private void btnCreateNewShortcut_Click(object sender, EventArgs e)
        {
            using (var newShortcutForm = new FrmCustomShortcutManagerNew())
            {
                newShortcutForm.ShowDialog(this);
            }
            RefreshCustomShortcuts();
        }

        private void btnDeleteCustomShortcut_Click(object sender, EventArgs e)
        {
            if (lstCustomShortcuts.SelectedItems.Count == 0)
                return;

            var customShortcut = (CustomShortcut) lstCustomShortcuts.SelectedItems[0];

            if (
                MessageBox.Show(
                    $"Are you sure you wish to delete the custom shortcut for {customShortcut.Text.QuoteWrap()}?", 
                    @"Are you sure?",
                    MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            try
            {
                customShortcut.Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Unable to clear up shortcuts." + ex);
            }

            //update our lists and refresh
            RefreshCustomShortcuts();
        }
    }
}