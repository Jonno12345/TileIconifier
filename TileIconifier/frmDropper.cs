using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

namespace TileIconifier
{
    public partial class frmDropper : Form
    {
        List<ShortcutItem> _shortcutsList;
        ShortcutItem _currentShortcut;

        public frmDropper()
        {
            InitializeComponent();
        }

        private void frmDropper_Load(object sender, EventArgs e)
        {
            Show();
            cmbColour.Text = "Custom";

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
            }
            catch (PowershellException) { return; }

            PowerShellUtils.MarryAppIDs(_shortcutsList);

            MarkPinnedShortcuts(tempFilePath);

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

            foreach (var PathToScan in PathsToScan)
            {
                var FileEnumeration = new DirectoryInfo(Environment.ExpandEnvironmentVariables(PathToScan)).EnumerateFiles("*.lnk", SearchOption.AllDirectories);
                _shortcutsList.AddRange(FileEnumeration.Select(f => new ShortcutItem() { ShortcutFileInfo = f })
                                                        .Where(f => !string.IsNullOrEmpty(f.ExeFilePath) && File.Exists(f.ExeFilePath)));
            }

            _shortcutsList = _shortcutsList.OrderBy(f => f.ShortcutFileInfo.Name).ToList();

        }

        //
        //
        //Code for drag and drop. Doesn't work unless you're dragging from an
        //elevated explorer window though, so left out for now.
        //

        //private void frmDropper_DragEnter(object sender, DragEventArgs e)
        //{
        //    e.Effect = DragDropEffects.Copy;
        //}

        //private void frmDropper_DragDrop(object sender, DragEventArgs e)
        //{
        //    string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        //    var lnkFiles = FileList.Where(f => Path.GetExtension(f).ToUpper() == ".LNK");

        //    try
        //    {
        //        foreach (var lnkFile in lnkFiles)
        //        {

        //            new TileIcon(BuildParameters());
        //        }
        //    }
        //    catch (UserCancellationException)
        //    {
        //        MessageBox.Show("Process cancelled.");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message + " - " + ex.ToString());
        //    }
        //}

        private TileIconParameters BuildParameters()
        {
            return new TileIconParameters()
            {
                BgColour = cmbColour.Text == "Custom" ? txtBGColour.Text : cmbColour.Text,
                FgText = radFGLight.Checked ? "light" : "dark",
                ShowNameOnSquare150x150Logo = chkFGTxtEnabled.Checked,
                Shortcut = _currentShortcut
            };
        }

        private void btnIconify_Click(object sender, EventArgs e)
        {
            if (!DoValidation())
                return;

            try
            {
                var TileIconify = new TileIcon(BuildParameters());
                TileIconify.RunIconify();
            }
            catch (UserCancellationException)
            {
                MessageBox.Show("Process cancelled.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - " + ex.ToString());
            }
        }


        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (!DoValidation(true))
                return;

            if (MessageBox.Show("Are you sure you wish to remove iconification?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var TileDeIconify = new TileIcon(BuildParameters());
                TileDeIconify.DeIconify();
            }
        }




        private bool DoValidation(bool pathOnly = false)
        {
            ResetValidation();
            CleanFields();

            return ValidateFields(pathOnly);
        }

        private void CleanFields()
        {
            txtLnkPath.Text = txtLnkPath.Text.Replace("\"", "");
        }

        private void ResetValidation()
        {
            txtLnkPath.BackColor = Color.White;
            txtBGColour.BackColor = Color.White;
        }

        private bool ValidateFields(bool pathOnly = false)
        {
            bool valid = true;

            Action<Control> controlInvalid = (c => { c.BackColor = Color.Red; valid = false; });

            if (!File.Exists(txtLnkPath.Text))
                controlInvalid(txtLnkPath);

            if (Path.GetExtension(txtLnkPath.Text).ToUpper() != ".LNK")
                controlInvalid(txtLnkPath);

            if (!pathOnly)
            {
                if (cmbColour.Text != "Custom")
                {
                    if (!Regex.Match(txtBGColour.Text, @"#\d{6}").Success)
                        controlInvalid(txtBGColour);
                }
            }

            return valid;
        }

        private void cmbColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBGColour.Enabled = (cmbColour.Text == "Custom");
        }

        private void chkFGTxtEnabled_CheckedChanged(object sender, EventArgs e)
        {
            radFGDark.Enabled = chkFGTxtEnabled.Checked;
            radFGLight.Enabled = chkFGTxtEnabled.Checked;
        }

        private void lstShortcuts_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentShortcut = (ShortcutItem)lstShortcuts.SelectedItem;
            UpdateShortcut();
        }

        private void UpdateShortcut()
        {
            txtLnkPath.Text = _currentShortcut.ShortcutFileInfo.FullName;
            txtLnkPath.SelectionStart = txtLnkPath.Text.Length;
            txtLnkPath.ScrollToCaret();

            txtExePath.Text = _currentShortcut.ExeFilePath;
            txtExePath.SelectionStart = txtExePath.Text.Length;
            txtExePath.ScrollToCaret();

            //pctCurrentIcon = _currentShortcut.
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void getPinnedItemsRequiresPowershellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (getPinnedItemsRequiresPowershellToolStripMenuItem.Checked)
            {
                getPinnedItemsRequiresPowershellToolStripMenuItem.Checked = false;
            }
            else
            {
                if(MessageBox.Show("Note- This feature uses Powershell and may take slightly longer to refresh. Continue?","Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
    }
}
