namespace TileIconifier.Forms
{
    partial class frmDropper
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
            this.txtLnkPath = new System.Windows.Forms.TextBox();
            this.lblLnkPath = new System.Windows.Forms.Label();
            this.lblBGColour = new System.Windows.Forms.Label();
            this.txtBGColour = new System.Windows.Forms.TextBox();
            this.btnIconify = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.cmbColour = new System.Windows.Forms.ComboBox();
            this.pnlFGColour = new System.Windows.Forms.Panel();
            this.radFGDark = new System.Windows.Forms.RadioButton();
            this.chkFGTxtEnabled = new System.Windows.Forms.CheckBox();
            this.lblFGText = new System.Windows.Forms.Label();
            this.radFGLight = new System.Windows.Forms.RadioButton();
            this.lstShortcuts = new System.Windows.Forms.ListBox();
            this.lblExePath = new System.Windows.Forms.Label();
            this.txtExePath = new System.Windows.Forms.TextBox();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getPinnedItemsRequiresPowershellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pctStandardIcon = new System.Windows.Forms.PictureBox();
            this.lblCurrentIcon = new System.Windows.Forms.Label();
            this.MediumIcon = new System.Windows.Forms.Label();
            this.pctMediumIcon = new System.Windows.Forms.PictureBox();
            this.lblSmallIcon = new System.Windows.Forms.Label();
            this.pctSmallIcon = new System.Windows.Forms.PictureBox();
            this.chkUseSameImg = new System.Windows.Forms.CheckBox();
            this.lblUnsaved = new System.Windows.Forms.Label();
            this.pnlFGColour.SuspendLayout();
            this.mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctStandardIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctMediumIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctSmallIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLnkPath
            // 
            this.txtLnkPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLnkPath.Location = new System.Drawing.Point(16, 397);
            this.txtLnkPath.Name = "txtLnkPath";
            this.txtLnkPath.ReadOnly = true;
            this.txtLnkPath.Size = new System.Drawing.Size(794, 26);
            this.txtLnkPath.TabIndex = 1;
            // 
            // lblLnkPath
            // 
            this.lblLnkPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLnkPath.AutoSize = true;
            this.lblLnkPath.Location = new System.Drawing.Point(12, 374);
            this.lblLnkPath.Name = "lblLnkPath";
            this.lblLnkPath.Size = new System.Drawing.Size(76, 20);
            this.lblLnkPath.TabIndex = 2;
            this.lblLnkPath.Text = "LNK Path";
            // 
            // lblBGColour
            // 
            this.lblBGColour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBGColour.AutoSize = true;
            this.lblBGColour.Location = new System.Drawing.Point(830, 374);
            this.lblBGColour.Name = "lblBGColour";
            this.lblBGColour.Size = new System.Drawing.Size(145, 20);
            this.lblBGColour.TabIndex = 5;
            this.lblBGColour.Text = "Background Colour";
            // 
            // txtBGColour
            // 
            this.txtBGColour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBGColour.Location = new System.Drawing.Point(1016, 396);
            this.txtBGColour.Name = "txtBGColour";
            this.txtBGColour.Size = new System.Drawing.Size(141, 26);
            this.txtBGColour.TabIndex = 4;
            this.txtBGColour.Text = "#323232";
            // 
            // btnIconify
            // 
            this.btnIconify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIconify.Location = new System.Drawing.Point(841, 522);
            this.btnIconify.Name = "btnIconify";
            this.btnIconify.Size = new System.Drawing.Size(129, 31);
            this.btnIconify.TabIndex = 8;
            this.btnIconify.Text = "Tile Iconify!";
            this.btnIconify.UseVisualStyleBackColor = true;
            this.btnIconify.Click += new System.EventHandler(this.btnIconify_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(1055, 522);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(144, 31);
            this.btnRemove.TabIndex = 9;
            this.btnRemove.Text = "Remove Iconify";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // cmbColour
            // 
            this.cmbColour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColour.FormattingEnabled = true;
            this.cmbColour.Items.AddRange(new object[] {
            "Custom",
            "black",
            "silver",
            "gray",
            "white",
            "maroon",
            "red",
            "purple",
            "fuchsia",
            "green",
            "lime",
            "olive",
            "yellow",
            "navy",
            "blue",
            "teal",
            "aqua"});
            this.cmbColour.Location = new System.Drawing.Point(834, 397);
            this.cmbColour.Name = "cmbColour";
            this.cmbColour.Size = new System.Drawing.Size(165, 28);
            this.cmbColour.TabIndex = 10;
            this.cmbColour.SelectedIndexChanged += new System.EventHandler(this.cmbColour_SelectedIndexChanged);
            // 
            // pnlFGColour
            // 
            this.pnlFGColour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFGColour.Controls.Add(this.radFGDark);
            this.pnlFGColour.Controls.Add(this.chkFGTxtEnabled);
            this.pnlFGColour.Controls.Add(this.lblFGText);
            this.pnlFGColour.Controls.Add(this.radFGLight);
            this.pnlFGColour.Location = new System.Drawing.Point(834, 442);
            this.pnlFGColour.Name = "pnlFGColour";
            this.pnlFGColour.Size = new System.Drawing.Size(370, 68);
            this.pnlFGColour.TabIndex = 11;
            // 
            // radFGDark
            // 
            this.radFGDark.AutoSize = true;
            this.radFGDark.Location = new System.Drawing.Point(87, 34);
            this.radFGDark.Name = "radFGDark";
            this.radFGDark.Size = new System.Drawing.Size(68, 24);
            this.radFGDark.TabIndex = 8;
            this.radFGDark.Text = "Dark";
            this.radFGDark.UseVisualStyleBackColor = true;
            // 
            // chkFGTxtEnabled
            // 
            this.chkFGTxtEnabled.AutoSize = true;
            this.chkFGTxtEnabled.Checked = true;
            this.chkFGTxtEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFGTxtEnabled.Location = new System.Drawing.Point(164, 7);
            this.chkFGTxtEnabled.Name = "chkFGTxtEnabled";
            this.chkFGTxtEnabled.Size = new System.Drawing.Size(94, 24);
            this.chkFGTxtEnabled.TabIndex = 7;
            this.chkFGTxtEnabled.Text = "Enabled";
            this.chkFGTxtEnabled.UseVisualStyleBackColor = true;
            this.chkFGTxtEnabled.CheckedChanged += new System.EventHandler(this.chkFGTxtEnabled_CheckedChanged);
            // 
            // lblFGText
            // 
            this.lblFGText.AutoSize = true;
            this.lblFGText.Location = new System.Drawing.Point(8, 8);
            this.lblFGText.Name = "lblFGText";
            this.lblFGText.Size = new System.Drawing.Size(126, 20);
            this.lblFGText.TabIndex = 6;
            this.lblFGText.Text = "Foreground Text";
            // 
            // radFGLight
            // 
            this.radFGLight.AutoSize = true;
            this.radFGLight.Checked = true;
            this.radFGLight.Location = new System.Drawing.Point(12, 34);
            this.radFGLight.Name = "radFGLight";
            this.radFGLight.Size = new System.Drawing.Size(69, 24);
            this.radFGLight.TabIndex = 0;
            this.radFGLight.TabStop = true;
            this.radFGLight.Text = "Light";
            this.radFGLight.UseVisualStyleBackColor = true;
            // 
            // lstShortcuts
            // 
            this.lstShortcuts.FormattingEnabled = true;
            this.lstShortcuts.ItemHeight = 20;
            this.lstShortcuts.Location = new System.Drawing.Point(16, 36);
            this.lstShortcuts.Name = "lstShortcuts";
            this.lstShortcuts.Size = new System.Drawing.Size(767, 284);
            this.lstShortcuts.TabIndex = 12;
            this.lstShortcuts.SelectedIndexChanged += new System.EventHandler(this.lstShortcuts_SelectedIndexChanged);
            // 
            // lblExePath
            // 
            this.lblExePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblExePath.AutoSize = true;
            this.lblExePath.Location = new System.Drawing.Point(12, 433);
            this.lblExePath.Name = "lblExePath";
            this.lblExePath.Size = new System.Drawing.Size(79, 20);
            this.lblExePath.TabIndex = 14;
            this.lblExePath.Text = "EXE Path";
            // 
            // txtExePath
            // 
            this.txtExePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtExePath.Location = new System.Drawing.Point(16, 456);
            this.txtExePath.Name = "txtExePath";
            this.txtExePath.ReadOnly = true;
            this.txtExePath.Size = new System.Drawing.Size(794, 26);
            this.txtExePath.TabIndex = 13;
            // 
            // mnuMain
            // 
            this.mnuMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.preferencesToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1209, 33);
            this.mnuMain.TabIndex = 15;
            this.mnuMain.Text = "MainMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(124, 30);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getPinnedItemsRequiresPowershellToolStripMenuItem,
            this.refreshAllToolStripMenuItem});
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(81, 29);
            this.preferencesToolStripMenuItem.Text = "Utilities";
            // 
            // getPinnedItemsRequiresPowershellToolStripMenuItem
            // 
            this.getPinnedItemsRequiresPowershellToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.getPinnedItemsRequiresPowershellToolStripMenuItem.Name = "getPinnedItemsRequiresPowershellToolStripMenuItem";
            this.getPinnedItemsRequiresPowershellToolStripMenuItem.Size = new System.Drawing.Size(402, 30);
            this.getPinnedItemsRequiresPowershellToolStripMenuItem.Text = "Get Pinned Items (Requires Powershell)";
            this.getPinnedItemsRequiresPowershellToolStripMenuItem.Click += new System.EventHandler(this.getPinnedItemsRequiresPowershellToolStripMenuItem_Click);
            // 
            // refreshAllToolStripMenuItem
            // 
            this.refreshAllToolStripMenuItem.Name = "refreshAllToolStripMenuItem";
            this.refreshAllToolStripMenuItem.Size = new System.Drawing.Size(402, 30);
            this.refreshAllToolStripMenuItem.Text = "Refresh All";
            this.refreshAllToolStripMenuItem.Click += new System.EventHandler(this.refreshAllToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(74, 29);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(61, 29);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // pctStandardIcon
            // 
            this.pctStandardIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pctStandardIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctStandardIcon.Location = new System.Drawing.Point(834, 59);
            this.pctStandardIcon.Name = "pctStandardIcon";
            this.pctStandardIcon.Size = new System.Drawing.Size(35, 35);
            this.pctStandardIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctStandardIcon.TabIndex = 16;
            this.pctStandardIcon.TabStop = false;
            // 
            // lblCurrentIcon
            // 
            this.lblCurrentIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentIcon.AutoSize = true;
            this.lblCurrentIcon.Location = new System.Drawing.Point(830, 36);
            this.lblCurrentIcon.Name = "lblCurrentIcon";
            this.lblCurrentIcon.Size = new System.Drawing.Size(114, 20);
            this.lblCurrentIcon.TabIndex = 17;
            this.lblCurrentIcon.Text = "Standard Icon:";
            // 
            // MediumIcon
            // 
            this.MediumIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MediumIcon.AutoSize = true;
            this.MediumIcon.Location = new System.Drawing.Point(830, 110);
            this.MediumIcon.Name = "MediumIcon";
            this.MediumIcon.Size = new System.Drawing.Size(104, 20);
            this.MediumIcon.TabIndex = 19;
            this.MediumIcon.Text = "Medium Icon:";
            // 
            // pctMediumIcon
            // 
            this.pctMediumIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pctMediumIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctMediumIcon.Location = new System.Drawing.Point(834, 133);
            this.pctMediumIcon.Name = "pctMediumIcon";
            this.pctMediumIcon.Size = new System.Drawing.Size(150, 150);
            this.pctMediumIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctMediumIcon.TabIndex = 18;
            this.pctMediumIcon.TabStop = false;
            this.pctMediumIcon.Click += new System.EventHandler(this.pctMediumIcon_Click);
            // 
            // lblSmallIcon
            // 
            this.lblSmallIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSmallIcon.AutoSize = true;
            this.lblSmallIcon.Location = new System.Drawing.Point(1012, 110);
            this.lblSmallIcon.Name = "lblSmallIcon";
            this.lblSmallIcon.Size = new System.Drawing.Size(87, 20);
            this.lblSmallIcon.TabIndex = 21;
            this.lblSmallIcon.Text = "Small Icon:";
            // 
            // pctSmallIcon
            // 
            this.pctSmallIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pctSmallIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctSmallIcon.Location = new System.Drawing.Point(1016, 133);
            this.pctSmallIcon.Name = "pctSmallIcon";
            this.pctSmallIcon.Size = new System.Drawing.Size(70, 70);
            this.pctSmallIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctSmallIcon.TabIndex = 20;
            this.pctSmallIcon.TabStop = false;
            this.pctSmallIcon.Click += new System.EventHandler(this.pctSmallIcon_Click);
            // 
            // chkUseSameImg
            // 
            this.chkUseSameImg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUseSameImg.AutoSize = true;
            this.chkUseSameImg.Checked = true;
            this.chkUseSameImg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseSameImg.Location = new System.Drawing.Point(834, 296);
            this.chkUseSameImg.Name = "chkUseSameImg";
            this.chkUseSameImg.Size = new System.Drawing.Size(344, 24);
            this.chkUseSameImg.TabIndex = 22;
            this.chkUseSameImg.Text = "Use same image for both medium and small";
            this.chkUseSameImg.UseVisualStyleBackColor = true;
            // 
            // lblUnsaved
            // 
            this.lblUnsaved.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUnsaved.AutoSize = true;
            this.lblUnsaved.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnsaved.ForeColor = System.Drawing.Color.Red;
            this.lblUnsaved.Location = new System.Drawing.Point(830, 335);
            this.lblUnsaved.Name = "lblUnsaved";
            this.lblUnsaved.Size = new System.Drawing.Size(263, 20);
            this.lblUnsaved.TabIndex = 23;
            this.lblUnsaved.Text = "This shortcut has unsaved changes!";
            this.lblUnsaved.Visible = false;
            // 
            // frmDropper
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1209, 559);
            this.Controls.Add(this.lblUnsaved);
            this.Controls.Add(this.chkUseSameImg);
            this.Controls.Add(this.lblSmallIcon);
            this.Controls.Add(this.pctSmallIcon);
            this.Controls.Add(this.MediumIcon);
            this.Controls.Add(this.pctMediumIcon);
            this.Controls.Add(this.lblCurrentIcon);
            this.Controls.Add(this.pctStandardIcon);
            this.Controls.Add(this.lblExePath);
            this.Controls.Add(this.txtExePath);
            this.Controls.Add(this.lstShortcuts);
            this.Controls.Add(this.pnlFGColour);
            this.Controls.Add(this.cmbColour);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnIconify);
            this.Controls.Add(this.lblBGColour);
            this.Controls.Add(this.txtBGColour);
            this.Controls.Add(this.lblLnkPath);
            this.Controls.Add(this.txtLnkPath);
            this.Controls.Add(this.mnuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmDropper";
            this.Text = "Tile Iconifier";
            this.Load += new System.EventHandler(this.frmDropper_Load);
            this.pnlFGColour.ResumeLayout(false);
            this.pnlFGColour.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctStandardIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctMediumIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctSmallIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtLnkPath;
        private System.Windows.Forms.Label lblLnkPath;
        private System.Windows.Forms.Label lblBGColour;
        private System.Windows.Forms.TextBox txtBGColour;
        private System.Windows.Forms.Button btnIconify;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ComboBox cmbColour;
        private System.Windows.Forms.Panel pnlFGColour;
        private System.Windows.Forms.RadioButton radFGLight;
        private System.Windows.Forms.CheckBox chkFGTxtEnabled;
        private System.Windows.Forms.Label lblFGText;
        private System.Windows.Forms.RadioButton radFGDark;
        private System.Windows.Forms.ListBox lstShortcuts;
        private System.Windows.Forms.Label lblExePath;
        private System.Windows.Forms.TextBox txtExePath;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getPinnedItemsRequiresPowershellToolStripMenuItem;
        private System.Windows.Forms.PictureBox pctStandardIcon;
        private System.Windows.Forms.Label lblCurrentIcon;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label MediumIcon;
        private System.Windows.Forms.PictureBox pctMediumIcon;
        private System.Windows.Forms.Label lblSmallIcon;
        private System.Windows.Forms.PictureBox pctSmallIcon;
        private System.Windows.Forms.CheckBox chkUseSameImg;
        private System.Windows.Forms.Label lblUnsaved;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshAllToolStripMenuItem;
    }
}

