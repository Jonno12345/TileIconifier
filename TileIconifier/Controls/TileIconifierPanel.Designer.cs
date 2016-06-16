using TileIconifier.Controls.Eyedropper;
using TileIconifier.Controls.PictureBox;

namespace TileIconifier.Controls
{
    partial class TileIconifierPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlImages = new System.Windows.Forms.Panel();
            this.lblSmallIcon = new System.Windows.Forms.Label();
            this.lblMediumIcon = new System.Windows.Forms.Label();
            this.panPctSmallIcon = new TileIconifier.Controls.PictureBox.PannablePictureBox();
            this.panPctMediumIcon = new TileIconifier.Controls.PictureBox.PannablePictureBox();
            this.chkUseSameImg = new System.Windows.Forms.CheckBox();
            this.pannablePictureBoxControlPanelMedium = new TileIconifier.Controls.PictureBox.PannablePictureBoxControlPanel();
            this.pannablePictureBoxControlPanelSmall = new TileIconifier.Controls.PictureBox.PannablePictureBoxControlPanel();
            this.btnColourPicker = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblUnsaved = new System.Windows.Forms.Label();
            this.pnlFGColour = new System.Windows.Forms.Panel();
            this.radFGDark = new System.Windows.Forms.RadioButton();
            this.chkFGTxtEnabled = new System.Windows.Forms.CheckBox();
            this.lblFGText = new System.Windows.Forms.Label();
            this.radFGLight = new System.Windows.Forms.RadioButton();
            this.cmbColour = new System.Windows.Forms.ComboBox();
            this.lblBGColour = new System.Windows.Forms.Label();
            this.txtBGColour = new System.Windows.Forms.TextBox();
            this.clrDialog = new System.Windows.Forms.ColorDialog();
            this.eyedropperColorPicker = new TileIconifier.Controls.Eyedropper.EyedropColorPicker();
            this.pnlImages.SuspendLayout();
            this.pnlFGColour.SuspendLayout();
            this.SuspendLayout();
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
            this.pnlImages.Location = new System.Drawing.Point(3, 3);
            this.pnlImages.Name = "pnlImages";
            this.pnlImages.Size = new System.Drawing.Size(297, 216);
            this.pnlImages.TabIndex = 40;
            // 
            // lblSmallIcon
            // 
            this.lblSmallIcon.Location = new System.Drawing.Point(180, 2);
            this.lblSmallIcon.Name = "lblSmallIcon";
            this.lblSmallIcon.Size = new System.Drawing.Size(58, 13);
            this.lblSmallIcon.TabIndex = 34;
            this.lblSmallIcon.Text = "Small icon:";
            // 
            // lblMediumIcon
            // 
            this.lblMediumIcon.Location = new System.Drawing.Point(20, 2);
            this.lblMediumIcon.Name = "lblMediumIcon";
            this.lblMediumIcon.Size = new System.Drawing.Size(70, 13);
            this.lblMediumIcon.TabIndex = 33;
            this.lblMediumIcon.Text = "Medium icon:";
            // 
            // panPctSmallIcon
            // 
            this.panPctSmallIcon.AssociatedSize = new System.Drawing.Size(0, 0);
            this.panPctSmallIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPctSmallIcon.Location = new System.Drawing.Point(183, 42);
            this.panPctSmallIcon.Margin = new System.Windows.Forms.Padding(0);
            this.panPctSmallIcon.Name = "panPctSmallIcon";
            this.panPctSmallIcon.Size = new System.Drawing.Size(50, 50);
            this.panPctSmallIcon.TabIndex = 30;
            this.panPctSmallIcon.Click += new System.EventHandler(this.panPctSmallIcon_Click);
            this.panPctSmallIcon.DoubleClick += new System.EventHandler(this.panPctSmallIcon_DoubleClick);
            // 
            // panPctMediumIcon
            // 
            this.panPctMediumIcon.AssociatedSize = new System.Drawing.Size(0, 0);
            this.panPctMediumIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPctMediumIcon.Location = new System.Drawing.Point(5, 17);
            this.panPctMediumIcon.Margin = new System.Windows.Forms.Padding(0);
            this.panPctMediumIcon.Name = "panPctMediumIcon";
            this.panPctMediumIcon.Size = new System.Drawing.Size(100, 100);
            this.panPctMediumIcon.TabIndex = 29;
            this.panPctMediumIcon.Click += new System.EventHandler(this.panPctMediumIcon_Click);
            this.panPctMediumIcon.DoubleClick += new System.EventHandler(this.panPctMediumIcon_DoubleClick);
            // 
            // chkUseSameImg
            // 
            this.chkUseSameImg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            // pannablePictureBoxControlPanelMedium
            // 
            this.pannablePictureBoxControlPanelMedium.Location = new System.Drawing.Point(3, 17);
            this.pannablePictureBoxControlPanelMedium.Name = "pannablePictureBoxControlPanelMedium";
            this.pannablePictureBoxControlPanelMedium.Size = new System.Drawing.Size(145, 165);
            this.pannablePictureBoxControlPanelMedium.TabIndex = 31;
            // 
            // pannablePictureBoxControlPanelSmall
            // 
            this.pannablePictureBoxControlPanelSmall.Location = new System.Drawing.Point(154, 17);
            this.pannablePictureBoxControlPanelSmall.Name = "pannablePictureBoxControlPanelSmall";
            this.pannablePictureBoxControlPanelSmall.Size = new System.Drawing.Size(140, 165);
            this.pannablePictureBoxControlPanelSmall.TabIndex = 32;
            // 
            // btnColourPicker
            // 
            this.btnColourPicker.BackgroundImage = global::TileIconifier.Properties.Resources.colorsquare;
            this.btnColourPicker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnColourPicker.FlatAppearance.BorderSize = 0;
            this.btnColourPicker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColourPicker.Location = new System.Drawing.Point(200, 266);
            this.btnColourPicker.Name = "btnColourPicker";
            this.btnColourPicker.Size = new System.Drawing.Size(27, 27);
            this.btnColourPicker.TabIndex = 39;
            this.btnColourPicker.UseVisualStyleBackColor = true;
            this.btnColourPicker.Click += new System.EventHandler(this.btnColourPicker_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(236, 230);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(64, 20);
            this.btnReset.TabIndex = 38;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblUnsaved
            // 
            this.lblUnsaved.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnsaved.ForeColor = System.Drawing.Color.Red;
            this.lblUnsaved.Location = new System.Drawing.Point(3, 234);
            this.lblUnsaved.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUnsaved.Name = "lblUnsaved";
            this.lblUnsaved.Size = new System.Drawing.Size(179, 13);
            this.lblUnsaved.TabIndex = 37;
            this.lblUnsaved.Text = "This shortcut has unsaved changes!";
            this.lblUnsaved.Visible = false;
            // 
            // pnlFGColour
            // 
            this.pnlFGColour.Controls.Add(this.radFGDark);
            this.pnlFGColour.Controls.Add(this.chkFGTxtEnabled);
            this.pnlFGColour.Controls.Add(this.lblFGText);
            this.pnlFGColour.Controls.Add(this.radFGLight);
            this.pnlFGColour.Location = new System.Drawing.Point(6, 304);
            this.pnlFGColour.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFGColour.Name = "pnlFGColour";
            this.pnlFGColour.Size = new System.Drawing.Size(294, 45);
            this.pnlFGColour.TabIndex = 36;
            // 
            // radFGDark
            // 
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
            this.chkFGTxtEnabled.Checked = true;
            this.chkFGTxtEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFGTxtEnabled.Location = new System.Drawing.Point(184, 4);
            this.chkFGTxtEnabled.Margin = new System.Windows.Forms.Padding(2);
            this.chkFGTxtEnabled.Name = "chkFGTxtEnabled";
            this.chkFGTxtEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkFGTxtEnabled.TabIndex = 7;
            this.chkFGTxtEnabled.Text = "Enabled";
            this.chkFGTxtEnabled.UseVisualStyleBackColor = true;
            this.chkFGTxtEnabled.CheckedChanged += new System.EventHandler(this.chkFGTxtEnabled_CheckedChanged);
            // 
            // lblFGText
            // 
            this.lblFGText.Location = new System.Drawing.Point(1, 5);
            this.lblFGText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFGText.Name = "lblFGText";
            this.lblFGText.Size = new System.Drawing.Size(179, 13);
            this.lblFGText.TabIndex = 6;
            this.lblFGText.Text = "Foreground Text (Medium Icon Only)";
            // 
            // radFGLight
            // 
            this.radFGLight.Checked = true;
            this.radFGLight.Location = new System.Drawing.Point(8, 23);
            this.radFGLight.Margin = new System.Windows.Forms.Padding(2);
            this.radFGLight.Name = "radFGLight";
            this.radFGLight.Size = new System.Drawing.Size(48, 17);
            this.radFGLight.TabIndex = 0;
            this.radFGLight.TabStop = true;
            this.radFGLight.Text = "Light";
            this.radFGLight.UseVisualStyleBackColor = true;
            this.radFGLight.CheckedChanged += new System.EventHandler(this.radFGLight_CheckedChanged);
            // 
            // cmbColour
            // 
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
            this.cmbColour.Location = new System.Drawing.Point(6, 270);
            this.cmbColour.Margin = new System.Windows.Forms.Padding(2);
            this.cmbColour.Name = "cmbColour";
            this.cmbColour.Size = new System.Drawing.Size(111, 21);
            this.cmbColour.TabIndex = 35;
            // 
            // lblBGColour
            // 
            this.lblBGColour.Location = new System.Drawing.Point(3, 254);
            this.lblBGColour.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBGColour.Name = "lblBGColour";
            this.lblBGColour.Size = new System.Drawing.Size(98, 13);
            this.lblBGColour.TabIndex = 34;
            this.lblBGColour.Text = "Background Colour";
            // 
            // txtBGColour
            // 
            this.txtBGColour.Location = new System.Drawing.Point(121, 270);
            this.txtBGColour.Margin = new System.Windows.Forms.Padding(2);
            this.txtBGColour.MaxLength = 7;
            this.txtBGColour.Name = "txtBGColour";
            this.txtBGColour.Size = new System.Drawing.Size(74, 20);
            this.txtBGColour.TabIndex = 33;
            this.txtBGColour.Text = "#323232";
            this.txtBGColour.TextChanged += new System.EventHandler(this.txtBGColour_TextChanged);
            // 
            // eyedropperColorPicker
            // 
            this.eyedropperColorPicker.Location = new System.Drawing.Point(236, 266);
            this.eyedropperColorPicker.Name = "eyedropperColorPicker";
            this.eyedropperColorPicker.SelectedColor = System.Drawing.Color.Empty;
            this.eyedropperColorPicker.Size = new System.Drawing.Size(30, 30);
            this.eyedropperColorPicker.TabIndex = 41;
            this.eyedropperColorPicker.Zoom = 4;
            this.eyedropperColorPicker.SelectedColorChanged += new System.EventHandler(this.eyedropperColorPicker_SelectedColorChanged);
            // 
            // TileIconifierPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.eyedropperColorPicker);
            this.Controls.Add(this.pnlImages);
            this.Controls.Add(this.btnColourPicker);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblUnsaved);
            this.Controls.Add(this.pnlFGColour);
            this.Controls.Add(this.cmbColour);
            this.Controls.Add(this.lblBGColour);
            this.Controls.Add(this.txtBGColour);
            this.Name = "TileIconifierPanel";
            this.Size = new System.Drawing.Size(305, 351);
            this.Load += new System.EventHandler(this.TileIconifierPanel_Load);
            this.pnlImages.ResumeLayout(false);
            this.pnlFGColour.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlImages;
        private System.Windows.Forms.Label lblSmallIcon;
        private System.Windows.Forms.Label lblMediumIcon;
        private PictureBox.PannablePictureBox panPctSmallIcon;
        private PictureBox.PannablePictureBox panPctMediumIcon;
        private System.Windows.Forms.CheckBox chkUseSameImg;
        private PannablePictureBoxControlPanel pannablePictureBoxControlPanelMedium;
        private PannablePictureBoxControlPanel pannablePictureBoxControlPanelSmall;
        private System.Windows.Forms.Button btnColourPicker;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblUnsaved;
        private System.Windows.Forms.Panel pnlFGColour;
        private System.Windows.Forms.RadioButton radFGDark;
        private System.Windows.Forms.CheckBox chkFGTxtEnabled;
        private System.Windows.Forms.Label lblFGText;
        private System.Windows.Forms.RadioButton radFGLight;
        private System.Windows.Forms.ComboBox cmbColour;
        private System.Windows.Forms.Label lblBGColour;
        private System.Windows.Forms.TextBox txtBGColour;
        private System.Windows.Forms.ColorDialog clrDialog;
        private EyedropColorPicker eyedropperColorPicker;
    }
}
