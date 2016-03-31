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
            this.lblExePath = new System.Windows.Forms.Label();
            this.txtExePath = new System.Windows.Forms.TextBox();
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
            this.chkUseSameImg = new System.Windows.Forms.CheckBox();
            this.lblUnsaved = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.srtlstShortcuts = new TileIconifier.Controls.SortableListView();
            this.clrDialog = new System.Windows.Forms.ColorDialog();
            this.btnColourPicker = new System.Windows.Forms.Button();
            this.panPctMediumIcon = new TileIconifier.Controls.PannablePictureBox.PannablePictureBox();
            this.panPctSmallIcon = new TileIconifier.Controls.PannablePictureBox.PannablePictureBox();
            this.pannablePictureBoxControlPanelMedium = new TileIconifier.Controls.PannablePictureBox.PannablePictureBoxControlPanel();
            this.pnlImages = new System.Windows.Forms.Panel();
            this.lblSmallIcon = new System.Windows.Forms.Label();
            this.lblMediumIcon = new System.Windows.Forms.Label();
            this.pannablePictureBoxControlPanelSmall = new TileIconifier.Controls.PannablePictureBox.PannablePictureBoxControlPanel();
            this.pnlFGColour.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.pnlImages.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLnkPath
            // 
            this.txtLnkPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLnkPath.Location = new System.Drawing.Point(11, 289);
            this.txtLnkPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtLnkPath.Name = "txtLnkPath";
            this.txtLnkPath.ReadOnly = true;
            this.txtLnkPath.Size = new System.Drawing.Size(494, 20);
            this.txtLnkPath.TabIndex = 1;
            // 
            // lblLnkPath
            // 
            this.lblLnkPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLnkPath.AutoSize = true;
            this.lblLnkPath.Location = new System.Drawing.Point(8, 273);
            this.lblLnkPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLnkPath.Name = "lblLnkPath";
            this.lblLnkPath.Size = new System.Drawing.Size(53, 13);
            this.lblLnkPath.TabIndex = 2;
            this.lblLnkPath.Text = "LNK Path";
            // 
            // lblBGColour
            // 
            this.lblBGColour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBGColour.AutoSize = true;
            this.lblBGColour.Location = new System.Drawing.Point(528, 272);
            this.lblBGColour.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBGColour.Name = "lblBGColour";
            this.lblBGColour.Size = new System.Drawing.Size(98, 13);
            this.lblBGColour.TabIndex = 5;
            this.lblBGColour.Text = "Background Colour";
            // 
            // txtBGColour
            // 
            this.txtBGColour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBGColour.Location = new System.Drawing.Point(646, 288);
            this.txtBGColour.Margin = new System.Windows.Forms.Padding(2);
            this.txtBGColour.MaxLength = 7;
            this.txtBGColour.Name = "txtBGColour";
            this.txtBGColour.Size = new System.Drawing.Size(74, 20);
            this.txtBGColour.TabIndex = 4;
            this.txtBGColour.Text = "#323232";
            // 
            // btnIconify
            // 
            this.btnIconify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIconify.Location = new System.Drawing.Point(578, 375);
            this.btnIconify.Margin = new System.Windows.Forms.Padding(2);
            this.btnIconify.Name = "btnIconify";
            this.btnIconify.Size = new System.Drawing.Size(86, 21);
            this.btnIconify.TabIndex = 8;
            this.btnIconify.Tag = "";
            this.btnIconify.Text = "Tile Iconify!";
            this.btnIconify.UseVisualStyleBackColor = true;
            this.btnIconify.Click += new System.EventHandler(this.btnIconify_Click);
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
            // cmbColour
            // 
            this.cmbColour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColour.FormattingEnabled = true;
            this.cmbColour.Items.AddRange(new object[] {
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
            "aqua",
            "Custom"});
            this.cmbColour.Location = new System.Drawing.Point(531, 288);
            this.cmbColour.Margin = new System.Windows.Forms.Padding(2);
            this.cmbColour.Name = "cmbColour";
            this.cmbColour.Size = new System.Drawing.Size(111, 21);
            this.cmbColour.TabIndex = 10;
            // 
            // pnlFGColour
            // 
            this.pnlFGColour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFGColour.Controls.Add(this.radFGDark);
            this.pnlFGColour.Controls.Add(this.chkFGTxtEnabled);
            this.pnlFGColour.Controls.Add(this.lblFGText);
            this.pnlFGColour.Controls.Add(this.radFGLight);
            this.pnlFGColour.Location = new System.Drawing.Point(531, 322);
            this.pnlFGColour.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFGColour.Name = "pnlFGColour";
            this.pnlFGColour.Size = new System.Drawing.Size(289, 45);
            this.pnlFGColour.TabIndex = 11;
            // 
            // radFGDark
            // 
            this.radFGDark.AutoSize = true;
            this.radFGDark.Location = new System.Drawing.Point(58, 23);
            this.radFGDark.Margin = new System.Windows.Forms.Padding(2);
            this.radFGDark.Name = "radFGDark";
            this.radFGDark.Size = new System.Drawing.Size(48, 17);
            this.radFGDark.TabIndex = 8;
            this.radFGDark.Text = "Dark";
            this.radFGDark.UseVisualStyleBackColor = true;
            // 
            // chkFGTxtEnabled
            // 
            this.chkFGTxtEnabled.AutoSize = true;
            this.chkFGTxtEnabled.Checked = true;
            this.chkFGTxtEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFGTxtEnabled.Location = new System.Drawing.Point(184, 4);
            this.chkFGTxtEnabled.Margin = new System.Windows.Forms.Padding(2);
            this.chkFGTxtEnabled.Name = "chkFGTxtEnabled";
            this.chkFGTxtEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkFGTxtEnabled.TabIndex = 7;
            this.chkFGTxtEnabled.Text = "Enabled";
            this.chkFGTxtEnabled.UseVisualStyleBackColor = true;
            // 
            // lblFGText
            // 
            this.lblFGText.AutoSize = true;
            this.lblFGText.Location = new System.Drawing.Point(1, 5);
            this.lblFGText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFGText.Name = "lblFGText";
            this.lblFGText.Size = new System.Drawing.Size(179, 13);
            this.lblFGText.TabIndex = 6;
            this.lblFGText.Text = "Foreground Text (Medium Icon Only)";
            // 
            // radFGLight
            // 
            this.radFGLight.AutoSize = true;
            this.radFGLight.Checked = true;
            this.radFGLight.Location = new System.Drawing.Point(8, 23);
            this.radFGLight.Margin = new System.Windows.Forms.Padding(2);
            this.radFGLight.Name = "radFGLight";
            this.radFGLight.Size = new System.Drawing.Size(48, 17);
            this.radFGLight.TabIndex = 0;
            this.radFGLight.TabStop = true;
            this.radFGLight.Text = "Light";
            this.radFGLight.UseVisualStyleBackColor = true;
            // 
            // lblExePath
            // 
            this.lblExePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblExePath.AutoSize = true;
            this.lblExePath.Location = new System.Drawing.Point(8, 313);
            this.lblExePath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblExePath.Name = "lblExePath";
            this.lblExePath.Size = new System.Drawing.Size(63, 13);
            this.lblExePath.TabIndex = 14;
            this.lblExePath.Text = "Target Path";
            // 
            // txtExePath
            // 
            this.txtExePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtExePath.Location = new System.Drawing.Point(11, 328);
            this.txtExePath.Margin = new System.Windows.Forms.Padding(2);
            this.txtExePath.Name = "txtExePath";
            this.txtExePath.ReadOnly = true;
            this.txtExePath.Size = new System.Drawing.Size(494, 20);
            this.txtExePath.TabIndex = 13;
            // 
            // mnuMain
            // 
            this.mnuMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.preferencesToolStripMenuItem,
            this.skinToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.helpToolStripMenuItem});
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
            // chkUseSameImg
            // 
            this.chkUseSameImg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUseSameImg.AutoSize = true;
            this.chkUseSameImg.Checked = true;
            this.chkUseSameImg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseSameImg.Location = new System.Drawing.Point(7, 187);
            this.chkUseSameImg.Margin = new System.Windows.Forms.Padding(2);
            this.chkUseSameImg.Name = "chkUseSameImg";
            this.chkUseSameImg.Size = new System.Drawing.Size(225, 17);
            this.chkUseSameImg.TabIndex = 22;
            this.chkUseSameImg.Text = "Change Medium and Small image together";
            this.chkUseSameImg.UseVisualStyleBackColor = true;
            // 
            // lblUnsaved
            // 
            this.lblUnsaved.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUnsaved.AutoSize = true;
            this.lblUnsaved.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnsaved.ForeColor = System.Drawing.Color.Red;
            this.lblUnsaved.Location = new System.Drawing.Point(528, 252);
            this.lblUnsaved.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUnsaved.Name = "lblUnsaved";
            this.lblUnsaved.Size = new System.Drawing.Size(179, 13);
            this.lblUnsaved.TabIndex = 23;
            this.lblUnsaved.Text = "This shortcut has unsaved changes!";
            this.lblUnsaved.Visible = false;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(757, 248);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(64, 20);
            this.btnReset.TabIndex = 25;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // srtlstShortcuts
            // 
            this.srtlstShortcuts.FullRowSelect = true;
            this.srtlstShortcuts.Location = new System.Drawing.Point(12, 23);
            this.srtlstShortcuts.MultiSelect = false;
            this.srtlstShortcuts.Name = "srtlstShortcuts";
            this.srtlstShortcuts.Size = new System.Drawing.Size(493, 216);
            this.srtlstShortcuts.TabIndex = 27;
            this.srtlstShortcuts.UseCompatibleStateImageBehavior = false;
            this.srtlstShortcuts.View = System.Windows.Forms.View.Details;
            // 
            // btnColourPicker
            // 
            this.btnColourPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColourPicker.BackgroundImage = global::TileIconifier.Properties.Resources.Actions_color_picker_black_icon;
            this.btnColourPicker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnColourPicker.FlatAppearance.BorderSize = 0;
            this.btnColourPicker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColourPicker.Location = new System.Drawing.Point(726, 288);
            this.btnColourPicker.Name = "btnColourPicker";
            this.btnColourPicker.Size = new System.Drawing.Size(23, 23);
            this.btnColourPicker.TabIndex = 28;
            this.btnColourPicker.UseVisualStyleBackColor = true;
            this.btnColourPicker.Click += new System.EventHandler(this.btnColourPicker_Click);
            // 
            // panPctMediumIcon
            // 
            this.panPctMediumIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPctMediumIcon.Location = new System.Drawing.Point(5, 17);
            this.panPctMediumIcon.Margin = new System.Windows.Forms.Padding(0);
            this.panPctMediumIcon.Name = "panPctMediumIcon";
            this.panPctMediumIcon.Size = new System.Drawing.Size(100, 100);
            this.panPctMediumIcon.TabIndex = 29;
            this.panPctMediumIcon.Click += new System.EventHandler(this.panPctMediumIcon_Click);
            this.panPctMediumIcon.DoubleClick += new System.EventHandler(this.panPctMediumIcon_DoubleClick);
            // 
            // panPctSmallIcon
            // 
            this.panPctSmallIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPctSmallIcon.Location = new System.Drawing.Point(183, 42);
            this.panPctSmallIcon.Margin = new System.Windows.Forms.Padding(0);
            this.panPctSmallIcon.Name = "panPctSmallIcon";
            this.panPctSmallIcon.Size = new System.Drawing.Size(50, 50);
            this.panPctSmallIcon.TabIndex = 30;
            this.panPctSmallIcon.Click += new System.EventHandler(this.panPctSmallIcon_Click);
            this.panPctSmallIcon.DoubleClick += new System.EventHandler(this.panPctSmallIcon_DoubleClick);
            // 
            // pannablePictureBoxControlPanelMedium
            // 
            this.pannablePictureBoxControlPanelMedium.Location = new System.Drawing.Point(3, 17);
            this.pannablePictureBoxControlPanelMedium.Name = "pannablePictureBoxControlPanelMedium";
            this.pannablePictureBoxControlPanelMedium.Size = new System.Drawing.Size(145, 165);
            this.pannablePictureBoxControlPanelMedium.TabIndex = 31;
            // 
            // pnlImages
            // 
            this.pnlImages.Controls.Add(this.lblSmallIcon);
            this.pnlImages.Controls.Add(this.lblMediumIcon);
            this.pnlImages.Controls.Add(this.panPctSmallIcon);
            this.pnlImages.Controls.Add(this.panPctMediumIcon);
            this.pnlImages.Controls.Add(this.chkUseSameImg);
            this.pnlImages.Controls.Add(this.pannablePictureBoxControlPanelMedium);
            this.pnlImages.Controls.Add(this.pannablePictureBoxControlPanelSmall);
            this.pnlImages.Location = new System.Drawing.Point(524, 23);
            this.pnlImages.Name = "pnlImages";
            this.pnlImages.Size = new System.Drawing.Size(297, 216);
            this.pnlImages.TabIndex = 32;
            // 
            // lblSmallIcon
            // 
            this.lblSmallIcon.AutoSize = true;
            this.lblSmallIcon.Location = new System.Drawing.Point(180, 2);
            this.lblSmallIcon.Name = "lblSmallIcon";
            this.lblSmallIcon.Size = new System.Drawing.Size(58, 13);
            this.lblSmallIcon.TabIndex = 34;
            this.lblSmallIcon.Text = "Small icon:";
            // 
            // lblMediumIcon
            // 
            this.lblMediumIcon.AutoSize = true;
            this.lblMediumIcon.Location = new System.Drawing.Point(20, 2);
            this.lblMediumIcon.Name = "lblMediumIcon";
            this.lblMediumIcon.Size = new System.Drawing.Size(70, 13);
            this.lblMediumIcon.TabIndex = 33;
            this.lblMediumIcon.Text = "Medium icon:";
            // 
            // pannablePictureBoxControlPanelSmall
            // 
            this.pannablePictureBoxControlPanelSmall.Location = new System.Drawing.Point(154, 17);
            this.pannablePictureBoxControlPanelSmall.Name = "pannablePictureBoxControlPanelSmall";
            this.pannablePictureBoxControlPanelSmall.Size = new System.Drawing.Size(140, 165);
            this.pannablePictureBoxControlPanelSmall.TabIndex = 32;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 407);
            this.Controls.Add(this.pnlImages);
            this.Controls.Add(this.btnColourPicker);
            this.Controls.Add(this.srtlstShortcuts);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblUnsaved);
            this.Controls.Add(this.lblExePath);
            this.Controls.Add(this.txtExePath);
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
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Text = "Tile Iconifier";
            this.Load += new System.EventHandler(this.frmDropper_Load);
            this.pnlFGColour.ResumeLayout(false);
            this.pnlFGColour.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.pnlImages.ResumeLayout(false);
            this.pnlImages.PerformLayout();
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
        private System.Windows.Forms.Label lblExePath;
        private System.Windows.Forms.TextBox txtExePath;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getPinnedItemsRequiresPowershellToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkUseSameImg;
        private System.Windows.Forms.Label lblUnsaved;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshAllToolStripMenuItem;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ToolStripMenuItem customShortcutManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateToolStripMenuItem;
        private SortableListView srtlstShortcuts;
        private System.Windows.Forms.ToolStripMenuItem skinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultSkinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkSkinToolStripMenuItem;
        private System.Windows.Forms.ColorDialog clrDialog;
        private System.Windows.Forms.Button btnColourPicker;
        private Controls.PannablePictureBox.PannablePictureBox panPctMediumIcon;
        private Controls.PannablePictureBox.PannablePictureBox panPctSmallIcon;
        private Controls.PannablePictureBox.PannablePictureBoxControlPanel pannablePictureBoxControlPanelMedium;
        private System.Windows.Forms.Panel pnlImages;
        private Controls.PannablePictureBox.PannablePictureBoxControlPanel pannablePictureBoxControlPanelSmall;
        private System.Windows.Forms.Label lblSmallIcon;
        private System.Windows.Forms.Label lblMediumIcon;
    }
}

