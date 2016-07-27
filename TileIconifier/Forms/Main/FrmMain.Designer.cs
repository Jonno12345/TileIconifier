using TileIconifier.Controls;

namespace TileIconifier.Forms.Main
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.btnDeleteCustomShortcut = new System.Windows.Forms.Button();
            this.btnBuildCustomShortcut = new System.Windows.Forms.Button();
            this.iconifyPanel = new TileIconifier.Controls.TileIconifierPanel();
            this.srtlstShortcuts = new TileIconifier.Controls.SortableListView();
            this.lblExePath = new System.Windows.Forms.Label();
            this.txtExePath = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnIconify = new System.Windows.Forms.Button();
            this.lblLnkPath = new System.Windows.Forms.Label();
            this.txtLnkPath = new System.Windows.Forms.TextBox();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customShortcutManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.getPinnedItemsRequiresPowershellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultSkinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkSkinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.russianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblBadShortcutWarning = new System.Windows.Forms.Label();
            this.mnuBatchOperations = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilter
            // 
            resources.ApplyResources(this.txtFilter, "txtFilter");
            this.txtFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // lblFilter
            // 
            resources.ApplyResources(this.lblFilter, "lblFilter");
            this.lblFilter.Name = "lblFilter";
            // 
            // btnDeleteCustomShortcut
            // 
            resources.ApplyResources(this.btnDeleteCustomShortcut, "btnDeleteCustomShortcut");
            this.btnDeleteCustomShortcut.Name = "btnDeleteCustomShortcut";
            this.btnDeleteCustomShortcut.UseVisualStyleBackColor = true;
            this.btnDeleteCustomShortcut.Click += new System.EventHandler(this.btnDeleteCustomShortcut_Click);
            // 
            // btnBuildCustomShortcut
            // 
            resources.ApplyResources(this.btnBuildCustomShortcut, "btnBuildCustomShortcut");
            this.btnBuildCustomShortcut.Name = "btnBuildCustomShortcut";
            this.btnBuildCustomShortcut.UseVisualStyleBackColor = true;
            this.btnBuildCustomShortcut.Click += new System.EventHandler(this.btnBuildCustomShortcut_Click);
            // 
            // iconifyPanel
            // 
            resources.ApplyResources(this.iconifyPanel, "iconifyPanel");
            this.iconifyPanel.CurrentShortcutItem = null;
            this.iconifyPanel.Name = "iconifyPanel";
            // 
            // srtlstShortcuts
            // 
            resources.ApplyResources(this.srtlstShortcuts, "srtlstShortcuts");
            this.srtlstShortcuts.FullRowSelect = true;
            this.srtlstShortcuts.HideSelection = false;
            this.srtlstShortcuts.MultiSelect = false;
            this.srtlstShortcuts.Name = "srtlstShortcuts";
            this.srtlstShortcuts.UseCompatibleStateImageBehavior = false;
            this.srtlstShortcuts.View = System.Windows.Forms.View.Details;
            this.srtlstShortcuts.SelectedIndexChanged += new System.EventHandler(this.srtlstShortcuts_SelectedIndexChanged);
            // 
            // lblExePath
            // 
            resources.ApplyResources(this.lblExePath, "lblExePath");
            this.lblExePath.Name = "lblExePath";
            // 
            // txtExePath
            // 
            resources.ApplyResources(this.txtExePath, "txtExePath");
            this.txtExePath.Name = "txtExePath";
            this.txtExePath.ReadOnly = true;
            // 
            // btnRemove
            // 
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnIconify
            // 
            resources.ApplyResources(this.btnIconify, "btnIconify");
            this.btnIconify.Name = "btnIconify";
            this.btnIconify.Tag = "";
            this.btnIconify.UseVisualStyleBackColor = true;
            this.btnIconify.Click += new System.EventHandler(this.btnIconify_Click);
            // 
            // lblLnkPath
            // 
            resources.ApplyResources(this.lblLnkPath, "lblLnkPath");
            this.lblLnkPath.Name = "lblLnkPath";
            // 
            // txtLnkPath
            // 
            resources.ApplyResources(this.txtLnkPath, "txtLnkPath");
            this.txtLnkPath.Name = "txtLnkPath";
            this.txtLnkPath.ReadOnly = true;
            // 
            // mnuMain
            // 
            this.mnuMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.preferencesToolStripMenuItem,
            this.skinToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.languageToolStripMenuItem,
            this.donateToolStripMenuItem});
            resources.ApplyResources(this.mnuMain, "mnuMain");
            this.mnuMain.Name = "mnuMain";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customShortcutManagerToolStripMenuItem,
            this.mnuBatchOperations,
            this.toolStripSeparator1,
            this.getPinnedItemsRequiresPowershellToolStripMenuItem,
            this.refreshAllToolStripMenuItem,
            this.checkForUpdateToolStripMenuItem});
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            resources.ApplyResources(this.preferencesToolStripMenuItem, "preferencesToolStripMenuItem");
            // 
            // customShortcutManagerToolStripMenuItem
            // 
            this.customShortcutManagerToolStripMenuItem.Name = "customShortcutManagerToolStripMenuItem";
            resources.ApplyResources(this.customShortcutManagerToolStripMenuItem, "customShortcutManagerToolStripMenuItem");
            this.customShortcutManagerToolStripMenuItem.Click += new System.EventHandler(this.customShortcutManagerToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // getPinnedItemsRequiresPowershellToolStripMenuItem
            // 
            this.getPinnedItemsRequiresPowershellToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.getPinnedItemsRequiresPowershellToolStripMenuItem.Name = "getPinnedItemsRequiresPowershellToolStripMenuItem";
            resources.ApplyResources(this.getPinnedItemsRequiresPowershellToolStripMenuItem, "getPinnedItemsRequiresPowershellToolStripMenuItem");
            this.getPinnedItemsRequiresPowershellToolStripMenuItem.Click += new System.EventHandler(this.getPinnedItemsRequiresPowershellToolStripMenuItem_Click);
            // 
            // refreshAllToolStripMenuItem
            // 
            this.refreshAllToolStripMenuItem.Name = "refreshAllToolStripMenuItem";
            resources.ApplyResources(this.refreshAllToolStripMenuItem, "refreshAllToolStripMenuItem");
            this.refreshAllToolStripMenuItem.Click += new System.EventHandler(this.refreshAllToolStripMenuItem_Click);
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            resources.ApplyResources(this.checkForUpdateToolStripMenuItem, "checkForUpdateToolStripMenuItem");
            this.checkForUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // skinToolStripMenuItem
            // 
            this.skinToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultSkinToolStripMenuItem,
            this.darkSkinToolStripMenuItem});
            this.skinToolStripMenuItem.Name = "skinToolStripMenuItem";
            resources.ApplyResources(this.skinToolStripMenuItem, "skinToolStripMenuItem");
            // 
            // defaultSkinToolStripMenuItem
            // 
            this.defaultSkinToolStripMenuItem.Checked = true;
            this.defaultSkinToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.defaultSkinToolStripMenuItem.Name = "defaultSkinToolStripMenuItem";
            resources.ApplyResources(this.defaultSkinToolStripMenuItem, "defaultSkinToolStripMenuItem");
            // 
            // darkSkinToolStripMenuItem
            // 
            this.darkSkinToolStripMenuItem.Name = "darkSkinToolStripMenuItem";
            resources.ApplyResources(this.darkSkinToolStripMenuItem, "darkSkinToolStripMenuItem");
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.russianToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            resources.ApplyResources(this.languageToolStripMenuItem, "languageToolStripMenuItem");
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Tag = "en-US";
            // 
            // russianToolStripMenuItem
            // 
            this.russianToolStripMenuItem.Name = "russianToolStripMenuItem";
            resources.ApplyResources(this.russianToolStripMenuItem, "russianToolStripMenuItem");
            this.russianToolStripMenuItem.Tag = "ru-RU";
            // 
            // donateToolStripMenuItem
            // 
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            resources.ApplyResources(this.donateToolStripMenuItem, "donateToolStripMenuItem");
            this.donateToolStripMenuItem.Click += new System.EventHandler(this.donateToolStripMenuItem_Click);
            // 
            // lblBadShortcutWarning
            // 
            resources.ApplyResources(this.lblBadShortcutWarning, "lblBadShortcutWarning");
            this.lblBadShortcutWarning.ForeColor = System.Drawing.Color.Red;
            this.lblBadShortcutWarning.Name = "lblBadShortcutWarning";
            // 
            // mnuBatchOperations
            // 
            this.mnuBatchOperations.Name = "mnuBatchOperations";
            resources.ApplyResources(this.mnuBatchOperations, "mnuBatchOperations");
            this.mnuBatchOperations.Click += new System.EventHandler(this.mnuBatchOperations_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lblBadShortcutWarning);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.btnDeleteCustomShortcut);
            this.Controls.Add(this.btnBuildCustomShortcut);
            this.Controls.Add(this.iconifyPanel);
            this.Controls.Add(this.srtlstShortcuts);
            this.Controls.Add(this.lblExePath);
            this.Controls.Add(this.txtExePath);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnIconify);
            this.Controls.Add(this.lblLnkPath);
            this.Controls.Add(this.txtLnkPath);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "FrmMain";
            this.Load += new System.EventHandler(this.frmDropper_Load);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtLnkPath;
        private System.Windows.Forms.Label lblLnkPath;
        private System.Windows.Forms.Button btnIconify;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lblExePath;
        private System.Windows.Forms.TextBox txtExePath;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getPinnedItemsRequiresPowershellToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customShortcutManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateToolStripMenuItem;
        private SortableListView srtlstShortcuts;
        private System.Windows.Forms.ToolStripMenuItem skinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultSkinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkSkinToolStripMenuItem;
        private TileIconifierPanel iconifyPanel;
        private System.Windows.Forms.Button btnBuildCustomShortcut;
        private System.Windows.Forms.Button btnDeleteCustomShortcut;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ToolStripMenuItem donateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem russianToolStripMenuItem;
        private System.Windows.Forms.Label lblBadShortcutWarning;
        private System.Windows.Forms.ToolStripMenuItem mnuBatchOperations;
    }
}

