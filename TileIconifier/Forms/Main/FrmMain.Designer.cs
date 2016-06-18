using TileIconifier.Controls;

namespace TileIconifier.Forms
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
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilter
            // 
            this.txtFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFilter.Location = new System.Drawing.Point(50, 27);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(455, 20);
            this.txtFilter.TabIndex = 32;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(12, 31);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(32, 13);
            this.lblFilter.TabIndex = 31;
            this.lblFilter.Text = "Filter:";
            // 
            // btnDeleteCustomShortcut
            // 
            this.btnDeleteCustomShortcut.Location = new System.Drawing.Point(12, 379);
            this.btnDeleteCustomShortcut.Name = "btnDeleteCustomShortcut";
            this.btnDeleteCustomShortcut.Size = new System.Drawing.Size(149, 20);
            this.btnDeleteCustomShortcut.TabIndex = 30;
            this.btnDeleteCustomShortcut.Text = "Delete Custom Shortcut";
            this.btnDeleteCustomShortcut.UseVisualStyleBackColor = true;
            this.btnDeleteCustomShortcut.Click += new System.EventHandler(this.btnDeleteCustomShortcut_Click);
            // 
            // btnBuildCustomShortcut
            // 
            this.btnBuildCustomShortcut.Location = new System.Drawing.Point(11, 353);
            this.btnBuildCustomShortcut.Name = "btnBuildCustomShortcut";
            this.btnBuildCustomShortcut.Size = new System.Drawing.Size(150, 20);
            this.btnBuildCustomShortcut.TabIndex = 29;
            this.btnBuildCustomShortcut.Text = "Quick Build Custom Shortcut";
            this.btnBuildCustomShortcut.UseVisualStyleBackColor = true;
            this.btnBuildCustomShortcut.Click += new System.EventHandler(this.btnBuildCustomShortcut_Click);
            // 
            // iconifyPanel
            // 
            this.iconifyPanel.CurrentShortcutItem = null;
            this.iconifyPanel.Location = new System.Drawing.Point(516, 23);
            this.iconifyPanel.Name = "iconifyPanel";
            this.iconifyPanel.Size = new System.Drawing.Size(305, 351);
            this.iconifyPanel.TabIndex = 28;
            // 
            // srtlstShortcuts
            // 
            this.srtlstShortcuts.FullRowSelect = true;
            this.srtlstShortcuts.HideSelection = false;
            this.srtlstShortcuts.Location = new System.Drawing.Point(12, 53);
            this.srtlstShortcuts.MultiSelect = false;
            this.srtlstShortcuts.Name = "srtlstShortcuts";
            this.srtlstShortcuts.Size = new System.Drawing.Size(493, 210);
            this.srtlstShortcuts.TabIndex = 27;
            this.srtlstShortcuts.UseCompatibleStateImageBehavior = false;
            this.srtlstShortcuts.View = System.Windows.Forms.View.Details;
            this.srtlstShortcuts.SelectedIndexChanged += new System.EventHandler(this.srtlstShortcuts_SelectedIndexChanged);
            // 
            // lblExePath
            // 
            this.lblExePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblExePath.Location = new System.Drawing.Point(9, 309);
            this.lblExePath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblExePath.Name = "lblExePath";
            this.lblExePath.Size = new System.Drawing.Size(63, 13);
            this.lblExePath.TabIndex = 14;
            this.lblExePath.Text = "Target Path";
            // 
            // txtExePath
            // 
            this.txtExePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtExePath.Location = new System.Drawing.Point(12, 324);
            this.txtExePath.Margin = new System.Windows.Forms.Padding(2);
            this.txtExePath.Name = "txtExePath";
            this.txtExePath.ReadOnly = true;
            this.txtExePath.Size = new System.Drawing.Size(494, 20);
            this.txtExePath.TabIndex = 13;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(720, 375);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(96, 21);
            this.btnRemove.TabIndex = 9;
            this.btnRemove.Text = "Remove Iconify";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnIconify
            // 
            this.btnIconify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIconify.Location = new System.Drawing.Point(521, 379);
            this.btnIconify.Margin = new System.Windows.Forms.Padding(2);
            this.btnIconify.Name = "btnIconify";
            this.btnIconify.Size = new System.Drawing.Size(86, 21);
            this.btnIconify.TabIndex = 8;
            this.btnIconify.Tag = "";
            this.btnIconify.Text = "Tile Iconify!";
            this.btnIconify.UseVisualStyleBackColor = true;
            this.btnIconify.Click += new System.EventHandler(this.btnIconify_Click);
            // 
            // lblLnkPath
            // 
            this.lblLnkPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLnkPath.Location = new System.Drawing.Point(9, 269);
            this.lblLnkPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLnkPath.Name = "lblLnkPath";
            this.lblLnkPath.Size = new System.Drawing.Size(53, 13);
            this.lblLnkPath.TabIndex = 2;
            this.lblLnkPath.Text = "LNK Path";
            // 
            // txtLnkPath
            // 
            this.txtLnkPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLnkPath.Location = new System.Drawing.Point(12, 285);
            this.txtLnkPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtLnkPath.Name = "txtLnkPath";
            this.txtLnkPath.ReadOnly = true;
            this.txtLnkPath.Size = new System.Drawing.Size(494, 20);
            this.txtLnkPath.TabIndex = 1;
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
            this.donateToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.mnuMain.Size = new System.Drawing.Size(827, 24);
            this.mnuMain.TabIndex = 15;
            this.mnuMain.Text = "MainMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customShortcutManagerToolStripMenuItem,
            this.toolStripSeparator1,
            this.getPinnedItemsRequiresPowershellToolStripMenuItem,
            this.refreshAllToolStripMenuItem,
            this.checkForUpdateToolStripMenuItem});
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(58, 22);
            this.preferencesToolStripMenuItem.Text = "Utilities";
            // 
            // customShortcutManagerToolStripMenuItem
            // 
            this.customShortcutManagerToolStripMenuItem.Name = "customShortcutManagerToolStripMenuItem";
            this.customShortcutManagerToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.customShortcutManagerToolStripMenuItem.Text = "Custom Shortcut Manager";
            this.customShortcutManagerToolStripMenuItem.Click += new System.EventHandler(this.customShortcutManagerToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(277, 6);
            // 
            // getPinnedItemsRequiresPowershellToolStripMenuItem
            // 
            this.getPinnedItemsRequiresPowershellToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.getPinnedItemsRequiresPowershellToolStripMenuItem.Name = "getPinnedItemsRequiresPowershellToolStripMenuItem";
            this.getPinnedItemsRequiresPowershellToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.getPinnedItemsRequiresPowershellToolStripMenuItem.Text = "Get Pinned Items (Requires Powershell)";
            this.getPinnedItemsRequiresPowershellToolStripMenuItem.Click += new System.EventHandler(this.getPinnedItemsRequiresPowershellToolStripMenuItem_Click);
            // 
            // refreshAllToolStripMenuItem
            // 
            this.refreshAllToolStripMenuItem.Name = "refreshAllToolStripMenuItem";
            this.refreshAllToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.refreshAllToolStripMenuItem.Text = "Refresh All";
            this.refreshAllToolStripMenuItem.Click += new System.EventHandler(this.refreshAllToolStripMenuItem_Click);
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            this.checkForUpdateToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.checkForUpdateToolStripMenuItem.Text = "Check For Updates";
            this.checkForUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // skinToolStripMenuItem
            // 
            this.skinToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultSkinToolStripMenuItem,
            this.darkSkinToolStripMenuItem});
            this.skinToolStripMenuItem.Name = "skinToolStripMenuItem";
            this.skinToolStripMenuItem.Size = new System.Drawing.Size(41, 22);
            this.skinToolStripMenuItem.Text = "Skin";
            // 
            // defaultSkinToolStripMenuItem
            // 
            this.defaultSkinToolStripMenuItem.Checked = true;
            this.defaultSkinToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.defaultSkinToolStripMenuItem.Name = "defaultSkinToolStripMenuItem";
            this.defaultSkinToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.defaultSkinToolStripMenuItem.Text = "Default Skin";
            // 
            // darkSkinToolStripMenuItem
            // 
            this.darkSkinToolStripMenuItem.Name = "darkSkinToolStripMenuItem";
            this.darkSkinToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.darkSkinToolStripMenuItem.Text = "Dark Skin";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // donateToolStripMenuItem
            // 
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            this.donateToolStripMenuItem.Size = new System.Drawing.Size(57, 22);
            this.donateToolStripMenuItem.Text = "Donate";
            this.donateToolStripMenuItem.Click += new System.EventHandler(this.donateToolStripMenuItem_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(827, 407);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mnuMain;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Text = "Tile Iconifier";
            this.Load += new System.EventHandler(this.frmDropper_Load);
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
    }
}

