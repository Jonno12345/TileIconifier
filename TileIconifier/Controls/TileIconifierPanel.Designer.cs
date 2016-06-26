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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TileIconifierPanel));
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
            resources.ApplyResources(this.pnlImages, "pnlImages");
            this.pnlImages.Controls.Add(this.lblSmallIcon);
            this.pnlImages.Controls.Add(this.lblMediumIcon);
            this.pnlImages.Controls.Add(this.panPctSmallIcon);
            this.pnlImages.Controls.Add(this.panPctMediumIcon);
            this.pnlImages.Controls.Add(this.chkUseSameImg);
            this.pnlImages.Controls.Add(this.pannablePictureBoxControlPanelMedium);
            this.pnlImages.Controls.Add(this.pannablePictureBoxControlPanelSmall);
            this.pnlImages.Name = "pnlImages";
            // 
            // lblSmallIcon
            // 
            resources.ApplyResources(this.lblSmallIcon, "lblSmallIcon");
            this.lblSmallIcon.Name = "lblSmallIcon";
            // 
            // lblMediumIcon
            // 
            resources.ApplyResources(this.lblMediumIcon, "lblMediumIcon");
            this.lblMediumIcon.Name = "lblMediumIcon";
            // 
            // panPctSmallIcon
            // 
            resources.ApplyResources(this.panPctSmallIcon, "panPctSmallIcon");
            this.panPctSmallIcon.AssociatedSize = new System.Drawing.Size(0, 0);
            this.panPctSmallIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPctSmallIcon.Name = "panPctSmallIcon";
            this.panPctSmallIcon.Click += new System.EventHandler(this.panPctSmallIcon_Click);
            this.panPctSmallIcon.DoubleClick += new System.EventHandler(this.panPctSmallIcon_DoubleClick);
            // 
            // panPctMediumIcon
            // 
            resources.ApplyResources(this.panPctMediumIcon, "panPctMediumIcon");
            this.panPctMediumIcon.AssociatedSize = new System.Drawing.Size(0, 0);
            this.panPctMediumIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPctMediumIcon.Name = "panPctMediumIcon";
            this.panPctMediumIcon.Click += new System.EventHandler(this.panPctMediumIcon_Click);
            this.panPctMediumIcon.DoubleClick += new System.EventHandler(this.panPctMediumIcon_DoubleClick);
            // 
            // chkUseSameImg
            // 
            resources.ApplyResources(this.chkUseSameImg, "chkUseSameImg");
            this.chkUseSameImg.Checked = true;
            this.chkUseSameImg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseSameImg.Name = "chkUseSameImg";
            this.chkUseSameImg.UseVisualStyleBackColor = true;
            // 
            // pannablePictureBoxControlPanelMedium
            // 
            resources.ApplyResources(this.pannablePictureBoxControlPanelMedium, "pannablePictureBoxControlPanelMedium");
            this.pannablePictureBoxControlPanelMedium.Name = "pannablePictureBoxControlPanelMedium";
            // 
            // pannablePictureBoxControlPanelSmall
            // 
            resources.ApplyResources(this.pannablePictureBoxControlPanelSmall, "pannablePictureBoxControlPanelSmall");
            this.pannablePictureBoxControlPanelSmall.Name = "pannablePictureBoxControlPanelSmall";
            // 
            // btnColourPicker
            // 
            resources.ApplyResources(this.btnColourPicker, "btnColourPicker");
            this.btnColourPicker.BackgroundImage = global::TileIconifier.Properties.Resources.colorsquare;
            this.btnColourPicker.FlatAppearance.BorderSize = 0;
            this.btnColourPicker.Name = "btnColourPicker";
            this.btnColourPicker.UseVisualStyleBackColor = true;
            this.btnColourPicker.Click += new System.EventHandler(this.btnColourPicker_Click);
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.Name = "btnReset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblUnsaved
            // 
            resources.ApplyResources(this.lblUnsaved, "lblUnsaved");
            this.lblUnsaved.ForeColor = System.Drawing.Color.Red;
            this.lblUnsaved.Name = "lblUnsaved";
            // 
            // pnlFGColour
            // 
            resources.ApplyResources(this.pnlFGColour, "pnlFGColour");
            this.pnlFGColour.Controls.Add(this.radFGDark);
            this.pnlFGColour.Controls.Add(this.chkFGTxtEnabled);
            this.pnlFGColour.Controls.Add(this.lblFGText);
            this.pnlFGColour.Controls.Add(this.radFGLight);
            this.pnlFGColour.Name = "pnlFGColour";
            // 
            // radFGDark
            // 
            resources.ApplyResources(this.radFGDark, "radFGDark");
            this.radFGDark.Name = "radFGDark";
            this.radFGDark.UseVisualStyleBackColor = true;
            // 
            // chkFGTxtEnabled
            // 
            resources.ApplyResources(this.chkFGTxtEnabled, "chkFGTxtEnabled");
            this.chkFGTxtEnabled.Checked = true;
            this.chkFGTxtEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFGTxtEnabled.Name = "chkFGTxtEnabled";
            this.chkFGTxtEnabled.UseVisualStyleBackColor = true;
            this.chkFGTxtEnabled.CheckedChanged += new System.EventHandler(this.chkFGTxtEnabled_CheckedChanged);
            // 
            // lblFGText
            // 
            resources.ApplyResources(this.lblFGText, "lblFGText");
            this.lblFGText.Name = "lblFGText";
            // 
            // radFGLight
            // 
            resources.ApplyResources(this.radFGLight, "radFGLight");
            this.radFGLight.Checked = true;
            this.radFGLight.Name = "radFGLight";
            this.radFGLight.TabStop = true;
            this.radFGLight.UseVisualStyleBackColor = true;
            this.radFGLight.CheckedChanged += new System.EventHandler(this.radFGLight_CheckedChanged);
            // 
            // cmbColour
            // 
            resources.ApplyResources(this.cmbColour, "cmbColour");
            this.cmbColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColour.FormattingEnabled = true;
            this.cmbColour.Items.AddRange(new object[] {
            resources.GetString("cmbColour.Items"),
            resources.GetString("cmbColour.Items1"),
            resources.GetString("cmbColour.Items2"),
            resources.GetString("cmbColour.Items3"),
            resources.GetString("cmbColour.Items4"),
            resources.GetString("cmbColour.Items5"),
            resources.GetString("cmbColour.Items6"),
            resources.GetString("cmbColour.Items7"),
            resources.GetString("cmbColour.Items8"),
            resources.GetString("cmbColour.Items9"),
            resources.GetString("cmbColour.Items10"),
            resources.GetString("cmbColour.Items11"),
            resources.GetString("cmbColour.Items12"),
            resources.GetString("cmbColour.Items13"),
            resources.GetString("cmbColour.Items14"),
            resources.GetString("cmbColour.Items15"),
            resources.GetString("cmbColour.Items16")});
            this.cmbColour.Name = "cmbColour";
            // 
            // lblBGColour
            // 
            resources.ApplyResources(this.lblBGColour, "lblBGColour");
            this.lblBGColour.Name = "lblBGColour";
            // 
            // txtBGColour
            // 
            resources.ApplyResources(this.txtBGColour, "txtBGColour");
            this.txtBGColour.Name = "txtBGColour";
            this.txtBGColour.TextChanged += new System.EventHandler(this.txtBGColour_TextChanged);
            // 
            // eyedropperColorPicker
            // 
            resources.ApplyResources(this.eyedropperColorPicker, "eyedropperColorPicker");
            this.eyedropperColorPicker.Name = "eyedropperColorPicker";
            this.eyedropperColorPicker.SelectedColor = System.Drawing.Color.Empty;
            this.eyedropperColorPicker.Zoom = 4;
            this.eyedropperColorPicker.SelectedColorChanged += new System.EventHandler(this.eyedropperColorPicker_SelectedColorChanged);
            // 
            // TileIconifierPanel
            // 
            resources.ApplyResources(this, "$this");
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
