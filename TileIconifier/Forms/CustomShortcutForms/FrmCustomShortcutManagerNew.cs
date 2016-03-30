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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TileIconifier.Custom;
using TileIconifier.Forms.Shared;
using TileIconifier.Properties;
using TileIconifier.Shortcut;
using TileIconifier.Steam;
using TileIconifier.TileIconify;
using TileIconifier.Utilities;

namespace TileIconifier.Forms.CustomShortcutForms
{
    public partial class FrmCustomShortcutManagerNew : SkinnableForm
    {
        //*********************************************************************
        // EXPLORER RELATED FIELDS
        //*********************************************************************

        private readonly NewCustomShortcutFormCache _explorerCache = new NewCustomShortcutFormCache();

        //*********************************************************************
        // OTHER RELATED FIELDS
        //*********************************************************************

        private readonly NewCustomShortcutFormCache _otherCache = new NewCustomShortcutFormCache();

        //*********************************************************************
        // STEAM RELATED FIELDS
        //*********************************************************************

        private readonly NewCustomShortcutFormCache _steamCache = new NewCustomShortcutFormCache();

        //*********************************************************************
        // GENERAL FIELDS
        //*********************************************************************

        private TabPage _previousTabPage;
        private List<SteamGame> _steamGames;


        //*********************************************************************
        // GENERAL METHODS
        //*********************************************************************

        public FrmCustomShortcutManagerNew()
        {
            InitializeComponent();
            _previousTabPage = tabShortcutType.SelectedTab;
            SetUpExplorer();
            SetUpSteam();
        }

        private NewCustomShortcutFormCache PreviousCache
        {
            get
            {
                if (_previousTabPage == tabExplorer)
                    return _explorerCache;
                if (_previousTabPage == tabSteam)
                    return _steamCache;
                if (_previousTabPage == tabOther)
                    return _otherCache;
                return null;
            }
        }

        private NewCustomShortcutFormCache CurrentCache
        {
            get
            {
                var current = tabShortcutType.SelectedTab;
                if (current == tabExplorer)
                    return _explorerCache;
                if (current == tabSteam)
                    return _steamCache;
                if (current == tabOther)
                    return _otherCache;
                return null;
            }
        }

        private void GenerateFullShortcut(
            string targetPath,
            string targetArguments,
            CustomShortcutType shortcutType,
            string basicShortcutIcon,
            string workingFolder = null
            )
        {
            var shortcutName = txtShortcutName.Text.CleanInvalidFilenameChars();
            byte[] imageToUse;

            try
            {
                ValidateFields(shortcutName, targetPath, out imageToUse);
            }
            catch (ValidationFailureException)
            {
                return;
            }
            //build our new custom shortcut
            var customShortcut = new CustomShortcut(shortcutName, targetPath, targetArguments, shortcutType,
                radShortcutLocation.PathSelection(), basicShortcutIcon, workingFolder);

            //If we didn't specify a shortcut icon path, make one
            if (basicShortcutIcon == null)
                customShortcut.BuildCustomShortcut(pctCurrentIcon.Image);
            else
                customShortcut.BuildCustomShortcut();


            //Iconify a TileIconifier shortcut for this with default settings
            var newShortcutItem = customShortcut.ShortcutItem;
            newShortcutItem.Properties.CurrentState.MediumImage.SetImage(imageToUse,
                ShortcutConstantsAndEnums.MediumShortcutSize);
            newShortcutItem.Properties.CurrentState.SmallImage.SetImage(imageToUse,
                ShortcutConstantsAndEnums.SmallShortcutSize);
            var iconify = new TileIcon(newShortcutItem);
            iconify.RunIconify();

            //confirm to the user the shortcut has been created
            MessageBox.Show(
                $"A shortcut for {shortcutName.QuoteWrap()} has been created in your start menu under TileIconify. The item will need to be pinned manually.",
                @"Shortcut created!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Hacked this in quickly - fix it up.
        private void ValidateFields(string shortcutName, string targetPath, out byte[] imageToUse)
        {
            //Check if there are invalid characters in the shortcut name
            if (txtShortcutName.Text != shortcutName || shortcutName.Length == 0)
            {
                MessageBox.Show(@"Invalid characters or invalid shortcut name!", @"Invalid characters or shortcut name",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                throw new ValidationFailureException();
            }

            if (targetPath.Length == 0)
            {
                MessageBox.Show(@"Target path is empty!", @"Target path empty", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                throw new ValidationFailureException();
            }

            if (pctCurrentIcon.Image == null)
            {
                MessageBox.Show(@"No icon has been selected!", @"Please select an icon", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                throw new ValidationFailureException();
            }

            try
            {
                imageToUse = ImageUtils.ImageToByteArray(pctCurrentIcon.Image);
            }
            catch
            {
                MessageBox.Show(
                    @"An issue occurred with the image selected. Please load the icon again or try another.",
                    @"Icon error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                CurrentCache.SetIconBytes(FrmIconSelector.GetImage(this, CustomShortcutGetters.ExplorerPath));
                pctCurrentIcon.Image = CurrentCache.GetIcon();
            }
            catch (UserCancellationException)
            {
            }
        }

        private void tabShortcutType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //save the previous tab state in it's cache before changing
            //This seems to be the only way to get the tab before it actually changes...?
            SaveCache(PreviousCache);

            _previousTabPage = tabShortcutType.SelectedTab;

            //load the cache of the new tab
            LoadCache(CurrentCache);
        }

        private void LoadCache(NewCustomShortcutFormCache cache)
        {
            pctCurrentIcon.Image = cache.GetIcon();
            txtShortcutName.Text = cache.ShortcutName;
            radShortcutLocation.SetCheckedRadio(cache.AllOrCurrentUser);
        }

        private void SaveCache(NewCustomShortcutFormCache cache)
        {
            cache.ShortcutName = txtShortcutName.Text;
            cache.AllOrCurrentUser = radShortcutLocation.GetCheckedRadio();
        }

        private void UpdatedCacheIcon(byte[] bytesIn)
        {
            CurrentCache.SetIconBytes(bytesIn);
            pctCurrentIcon.Image = CurrentCache.GetIcon();
        }

        //*********************************************************************
        // EXPLORER RELATED METHODS
        //*********************************************************************

        private void SetUpExplorer()
        {
            cmbExplorerGuids.DisplayMember = "Key";
            cmbExplorerGuids.ValueMember = "Value";
            cmbExplorerGuids.DataSource = new BindingSource(CustomShortcutGetters.ExplorerGuids, null);
            pctCurrentIcon.Image = Resources.ExplorerIco.ToBitmap();
        }

        private void radSpecialFolder_CheckedChanged(object sender, EventArgs e)
        {
            txtCustomFolder.Enabled = !radSpecialFolder.Checked;
            cmbExplorerGuids.Enabled = radSpecialFolder.Checked;
        }

        private void GenerateExplorerShortcut()
        {
            var targetArguments = radSpecialFolder.Checked
                ? $"shell:::{cmbExplorerGuids.SelectedValue}"
                : txtCustomFolder.Text;
            var workingFolder = radSpecialFolder.Checked ? null : txtCustomFolder.Text;

            GenerateFullShortcut(CustomShortcutGetters.ExplorerPath, targetArguments, CustomShortcutType.Explorer,
                CustomShortcutGetters.ExplorerPath, workingFolder
                );
        }

        private void cmbExplorerGuids_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbExplorerGuids.SelectedIndex >= 0)
                txtShortcutName.Text = cmbExplorerGuids.Text.CleanInvalidFilenameChars();
        }

        private void btnExplorerBrowse_Click(object sender, EventArgs e)
        {
            fldBrowser.Description = @"Select a folder";
            if (fldBrowser.ShowDialog(this) != DialogResult.OK)
                return;

            txtCustomFolder.Text = fldBrowser.SelectedPath;
            radCustomFolder.Checked = true;
        }

        //*********************************************************************
        // STEAM RELATED METHODS
        //*********************************************************************

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
            var steamDetected = true;
            try
            {
                var steamInstallationPath = SteamLibrary.Instance.GetSteamInstallationFolder();
                txtSteamInstallationPath.Text = $"Steam installation path: {steamInstallationPath}";
            }
            catch (SteamInstallationPathNotFoundException)
            {
                txtSteamInstallationPath.Text = @"Steam installation path not found!!!";
                steamDetected = false;
            }

            try
            {
                var steamExecutable = SteamLibrary.Instance.GetSteamExePath();
                txtSteamExecutablePath.Text = $"Steam executable path: {steamExecutable}";
            }
            catch (SteamExecutableNotFoundException)
            {
                txtSteamExecutablePath.Text = @"Steam executable path not found!!!";
                steamDetected = false;
            }

            try
            {
                var steamLibraryFolders = SteamLibrary.Instance.GetLibraryFolders();
                if (steamLibraryFolders.Count > 0)
                {
                    txtSteamLibraryPaths.Text = $"Steam library folders: {string.Join("; ", steamLibraryFolders)}";
                }
                else
                {
                    txtSteamLibraryPaths.Text = @"Steam library paths not found!!!";
                    steamDetected = false;
                }
            }
            catch
            {
                txtSteamLibraryPaths.Text = @"Steam library paths not found!!!";
                steamDetected = false;
            }

            return steamDetected;
        }

        private void SetUpSteamListBox()
        {
            lstSteamGames.Clear();
            lstSteamGames.Columns.Clear();

            lstSteamGames.Columns.Add("App Id", lstSteamGames.Width/8 - 10, HorizontalAlignment.Left);
            lstSteamGames.Columns.Add("Game Name", lstSteamGames.Width/8*7 - 4, HorizontalAlignment.Left);

            lstSteamGames.Items.AddRange(_steamGames.OrderBy(s => s.GameName).ToArray<ListViewItem>());
        }

        private void LoadSteamGames()
        {
            _steamGames = SteamLibrary.Instance.GetAllSteamGames();
        }

        private void btnInstallationChange_Click(object sender, EventArgs e)
        {
            fldBrowser.Description = @"Select the folder containing your Steam installation";
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

            var steamGame = (SteamGame) lstSteamGames.SelectedItems[0];
            UpdatedCacheIcon(steamGame.IconAsBytes);
            txtShortcutName.Text = steamGame.GameName.CleanInvalidFilenameChars();
        }

        private void GenerateSteamShortcut()
        {
            if (lstSteamGames.SelectedItems.Count == 0) return;

            var steamGame = (SteamGame) lstSteamGames.SelectedItems[0];

            GenerateFullShortcut(SteamLibrary.Instance.GetSteamExePath(), steamGame.GameExecutionArgument,
                CustomShortcutType.Steam, steamGame.IconPath);
        }

        private void btnSteamLibrariesPath_Click(object sender, EventArgs e)
        {
            var isValid = false;
            while (!isValid)
            {
                fldBrowser.Description =
                    @"Select a folder containing your Steam library (This will be a folder containing a ""steamapps"" folder)";

                if (fldBrowser.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    SteamLibrary.Instance.AddLibraryFolder(fldBrowser.SelectedPath);
                    isValid = true;
                }
                catch (SteamLibraryPathNotFoundException)
                {
                    MessageBox.Show(this,
                        @"Selected folder is invalid (valid folders should contain a subfolder named ""steamapps""), please try again or cancel.");
                }
            }
        }

        //*********************************************************************
        // OTHER RELATED METHODS
        //*********************************************************************

        private void btnOtherTargetBrowse_Click(object sender, EventArgs e)
        {
            if (opnOtherTarget.ShowDialog() != DialogResult.OK)
                return;

            var filePath = opnOtherTarget.FileName;
            txtOtherTargetPath.Text = filePath;
            txtShortcutName.Text = Path.GetFileNameWithoutExtension(filePath);

            try
            {
                UpdatedCacheIcon(
                    ImageUtils.ImageToByteArray(new IconExtractor.IconExtractor(filePath).GetIcon(0).ToBitmap()));
            }
            catch
            {
                // ignored
            }
        }

        private void GenerateOtherShortcut()
        {
            GenerateFullShortcut(txtOtherTargetPath.Text, txtOtherShortcutArguments.Text, CustomShortcutType.Other, null);
        }
    }
}