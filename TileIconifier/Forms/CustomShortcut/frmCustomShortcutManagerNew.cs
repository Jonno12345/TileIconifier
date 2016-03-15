using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TileIconifier;
using TileIconifier.Steam;
using TileIconifier.Utilities;
using TileIconifier.Custom;
using System.IO;
using TsudaKageyu;
using TileIconifier.Properties;

namespace TileIconifier.Forms
{
    public partial class frmCustomShortcutManagerNew : Form
    {
        //*********************************************************************
        // GENERAL
        //*********************************************************************

        private TabPage previousTabPage;

        public frmCustomShortcutManagerNew()
        {
            InitializeComponent();
            previousTabPage = tabShortcutType.SelectedTab;
            SetUpExplorer();
            SetUpSteam();
        }

        private void GenerateFullShortcut(
            string targetPath,
            string targetArguments,
            CustomShortcutType shortcutType,
            string basicShortcutIcon
            )
        {
            var shortcutName = txtShortcutName.Text.CleanInvalidFilenameChars();
            Bitmap ImageToUse = null;

            try
            {
                ValidateFields(shortcutName, targetPath, out ImageToUse);
            }
            catch (ValidationFailureException) { return; }
            //build our new custom shortcut
            var customShortcut = new CustomShortcut(
                shortcutName: shortcutName,
                targetPath: targetPath,
                targetArguments: targetArguments,
                shortcutType: shortcutType,
                shortcutRootFolder: radShortcutLocation.PathSelection(),
                basicIconToUse: basicShortcutIcon);

            //If we didn't specify a shortcut icon path, make one
            if (basicShortcutIcon == null)
                customShortcut.BuildCustomShortcut((Image)pctCurrentIcon.Image.Clone());
            else
                customShortcut.BuildCustomShortcut();


            //Iconify a TileIconifier shortcut for this with default settings
            var newShortcutItem = customShortcut.ShortcutItem;
            newShortcutItem.MediumImage = ImageToUse;
            newShortcutItem.SmallImage = ImageToUse;
            TileIcon iconify = new TileIcon(newShortcutItem);
            iconify.RunIconify();

            //confirm to the user the shortcut has been created
            MessageBox.Show(string.Format("A shortcut for {0} has been created in your start menu under TileIconify. The item will need to be pinned manually.", shortcutName.QuoteWrap()), "Shortcut created!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Hacked this in quickly - fix it up.
        private void ValidateFields(string shortcutName, string targetPath, out Bitmap imageToUse)
        {
            //Check if there are invalid characters in the shortcut name
            if (txtShortcutName.Text != shortcutName || shortcutName.Length == 0)
            {
                MessageBox.Show("Invalid characters or invalid shortcut name!", "Invalid characters or shortcut name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                throw new ValidationFailureException();
            }

            if (targetPath.Length == 0)
            {
                MessageBox.Show("Target path is empty!", "Target path empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                throw new ValidationFailureException();
            }

            if (pctCurrentIcon.Image == null)
            {
                MessageBox.Show("No icon has been selected!", "Please select an icon", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                throw new ValidationFailureException();
            }

            try
            {
                imageToUse = new Bitmap((Image)pctCurrentIcon.Image.Clone());
            }
            catch
            {
                MessageBox.Show("An issue occurred with the image selected. Please load the icon again or try another.", "Icon error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                throw new ValidationFailureException();
            }
        }

        private void btnGenerateShortcut_Click(object sender, EventArgs e)
        {
            if (tabShortcutType.SelectedTab == tabSteam)
            {
                GenerateSteamShortcut();
            }
            else if (tabShortcutType.SelectedTab == tabExplorer)
            {
                GenerateExplorerShortcut();
            }
            else if (tabShortcutType.SelectedTab == tabOther)
            {
                GenerateOtherShortcut();
            }
        }

        private void pctCurrentIcon_Click(object sender, EventArgs e)
        {
            try
            {
                pctCurrentIcon.Image = ImageUtils.GetImage(this, CustomShortcutConstants.EXPLORER_PATH);
            }
            catch (UserCancellationException) { }
        }

        private void tabShortcutType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //save the previous tab state in it's cache before changing
            //This seems to be the only way to get the tab before it actually changes...?
            if (previousTabPage == tabExplorer)
                SaveCache(_explorerCache);
            else if (previousTabPage == tabSteam)
                SaveCache(_steamCache);
            else if (previousTabPage == tabOther)
                SaveCache(_otherCache);

            previousTabPage = tabShortcutType.SelectedTab;

            //load the cache of the new tab
            TabPage current = (sender as TabControl).SelectedTab;
            if (current == tabExplorer)
                LoadCache(_explorerCache);
            else if (current == tabSteam)
                LoadCache(_steamCache);
            else if (current == tabOther)
                LoadCache(_otherCache);
        }

        private void LoadCache(NewCustomShortcutFormCache cache)
        {
            pctCurrentIcon.Image = cache.Icon != null ? (Image)cache.Icon.Clone() : null;
            txtShortcutName.Text = cache.ShortcutName;
            radShortcutLocation.SetCheckedRadio(cache.AllOrCurrentUser);
        }

        private void SaveCache(NewCustomShortcutFormCache cache)
        {
            cache.Icon = pctCurrentIcon.Image != null ? (Image)pctCurrentIcon.Image.Clone() : null;
            cache.ShortcutName = txtShortcutName.Text;
            cache.AllOrCurrentUser = radShortcutLocation.GetCheckedRadio();
        }

        //*********************************************************************
        // EXPLORER RELATED
        //*********************************************************************

        private NewCustomShortcutFormCache _explorerCache = new NewCustomShortcutFormCache();
        private void SetUpExplorer()
        {
            cmbExplorerGuids.DisplayMember = "Key";
            cmbExplorerGuids.ValueMember = "Value";
            cmbExplorerGuids.DataSource = new BindingSource(CustomShortcutConstants.EXPLORER_GUIDS, null);
            pctCurrentIcon.Image = Resources.ExplorerIco.ToBitmap();
        }

        private void radSpecialFolder_CheckedChanged(object sender, EventArgs e)
        {
            txtCustomFolder.Enabled = !radSpecialFolder.Checked;
            cmbExplorerGuids.Enabled = radSpecialFolder.Checked;
        }

        private void GenerateExplorerShortcut()
        {
            string targetArguments = radSpecialFolder.Checked ? string.Format("shell:::{0}", cmbExplorerGuids.SelectedValue) : txtCustomFolder.Text;
            string workingFolder = radSpecialFolder.Checked ? null : txtCustomFolder.Text;

            //radSpecialFolder.Checked ? cmbExplorerGuids.SelectedValue

            GenerateFullShortcut(
                targetPath: CustomShortcutConstants.EXPLORER_PATH,
                targetArguments: targetArguments,
                shortcutType: CustomShortcutType.EXPLORER,
                basicShortcutIcon: CustomShortcutConstants.EXPLORER_PATH
            );
        }

        private void cmbExplorerGuids_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbExplorerGuids.SelectedIndex >= 0)
                txtShortcutName.Text = cmbExplorerGuids.Text.CleanInvalidFilenameChars();
        }

        //*********************************************************************
        // STEAM RELATED
        //*********************************************************************

        List<SteamGame> _steamGames;
        private NewCustomShortcutFormCache _steamCache = new NewCustomShortcutFormCache();

        private void SetUpSteam()
        {
            if (TestSteamDetection())
            {
                LoadSteamGames();
                SetUpSteamListBox();
            }
        }

        private bool TestSteamDetection()
        {
            bool steamDetected = true;
            try
            {
                var steamInstallationPath = SteamLibrary.Instance.GetSteamInstallationFolder();
                txtSteamInstallationPath.Text = string.Format("Steam installation path: {0}", steamInstallationPath);
            }
            catch (SteamInstallationPathNotFoundException)
            {
                txtSteamInstallationPath.Text = "Steam installation path not found!!!";
                steamDetected = false;
            }

            try
            {
                var steamExecutable = SteamLibrary.Instance.GetSteamExePath();
                txtSteamExecutablePath.Text = string.Format("Steam executable path: {0}", steamExecutable);
            }
            catch (SteamExecutableNotFoundException)
            {
                txtSteamExecutablePath.Text = "Steam executable path not found!!!";
                steamDetected = false;
            }

            try
            {
                var steamLibraryFolders = SteamLibrary.Instance.GetLibraryFolders();
                if (steamLibraryFolders.Count > 0)
                {
                    txtSteamLibraryPaths.Text = string.Format("Steam library folders: {0}", string.Join("; ", steamLibraryFolders));
                }
                else
                {
                    txtSteamLibraryPaths.Text = "Steam library paths not found!!!";
                    steamDetected = false;
                }
            }
            catch
            {
                txtSteamLibraryPaths.Text = "Steam library paths not found!!!";
                steamDetected = false;
            }

            return steamDetected;
        }

        private void SetUpSteamListBox()
        {
            lstSteamGames.Clear();
            lstSteamGames.Columns.Clear();

            lstSteamGames.Columns.Add("App Id", (lstSteamGames.Width / 8) - 10, HorizontalAlignment.Left);
            lstSteamGames.Columns.Add("Game Name", ((lstSteamGames.Width / 8) * 7) - 10, HorizontalAlignment.Left);

            lstSteamGames.Items.AddRange(_steamGames.OrderBy(s => s.GameName).ToArray());
        }

        private void LoadSteamGames()
        {
            _steamGames = SteamLibrary.Instance.GetAllSteamGames();

        }

        private void lstSteamGames_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lstSteamGames.Columns[e.ColumnIndex].Width;
        }

        private void btnInstallationChange_Click(object sender, EventArgs e)
        {
            fldBrowser.Description = "Select the folder containing your Steam installation";
            if (fldBrowser.ShowDialog(this) != DialogResult.OK)
                return;

            SteamLibrary.Instance.SetSteamInstallationFolder(fldBrowser.SelectedPath);
            SetUpSteam();
        }

        private void btnSteamExeChange_Click(object sender, EventArgs e)
        {
            if (opnSteamExe.ShowDialog(this) != DialogResult.OK)
                return;

            SteamLibrary.Instance.SetSteamExePath(opnSteamExe.FileName);
            SetUpSteam();
        }

        private void lstSteamGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSteamGames.SelectedItems.Count == 0)
                return;

            SteamGame steamGame = ((SteamGame)lstSteamGames.SelectedItems[0]);
            pctCurrentIcon.Image = (Image)steamGame.IconAsBitmap.Clone();
            txtShortcutName.Text = steamGame.GameName.CleanInvalidFilenameChars();
        }

        private void GenerateSteamShortcut()
        {
            if (lstSteamGames.SelectedItems.Count == 0) return;

            var steamGame = ((SteamGame)lstSteamGames.SelectedItems[0]);

            GenerateFullShortcut(
                targetPath: SteamLibrary.Instance.GetSteamExePath(),
                targetArguments: steamGame.GameExecutionArgument,
                shortcutType: CustomShortcutType.STEAM,
                basicShortcutIcon: steamGame.IconPath);
        }

        private void btnSteamLibrariesPath_Click(object sender, EventArgs e)
        {

            bool isValid = false;
            while (!isValid)
            {
                fldBrowser.Description = string.Format("Select a folder containing your Steam library (This will be a folder containing a {0} folder)", "steamapps".QuoteWrap());

                if (fldBrowser.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    SteamLibrary.Instance.AddLibraryFolder(fldBrowser.SelectedPath);
                    isValid = true;
                }
                catch (SteamLibraryPathNotFoundException)
                {
                    MessageBox.Show(this, "Selected folder is invalid (valid folders should contain a subfolder named \"steamapps\"), please try again or cancel.");
                }
            }
        }

        //*********************************************************************
        // OTHER RELATED
        //*********************************************************************

        private NewCustomShortcutFormCache _otherCache = new NewCustomShortcutFormCache();

        private void btnOtherTargetBrowse_Click(object sender, EventArgs e)
        {
            if (opnOtherTarget.ShowDialog() != DialogResult.OK)
                return;

            string filePath = opnOtherTarget.FileName;
            txtOtherTargetPath.Text = filePath;
            txtShortcutName.Text = Path.GetFileNameWithoutExtension(filePath);

            try
            {
                pctCurrentIcon.Image = new IconExtractor(filePath).GetIcon(0).ToBitmap();
            }
            catch { }
        }

        private void GenerateOtherShortcut()
        {
            GenerateFullShortcut(
                targetPath: txtOtherTargetPath.Text,
                targetArguments: txtOtherShortcutArguments.Text,
                shortcutType: CustomShortcutType.OTHER,
                basicShortcutIcon: null);
        }

        private void btnExplorerBrowse_Click(object sender, EventArgs e)
        {
            fldBrowser.Description = "Select a folder";
            if (fldBrowser.ShowDialog(this) != DialogResult.OK)
                return;

            txtCustomFolder.Text = fldBrowser.SelectedPath;
            radCustomFolder.Checked = true;
        }
    }
}
