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
            this.pnlExplorer = new System.Windows.Forms.Panel();
            this.btnExplorerBrowse = new TileIconifier.Controls.SkinnableButton();
            this.txtCustomFolder = new TileIconifier.Controls.SkinnableTextBox();
            this.radCustomFolder = new TileIconifier.Controls.SkinnableRadioButton();
            this.radSpecialFolder = new TileIconifier.Controls.SkinnableRadioButton();
            this.cmbExplorerGuids = new TileIconifier.Controls.SkinnableComboBox();
            this.tabSteam = new System.Windows.Forms.TabPage();
            this.btnSteamLibrariesPath = new TileIconifier.Controls.SkinnableButton();
            this.btnSteamExeChange = new TileIconifier.Controls.SkinnableButton();
            this.btnSteamInstallationChange = new TileIconifier.Controls.SkinnableButton();
            this.txtSteamInstallationPath = new TileIconifier.Controls.SkinnableTextBox();
            this.txtSteamExecutablePath = new TileIconifier.Controls.SkinnableTextBox();
            this.txtSteamLibraryPaths = new TileIconifier.Controls.SkinnableTextBox();
            this.lstSteamGames = new TileIconifier.Controls.SortableListView();
            this.tabChromeApps = new System.Windows.Forms.TabPage();
            this.lstChromeAppItems = new TileIconifier.Controls.SortableListView();
            this.btnChromeAppPathChange = new TileIconifier.Controls.SkinnableButton();
            this.txtChromeAppPath = new TileIconifier.Controls.SkinnableTextBox();
            this.btnChromeExePathChange = new TileIconifier.Controls.SkinnableButton();
            this.txtChromeExePath = new TileIconifier.Controls.SkinnableTextBox();
            this.tabWindowsStore = new System.Windows.Forms.TabPage();
            this.lstWindowsStoreApps = new TileIconifier.Controls.SortableListView();
            this.tabURI = new System.Windows.Forms.TabPage();
            this.txtUriString = new TileIconifier.Controls.SkinnableTextBox();
            this.lblUriString = new System.Windows.Forms.Label();
            this.lblUriExplain = new System.Windows.Forms.Label();
            this.tabOther = new System.Windows.Forms.TabPage();
            this.lblOtherShortcutArguments = new System.Windows.Forms.Label();
            this.txtOtherShortcutArguments = new TileIconifier.Controls.SkinnableTextBox();
            this.lblOtherTargetPath = new System.Windows.Forms.Label();
            this.btnOtherTargetBrowse = new TileIconifier.Controls.SkinnableButton();
            this.txtOtherTargetPath = new TileIconifier.Controls.SkinnableTextBox();
            this.menuStrip1 = new SkinnableMenuStrip();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pctCurrentIcon)).BeginInit();
            this.tabShortcutType.SuspendLayout();
            this.tabExplorer.SuspendLayout();
            this.pnlExplorer.SuspendLayout();
            this.tabSteam.SuspendLayout();
            this.tabChromeApps.SuspendLayout();
            this.tabWindowsStore.SuspendLayout();
            this.tabURI.SuspendLayout();
            this.tabOther.SuspendLayout();
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
            this.tabExplorer.Controls.Add(this.pnlExplorer);
            resources.ApplyResources(this.tabExplorer, "tabExplorer");
            this.tabExplorer.Name = "tabExplorer";
            this.tabExplorer.UseVisualStyleBackColor = true;
            // 
            // pnlExplorer
            // 
            resources.ApplyResources(this.pnlExplorer, "pnlExplorer");
            this.pnlExplorer.Controls.Add(this.btnExplorerBrowse);
            this.pnlExplorer.Controls.Add(this.txtCustomFolder);
            this.pnlExplorer.Controls.Add(this.radCustomFolder);
            this.pnlExplorer.Controls.Add(this.radSpecialFolder);
            this.pnlExplorer.Controls.Add(this.cmbExplorerGuids);
            this.pnlExplorer.Name = "pnlExplorer";
            // 
            // btnExplorerBrowse
            // 
            resources.ApplyResources(this.btnExplorerBrowse, "btnExplorerBrowse");
            this.btnExplorerBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExplorerBrowse.FlatAppearance.BorderSize = 0;
            this.btnExplorerBrowse.Name = "btnExplorerBrowse";
            this.btnExplorerBrowse.UseVisualStyleBackColor = true;
            this.btnExplorerBrowse.Click += new System.EventHandler(this.btnExplorerBrowse_Click);
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
            // radSpecialFolder
            // 
            this.radSpecialFolder.Checked = true;
            resources.ApplyResources(this.radSpecialFolder, "radSpecialFolder");
            this.radSpecialFolder.Name = "radSpecialFolder";
            this.radSpecialFolder.TabStop = true;
            this.radSpecialFolder.UseVisualStyleBackColor = true;
            this.radSpecialFolder.CheckedChanged += new System.EventHandler(this.radSpecialFolder_CheckedChanged);
            // 
            // cmbExplorerGuids
            // 
            resources.ApplyResources(this.cmbExplorerGuids, "cmbExplorerGuids");
            this.cmbExplorerGuids.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExplorerGuids.FormattingEnabled = true;
            this.cmbExplorerGuids.Name = "cmbExplorerGuids";
            this.cmbExplorerGuids.SelectedIndexChanged += new System.EventHandler(this.cmbExplorerGuids_SelectedIndexChanged);
            // 
            // tabSteam
            // 
            this.tabSteam.Controls.Add(this.btnSteamLibrariesPath);
            this.tabSteam.Controls.Add(this.btnSteamExeChange);
            this.tabSteam.Controls.Add(this.btnSteamInstallationChange);
            this.tabSteam.Controls.Add(this.txtSteamInstallationPath);
            this.tabSteam.Controls.Add(this.txtSteamExecutablePath);
            this.tabSteam.Controls.Add(this.txtSteamLibraryPaths);
            this.tabSteam.Controls.Add(this.lstSteamGames);
            resources.ApplyResources(this.tabSteam, "tabSteam");
            this.tabSteam.Name = "tabSteam";
            this.tabSteam.UseVisualStyleBackColor = true;
            // 
            // btnSteamLibrariesPath
            // 
            resources.ApplyResources(this.btnSteamLibrariesPath, "btnSteamLibrariesPath");
            this.btnSteamLibrariesPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSteamLibrariesPath.FlatAppearance.BorderSize = 0;
            this.btnSteamLibrariesPath.Name = "btnSteamLibrariesPath";
            this.btnSteamLibrariesPath.UseVisualStyleBackColor = true;
            this.btnSteamLibrariesPath.Click += new System.EventHandler(this.btnSteamLibrariesPath_Click);
            // 
            // btnSteamExeChange
            // 
            resources.ApplyResources(this.btnSteamExeChange, "btnSteamExeChange");
            this.btnSteamExeChange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSteamExeChange.FlatAppearance.BorderSize = 0;
            this.btnSteamExeChange.Name = "btnSteamExeChange";
            this.btnSteamExeChange.UseVisualStyleBackColor = true;
            this.btnSteamExeChange.Click += new System.EventHandler(this.btnSteamExeChange_Click);
            // 
            // btnSteamInstallationChange
            // 
            resources.ApplyResources(this.btnSteamInstallationChange, "btnSteamInstallationChange");
            this.btnSteamInstallationChange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSteamInstallationChange.FlatAppearance.BorderSize = 0;
            this.btnSteamInstallationChange.Name = "btnSteamInstallationChange";
            this.btnSteamInstallationChange.UseVisualStyleBackColor = true;
            this.btnSteamInstallationChange.Click += new System.EventHandler(this.btnInstallationChange_Click);
            // 
            // txtSteamInstallationPath
            // 
            resources.ApplyResources(this.txtSteamInstallationPath, "txtSteamInstallationPath");
            this.txtSteamInstallationPath.Name = "txtSteamInstallationPath";
            this.txtSteamInstallationPath.ReadOnly = true;
            // 
            // txtSteamExecutablePath
            // 
            resources.ApplyResources(this.txtSteamExecutablePath, "txtSteamExecutablePath");
            this.txtSteamExecutablePath.Name = "txtSteamExecutablePath";
            this.txtSteamExecutablePath.ReadOnly = true;
            // 
            // txtSteamLibraryPaths
            // 
            resources.ApplyResources(this.txtSteamLibraryPaths, "txtSteamLibraryPaths");
            this.txtSteamLibraryPaths.Name = "txtSteamLibraryPaths";
            this.txtSteamLibraryPaths.ReadOnly = true;
            // 
            // lstSteamGames
            // 
            resources.ApplyResources(this.lstSteamGames, "lstSteamGames");
            this.lstSteamGames.FullRowSelect = true;
            this.lstSteamGames.HideSelection = false;
            this.lstSteamGames.MultiSelect = false;
            this.lstSteamGames.Name = "lstSteamGames";
            this.lstSteamGames.UseCompatibleStateImageBehavior = false;
            this.lstSteamGames.View = System.Windows.Forms.View.Details;
            this.lstSteamGames.SelectedIndexChanged += new System.EventHandler(this.lstSteamGames_SelectedIndexChanged);
            // 
            // tabChromeApps
            // 
            this.tabChromeApps.Controls.Add(this.lstChromeAppItems);
            this.tabChromeApps.Controls.Add(this.btnChromeAppPathChange);
            this.tabChromeApps.Controls.Add(this.txtChromeAppPath);
            this.tabChromeApps.Controls.Add(this.btnChromeExePathChange);
            this.tabChromeApps.Controls.Add(this.txtChromeExePath);
            resources.ApplyResources(this.tabChromeApps, "tabChromeApps");
            this.tabChromeApps.Name = "tabChromeApps";
            this.tabChromeApps.UseVisualStyleBackColor = true;
            // 
            // lstChromeAppItems
            // 
            resources.ApplyResources(this.lstChromeAppItems, "lstChromeAppItems");
            this.lstChromeAppItems.FullRowSelect = true;
            this.lstChromeAppItems.HideSelection = false;
            this.lstChromeAppItems.MultiSelect = false;
            this.lstChromeAppItems.Name = "lstChromeAppItems";
            this.lstChromeAppItems.UseCompatibleStateImageBehavior = false;
            this.lstChromeAppItems.View = System.Windows.Forms.View.Details;
            this.lstChromeAppItems.SelectedIndexChanged += new System.EventHandler(this.lstChromeAppItems_SelectedIndexChanged);
            // 
            // btnChromeAppPathChange
            // 
            resources.ApplyResources(this.btnChromeAppPathChange, "btnChromeAppPathChange");
            this.btnChromeAppPathChange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChromeAppPathChange.FlatAppearance.BorderSize = 0;
            this.btnChromeAppPathChange.Name = "btnChromeAppPathChange";
            this.btnChromeAppPathChange.UseVisualStyleBackColor = true;
            this.btnChromeAppPathChange.Click += new System.EventHandler(this.btnChromeAppPathChange_Click);
            // 
            // txtChromeAppPath
            // 
            resources.ApplyResources(this.txtChromeAppPath, "txtChromeAppPath");
            this.txtChromeAppPath.Name = "txtChromeAppPath";
            this.txtChromeAppPath.ReadOnly = true;
            // 
            // btnChromeExePathChange
            // 
            resources.ApplyResources(this.btnChromeExePathChange, "btnChromeExePathChange");
            this.btnChromeExePathChange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChromeExePathChange.FlatAppearance.BorderSize = 0;
            this.btnChromeExePathChange.Name = "btnChromeExePathChange";
            this.btnChromeExePathChange.UseVisualStyleBackColor = true;
            this.btnChromeExePathChange.Click += new System.EventHandler(this.btnChromeExePathChange_Click);
            // 
            // txtChromeExePath
            // 
            resources.ApplyResources(this.txtChromeExePath, "txtChromeExePath");
            this.txtChromeExePath.Name = "txtChromeExePath";
            this.txtChromeExePath.ReadOnly = true;
            // 
            // tabWindowsStore
            // 
            this.tabWindowsStore.Controls.Add(this.lstWindowsStoreApps);
            resources.ApplyResources(this.tabWindowsStore, "tabWindowsStore");
            this.tabWindowsStore.Name = "tabWindowsStore";
            this.tabWindowsStore.UseVisualStyleBackColor = true;
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
            this.tabURI.Controls.Add(this.txtUriString);
            this.tabURI.Controls.Add(this.lblUriString);
            this.tabURI.Controls.Add(this.lblUriExplain);
            resources.ApplyResources(this.tabURI, "tabURI");
            this.tabURI.Name = "tabURI";
            this.tabURI.UseVisualStyleBackColor = true;
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
            // lblUriExplain
            // 
            resources.ApplyResources(this.lblUriExplain, "lblUriExplain");
            this.lblUriExplain.Name = "lblUriExplain";
            // 
            // tabOther
            // 
            this.tabOther.Controls.Add(this.lblOtherShortcutArguments);
            this.tabOther.Controls.Add(this.txtOtherShortcutArguments);
            this.tabOther.Controls.Add(this.lblOtherTargetPath);
            this.tabOther.Controls.Add(this.btnOtherTargetBrowse);
            this.tabOther.Controls.Add(this.txtOtherTargetPath);
            resources.ApplyResources(this.tabOther, "tabOther");
            this.tabOther.Name = "tabOther";
            this.tabOther.UseVisualStyleBackColor = true;
            // 
            // lblOtherShortcutArguments
            // 
            resources.ApplyResources(this.lblOtherShortcutArguments, "lblOtherShortcutArguments");
            this.lblOtherShortcutArguments.Name = "lblOtherShortcutArguments";
            // 
            // txtOtherShortcutArguments
            // 
            resources.ApplyResources(this.txtOtherShortcutArguments, "txtOtherShortcutArguments");
            this.txtOtherShortcutArguments.Name = "txtOtherShortcutArguments";
            // 
            // lblOtherTargetPath
            // 
            resources.ApplyResources(this.lblOtherTargetPath, "lblOtherTargetPath");
            this.lblOtherTargetPath.Name = "lblOtherTargetPath";
            // 
            // btnOtherTargetBrowse
            // 
            resources.ApplyResources(this.btnOtherTargetBrowse, "btnOtherTargetBrowse");
            this.btnOtherTargetBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOtherTargetBrowse.FlatAppearance.BorderSize = 0;
            this.btnOtherTargetBrowse.Name = "btnOtherTargetBrowse";
            this.btnOtherTargetBrowse.UseVisualStyleBackColor = true;
            this.btnOtherTargetBrowse.Click += new System.EventHandler(this.btnOtherTargetBrowse_Click);
            // 
            // txtOtherTargetPath
            // 
            resources.ApplyResources(this.txtOtherTargetPath, "txtOtherTargetPath");
            this.txtOtherTargetPath.Name = "txtOtherTargetPath";
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
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
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
            this.Load += new System.EventHandler(this.FrmCustomShortcutManagerNew_Load);
            this.Resize += new System.EventHandler(this.FrmCustomShortcutManagerNew_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pctCurrentIcon)).EndInit();
            this.tabShortcutType.ResumeLayout(false);
            this.tabExplorer.ResumeLayout(false);
            this.pnlExplorer.ResumeLayout(false);
            this.pnlExplorer.PerformLayout();
            this.tabSteam.ResumeLayout(false);
            this.tabSteam.PerformLayout();
            this.tabChromeApps.ResumeLayout(false);
            this.tabChromeApps.PerformLayout();
            this.tabWindowsStore.ResumeLayout(false);
            this.tabURI.ResumeLayout(false);
            this.tabURI.PerformLayout();
            this.tabOther.ResumeLayout(false);
            this.tabOther.PerformLayout();
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
        private System.Windows.Forms.Panel pnlExplorer;
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
        private SkinnableMenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
    }
}