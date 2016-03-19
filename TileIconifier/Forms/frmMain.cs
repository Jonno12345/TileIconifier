using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TileIconifier.Forms.CustomShortcutForms;
using TileIconifier.Shortcut;
using TileIconifier.TileIconify;
using TileIconifier.Utilities;

namespace TileIconifier.Forms
{
    public partial class FrmMain : Form
    {
        private ShortcutItem _currentShortcut;
        private List<ShortcutItem> _shortcutsList;

        public FrmMain()
        {
            InitializeComponent();
            AddEventHandlers();
        }

        private void frmDropper_Load(object sender, EventArgs e)
        {
            Show();
            StartFullUpdate();
        }

        private void StartFullUpdate()
        {
            var loadingSplash = new FrmLoadingSplash { StartPosition = FormStartPosition.CenterParent };
            loadingSplash.SetTitle("Refreshing");

            var updateThread = new BackgroundWorker();
            updateThread.DoWork += FullUpdate;
            updateThread.RunWorkerCompleted += (o, e) => { loadingSplash.WorkCompleted(); };

            updateThread.RunWorkerAsync();
            loadingSplash.ShowDialog(this);
        }

        private void FullUpdate(object sender, DoWorkEventArgs e)
        {
            if (getPinnedItemsRequiresPowershellToolStripMenuItem.Checked)
            {
                Exception pinningException;
                _shortcutsList = ShortcutItemEnumeration.TryGetShortcutsWithPinning(out pinningException, true);
                if (pinningException != null)
                {
                    MessageBox.Show(
                        @"A problem occurred with PowerShell functionality. It has been disabled
" + pinningException,
                        @"PowerShell failure", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    getPinnedItemsRequiresPowershellToolStripMenuItem_Click(this, null);
                }
            }
            else
            {
                _shortcutsList = ShortcutItemEnumeration.GetShortcuts(true);
            }

            if (lstShortcuts.InvokeRequired)
                lstShortcuts.Invoke(new Action(() => { lstShortcuts.DataSource = _shortcutsList; }));
            else
                lstShortcuts.DataSource = _shortcutsList;
        }

        private void btnIconify_Click(object sender, EventArgs e)
        {
            if (!DoValidation())
                return;

            try
            {
                var tileIconify = new TileIcon(_currentShortcut);
                tileIconify.RunIconify();
                _currentShortcut.CommitChanges();
                UpdateShortcut();
            }
            catch (UserCancellationException)
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + @" - " + ex);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (!DoValidation())
                return;

            if (
                MessageBox.Show(@"Are you sure you wish to remove iconification?", @"Confirm", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var tileDeIconify = new TileIcon(_currentShortcut);
                tileDeIconify.DeIconify();
                _currentShortcut.ResetParameters();
                UpdateShortcut();
            }
        }

        private bool DoValidation()
        {
            ResetValidation();

            return ValidateColour();
        }

        private void ResetValidation()
        {
            txtBGColour.BackColor = Color.White;
            pctMediumIcon.BackColor = SystemColors.Control;
            pctSmallIcon.BackColor = SystemColors.Control;
        }

        private bool ValidateColour()
        {
            var valid = true;

            Action<Control> controlInvalid = c =>
            {
                c.BackColor = Color.Red;
                valid = false;
            };

            if (cmbColour.Text == @"Custom" && !Regex.Match(txtBGColour.Text, @"^#[0-9a-fA-F]{6}$").Success)
                controlInvalid(txtBGColour);

            if (_currentShortcut.MediumImage == null)
                controlInvalid(pctMediumIcon);

            if (_currentShortcut.SmallImage == null)
                controlInvalid(pctSmallIcon);

            return valid;
        }

        private void cmbColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBGColour.Enabled = cmbColour.Text == @"Custom";
            if (_currentShortcut != null)
            {
                _currentShortcut.BackgroundColor = cmbColour.Text == @"Custom" ? txtBGColour.Text : cmbColour.Text;
                UpdateShortcut();
            }
        }

        private void chkFGTxtEnabled_CheckedChanged(object sender, EventArgs e)
        {
            radFGDark.Enabled = chkFGTxtEnabled.Checked;
            radFGLight.Enabled = chkFGTxtEnabled.Checked;
            _currentShortcut.ShowNameOnSquare150X150Logo = chkFGTxtEnabled.Checked;
            UpdateShortcut();
        }

        private void lstShortcuts_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentShortcut = (ShortcutItem)lstShortcuts.SelectedItem;
            UpdateShortcut();
        }

        private void UpdateShortcut()
        {
            //disable event handlers whilst updating things programatically
            RemoveEventHandlers();

            //check if unsaved once per update
            var hasUnsavedChanges = _currentShortcut.HasUnsavedChanges;

            //set shortcut path box to value stored in shortcut
            txtLnkPath.Text = _currentShortcut.ShortcutFileInfo.FullName;
            txtLnkPath.SelectionStart = txtLnkPath.Text.Length;
            txtLnkPath.ScrollToCaret();

            //set exe path box to value stored in shortcut
            txtExePath.Text = _currentShortcut.TargetFilePath;
            txtExePath.SelectionStart = txtExePath.Text.Length;
            txtExePath.ScrollToCaret();

            //only show remove if the icon is successfully iconified
            btnRemove.Enabled = _currentShortcut.IsIconified;

            //update the picture boxes to show the relevant images
            pctStandardIcon.Image = (Image)_currentShortcut.StandardIcon?.Clone();
            pctMediumIcon.Image = (Image)_currentShortcut.MediumImage?.Clone();
            pctSmallIcon.Image = _currentShortcut.MediumImage != null
                ? (Image)_currentShortcut.SmallImage?.Clone()
                : null;

            //set relevant unsaved changes controls to required visibility/enabled states
            lblUnsaved.Visible = hasUnsavedChanges;
            btnIconify.Enabled = hasUnsavedChanges;
            btnReset.Enabled = hasUnsavedChanges;

            //reset the combo box - choose actual colour, or custom if none of the combobox items
            if (cmbColour.Items.Contains(_currentShortcut.BackgroundColor))
            {
                cmbColour.SelectedItem = _currentShortcut.BackgroundColor;
                txtBGColour.Enabled = false;
            }
            else
            {
                cmbColour.Text = @"Custom";
                txtBGColour.Enabled = true;
                txtBGColour.Text = _currentShortcut.BackgroundColor;
            }

            //set the foreground text checkbox based on value stored for this shortcut
            chkFGTxtEnabled.Checked = _currentShortcut.ShowNameOnSquare150X150Logo;

            //enable radio buttons if the foreground text is enabled
            radFGDark.Enabled = chkFGTxtEnabled.Checked;
            radFGLight.Enabled = chkFGTxtEnabled.Checked;

            //set the radio buttons based on the current shortcuts selection
            radFGDark.Checked = _currentShortcut.ForegroundText == "dark";
            radFGLight.Checked = _currentShortcut.ForegroundText == "light";

            //reset any validation failures
            ResetValidation();

            //re-add the event handlers now we've finished updating
            AddEventHandlers();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void getPinnedItemsRequiresPowershellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (getPinnedItemsRequiresPowershellToolStripMenuItem.Checked)
            {
                if (InvokeRequired)
                    Invoke(new Action(() => { getPinnedItemsRequiresPowershellToolStripMenuItem.Checked = false; }));
                else
                    getPinnedItemsRequiresPowershellToolStripMenuItem.Checked = false;
            }
            else
            {
                if (
                    MessageBox.Show(
                        @"Note- This feature uses Powershell and may take slightly longer to refresh. Continue?",
                        @"Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    getPinnedItemsRequiresPowershellToolStripMenuItem.Checked = true;
                }
            }
            StartFullUpdate();
        }

        private void pctMediumIcon_Click(object sender, EventArgs e)
        {
            try
            {
                var imageToUse = ImageUtils.GetImage(this, _currentShortcut.TargetFilePath);
                _currentShortcut.MediumImage = imageToUse;

                if (chkUseSameImg.Checked)
                    _currentShortcut.SmallImage = imageToUse;

                UpdateShortcut();
            }
            catch (UserCancellationException)
            {
            }
        }

        private void pctSmallIcon_Click(object sender, EventArgs e)
        {
            try
            {
                var imageToUse = ImageUtils.GetImage(this, _currentShortcut.TargetFilePath);
                _currentShortcut.SmallImage = imageToUse;

                if (chkUseSameImg.Checked)
                    _currentShortcut.MediumImage = imageToUse;

                UpdateShortcut();
            }
            catch (UserCancellationException)
            {
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUtils.ShowCenteredDialogForm<FrmAbout>(this);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUtils.ShowCenteredDialogForm<FrmHelp>(this);
        }

        private void customShortcutManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var customShortcutManager = new FrmCustomShortcutManagerMain())
            {
                customShortcutManager.ShowDialog(this);
            }
            StartFullUpdate();
        }

        private void refreshAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartFullUpdate();
        }

        private void txtBGColour_TextChanged(object sender, EventArgs e)
        {
            _currentShortcut.BackgroundColor = txtBGColour.Text;
            UpdateShortcut();
        }

        private void radFGLight_CheckedChanged(object sender, EventArgs e)
        {
            _currentShortcut.ForegroundText = radFGLight.Checked ? "light" : "dark";

            UpdateShortcut();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _currentShortcut.UndoChanges();

            UpdateShortcut();
        }

        private void AddEventHandlers()
        {
            txtBGColour.TextChanged += txtBGColour_TextChanged;
            cmbColour.SelectedIndexChanged += cmbColour_SelectedIndexChanged;
            chkFGTxtEnabled.CheckedChanged += chkFGTxtEnabled_CheckedChanged;
            radFGLight.CheckedChanged += radFGLight_CheckedChanged;
            lstShortcuts.SelectedIndexChanged += lstShortcuts_SelectedIndexChanged;
        }

        private void RemoveEventHandlers()
        {
            txtBGColour.TextChanged -= txtBGColour_TextChanged;
            cmbColour.SelectedIndexChanged -= cmbColour_SelectedIndexChanged;
            chkFGTxtEnabled.CheckedChanged -= chkFGTxtEnabled_CheckedChanged;
            radFGLight.CheckedChanged -= radFGLight_CheckedChanged;
            lstShortcuts.SelectedIndexChanged -= lstShortcuts_SelectedIndexChanged;
        }

        private async void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var updateDetails = await UpdateUtils.CheckForUpdate();

                if (updateDetails.UpdateAvailable)
                {
                    if (MessageBox.Show(
                        $@"An update is available! Would you like to visit the releases page? (Your version: {updateDetails.CurrentVersion} - Latest version: ({updateDetails.LatestVersion})",
                        @"New version available!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) == DialogResult.Yes
                        )
                    {
                        Process.Start("https://github.com/Jonno12345/TileIconify");
                    }
                }
                else
                {
                    MessageBox.Show(@"You are already on the latest version!", @"Up-to-date");
                }
            }
            catch
            {
                MessageBox.Show(
                    $@"An error occurred getting latest release information. Click Ok to visit the latest releases page to check manually. (Your version: {UpdateUtils.CurrentVersion})",
                    @"Unable to check server!",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation);
            }
}
    }
}