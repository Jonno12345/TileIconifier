using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using TileIconifier.Utilities;

namespace TileIconifier.Forms
{
    public partial class frmMain : Form
    {
        List<ShortcutItem> _shortcutsList;
        ShortcutItem _currentShortcut;

        public frmMain()
        {
            InitializeComponent();
            pctMediumIcon.MouseMove += (s, e) => { pctMediumIcon.Cursor = Cursors.Hand; };
            pctSmallIcon.MouseMove += (s, e) => { pctSmallIcon.Cursor = Cursors.Hand; };
            AddEventHandlers();
        }

        private void frmDropper_Load(object sender, EventArgs e)
        {
            Show();
            StartFullUpdate();
        }

        private void StartFullUpdate()
        {
            frmLoadingSplash loadingSplash = new frmLoadingSplash();
            loadingSplash.StartPosition = FormStartPosition.CenterParent;
            loadingSplash.SetTitle("Refreshing");

            BackgroundWorker updateThread = new BackgroundWorker();
            updateThread.DoWork += new DoWorkEventHandler(FullUpdate);
            updateThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler((o, e) =>
            {
                loadingSplash.WorkCompleted();
            });

            updateThread.RunWorkerAsync();
            loadingSplash.ShowDialog();
        }

        private void FullUpdate(object sender, DoWorkEventArgs e)
        {
            EnumerateShortcuts();
            GetPinnedStartMenuInformation();

            if (lstShortcuts.InvokeRequired)
                lstShortcuts.Invoke(new Action(() => { lstShortcuts.DataSource = _shortcutsList; }));
            else
                lstShortcuts.DataSource = _shortcutsList;
        }

        private void GetPinnedStartMenuInformation()
        {
            if (!getPinnedItemsRequiresPowershellToolStripMenuItem.Checked)
                return;

            var tempOutputPath = string.Format("{0}{1}\\", Path.GetTempPath(), "TileIconifier");
            if (!Directory.Exists(tempOutputPath))
                Directory.CreateDirectory(tempOutputPath);

            var tempFilePath = string.Format("{0}{1}.xml", tempOutputPath, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));

            try
            {
                PowerShellUtils.DumpStartLayout(tempFilePath);
                PowerShellUtils.MarryAppIDs(_shortcutsList);

                MarkPinnedShortcuts(tempFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("A problem occurred with PowerShell functionality. It has been disabled.\r\n" + ex.ToString() + "\r\n\r\n" + ex.Message, "PowerShell failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                getPinnedItemsRequiresPowershellToolStripMenuItem_Click(this, null);
                return;
            }

            _shortcutsList = _shortcutsList.OrderByDescending(s => s.IsPinned)
                                            .ThenBy(s => s.ShortcutFileInfo.Name)
                                            .ToList();

            try
            {
                File.Delete(tempFilePath);
            }
            catch { }
        }

        private void MarkPinnedShortcuts(string tempFilePath)
        {
            var startLayout = File.ReadAllText(tempFilePath);

            var regexMatches = Regex.Matches(startLayout, "<start:DesktopApplicationTile.*DesktopApplicationID=\"(.*)\".*");

            foreach (Match regexMatch in regexMatches)
            {
                try
                {
                    var groupData = regexMatch.Groups[1].Value;

                    var shortcutId = _shortcutsList.Where(s => s.AppId == groupData)
                    .First();
                    shortcutId.IsPinned = true;
                }
                catch { }
            }
        }

        private void EnumerateShortcuts()
        {
            _shortcutsList = new List<ShortcutItem>();

            List<string> PathsToScan = new List<string>()
            {
                @"%PROGRAMDATA%\Microsoft\Windows\Start Menu",
                @"%APPDATA%\Microsoft\Windows\Start Menu"
            };

            foreach (var pathToScan in PathsToScan)
            {
                ApplyAllFiles(Environment.ExpandEnvironmentVariables(pathToScan), f =>
                {
                    var fi = new FileInfo(f);
                    if (!fi.Extension.Equals(".lnk", StringComparison.OrdinalIgnoreCase))
                        return;

                    var shortcutItem = new ShortcutItem(fi);
                    if (!string.IsNullOrEmpty(shortcutItem.ExeFilePath) && File.Exists(shortcutItem.ExeFilePath))
                        _shortcutsList.Add(shortcutItem);
                });
            }

            _shortcutsList = _shortcutsList.OrderBy(f => f.ShortcutFileInfo.Name).ToList();
        }

        private void ApplyAllFiles(string folder, Action<string> fileAction)
        {
            foreach (string file in Directory.GetFiles(folder))
            {
                fileAction(file);
            }
            foreach (string subDir in Directory.GetDirectories(folder))
            {
                try
                {
                    ApplyAllFiles(subDir, fileAction);
                }
                catch
                {
                }
            }
        }

        private void btnIconify_Click(object sender, EventArgs e)
        {
            if (!DoValidation())
                return;

            try
            {
                var TileIconify = new TileIcon(_currentShortcut);
                TileIconify.RunIconify();
                _currentShortcut.CommitChanges();
                UpdateShortcut();
            }
            catch (UserCancellationException)
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - " + ex.ToString());
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (!DoValidation())
                return;

            if (MessageBox.Show("Are you sure you wish to remove iconification?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var TileDeIconify = new TileIcon(_currentShortcut);
                TileDeIconify.DeIconify();
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
            bool valid = true;

            Action<Control> controlInvalid = (c => { c.BackColor = Color.Red; valid = false; });

            if (cmbColour.Text == "Custom" && !Regex.Match(txtBGColour.Text, @"^#\d{6}$").Success)
                controlInvalid(txtBGColour);

            if (_currentShortcut.MediumImage == null)
                controlInvalid(pctMediumIcon);

            if (_currentShortcut.SmallImage == null)
                controlInvalid(pctSmallIcon);

            return valid;
        }

        private void cmbColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBGColour.Enabled = (cmbColour.Text == "Custom");
            if (_currentShortcut != null)
            {
                if (cmbColour.Text == "Custom")
                    _currentShortcut.BackgroundColor = txtBGColour.Text;
                else
                    _currentShortcut.BackgroundColor = cmbColour.Text;
                UpdateShortcut();
            }
        }

        private void chkFGTxtEnabled_CheckedChanged(object sender, EventArgs e)
        {
            radFGDark.Enabled = chkFGTxtEnabled.Checked;
            radFGLight.Enabled = chkFGTxtEnabled.Checked;
            _currentShortcut.ShowNameOnSquare150x150Logo = chkFGTxtEnabled.Checked;
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
            txtExePath.Text = _currentShortcut.ExeFilePath;
            txtExePath.SelectionStart = txtExePath.Text.Length;
            txtExePath.ScrollToCaret();

            //only show remove if the icon is successfully iconified
            btnRemove.Enabled = _currentShortcut.IsIconified;

            //update the picture boxes to show the relevant images
            pctStandardIcon.Image = _currentShortcut.StandardIcon.ToBitmap();
            pctMediumIcon.Image = _currentShortcut.MediumImage;
            pctSmallIcon.Image = _currentShortcut.SmallImage;

            //set relevant unsaved changes controls to required visibility/enabled states
            lblUnsaved.Visible = hasUnsavedChanges;
            btnIconify.Enabled = hasUnsavedChanges;
            btnUndo.Enabled = hasUnsavedChanges;

            //reset the combo box - choose actual colour, or custom if none of the combobox items
            if (cmbColour.Items.Contains(_currentShortcut.BackgroundColor))
            {
                cmbColour.SelectedItem = _currentShortcut.BackgroundColor;
                txtBGColour.Enabled = false;
            }
            else
            {
                cmbColour.Text = "Custom";
                txtBGColour.Enabled = true;
                txtBGColour.Text = _currentShortcut.BackgroundColor;
            }

            //set the foreground text checkbox based on value stored for this shortcut
            chkFGTxtEnabled.Checked = _currentShortcut.ShowNameOnSquare150x150Logo;

            //enable radio buttons if the foreground text is enabled
            radFGDark.Enabled = chkFGTxtEnabled.Checked;
            radFGLight.Enabled = chkFGTxtEnabled.Checked;

            //set the radio buttons based on the current shortcuts selection
            radFGDark.Checked = (_currentShortcut.ForegroundText == "dark");
            radFGLight.Checked = (_currentShortcut.ForegroundText == "light");

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
                    Invoke(new Action(() =>
                    {
                        getPinnedItemsRequiresPowershellToolStripMenuItem.Checked = false;
                    }));
                else
                    getPinnedItemsRequiresPowershellToolStripMenuItem.Checked = false;
            }
            else
            {
                if (MessageBox.Show("Note- This feature uses Powershell and may take slightly longer to refresh. Continue?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    getPinnedItemsRequiresPowershellToolStripMenuItem.Checked = true;
                }
            }
            StartFullUpdate();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout AboutForm = new frmAbout();
            AboutForm.StartPosition = FormStartPosition.CenterScreen;
            AboutForm.ShowDialog();
        }

        private Bitmap GetImage()
        {
            frmIconSelector iconSelector = new frmIconSelector(_currentShortcut.ExeFilePath);
            iconSelector.ShowDialog();
            if (iconSelector.ReturnedBitmap == null)
                throw new UserCancellationException();
            return iconSelector.ReturnedBitmap;
        }

        private void pctMediumIcon_Click(object sender, EventArgs e)
        {
            try
            {
                var ImageToUse = GetImage();
                _currentShortcut.MediumImage = ImageToUse;

                if (chkUseSameImg.Checked)
                    _currentShortcut.SmallImage = ImageToUse;

                UpdateShortcut();
            }
            catch (UserCancellationException) { }
        }

        private void pctSmallIcon_Click(object sender, EventArgs e)
        {
            try
            {
                var ImageToUse = GetImage();
                _currentShortcut.SmallImage = ImageToUse;

                if (chkUseSameImg.Checked)
                    _currentShortcut.MediumImage = ImageToUse;

                UpdateShortcut();
            }
            catch (UserCancellationException) { }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHelp helpForm = new frmHelp();
            helpForm.StartPosition = FormStartPosition.CenterParent;
            helpForm.ShowDialog();
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
            if (radFGLight.Checked)
                _currentShortcut.ForegroundText = "light";
            else
                _currentShortcut.ForegroundText = "dark";

            UpdateShortcut();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            _currentShortcut.UndoChanges();

            UpdateShortcut();
        }

        private void AddEventHandlers()
        {
            this.txtBGColour.TextChanged += new System.EventHandler(this.txtBGColour_TextChanged);
            this.cmbColour.SelectedIndexChanged += new System.EventHandler(this.cmbColour_SelectedIndexChanged);
            this.chkFGTxtEnabled.CheckedChanged += new System.EventHandler(this.chkFGTxtEnabled_CheckedChanged);
            this.radFGLight.CheckedChanged += new System.EventHandler(this.radFGLight_CheckedChanged);
            this.lstShortcuts.SelectedIndexChanged += new System.EventHandler(this.lstShortcuts_SelectedIndexChanged);
        }

        private void RemoveEventHandlers()
        {
            this.txtBGColour.TextChanged -= new System.EventHandler(this.txtBGColour_TextChanged);
            this.cmbColour.SelectedIndexChanged -= new System.EventHandler(this.cmbColour_SelectedIndexChanged);
            this.chkFGTxtEnabled.CheckedChanged -= new System.EventHandler(this.chkFGTxtEnabled_CheckedChanged);
            this.radFGLight.CheckedChanged -= new System.EventHandler(this.radFGLight_CheckedChanged);
            this.lstShortcuts.SelectedIndexChanged -= new System.EventHandler(this.lstShortcuts_SelectedIndexChanged);
        }
    }
}