using TileIconifier.Controls;

namespace TileIconifier.Forms.CustomShortcutForms
{
    partial class FrmCustomShortcutManagerNew
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCustomShortcutManagerNew));
            this.fldBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.opnSteamExe = new System.Windows.Forms.OpenFileDialog();
            this.opnOtherTarget = new System.Windows.Forms.OpenFileDialog();
            this.opnChromeExe = new System.Windows.Forms.OpenFileDialog();
            this.radShortcutLocation = new TileIconifier.Controls.AllOrCurrentUserRadios();
            this.txtShortcutName = new TileIconifier.Controls.SkinnableTextBox();
            this.lblShortcutName = new System.Windows.Forms.Label();
            this.btnGenerateShortcut = new TileIconifier.Controls.SkinnableButton();
            this.lblCurrentIcon = new System.Windows.Forms.Label();
            this.pctCurrentIcon = new System.Windows.Forms.PictureBox();
            this.tabShortcutType = new TileIconifier.Controls.SkinnableTabControl();
            this.tabExplorer = new System.Windows.Forms.TabPage();
            this.tlpExplorer = new System.Windows.Forms.TableLayoutPanel();
            this.btnExplorerBrowse = new TileIconifier.Controls.SkinnableButton();
            this.radSpecialFolder = new TileIconifier.Controls.SkinnableRadioButton();
            this.txtCustomFolder = new TileIconifier.Controls.SkinnableTextBox();
            this.radCustomFolder = new TileIconifier.Controls.SkinnableRadioButton();
            this.cmbExplorerGuids = new TileIconifier.Controls.SkinnableComboBox();
            this.tabSteam = new System.Windows.Forms.TabPage();
            this.tlpSteam = new System.Windows.Forms.TableLayoutPanel();
            this.txtSteamInstallationPath = new TileIconifier.Controls.SkinnableTextBox();
            this.lstSteamGames = new TileIconifier.Controls.SortableListView();
            this.btnSteamLibrariesPath = new TileIconifier.Controls.SkinnableButton();
            this.btnSteamInstallationChange = new TileIconifier.Controls.SkinnableButton();
            this.txtSteamLibraryPaths = new TileIconifier.Controls.SkinnableTextBox();
            this.btnSteamExeChange = new TileIconifier.Controls.SkinnableButton();
            this.txtSteamExecutablePath = new TileIconifier.Controls.SkinnableTextBox();
            this.tabChromeApps = new System.Windows.Forms.TabPage();
            this.tlpChromeApps = new System.Windows.Forms.TableLayoutPanel();
            this.txtChromeExePath = new TileIconifier.Controls.SkinnableTextBox();
            this.lstChromeAppItems = new TileIconifier.Controls.SortableListView();
            this.txtChromeAppPath = new TileIconifier.Controls.SkinnableTextBox();
            this.btnChromeAppPathChange = new TileIconifier.Controls.SkinnableButton();
            this.btnChromeExePathChange = new TileIconifier.Controls.SkinnableButton();
            this.tabWindowsStore = new System.Windows.Forms.TabPage();
            this.tlpWindowsStore = new System.Windows.Forms.TableLayoutPanel();
            this.lstWindowsStoreApps = new TileIconifier.Controls.SortableListView();
            this.tabURI = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblUriExplain = new System.Windows.Forms.Label();
            this.txtUriString = new TileIconifier.Controls.SkinnableTextBox();
            this.lblUriString = new System.Windows.Forms.Label();
            this.tabOther = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblOtherTargetPath = new System.Windows.Forms.Label();
            this.txtOtherShortcutArguments = new TileIconifier.Controls.SkinnableTextBox();
            this.lblOtherShortcutArguments = new System.Windows.Forms.Label();
            this.txtOtherTargetPath = new TileIconifier.Controls.SkinnableTextBox();
            this.btnOtherTargetBrowse = new TileIconifier.Controls.SkinnableButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pctCurrentIcon)).BeginInit();
            this.tabShortcutType.SuspendLayout();
            this.tabExplorer.SuspendLayout();
            this.tlpExplorer.SuspendLayout();
            this.tabSteam.SuspendLayout();
            this.tlpSteam.SuspendLayout();
            this.tabChromeApps.SuspendLayout();
            this.tlpChromeApps.SuspendLayout();
            this.tabWindowsStore.SuspendLayout();
            this.tlpWindowsStore.SuspendLayout();
            this.tabURI.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabOther.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fldBrowser
            // 
            this.fldBrowser.ShowNewFolderButton = false;
            // 
            // opnSteamExe
            // 
            this.opnSteamExe.FileName = "Steam.exe";
            resources.ApplyResources(this.opnSteamExe, "opnSteamExe");
            // 
            // opnOtherTarget
            // 
            resources.ApplyResources(this.opnOtherTarget, "opnOtherTarget");
            // 
            // opnChromeExe
            // 
            this.opnChromeExe.FileName = "chrome.exe";
            resources.ApplyResources(this.opnChromeExe, "opnChromeExe");
            // 
            // radShortcutLocation
            // 
            resources.ApplyResources(this.radShortcutLocation, "radShortcutLocation");
            this.radShortcutLocation.Name = "radShortcutLocation";
            // 
            // txtShortcutName
            // 
            resources.ApplyResources(this.txtShortcutName, "txtShortcutName");
            this.txtShortcutName.Name = "txtShortcutName";
            // 
            // lblShortcutName
            // 
            resources.ApplyResources(this.lblShortcutName, "lblShortcutName");
            this.lblShortcutName.Name = "lblShortcutName";
            // 
            // btnGenerateShortcut
            // 
            resources.ApplyResources(this.btnGenerateShortcut, "btnGenerateShortcut");
            this.btnGenerateShortcut.Name = "btnGenerateShortcut";
            this.btnGenerateShortcut.UseVisualStyleBackColor = true;
            this.btnGenerateShortcut.Click += new System.EventHandler(this.btnGenerateShortcut_Click);
            // 
            // lblCurrentIcon
            // 
            resources.ApplyResources(this.lblCurrentIcon, "lblCurrentIcon");
            this.lblCurrentIcon.Name = "lblCurrentIcon";
            // 
            // pctCurrentIcon
            // 
            resources.ApplyResources(this.pctCurrentIcon, "pctCurrentIcon");
            this.pctCurrentIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctCurrentIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctCurrentIcon.Name = "pctCurrentIcon";
            this.pctCurrentIcon.TabStop = false;
            this.pctCurrentIcon.Click += new System.EventHandler(this.pctCurrentIcon_Click);
            // 
            // tabShortcutType
            // 
            resources.ApplyResources(this.tabShortcutType, "tabShortcutType");
            this.tabShortcutType.Controls.Add(this.tabExplorer);
            this.tabShortcutType.Controls.Add(this.tabSteam);
            this.tabShortcutType.Controls.Add(this.tabChromeApps);
            this.tabShortcutType.Controls.Add(this.tabWindowsStore);
            this.tabShortcutType.Controls.Add(this.tabURI);
            this.tabShortcutType.Controls.Add(this.tabOther);
            this.tabShortcutType.Name = "tabShortcutType";
            this.tabShortcutType.SelectedIndex = 0;
            this.tabShortcutType.SelectedIndexChanged += new System.EventHandler(this.tabShortcutType_SelectedIndexChanged);
            // 
            // tabExplorer
            // 
            this.tabExplorer.Controls.Add(this.tlpExplorer);
            resources.ApplyResources(this.tabExplorer, "tabExplorer");
            this.tabExplorer.Name = "tabExplorer";
            this.tabExplorer.UseVisualStyleBackColor = true;
            // 
            // tlpExplorer
            // 
            resources.ApplyResources(this.tlpExplorer, "tlpExplorer");
            this.tlpExplorer.Controls.Add(this.btnExplorerBrowse, 2, 1);
            this.tlpExplorer.Controls.Add(this.radSpecialFolder, 0, 0);
            this.tlpExplorer.Controls.Add(this.txtCustomFolder, 1, 1);
            this.tlpExplorer.Controls.Add(this.radCustomFolder, 0, 1);
            this.tlpExplorer.Controls.Add(this.cmbExplorerGuids, 1, 0);
            this.tlpExplorer.Name = "tlpExplorer";
            // 
            // btnExplorerBrowse
            // 
            resources.ApplyResources(this.btnExplorerBrowse, "btnExplorerBrowse");
            this.btnExplorerBrowse.FlatAppearance.BorderSize = 0;
            this.btnExplorerBrowse.Name = "btnExplorerBrowse";
            this.btnExplorerBrowse.UseVisualStyleBackColor = true;
            this.btnExplorerBrowse.Click += new System.EventHandler(this.btnExplorerBrowse_Click);
            // 
            // radSpecialFolder
            // 
            resources.ApplyResources(this.radSpecialFolder, "radSpecialFolder");
            this.radSpecialFolder.Checked = true;
            this.radSpecialFolder.Name = "radSpecialFolder";
            this.radSpecialFolder.TabStop = true;
            this.radSpecialFolder.UseVisualStyleBackColor = true;
            this.radSpecialFolder.CheckedChanged += new System.EventHandler(this.radSpecialFolder_CheckedChanged);
            // 
            // txtCustomFolder
            // 
            resources.ApplyResources(this.txtCustomFolder, "txtCustomFolder");
            this.txtCustomFolder.Name = "txtCustomFolder";
            // 
            // radCustomFolder
            // 
            resources.ApplyResources(this.radCustomFolder, "radCustomFolder");
            this.radCustomFolder.Name = "radCustomFolder";
            this.radCustomFolder.UseVisualStyleBackColor = true;
            // 
            // cmbExplorerGuids
            // 
            resources.ApplyResources(this.cmbExplorerGuids, "cmbExplorerGuids");
            this.tlpExplorer.SetColumnSpan(this.cmbExplorerGuids, 2);
            this.cmbExplorerGuids.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExplorerGuids.FormattingEnabled = true;
            this.cmbExplorerGuids.Name = "cmbExplorerGuids";
            this.cmbExplorerGuids.SelectedIndexChanged += new System.EventHandler(this.cmbExplorerGuids_SelectedIndexChanged);
            // 
            // tabSteam
            // 
            this.tabSteam.Controls.Add(this.tlpSteam);
            resources.ApplyResources(this.tabSteam, "tabSteam");
            this.tabSteam.Name = "tabSteam";
            this.tabSteam.UseVisualStyleBackColor = true;
            // 
            // tlpSteam
            // 
            resources.ApplyResources(this.tlpSteam, "tlpSteam");
            this.tlpSteam.Controls.Add(this.txtSteamInstallationPath, 0, 0);
            this.tlpSteam.Controls.Add(this.lstSteamGames, 0, 3);
            this.tlpSteam.Controls.Add(this.btnSteamLibrariesPath, 1, 2);
            this.tlpSteam.Controls.Add(this.btnSteamInstallationChange, 1, 0);
            this.tlpSteam.Controls.Add(this.txtSteamLibraryPaths, 0, 2);
            this.tlpSteam.Controls.Add(this.btnSteamExeChange, 1, 1);
            this.tlpSteam.Controls.Add(this.txtSteamExecutablePath, 0, 1);
            this.tlpSteam.Name = "tlpSteam";
            // 
            // txtSteamInstallationPath
            // 
            resources.ApplyResources(this.txtSteamInstallationPath, "txtSteamInstallationPath");
            this.txtSteamInstallationPath.Name = "txtSteamInstallationPath";
            this.txtSteamInstallationPath.ReadOnly = true;
            // 
            // lstSteamGames
            // 
            this.tlpSteam.SetColumnSpan(this.lstSteamGames, 2);
            resources.ApplyResources(this.lstSteamGames, "lstSteamGames");
            this.lstSteamGames.FullRowSelect = true;
            this.lstSteamGames.HideSelection = false;
            this.lstSteamGames.MultiSelect = false;
            this.lstSteamGames.Name = "lstSteamGames";
            this.lstSteamGames.UseCompatibleStateImageBehavior = false;
            this.lstSteamGames.View = System.Windows.Forms.View.Details;
            this.lstSteamGames.SelectedIndexChanged += new System.EventHandler(this.lstSteamGames_SelectedIndexChanged);
            // 
            // btnSteamLibrariesPath
            // 
            resources.ApplyResources(this.btnSteamLibrariesPath, "btnSteamLibrariesPath");
            this.btnSteamLibrariesPath.FlatAppearance.BorderSize = 0;
            this.btnSteamLibrariesPath.Name = "btnSteamLibrariesPath";
            this.btnSteamLibrariesPath.UseVisualStyleBackColor = true;
            this.btnSteamLibrariesPath.Click += new System.EventHandler(this.btnSteamLibrariesPath_Click);
            // 
            // btnSteamInstallationChange
            // 
            resources.ApplyResources(this.btnSteamInstallationChange, "btnSteamInstallationChange");
            this.btnSteamInstallationChange.FlatAppearance.BorderSize = 0;
            this.btnSteamInstallationChange.Name = "btnSteamInstallationChange";
            this.btnSteamInstallationChange.UseVisualStyleBackColor = true;
            this.btnSteamInstallationChange.Click += new System.EventHandler(this.btnInstallationChange_Click);
            // 
            // txtSteamLibraryPaths
            // 
            resources.ApplyResources(this.txtSteamLibraryPaths, "txtSteamLibraryPaths");
            this.txtSteamLibraryPaths.Name = "txtSteamLibraryPaths";
            this.txtSteamLibraryPaths.ReadOnly = true;
            // 
            // btnSteamExeChange
            // 
            resources.ApplyResources(this.btnSteamExeChange, "btnSteamExeChange");
            this.btnSteamExeChange.FlatAppearance.BorderSize = 0;
            this.btnSteamExeChange.Name = "btnSteamExeChange";
            this.btnSteamExeChange.UseVisualStyleBackColor = true;
            this.btnSteamExeChange.Click += new System.EventHandler(this.btnSteamExeChange_Click);
            // 
            // txtSteamExecutablePath
            // 
            resources.ApplyResources(this.txtSteamExecutablePath, "txtSteamExecutablePath");
            this.txtSteamExecutablePath.Name = "txtSteamExecutablePath";
            this.txtSteamExecutablePath.ReadOnly = true;
            // 
            // tabChromeApps
            // 
            this.tabChromeApps.Controls.Add(this.tlpChromeApps);
            resources.ApplyResources(this.tabChromeApps, "tabChromeApps");
            this.tabChromeApps.Name = "tabChromeApps";
            this.tabChromeApps.UseVisualStyleBackColor = true;
            // 
            // tlpChromeApps
            // 
            resources.ApplyResources(this.tlpChromeApps, "tlpChromeApps");
            this.tlpChromeApps.Controls.Add(this.txtChromeExePath, 0, 0);
            this.tlpChromeApps.Controls.Add(this.lstChromeAppItems, 0, 2);
            this.tlpChromeApps.Controls.Add(this.txtChromeAppPath, 0, 1);
            this.tlpChromeApps.Controls.Add(this.btnChromeAppPathChange, 1, 1);
            this.tlpChromeApps.Controls.Add(this.btnChromeExePathChange, 1, 0);
            this.tlpChromeApps.Name = "tlpChromeApps";
            // 
            // txtChromeExePath
            // 
            resources.ApplyResources(this.txtChromeExePath, "txtChromeExePath");
            this.txtChromeExePath.Name = "txtChromeExePath";
            this.txtChromeExePath.ReadOnly = true;
            // 
            // lstChromeAppItems
            // 
            this.tlpChromeApps.SetColumnSpan(this.lstChromeAppItems, 2);
            resources.ApplyResources(this.lstChromeAppItems, "lstChromeAppItems");
            this.lstChromeAppItems.FullRowSelect = true;
            this.lstChromeAppItems.HideSelection = false;
            this.lstChromeAppItems.MultiSelect = false;
            this.lstChromeAppItems.Name = "lstChromeAppItems";
            this.lstChromeAppItems.UseCompatibleStateImageBehavior = false;
            this.lstChromeAppItems.View = System.Windows.Forms.View.Details;
            this.lstChromeAppItems.SelectedIndexChanged += new System.EventHandler(this.lstChromeAppItems_SelectedIndexChanged);
            // 
            // txtChromeAppPath
            // 
            resources.ApplyResources(this.txtChromeAppPath, "txtChromeAppPath");
            this.txtChromeAppPath.Name = "txtChromeAppPath";
            this.txtChromeAppPath.ReadOnly = true;
            // 
            // btnChromeAppPathChange
            // 
            resources.ApplyResources(this.btnChromeAppPathChange, "btnChromeAppPathChange");
            this.btnChromeAppPathChange.FlatAppearance.BorderSize = 0;
            this.btnChromeAppPathChange.Name = "btnChromeAppPathChange";
            this.btnChromeAppPathChange.UseVisualStyleBackColor = true;
            this.btnChromeAppPathChange.Click += new System.EventHandler(this.btnChromeAppPathChange_Click);
            // 
            // btnChromeExePathChange
            // 
            resources.ApplyResources(this.btnChromeExePathChange, "btnChromeExePathChange");
            this.btnChromeExePathChange.FlatAppearance.BorderSize = 0;
            this.btnChromeExePathChange.Name = "btnChromeExePathChange";
            this.btnChromeExePathChange.UseVisualStyleBackColor = true;
            this.btnChromeExePathChange.Click += new System.EventHandler(this.btnChromeExePathChange_Click);
            // 
            // tabWindowsStore
            // 
            this.tabWindowsStore.Controls.Add(this.tlpWindowsStore);
            resources.ApplyResources(this.tabWindowsStore, "tabWindowsStore");
            this.tabWindowsStore.Name = "tabWindowsStore";
            this.tabWindowsStore.UseVisualStyleBackColor = true;
            // 
            // tlpWindowsStore
            // 
            resources.ApplyResources(this.tlpWindowsStore, "tlpWindowsStore");
            this.tlpWindowsStore.Controls.Add(this.lstWindowsStoreApps, 0, 0);
            this.tlpWindowsStore.Name = "tlpWindowsStore";
            // 
            // lstWindowsStoreApps
            // 
            resources.ApplyResources(this.lstWindowsStoreApps, "lstWindowsStoreApps");
            this.lstWindowsStoreApps.FullRowSelect = true;
            this.lstWindowsStoreApps.HideSelection = false;
            this.lstWindowsStoreApps.MultiSelect = false;
            this.lstWindowsStoreApps.Name = "lstWindowsStoreApps";
            this.lstWindowsStoreApps.UseCompatibleStateImageBehavior = false;
            this.lstWindowsStoreApps.View = System.Windows.Forms.View.Details;
            this.lstWindowsStoreApps.SelectedIndexChanged += new System.EventHandler(this.lstWindowsStoreApps_SelectedIndexChanged);
            // 
            // tabURI
            // 
            this.tabURI.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.tabURI, "tabURI");
            this.tabURI.Name = "tabURI";
            this.tabURI.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lblUriExplain, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtUriString, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblUriString, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // lblUriExplain
            // 
            resources.ApplyResources(this.lblUriExplain, "lblUriExplain");
            this.tableLayoutPanel1.SetColumnSpan(this.lblUriExplain, 2);
            this.lblUriExplain.Name = "lblUriExplain";
            // 
            // txtUriString
            // 
            resources.ApplyResources(this.txtUriString, "txtUriString");
            this.txtUriString.Name = "txtUriString";
            // 
            // lblUriString
            // 
            resources.ApplyResources(this.lblUriString, "lblUriString");
            this.lblUriString.Name = "lblUriString";
            // 
            // tabOther
            // 
            this.tabOther.Controls.Add(this.tableLayoutPanel2);
            resources.ApplyResources(this.tabOther, "tabOther");
            this.tabOther.Name = "tabOther";
            this.tabOther.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.lblOtherTargetPath, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtOtherShortcutArguments, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblOtherShortcutArguments, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtOtherTargetPath, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnOtherTargetBrowse, 2, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // lblOtherTargetPath
            // 
            resources.ApplyResources(this.lblOtherTargetPath, "lblOtherTargetPath");
            this.lblOtherTargetPath.Name = "lblOtherTargetPath";
            // 
            // txtOtherShortcutArguments
            // 
            resources.ApplyResources(this.txtOtherShortcutArguments, "txtOtherShortcutArguments");
            this.txtOtherShortcutArguments.Name = "txtOtherShortcutArguments";
            // 
            // lblOtherShortcutArguments
            // 
            resources.ApplyResources(this.lblOtherShortcutArguments, "lblOtherShortcutArguments");
            this.lblOtherShortcutArguments.Name = "lblOtherShortcutArguments";
            // 
            // txtOtherTargetPath
            // 
            resources.ApplyResources(this.txtOtherTargetPath, "txtOtherTargetPath");
            this.txtOtherTargetPath.Name = "txtOtherTargetPath";
            // 
            // btnOtherTargetBrowse
            // 
            resources.ApplyResources(this.btnOtherTargetBrowse, "btnOtherTargetBrowse");
            this.btnOtherTargetBrowse.FlatAppearance.BorderSize = 0;
            this.btnOtherTargetBrowse.Name = "btnOtherTargetBrowse";
            this.btnOtherTargetBrowse.UseVisualStyleBackColor = true;
            this.btnOtherTargetBrowse.Click += new System.EventHandler(this.btnOtherTargetBrowse_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            resources.ApplyResources(this.refreshToolStripMenuItem, "refreshToolStripMenuItem");
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // FrmCustomShortcutManagerNew
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radShortcutLocation);
            this.Controls.Add(this.txtShortcutName);
            this.Controls.Add(this.lblShortcutName);
            this.Controls.Add(this.btnGenerateShortcut);
            this.Controls.Add(this.lblCurrentIcon);
            this.Controls.Add(this.pctCurrentIcon);
            this.Controls.Add(this.tabShortcutType);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmCustomShortcutManagerNew";
            this.Shown += new System.EventHandler(this.FrmCustomShortcutManagerNew_Shown);
            this.Resize += new System.EventHandler(this.FrmCustomShortcutManagerNew_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pctCurrentIcon)).EndInit();
            this.tabShortcutType.ResumeLayout(false);
            this.tabExplorer.ResumeLayout(false);
            this.tlpExplorer.ResumeLayout(false);
            this.tlpExplorer.PerformLayout();
            this.tabSteam.ResumeLayout(false);
            this.tlpSteam.ResumeLayout(false);
            this.tlpSteam.PerformLayout();
            this.tabChromeApps.ResumeLayout(false);
            this.tlpChromeApps.ResumeLayout(false);
            this.tlpChromeApps.PerformLayout();
            this.tabWindowsStore.ResumeLayout(false);
            this.tlpWindowsStore.ResumeLayout(false);
            this.tabURI.ResumeLayout(false);
            this.tabURI.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabOther.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TileIconifier.Controls.SkinnableTabControl tabShortcutType;
        private System.Windows.Forms.TabPage tabExplorer;
        private System.Windows.Forms.TabPage tabSteam;
        private SortableListView lstSteamGames;
        private TileIconifier.Controls.SkinnableTextBox txtSteamLibraryPaths;
        private TileIconifier.Controls.SkinnableTextBox txtSteamInstallationPath;
        private TileIconifier.Controls.SkinnableTextBox txtSteamExecutablePath;
        private TileIconifier.Controls.SkinnableButton btnSteamInstallationChange;
        private TileIconifier.Controls.SkinnableButton btnSteamExeChange;
        private System.Windows.Forms.OpenFileDialog opnSteamExe;
        private TileIconifier.Controls.SkinnableButton btnSteamLibrariesPath;
        private System.Windows.Forms.FolderBrowserDialog fldBrowser;
        private AllOrCurrentUserRadios radShortcutLocation;
        private TileIconifier.Controls.SkinnableTextBox txtShortcutName;
        private System.Windows.Forms.Label lblShortcutName;
        private TileIconifier.Controls.SkinnableButton btnGenerateShortcut;
        private System.Windows.Forms.Label lblCurrentIcon;
        private System.Windows.Forms.PictureBox pctCurrentIcon;
        private System.Windows.Forms.TabPage tabOther;
        private System.Windows.Forms.Label lblOtherShortcutArguments;
        private TileIconifier.Controls.SkinnableTextBox txtOtherShortcutArguments;
        private System.Windows.Forms.Label lblOtherTargetPath;
        private TileIconifier.Controls.SkinnableButton btnOtherTargetBrowse;
        private TileIconifier.Controls.SkinnableTextBox txtOtherTargetPath;
        private System.Windows.Forms.OpenFileDialog opnOtherTarget;
        private TileIconifier.Controls.SkinnableTextBox txtCustomFolder;
        private TileIconifier.Controls.SkinnableRadioButton radCustomFolder;
        private TileIconifier.Controls.SkinnableRadioButton radSpecialFolder;
        private TileIconifier.Controls.SkinnableComboBox cmbExplorerGuids;
        private TileIconifier.Controls.SkinnableButton btnExplorerBrowse;
        private System.Windows.Forms.TabPage tabChromeApps;
        private TileIconifier.Controls.SkinnableButton btnChromeExePathChange;
        private TileIconifier.Controls.SkinnableTextBox txtChromeExePath;
        private TileIconifier.Controls.SkinnableButton btnChromeAppPathChange;
        private TileIconifier.Controls.SkinnableTextBox txtChromeAppPath;
        private SortableListView lstChromeAppItems;
        private System.Windows.Forms.TabPage tabWindowsStore;
        private SortableListView lstWindowsStoreApps;
        private System.Windows.Forms.OpenFileDialog opnChromeExe;
        private System.Windows.Forms.TabPage tabURI;
        private TileIconifier.Controls.SkinnableTextBox txtUriString;
        private System.Windows.Forms.Label lblUriString;
        private System.Windows.Forms.Label lblUriExplain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tlpExplorer;
        private System.Windows.Forms.TableLayoutPanel tlpSteam;
        private System.Windows.Forms.TableLayoutPanel tlpChromeApps;
        private System.Windows.Forms.TableLayoutPanel tlpWindowsStore;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}