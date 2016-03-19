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
            this.tabShortcutType = new System.Windows.Forms.TabControl();
            this.tabExplorer = new System.Windows.Forms.TabPage();
            this.pnlExplorer = new System.Windows.Forms.Panel();
            this.btnExplorerBrowse = new System.Windows.Forms.Button();
            this.txtCustomFolder = new System.Windows.Forms.TextBox();
            this.radCustomFolder = new System.Windows.Forms.RadioButton();
            this.radSpecialFolder = new System.Windows.Forms.RadioButton();
            this.cmbExplorerGuids = new System.Windows.Forms.ComboBox();
            this.tabSteam = new System.Windows.Forms.TabPage();
            this.btnSteamLibrariesPath = new System.Windows.Forms.Button();
            this.btnSteamExeChange = new System.Windows.Forms.Button();
            this.btnInstallationChange = new System.Windows.Forms.Button();
            this.txtSteamInstallationPath = new System.Windows.Forms.TextBox();
            this.txtSteamExecutablePath = new System.Windows.Forms.TextBox();
            this.txtSteamLibraryPaths = new System.Windows.Forms.TextBox();
            this.lstSteamGames = new TileIconifier.Controls.SortableListView();
            this.tabOther = new System.Windows.Forms.TabPage();
            this.lblOtherShortcutArguments = new System.Windows.Forms.Label();
            this.txtOtherShortcutArguments = new System.Windows.Forms.TextBox();
            this.lblOtherTargetPath = new System.Windows.Forms.Label();
            this.btnOtherTargetBrowse = new System.Windows.Forms.Button();
            this.txtOtherTargetPath = new System.Windows.Forms.TextBox();
            this.fldBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.opnSteamExe = new System.Windows.Forms.OpenFileDialog();
            this.txtShortcutName = new System.Windows.Forms.TextBox();
            this.lblShortcutName = new System.Windows.Forms.Label();
            this.btnGenerateShortcut = new System.Windows.Forms.Button();
            this.lblCurrentIcon = new System.Windows.Forms.Label();
            this.pctCurrentIcon = new System.Windows.Forms.PictureBox();
            this.opnOtherTarget = new System.Windows.Forms.OpenFileDialog();
            this.radShortcutLocation = new TileIconifier.Controls.AllOrCurrentUserRadios();
            this.tabShortcutType.SuspendLayout();
            this.tabExplorer.SuspendLayout();
            this.pnlExplorer.SuspendLayout();
            this.tabSteam.SuspendLayout();
            this.tabOther.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctCurrentIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // tabShortcutType
            // 
            this.tabShortcutType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabShortcutType.Controls.Add(this.tabExplorer);
            this.tabShortcutType.Controls.Add(this.tabSteam);
            this.tabShortcutType.Controls.Add(this.tabOther);
            this.tabShortcutType.Location = new System.Drawing.Point(11, 8);
            this.tabShortcutType.Name = "tabShortcutType";
            this.tabShortcutType.SelectedIndex = 0;
            this.tabShortcutType.Size = new System.Drawing.Size(630, 274);
            this.tabShortcutType.TabIndex = 0;
            this.tabShortcutType.SelectedIndexChanged += new System.EventHandler(this.tabShortcutType_SelectedIndexChanged);
            // 
            // tabExplorer
            // 
            this.tabExplorer.Controls.Add(this.pnlExplorer);
            this.tabExplorer.Location = new System.Drawing.Point(4, 22);
            this.tabExplorer.Name = "tabExplorer";
            this.tabExplorer.Padding = new System.Windows.Forms.Padding(3);
            this.tabExplorer.Size = new System.Drawing.Size(622, 248);
            this.tabExplorer.TabIndex = 0;
            this.tabExplorer.Text = "Explorer";
            this.tabExplorer.UseVisualStyleBackColor = true;
            // 
            // pnlExplorer
            // 
            this.pnlExplorer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlExplorer.Controls.Add(this.btnExplorerBrowse);
            this.pnlExplorer.Controls.Add(this.txtCustomFolder);
            this.pnlExplorer.Controls.Add(this.radCustomFolder);
            this.pnlExplorer.Controls.Add(this.radSpecialFolder);
            this.pnlExplorer.Controls.Add(this.cmbExplorerGuids);
            this.pnlExplorer.Location = new System.Drawing.Point(6, 33);
            this.pnlExplorer.Name = "pnlExplorer";
            this.pnlExplorer.Size = new System.Drawing.Size(610, 72);
            this.pnlExplorer.TabIndex = 1;
            // 
            // btnExplorerBrowse
            // 
            this.btnExplorerBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExplorerBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExplorerBrowse.FlatAppearance.BorderSize = 0;
            this.btnExplorerBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExplorerBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExplorerBrowse.Location = new System.Drawing.Point(539, 36);
            this.btnExplorerBrowse.Margin = new System.Windows.Forms.Padding(0);
            this.btnExplorerBrowse.Name = "btnExplorerBrowse";
            this.btnExplorerBrowse.Size = new System.Drawing.Size(68, 26);
            this.btnExplorerBrowse.TabIndex = 9;
            this.btnExplorerBrowse.Text = "Browse";
            this.btnExplorerBrowse.UseVisualStyleBackColor = true;
            this.btnExplorerBrowse.Click += new System.EventHandler(this.btnExplorerBrowse_Click);
            // 
            // txtCustomFolder
            // 
            this.txtCustomFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomFolder.Enabled = false;
            this.txtCustomFolder.Location = new System.Drawing.Point(123, 40);
            this.txtCustomFolder.Name = "txtCustomFolder";
            this.txtCustomFolder.Size = new System.Drawing.Size(413, 20);
            this.txtCustomFolder.TabIndex = 3;
            // 
            // radCustomFolder
            // 
            this.radCustomFolder.AutoSize = true;
            this.radCustomFolder.Location = new System.Drawing.Point(3, 40);
            this.radCustomFolder.Name = "radCustomFolder";
            this.radCustomFolder.Size = new System.Drawing.Size(114, 17);
            this.radCustomFolder.TabIndex = 2;
            this.radCustomFolder.Text = "Use Custom Folder";
            this.radCustomFolder.UseVisualStyleBackColor = true;
            // 
            // radSpecialFolder
            // 
            this.radSpecialFolder.AutoSize = true;
            this.radSpecialFolder.Checked = true;
            this.radSpecialFolder.Location = new System.Drawing.Point(3, 7);
            this.radSpecialFolder.Name = "radSpecialFolder";
            this.radSpecialFolder.Size = new System.Drawing.Size(114, 17);
            this.radSpecialFolder.TabIndex = 1;
            this.radSpecialFolder.TabStop = true;
            this.radSpecialFolder.Text = "Use Special Folder";
            this.radSpecialFolder.UseVisualStyleBackColor = true;
            this.radSpecialFolder.CheckedChanged += new System.EventHandler(this.radSpecialFolder_CheckedChanged);
            // 
            // cmbExplorerGuids
            // 
            this.cmbExplorerGuids.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbExplorerGuids.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExplorerGuids.FormattingEnabled = true;
            this.cmbExplorerGuids.Location = new System.Drawing.Point(123, 7);
            this.cmbExplorerGuids.Name = "cmbExplorerGuids";
            this.cmbExplorerGuids.Size = new System.Drawing.Size(484, 21);
            this.cmbExplorerGuids.TabIndex = 0;
            this.cmbExplorerGuids.SelectedIndexChanged += new System.EventHandler(this.cmbExplorerGuids_SelectedIndexChanged);
            // 
            // tabSteam
            // 
            this.tabSteam.Controls.Add(this.btnSteamLibrariesPath);
            this.tabSteam.Controls.Add(this.btnSteamExeChange);
            this.tabSteam.Controls.Add(this.btnInstallationChange);
            this.tabSteam.Controls.Add(this.txtSteamInstallationPath);
            this.tabSteam.Controls.Add(this.txtSteamExecutablePath);
            this.tabSteam.Controls.Add(this.txtSteamLibraryPaths);
            this.tabSteam.Controls.Add(this.lstSteamGames);
            this.tabSteam.Location = new System.Drawing.Point(4, 22);
            this.tabSteam.Name = "tabSteam";
            this.tabSteam.Padding = new System.Windows.Forms.Padding(3);
            this.tabSteam.Size = new System.Drawing.Size(622, 248);
            this.tabSteam.TabIndex = 1;
            this.tabSteam.Text = "Steam";
            this.tabSteam.UseVisualStyleBackColor = true;
            // 
            // btnSteamLibrariesPath
            // 
            this.btnSteamLibrariesPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSteamLibrariesPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSteamLibrariesPath.FlatAppearance.BorderSize = 0;
            this.btnSteamLibrariesPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSteamLibrariesPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSteamLibrariesPath.Location = new System.Drawing.Point(545, 60);
            this.btnSteamLibrariesPath.Margin = new System.Windows.Forms.Padding(0);
            this.btnSteamLibrariesPath.Name = "btnSteamLibrariesPath";
            this.btnSteamLibrariesPath.Size = new System.Drawing.Size(68, 26);
            this.btnSteamLibrariesPath.TabIndex = 21;
            this.btnSteamLibrariesPath.Text = "Change";
            this.btnSteamLibrariesPath.UseVisualStyleBackColor = true;
            this.btnSteamLibrariesPath.Click += new System.EventHandler(this.btnSteamLibrariesPath_Click);
            // 
            // btnSteamExeChange
            // 
            this.btnSteamExeChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSteamExeChange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSteamExeChange.FlatAppearance.BorderSize = 0;
            this.btnSteamExeChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSteamExeChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSteamExeChange.Location = new System.Drawing.Point(545, 34);
            this.btnSteamExeChange.Margin = new System.Windows.Forms.Padding(0);
            this.btnSteamExeChange.Name = "btnSteamExeChange";
            this.btnSteamExeChange.Size = new System.Drawing.Size(68, 26);
            this.btnSteamExeChange.TabIndex = 9;
            this.btnSteamExeChange.Text = "Change";
            this.btnSteamExeChange.UseVisualStyleBackColor = true;
            this.btnSteamExeChange.Click += new System.EventHandler(this.btnSteamExeChange_Click);
            // 
            // btnInstallationChange
            // 
            this.btnInstallationChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInstallationChange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInstallationChange.FlatAppearance.BorderSize = 0;
            this.btnInstallationChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstallationChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInstallationChange.Location = new System.Drawing.Point(545, 8);
            this.btnInstallationChange.Margin = new System.Windows.Forms.Padding(0);
            this.btnInstallationChange.Name = "btnInstallationChange";
            this.btnInstallationChange.Size = new System.Drawing.Size(68, 26);
            this.btnInstallationChange.TabIndex = 8;
            this.btnInstallationChange.Text = "Change";
            this.btnInstallationChange.UseVisualStyleBackColor = true;
            this.btnInstallationChange.Click += new System.EventHandler(this.btnInstallationChange_Click);
            // 
            // txtSteamInstallationPath
            // 
            this.txtSteamInstallationPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSteamInstallationPath.Location = new System.Drawing.Point(6, 12);
            this.txtSteamInstallationPath.Name = "txtSteamInstallationPath";
            this.txtSteamInstallationPath.ReadOnly = true;
            this.txtSteamInstallationPath.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtSteamInstallationPath.Size = new System.Drawing.Size(536, 20);
            this.txtSteamInstallationPath.TabIndex = 7;
            this.txtSteamInstallationPath.Text = "txtSteamInstallationPath";
            this.txtSteamInstallationPath.WordWrap = false;
            // 
            // txtSteamExecutablePath
            // 
            this.txtSteamExecutablePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSteamExecutablePath.Location = new System.Drawing.Point(6, 38);
            this.txtSteamExecutablePath.Name = "txtSteamExecutablePath";
            this.txtSteamExecutablePath.ReadOnly = true;
            this.txtSteamExecutablePath.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtSteamExecutablePath.Size = new System.Drawing.Size(536, 20);
            this.txtSteamExecutablePath.TabIndex = 6;
            this.txtSteamExecutablePath.Text = "txtSteamExecutablePath";
            this.txtSteamExecutablePath.WordWrap = false;
            // 
            // txtSteamLibraryPaths
            // 
            this.txtSteamLibraryPaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSteamLibraryPaths.Location = new System.Drawing.Point(6, 64);
            this.txtSteamLibraryPaths.Name = "txtSteamLibraryPaths";
            this.txtSteamLibraryPaths.ReadOnly = true;
            this.txtSteamLibraryPaths.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtSteamLibraryPaths.Size = new System.Drawing.Size(536, 20);
            this.txtSteamLibraryPaths.TabIndex = 5;
            this.txtSteamLibraryPaths.Text = "txtSteamLibraryPaths";
            this.txtSteamLibraryPaths.WordWrap = false;
            // 
            // lstSteamGames
            // 
            this.lstSteamGames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSteamGames.FullRowSelect = true;
            this.lstSteamGames.Location = new System.Drawing.Point(6, 90);
            this.lstSteamGames.MultiSelect = false;
            this.lstSteamGames.Name = "lstSteamGames";
            this.lstSteamGames.Size = new System.Drawing.Size(607, 152);
            this.lstSteamGames.TabIndex = 1;
            this.lstSteamGames.UseCompatibleStateImageBehavior = false;
            this.lstSteamGames.View = System.Windows.Forms.View.Details;
            this.lstSteamGames.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lstSteamGames_ColumnWidthChanging);
            this.lstSteamGames.SelectedIndexChanged += new System.EventHandler(this.lstSteamGames_SelectedIndexChanged);
            // 
            // tabOther
            // 
            this.tabOther.Controls.Add(this.lblOtherShortcutArguments);
            this.tabOther.Controls.Add(this.txtOtherShortcutArguments);
            this.tabOther.Controls.Add(this.lblOtherTargetPath);
            this.tabOther.Controls.Add(this.btnOtherTargetBrowse);
            this.tabOther.Controls.Add(this.txtOtherTargetPath);
            this.tabOther.Location = new System.Drawing.Point(4, 22);
            this.tabOther.Name = "tabOther";
            this.tabOther.Size = new System.Drawing.Size(622, 248);
            this.tabOther.TabIndex = 2;
            this.tabOther.Text = "Other";
            this.tabOther.UseVisualStyleBackColor = true;
            // 
            // lblOtherShortcutArguments
            // 
            this.lblOtherShortcutArguments.AutoSize = true;
            this.lblOtherShortcutArguments.Location = new System.Drawing.Point(15, 41);
            this.lblOtherShortcutArguments.Name = "lblOtherShortcutArguments";
            this.lblOtherShortcutArguments.Size = new System.Drawing.Size(100, 13);
            this.lblOtherShortcutArguments.TabIndex = 12;
            this.lblOtherShortcutArguments.Text = "Shortcut Arguments";
            // 
            // txtOtherShortcutArguments
            // 
            this.txtOtherShortcutArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOtherShortcutArguments.Location = new System.Drawing.Point(121, 38);
            this.txtOtherShortcutArguments.Name = "txtOtherShortcutArguments";
            this.txtOtherShortcutArguments.Size = new System.Drawing.Size(421, 20);
            this.txtOtherShortcutArguments.TabIndex = 11;
            // 
            // lblOtherTargetPath
            // 
            this.lblOtherTargetPath.AutoSize = true;
            this.lblOtherTargetPath.Location = new System.Drawing.Point(15, 15);
            this.lblOtherTargetPath.Name = "lblOtherTargetPath";
            this.lblOtherTargetPath.Size = new System.Drawing.Size(81, 13);
            this.lblOtherTargetPath.TabIndex = 10;
            this.lblOtherTargetPath.Text = "Shortcut Target";
            // 
            // btnOtherTargetBrowse
            // 
            this.btnOtherTargetBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOtherTargetBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOtherTargetBrowse.FlatAppearance.BorderSize = 0;
            this.btnOtherTargetBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOtherTargetBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOtherTargetBrowse.Location = new System.Drawing.Point(545, 8);
            this.btnOtherTargetBrowse.Margin = new System.Windows.Forms.Padding(0);
            this.btnOtherTargetBrowse.Name = "btnOtherTargetBrowse";
            this.btnOtherTargetBrowse.Size = new System.Drawing.Size(68, 26);
            this.btnOtherTargetBrowse.TabIndex = 9;
            this.btnOtherTargetBrowse.Text = "Browse";
            this.btnOtherTargetBrowse.UseVisualStyleBackColor = true;
            this.btnOtherTargetBrowse.Click += new System.EventHandler(this.btnOtherTargetBrowse_Click);
            // 
            // txtOtherTargetPath
            // 
            this.txtOtherTargetPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOtherTargetPath.Location = new System.Drawing.Point(121, 12);
            this.txtOtherTargetPath.Name = "txtOtherTargetPath";
            this.txtOtherTargetPath.Size = new System.Drawing.Size(421, 20);
            this.txtOtherTargetPath.TabIndex = 0;
            // 
            // fldBrowser
            // 
            this.fldBrowser.ShowNewFolderButton = false;
            // 
            // opnSteamExe
            // 
            this.opnSteamExe.FileName = "Steam.exe";
            this.opnSteamExe.Filter = "Steam.exe|Steam.exe";
            this.opnSteamExe.Title = "Please locate \"steam.exe\"...";
            // 
            // txtShortcutName
            // 
            this.txtShortcutName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtShortcutName.Location = new System.Drawing.Point(98, 299);
            this.txtShortcutName.Name = "txtShortcutName";
            this.txtShortcutName.Size = new System.Drawing.Size(217, 20);
            this.txtShortcutName.TabIndex = 29;
            // 
            // lblShortcutName
            // 
            this.lblShortcutName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblShortcutName.AutoSize = true;
            this.lblShortcutName.Location = new System.Drawing.Point(95, 283);
            this.lblShortcutName.Name = "lblShortcutName";
            this.lblShortcutName.Size = new System.Drawing.Size(81, 13);
            this.lblShortcutName.TabIndex = 28;
            this.lblShortcutName.Text = "Shortcut Name:";
            // 
            // btnGenerateShortcut
            // 
            this.btnGenerateShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateShortcut.Location = new System.Drawing.Point(493, 308);
            this.btnGenerateShortcut.Name = "btnGenerateShortcut";
            this.btnGenerateShortcut.Size = new System.Drawing.Size(144, 41);
            this.btnGenerateShortcut.TabIndex = 27;
            this.btnGenerateShortcut.Text = "Generate Shortcut";
            this.btnGenerateShortcut.UseVisualStyleBackColor = true;
            this.btnGenerateShortcut.Click += new System.EventHandler(this.btnGenerateShortcut_Click);
            // 
            // lblCurrentIcon
            // 
            this.lblCurrentIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCurrentIcon.AutoSize = true;
            this.lblCurrentIcon.Location = new System.Drawing.Point(12, 283);
            this.lblCurrentIcon.Name = "lblCurrentIcon";
            this.lblCurrentIcon.Size = new System.Drawing.Size(31, 13);
            this.lblCurrentIcon.TabIndex = 26;
            this.lblCurrentIcon.Text = "Icon:";
            // 
            // pctCurrentIcon
            // 
            this.pctCurrentIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pctCurrentIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctCurrentIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctCurrentIcon.Location = new System.Drawing.Point(15, 299);
            this.pctCurrentIcon.Name = "pctCurrentIcon";
            this.pctCurrentIcon.Size = new System.Drawing.Size(50, 50);
            this.pctCurrentIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctCurrentIcon.TabIndex = 25;
            this.pctCurrentIcon.TabStop = false;
            this.pctCurrentIcon.Click += new System.EventHandler(this.pctCurrentIcon_Click);
            // 
            // opnOtherTarget
            // 
            this.opnOtherTarget.Filter = "All Files|*";
            // 
            // radShortcutLocation
            // 
            this.radShortcutLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radShortcutLocation.Location = new System.Drawing.Point(391, 283);
            this.radShortcutLocation.Name = "radShortcutLocation";
            this.radShortcutLocation.Size = new System.Drawing.Size(96, 66);
            this.radShortcutLocation.TabIndex = 30;
            // 
            // FrmCustomShortcutManagerNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 370);
            this.Controls.Add(this.radShortcutLocation);
            this.Controls.Add(this.txtShortcutName);
            this.Controls.Add(this.lblShortcutName);
            this.Controls.Add(this.btnGenerateShortcut);
            this.Controls.Add(this.lblCurrentIcon);
            this.Controls.Add(this.pctCurrentIcon);
            this.Controls.Add(this.tabShortcutType);
            this.MinimumSize = new System.Drawing.Size(668, 409);
            this.Name = "FrmCustomShortcutManagerNew";
            this.Text = "New Custom Shortcut";
            this.tabShortcutType.ResumeLayout(false);
            this.tabExplorer.ResumeLayout(false);
            this.pnlExplorer.ResumeLayout(false);
            this.pnlExplorer.PerformLayout();
            this.tabSteam.ResumeLayout(false);
            this.tabSteam.PerformLayout();
            this.tabOther.ResumeLayout(false);
            this.tabOther.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctCurrentIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabShortcutType;
        private System.Windows.Forms.TabPage tabExplorer;
        private System.Windows.Forms.TabPage tabSteam;
        private TileIconifier.Controls.SortableListView lstSteamGames;
        private System.Windows.Forms.TextBox txtSteamLibraryPaths;
        private System.Windows.Forms.TextBox txtSteamInstallationPath;
        private System.Windows.Forms.TextBox txtSteamExecutablePath;
        private System.Windows.Forms.Button btnInstallationChange;
        private System.Windows.Forms.Button btnSteamExeChange;
        private System.Windows.Forms.OpenFileDialog opnSteamExe;
        private System.Windows.Forms.Button btnSteamLibrariesPath;
        private System.Windows.Forms.FolderBrowserDialog fldBrowser;
        private Controls.AllOrCurrentUserRadios radShortcutLocation;
        private System.Windows.Forms.TextBox txtShortcutName;
        private System.Windows.Forms.Label lblShortcutName;
        private System.Windows.Forms.Button btnGenerateShortcut;
        private System.Windows.Forms.Label lblCurrentIcon;
        private System.Windows.Forms.PictureBox pctCurrentIcon;
        private System.Windows.Forms.TabPage tabOther;
        private System.Windows.Forms.Label lblOtherShortcutArguments;
        private System.Windows.Forms.TextBox txtOtherShortcutArguments;
        private System.Windows.Forms.Label lblOtherTargetPath;
        private System.Windows.Forms.Button btnOtherTargetBrowse;
        private System.Windows.Forms.TextBox txtOtherTargetPath;
        private System.Windows.Forms.OpenFileDialog opnOtherTarget;
        private System.Windows.Forms.Panel pnlExplorer;
        private System.Windows.Forms.TextBox txtCustomFolder;
        private System.Windows.Forms.RadioButton radCustomFolder;
        private System.Windows.Forms.RadioButton radSpecialFolder;
        private System.Windows.Forms.ComboBox cmbExplorerGuids;
        private System.Windows.Forms.Button btnExplorerBrowse;
    }
}