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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TileIconifier.Controls.Custom.Chrome;
using TileIconifier.Controls.Custom.Steam;
using TileIconifier.Controls.Custom.WindowsStoreShellMethod;
using TileIconifier.Core;
using TileIconifier.Core.Custom;
using TileIconifier.Core.Custom.Builder;
using TileIconifier.Core.Custom.Chrome;
using TileIconifier.Core.Custom.Steam;
using TileIconifier.Core.IconExtractor;
using TileIconifier.Core.Shortcut;
using TileIconifier.Core.TileIconify;
using TileIconifier.Core.Utilities;
using TileIconifier.Forms.Shared;
using TileIconifier.Properties;
using TileIconifier.Utilities;

namespace TileIconifier.Forms.CustomShortcutForms
{
    public partial class FrmCustomShortcutManagerNew : SkinnableForm
    {
        private readonly NewCustomShortcutFormCache _chromeCache = new NewCustomShortcutFormCache();
        private readonly NewCustomShortcutFormCache _explorerCache = new NewCustomShortcutFormCache();
        private readonly NewCustomShortcutFormCache _otherCache = new NewCustomShortcutFormCache();
        private readonly NewCustomShortcutFormCache _steamCache = new NewCustomShortcutFormCache();
        private readonly NewCustomShortcutFormCache _uriCache = new NewCustomShortcutFormCache();
        private readonly NewCustomShortcutFormCache _windowsStoreCache = new NewCustomShortcutFormCache();
        private TabPage _previousTabPage;


        //*********************************************************************
        // GENERAL METHODS
        //*********************************************************************

        public FrmCustomShortcutManagerNew()
        {
            InitializeComponent();
            _previousTabPage = tabShortcutType.SelectedTab;
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
                if (_previousTabPage == tabWindowsStore)
                    return _windowsStoreCache;
                if (_previousTabPage == tabChromeApps)
                    return _chromeCache;
                if (_previousTabPage == tabURI)
                    return _uriCache;
                return null;
            }
        }

        private NewCustomShortcutFormCache CurrentCache
        {
            get
            {
                var currentTab = tabShortcutType.SelectedTab;
                if (currentTab == tabExplorer)
                    return _explorerCache;
                if (currentTab == tabSteam)
                    return _steamCache;
                if (currentTab == tabOther)
                    return _otherCache;
                if (currentTab == tabWindowsStore)
                    return _windowsStoreCache;
                if (currentTab == tabChromeApps)
                    return _chromeCache;
                if (currentTab == tabURI)
                    return _uriCache;
                return null;
            }
        }

        private void FrmCustomShortcutManagerNew_Shown(object sender, EventArgs e)
        {            
            FormUtils.DoBackgroundWorkWithSplash(this, FullUpdate, Strings.Loading);
            LoadCache(CurrentCache);
        }

        private void FullUpdate(object sender, DoWorkEventArgs e)
        {
            RefreshLibraries();
            PopulateAllTabs();
        }

        private void FullUpdateForceRefresh(object sender, DoWorkEventArgs e)
        {
            RefreshLibraries(true);
            PopulateAllTabs();
        }

        private static void RefreshLibraries(bool force = false)
        {
            SteamGameListViewItemLibrary.RefreshList(force);
            ChromeAppListViewItemLibrary.RefreshList(force);
            WindowsStoreAppListViewItemLibrary.RefreshList(force);
        }

        private void PopulateAllTabs()
        {
            Invoke(new Action(() =>
            {
                try
                {
                    SetUpExplorer();
                    SetUpSteam();
                    SetUpChrome();
                    SetUpWindowsStore();
                }
                catch (Exception ex)
                {
                    var frmException = new FrmException(ex);
                    frmException.ShowDialog();
                }
            }));
        }


        private void GenerateShortcut(BaseCustomShortcutBuilder baseCustomShortcutBuilder)
        {
            var shortcutName = txtShortcutName.Text.CleanInvalidFilenameChars();

            try
            {
                ValidateFields(shortcutName, baseCustomShortcutBuilder.Parameters.ShortcutTarget);
            }
            catch (ValidationFailureException)
            {
                return;
            }

            try
            {
                var customShortcut = baseCustomShortcutBuilder.GenerateCustomShortcut(shortcutName);

                BuildIconifiedTile(ImageUtils.ImageToByteArray(pctCurrentIcon.Image), customShortcut);

                //confirm to the user the shortcut has been created
                ConfirmToUser(this ,shortcutName);
            }
            catch (FileNotFoundException ex)
            {
                FormUtils.ShowMessage(this, ex.Message, Strings.FileCouldNotBeFound,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                FrmException.ShowExceptionHandler(ex);
            }
        }

        private GenerateCustomShortcutParams GenerateParams(string shortcutTarget, string shortcutArguments,
            bool useImage = false)
        {
            return new GenerateCustomShortcutParams(shortcutTarget, shortcutArguments,
                radShortcutLocation.PathSelection())
            {
                Image = useImage ? pctCurrentIcon.Image : null
            };
        }

        private static void BuildIconifiedTile(byte[] imageToUse, CustomShortcut customShortcut)
        {
            //Iconify a TileIconifier shortcut for this with default settings
            var newShortcutItem = customShortcut.ShortcutItem;
            newShortcutItem.Properties.CurrentState.MediumImage.SetImage(imageToUse,
                ShortcutConstantsAndEnums.MediumShortcutDisplaySize);
            newShortcutItem.Properties.CurrentState.SmallImage.SetImage(imageToUse,
                ShortcutConstantsAndEnums.SmallShortcutDisplaySize);
            var iconify = new TileIcon(newShortcutItem);
            iconify.RunIconify();
        }

        private static void ConfirmToUser(IWin32Window dialogOwner, string shortcutName)
        {
            FormUtils.ShowMessage(dialogOwner,
                string.Format(
                    Strings.ShortcutCreatedNeedsPinning,
                    shortcutName.QuoteWrap()),
                Strings.ShortcutCreated, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Hacked this in quickly - fix it up.
        private void ValidateFields(string shortcutName, string targetPath)
        {
            //Check if there are invalid characters in the shortcut name
            if (txtShortcutName.Text != shortcutName || shortcutName.Length == 0)
            {
                FormUtils.ShowMessage(this, Strings.InvalidCharactersOrInvalidShortcutName,
                    Strings.InvalidCharactersOrInvalidShortcutName,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                throw new ValidationFailureException();
            }

            if (targetPath.Length == 0)
            {
                FormUtils.ShowMessage(this, Strings.TargetPathIsEmpty, Strings.TargetPathIsEmpty, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                throw new ValidationFailureException();
            }

            if (pctCurrentIcon.Image == null)
            {
                FormUtils.ShowMessage(this, Strings.NoIconHasBeenSelected, Strings.PleaseSelectAnIcon, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                throw new ValidationFailureException();
            }

            try
            {
                ImageUtils.ImageToByteArray(pctCurrentIcon.Image);
            }
            catch
            {
                FormUtils.ShowMessage(this,
                    Strings.IssueWithImageSelected,
                    Strings.IconError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            else if (tabShortcutType.SelectedTab == tabChromeApps)
            {
                GenerateChromeAppShortcut();
            }
            else if (tabShortcutType.SelectedTab == tabWindowsStore)
            {
                GenerateWindowsStoreAppShortcut();
            }
            else if (tabShortcutType.SelectedTab == tabURI)
            {
                GenerateUriShortcut();
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
                CurrentCache.SetIconBytes(FrmIconSelector.GetImage(this, CustomShortcutGetters.ExplorerPath).ImageBytes);
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

            RebuildAllListBoxColumns();
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

        private void UpdateCacheIcon(byte[] bytesIn)
        {
            CurrentCache.SetIconBytes(bytesIn);
            pctCurrentIcon.Image = CurrentCache.GetIcon();
        }

        private void FrmCustomShortcutManagerNew_Resize(object sender, EventArgs e)
        {
            RebuildAllListBoxColumns();
        }

        private void RebuildAllListBoxColumns()
        {
            BuildChromeListBoxColumns();
            BuildSteamListBoxColumns();
            BuildWindowsStoreListBoxColumns();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUtils.DoBackgroundWorkWithSplash(this, FullUpdateForceRefresh, Strings.Loading);
        }

        #region URI Shortcut Methods

        //*********************************************************************
        // URI RELATED METHODS
        //*********************************************************************

        private void GenerateUriShortcut()
        {
            var uriString = txtUriString.Text;

            var parameters =
                GenerateParams(uriString, string.Empty, true);

            GenerateShortcut(new UriCustomShortcutBuilder(parameters));
        }

        #endregion

        #region Explorer Methods

        //*********************************************************************
        // EXPLORER RELATED METHODS
        //*********************************************************************

        private void SetUpExplorer()
        {
            if (cmbExplorerGuids.DataSource != null) return;
            cmbExplorerGuids.DisplayMember = "Key";
            cmbExplorerGuids.ValueMember = "Value";
            cmbExplorerGuids.DataSource = new BindingSource(CustomShortcutGetters.ExplorerGuids, null);
            _explorerCache.SetIconBytes(ImageUtils.ImageToByteArray(Resources.ExplorerIco.ToBitmap()));
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

            var parameters =
                GenerateParams(CustomShortcutGetters.ExplorerPath, targetArguments);
            parameters.WorkingFolder = workingFolder;
            parameters.IconPath = CustomShortcutGetters.ExplorerPath;

            GenerateShortcut(new ExplorerCustomShortcutBuilder(parameters));
        }

        private void cmbExplorerGuids_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbExplorerGuids.SelectedIndex >= 0)
                txtShortcutName.Text = cmbExplorerGuids.Text.CleanInvalidFilenameChars();
        }

        private void btnExplorerBrowse_Click(object sender, EventArgs e)
        {
            fldBrowser.Description = Strings.SelectAFolder;
            if (fldBrowser.ShowDialog(this) != DialogResult.OK)
                return;

            txtCustomFolder.Text = fldBrowser.SelectedPath;
            radCustomFolder.Checked = true;
        }

        #endregion

        #region Steam Methods

        //*********************************************************************
        // STEAM RELATED METHODS
        //*********************************************************************

        private void SetUpSteam()
        {
            txtSteamLibraryPaths.Text = SteamGameListViewItemLibrary.SteamLibraryPathsResolvedString();
            txtSteamExecutablePath.Text = SteamGameListViewItemLibrary.SteamExecutablePathResolvedString();
            txtSteamInstallationPath.Text = SteamGameListViewItemLibrary.SteamInstallationPathResolvedString();

            lstSteamGames.Clear();
            BuildSteamListBoxColumns();
            lstSteamGames.Items.AddRange(
                SteamGameListViewItemLibrary.LibraryAsListViewItems.OrderBy(s => s.SteamGameItem.GameName)
                    .ToArray<ListViewItem>());
        }

        private void BuildSteamListBoxColumns()
        {
            lstSteamGames.Columns.Clear();

            lstSteamGames.Columns.Add(Strings.AppId, lstSteamGames.Width/7, HorizontalAlignment.Left);
            lstSteamGames.Columns.Add(Strings.GameName, lstSteamGames.Width/7*6 + 3, HorizontalAlignment.Left);
        }

        private void btnInstallationChange_Click(object sender, EventArgs e)
        {
            fldBrowser.Description = Strings.SelectSteamInstallationPath;
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

            var steamGame = ((SteamGameListViewItem) lstSteamGames.SelectedItems[0]).SteamGameItem;
            UpdateCacheIcon(steamGame.IconAsBytes);
            txtShortcutName.Text = steamGame.GameName.CleanInvalidFilenameChars();
        }

        private void GenerateSteamShortcut()
        {
            if (lstSteamGames.SelectedItems.Count == 0) return;

            var steamGame = ((SteamGameListViewItem) lstSteamGames.SelectedItems[0]).SteamGameItem;

            var parameters =
                GenerateParams(SteamLibrary.Instance.GetSteamExePath(), steamGame.GameExecutionArgument);
            parameters.IconPath = steamGame.IconPath;

            GenerateShortcut(new SteamCustomShortcutBuilder(parameters));
        }

        private void btnSteamLibrariesPath_Click(object sender, EventArgs e)
        {
            var isValid = false;
            while (!isValid)
            {
                fldBrowser.Description =
                    Strings.SelectSteamLibraryPath;

                if (fldBrowser.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    SteamLibrary.Instance.AddLibraryFolder(fldBrowser.SelectedPath);
                    isValid = true;
                }
                catch (SteamLibraryPathNotFoundException)
                {
                    FormUtils.ShowMessage(this,
                        Strings.InvalidSteamLibraryPath);
                }
            }
            SetUpSteam();
        }

        #endregion

        #region Chrome Methods

        //*********************************************************************
        // CHROME RELATED METHODS
        //*********************************************************************

        private void SetUpChrome()
        {
            PopulateChromeInstallationPath();
            PopulateGoogleAppLibraryPath();

            SetUpChromeListView();
        }

        private void PopulateGoogleAppLibraryPath()
        {
            txtChromeAppPath.Text = ChromeAppLibrary.ChromeAppLibraryPathExists()
                ? ChromeAppLibrary.AppLibraryPath
                : $"{Strings.PathNotFound}: {ChromeAppLibrary.AppLibraryPath}";
        }

        private void PopulateChromeInstallationPath()
        {
            txtChromeExePath.Text = ChromeAppLibrary.ChromeInstallationPathExists()
                ? ChromeAppLibrary.ChromeInstallationPath
                : Strings.UnableToFindChromeInstallationPath;
        }

        private void SetUpChromeListView()
        {
            lstChromeAppItems.Clear();

            BuildChromeListBoxColumns();

            lstChromeAppItems.Items.AddRange(
                ChromeAppListViewItemLibrary.LibraryAsListViewItems.OrderBy(a => a.ChromeAppItem.AppName)
                    .ToArray<ListViewItem>());
        }

        private void BuildChromeListBoxColumns()
        {
            lstChromeAppItems.Columns.Clear();

            lstChromeAppItems.Columns.Add(Strings.AppId, lstSteamGames.Width/8*3, HorizontalAlignment.Left);
            lstChromeAppItems.Columns.Add(Strings.AppName, lstSteamGames.Width/8*5 + 3, HorizontalAlignment.Left);
        }

        private void lstChromeAppItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstChromeAppItems.SelectedItems.Count == 0)
                return;

            var chromeApp = ((ChromeAppListViewItem) lstChromeAppItems.SelectedItems[0]).ChromeAppItem;
            UpdateCacheIcon(chromeApp.IconAsBytes);
            txtShortcutName.Text = chromeApp.AppName.CleanInvalidFilenameChars();
        }

        private void GenerateChromeAppShortcut()
        {
            if (lstChromeAppItems.SelectedItems.Count == 0) return;

            var chromeApp = ((ChromeAppListViewItem) lstChromeAppItems.SelectedItems[0]).ChromeAppItem;

            var parameters =
                GenerateParams(txtChromeExePath.Text, chromeApp.ChromeAppExecutionArgument, true);

            GenerateShortcut(new ChromeCustomShortcutBuilder(parameters));
        }

        private void btnChromeExePathChange_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtChromeExePath.Text))
                opnChromeExe.InitialDirectory = new FileInfo(txtChromeExePath.Text).Directory?.FullName;

            if (opnChromeExe.ShowDialog(this) != DialogResult.OK)
                return;

            ChromeAppLibrary.ChromeInstallationPath = opnChromeExe.FileName;
            PopulateChromeInstallationPath();
        }

        private void btnChromeAppPathChange_Click(object sender, EventArgs e)
        {
            fldBrowser.Description = Strings.ChromeExtensionsFolder;
            if (fldBrowser.ShowDialog(this) != DialogResult.OK)
                return;

            ChromeAppLibrary.AppLibraryPath = fldBrowser.SelectedPath;
            PopulateGoogleAppLibraryPath();
        }

        #endregion

        #region Windows Store Methods

        //*********************************************************************
        // WINDOWS STORE RELATED METHODS
        //*********************************************************************


        private void SetUpWindowsStore()
        {
            try
            {
                SetUpWindowsStoreListView();
            }
            catch (WindowsStoreRegistryKeyNotFoundException)
            {
                // handle error
            }
        }

        private void SetUpWindowsStoreListView()
        {
            lstWindowsStoreApps.Clear();
            BuildWindowsStoreListBoxColumns();
            try
            {
                lstWindowsStoreApps.Items.AddRange(
                    WindowsStoreAppListViewItemLibrary.LibraryAsListViewItems.OrderBy(w => w.Text)
                        .ToArray<ListViewItem>());
            }
            catch (Exception ex)
            {
                FrmException.ShowExceptionHandler(ex);
            }

        }

        private void BuildWindowsStoreListBoxColumns()
        {
            lstWindowsStoreApps.Columns.Clear();

            lstWindowsStoreApps.Columns.Add(Strings.AppName, lstWindowsStoreApps.Width);
        }

        private void lstWindowsStoreApps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstWindowsStoreApps.SelectedItems.Count == 0) return;

            var windowsStoreAppListViewItem = (WindowsStoreAppListViewItemGroup) lstWindowsStoreApps.SelectedItems[0];

            txtShortcutName.Text = windowsStoreAppListViewItem.Text;
            UpdateCacheIcon(ImageUtils.LoadFileToByteArray(windowsStoreAppListViewItem.WindowsStoreApp.LogoPath));
        }

        private void GenerateWindowsStoreAppShortcut()
        {
            if (lstWindowsStoreApps.SelectedItems.Count == 0)
                return;

            var selectedItem = (WindowsStoreAppListViewItemGroup) lstWindowsStoreApps.SelectedItems[0];

            var parameters =
                GenerateParams(CustomShortcutGetters.ExplorerPath,
                    $@"shell:AppsFolder\{selectedItem.WindowsStoreApp.AppUserModelId}", true);
            parameters.WindowType = WindowType.Hidden;

            GenerateShortcut(new WindowsStoreAppCustomShortcutBuilder(parameters));
        }

        #endregion

        #region Other Shortcut Methods

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
                UpdateCacheIcon(
                    ImageUtils.ImageToByteArray(new IconExtractor(filePath).GetIcon(0).ToBitmap()));
            }
            catch
            {
                // ignored
            }
        }

        private void GenerateOtherShortcut()
        {
            var parameters =
                GenerateParams(txtOtherTargetPath.Text, txtOtherShortcutArguments.Text, true);

            GenerateShortcut(new OtherCustomShortcutBuilder(parameters));
        }

        #endregion
    }
}