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
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TileIconifier.Controls.Shortcut;
using TileIconifier.Core;
using TileIconifier.Core.Shortcut;
using TileIconifier.Core.TileIconify;
using TileIconifier.Core.Utilities;
using TileIconifier.Forms.Shared;
using TileIconifier.Localization;
using TileIconifier.Properties;
using TileIconifier.Skinning;
using TileIconifier.Skinning.Skins;
using TileIconifier.Utilities;

namespace TileIconifier.Forms.Main
{
    public partial class FrmMain
    {
        public event LocalizationEventHandler LanguageChangedEvent;

        protected virtual void OnLanguageChangedEvent(string newCulture)
        {
            LanguageChangedEvent?.Invoke(this, new LocalizationEventArgs(newCulture));
            Config.Instance.LocaleToUse = newCulture;
            Config.Instance.SaveConfig();
        }

        private void StartFullUpdate()
        {
            FormUtils.DoBackgroundWorkWithSplash(this, FullUpdate, Strings.Refreshing, true);
        }

        private void FullUpdate(object sender, DoWorkEventArgs e)
        {
            if (getPinnedItemsRequiresPowershellToolStripMenuItem.Checked)
            {
                try
                {
                    ShortcutItemListViewItemLibrary.RefreshList(true, true);
                }
                catch (Exception ex)
                {
                    FrmException.ShowExceptionHandler(ex);
                    FormUtils.ShowMessage(this,
                        Strings.PowershellErrorFull,
                        Strings.PowershellFailure, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Invoke(new Action(() => getPinnedItemsRequiresPowershellToolStripMenuItem_Click(this, null)));
                }
            }
            else
            {
                ShortcutItemListViewItemLibrary.RefreshList(true);
            }

            UpdateFilteredList();

            if (srtlstShortcuts.InvokeRequired)
            {
                srtlstShortcuts.Invoke(new Action(BuildListViewContent));
            }
            else
            {
                BuildListViewContent();
            }
        }

        /// <summary>
        ///     Updates the items and the images of the list view.
        /// </summary>
        private void BuildListViewContent()
        {
            BuildListViewImageList();

            srtlstShortcuts.Items.Clear();            
            for (var i = 0; i < _filteredList.Count; i++)
            {
                var shortcutItem = _filteredList[i];
                srtlstShortcuts.Items.Add(shortcutItem);                
                shortcutItem.ImageIndex = i;
            }            

            if (srtlstShortcuts.Items.Count > 0)
            {
                srtlstShortcuts.Items[0].Selected = true;
            }
        }

        /// <summary>
        ///     Updates the images of the list view.
        /// </summary>
        private void BuildListViewImageList()
        {
            ilsShortcutItemsSmallIcons.Images.Clear();
            for (var i = 0; i < _filteredList.Count; i++)
            {
                var shortcutItem = _filteredList[i].ShortcutItem;
                ilsShortcutItemsSmallIcons.Images.Add(shortcutItem.StandardIcon ??
                                          Resources.QuestionMark);
            }
        }

        private void CheckPowershellPinningFromConfig()
        {
            getPinnedItemsRequiresPowershellToolStripMenuItem.Checked = Config.Instance.GetPinnedItems;
        }

        private void UpdatePowershellPinning(bool value)
        {
            Config.Instance.GetPinnedItems = value;
            Config.Instance.SaveConfig();
            CheckPowershellPinningFromConfig();
        }

        private static void CheckMenuItem(ToolStripDropDownItem mnu,
            ToolStripMenuItem checkedItem)
        {
            // Uncheck the menu items except checked item.
            foreach (var menuItem in mnu.DropDownItems.OfType<ToolStripMenuItem>()
                .Select(item => item))
            {
                menuItem.Checked = Equals(menuItem, checkedItem);
            }
        }

        private void UpdateSkin()
        {
            BaseSkin skin = SkinHandler.DefaultSkin;
            if (darkSkinToolStripMenuItem.Checked)
            {
                skin = new DarkSkin();
            }

            try
            {
                Config.Instance.LastSkin = skin.GetType().Name;
                Config.Instance.SaveConfig();
            }
            catch
            {
                //ignore
            }

            SkinHandler.SetCurrentSkin(skin);
        }

        private void UpdateFormControls()
        {
            //set path boxes to value stored in shortcut
            Action<TextBox, string> updateTextBox = (textBox, str) =>
            {
                textBox.Text = str;
                textBox.SelectionStart = txtLnkPath.Text.Length;
                textBox.ScrollToCaret();
            };
            updateTextBox(txtLnkPath, CurrentShortcutItem.ShortcutFileInfo.FullName);
            updateTextBox(txtExePath, CurrentShortcutItem.TargetFilePath);

            //only show remove if the icon is currently iconified
            btnRemove.Enabled = CurrentShortcutItem.IsIconified;

            //only enable Iconify button if shortcut has unsaved changes
            btnIconify.Enabled = CurrentShortcutItem.Properties.HasUnsavedChanges;

            //disable Build Custom Shortcut for items that are already custom shortcuts
            btnBuildCustomShortcut.Enabled = !CurrentShortcutItem.IsTileIconifierCustomShortcut;

            //disable delete Custom Shortcut for items that are already custom shortcuts
            btnDeleteCustomShortcut.Enabled = CurrentShortcutItem.IsTileIconifierCustomShortcut;

            lblBadShortcutWarning.Visible = NotifyIncompatibleShortcut();

            //update the column view
            _currentShortcutListViewItem.UpdateColumns();
            var currentShortcutIndex = srtlstShortcuts.Items.IndexOf(_currentShortcutListViewItem);
            if (currentShortcutIndex >= 0)
            {
                srtlstShortcuts.RedrawItems(
                    currentShortcutIndex,
                    currentShortcutIndex,
                    false);
            }
        }

        private void UpdateShortcut()
        {
            iconifyPanel.CurrentShortcutItem = CurrentShortcutItem;
            iconifyPanel.UpdateControlsToShortcut();
            UpdateFormControls();
        }

        private void JumpToShortcutItem(ShortcutItem shortcutItem)
        {
            UpdateFilteredList(true);
            var shortcutListViewItem =
                srtlstShortcuts.Items.Cast<ShortcutItemListViewItem>().First(
                    s => s.ShortcutItem.ShortcutFileInfo.FullName == shortcutItem.ShortcutFileInfo.FullName);
            var itemInListView = srtlstShortcuts.Items[srtlstShortcuts.Items.IndexOf(shortcutListViewItem)];
            itemInListView.Selected = true;
            itemInListView.EnsureVisible();
        }

        private TileIcon GenerateTileIcon()
        {
            return new TileIcon(CurrentShortcutItem);
        }

        private static async void CheckForUpdates(bool silentIfNoUpdateDetected)
        {
            try
            {
                var updateDetails = await UpdateUtils.CheckForUpdate();

                if (updateDetails.UpdateAvailable)
                {
                    if (FormUtils.ShowMessage(null,
                        string.Format(
                            Strings.UpdateAvailableFull,
                            updateDetails.CurrentVersion, updateDetails.LatestVersion),
                        Strings.NewVersionAvailable,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        defaultButton: MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        UrlUtils.OpenUrlInBrowser("https://github.com/Jonno12345/TileIconifier/releases");
                    }
                }
                else if (!silentIfNoUpdateDetected)
                {
                    FormUtils.ShowMessage(null, Strings.AlreadyLatest, Strings.UpToDate, icon: MessageBoxIcon.Information);
                }
            }
            catch
            {
                if (silentIfNoUpdateDetected)
                {
                    return;
                }

                if (FormUtils.ShowMessage(null,
                    string.Format(
                        Strings.UpdateAvailableError,
                        UpdateUtils
                            .CurrentVersion),
                    Strings.UnableToCheckServer,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation) == DialogResult.OK)
                {
                    UrlUtils.OpenUrlInBrowser("https://github.com/Jonno12345/TileIconifier/releases");
                }
            }
        }

        private void UpdateFilteredList(bool resetTextBox = false)
        {
            if (resetTextBox)
            {
                txtFilter.Text = string.Empty;
            }
            _filteredList =
                ShortcutItemListViewItemLibrary.LibraryAsListViewItems.Where(s => s.Text.ToUpper().Contains(txtFilter.Text.ToUpper()))
                    .ToList();
        }

        private void InitializeListboxColumns()
        {
            srtlstShortcuts.BeginUpdate();
            srtlstShortcuts.Columns.Clear();
            srtlstShortcuts.Columns.Add(Strings.ShortcutName, 0, HorizontalAlignment.Left);
            srtlstShortcuts.Columns.Add(Strings.IsCustom, 0, HorizontalAlignment.Left);
            srtlstShortcuts.Columns.Add(Strings.IsIconified, 0, HorizontalAlignment.Left);
            srtlstShortcuts.Columns.Add(Strings.IsPinned, 0, HorizontalAlignment.Left);
            UpdateListBoxColumnsSize();
            srtlstShortcuts.EndUpdate();
        }

        private void UpdateListBoxColumnsSize()
        {
            //Width of the list box columns in percent. Their sum should be 100.
            const int SRTLSTSHORTCUTS_COLUMN1_WIDTH = 55;
            const int SRTLSTSHORTCUTS_COLUMN2_WIDTH = 15;
            const int SRTLSTSHORTCUTS_COLUMN3_WIDTH = 15;
            const int SRTLSTSHORTCUTS_COLUMN4_WIDTH = 15;

            if (srtlstShortcuts.Columns.Count < 4)
            {
                return;
            }

            int clientWidth = srtlstShortcuts.ClientSize.Width;

            srtlstShortcuts.BeginUpdate();

            srtlstShortcuts.Columns[0].Width = clientWidth * SRTLSTSHORTCUTS_COLUMN1_WIDTH / 100;
            srtlstShortcuts.Columns[1].Width = clientWidth * SRTLSTSHORTCUTS_COLUMN2_WIDTH / 100;
            srtlstShortcuts.Columns[2].Width = clientWidth * SRTLSTSHORTCUTS_COLUMN3_WIDTH / 100;
            srtlstShortcuts.Columns[3].Width = clientWidth * SRTLSTSHORTCUTS_COLUMN4_WIDTH / 100;

            srtlstShortcuts.EndUpdate();
        }

        private void LanguageToolStripMenuClick(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            CheckMenuItem(languageToolStripMenuItem, item);
            UpdateLanguage(item?.Tag.ToString());
        }

        private void UpdateLanguage(string language)
        {
            if (!Thread.CurrentThread.CurrentUICulture.Equals(CultureInfo.GetCultureInfo(language)))
            {
                OnLanguageChangedEvent(language);
            }
        }

        private void SetCurrentLanguage()
        {
            var currentLanguage = Thread.CurrentThread.CurrentUICulture.Name;
            foreach (
                var dropDownItem in
                    languageToolStripMenuItem.DropDownItems.Cast<ToolStripMenuItem>()
                        .Where(dropDownItem => (string)dropDownItem.Tag == currentLanguage))
            {
                CheckMenuItem(languageToolStripMenuItem, dropDownItem);
                return;
            }
            CheckMenuItem(languageToolStripMenuItem, englishToolStripMenuItem);
        }

        private void SetCurrentSkin()
        {
            string skinTag;
            if (SkinHandler.GetCurrentSkin() == SkinHandler.DefaultSkin)
            {
                skinTag = "DefaultSkin";
            }
            else
            {
                skinTag = SkinHandler.GetCurrentSkin().GetType().Name;
            }

            var itemToCheck = skinToolStripMenuItem.DropDownItems.Cast<ToolStripMenuItem>()
                        .SingleOrDefault(dropDownItem => (string)dropDownItem.Tag == skinTag);

            if (itemToCheck != null)
            {
                CheckMenuItem(skinToolStripMenuItem, itemToCheck);
            }
        }

        private bool NotifyIncompatibleShortcut()
        {
            return !_currentShortcutListViewItem.ShortcutItem.IsTileIconifierCustomShortcut
                   && ShortcutConstantsAndEnums.KnownShortcutTargetsWithIssues.Any(s =>
                       _currentShortcutListViewItem.ShortcutItem.TargetFilePath.ToUpper().EndsWith(s.ToUpper()));
        }
    }
}