namespace TileIconifier.Controls.IconifierPanel
{
    partial class ColorPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorPanel));
            this.panel1 = new System.Windows.Forms.Panel();
            this.eyedropperColorPicker = new TileIconifier.Controls.Eyedropper.EyedropColorPicker();
            this.btnColourPicker = new TileIconifier.Controls.SkinnableButton();
            this.pnlFGColour = new System.Windows.Forms.Panel();
            this.chkFGTxtEnabled = new TileIconifier.Controls.SkinnableCheckBox();
            this.radFGDark = new TileIconifier.Controls.SkinnableRadioButton();
            this.lblFGText = new System.Windows.Forms.Label();
            this.radFGLight = new TileIconifier.Controls.SkinnableRadioButton();
            this.cmbColour = new TileIconifier.Controls.SkinnableComboBox();
            this.lblBGColour = new System.Windows.Forms.Label();
            this.txtBGColour = new TileIconifier.Controls.SkinnableTextBox();
            this.clrDialog = new System.Windows.Forms.ColorDialog();
            this.panel1.SuspendLayout();
            this.pnlFGColour.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.eyedropperColorPicker);
            this.panel1.Controls.Add(this.btnColourPicker);
            this.panel1.Controls.Add(this.pnlFGColour);
            this.panel1.Controls.Add(this.cmbColour);
            this.panel1.Controls.Add(this.lblBGColour);
            this.panel1.Controls.Add(this.txtBGColour);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // eyedropperColorPicker
            // 
            resources.ApplyResources(this.eyedropperColorPicker, "eyedropperColorPicker");
            this.eyedropperColorPicker.Name = "eyedropperColorPicker";
            this.eyedropperColorPicker.SelectedColor = System.Drawing.Color.Empty;
            this.eyedropperColorPicker.SelectedColorChanged += new System.EventHandler(this.eyedropperColorPicker_SelectedColorChanged);
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
            // pnlFGColour
            // 
            resources.ApplyResources(this.pnlFGColour, "pnlFGColour");
            this.pnlFGColour.Controls.Add(this.chkFGTxtEnabled);
            this.pnlFGColour.Controls.Add(this.radFGDark);
            this.pnlFGColour.Controls.Add(this.lblFGText);
            this.pnlFGColour.Controls.Add(this.radFGLight);
            this.pnlFGColour.Name = "pnlFGColour";
            // 
            // chkFGTxtEnabled
            // 
            this.chkFGTxtEnabled.Checked = true;
            this.chkFGTxtEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.chkFGTxtEnabled, "chkFGTxtEnabled");
            this.chkFGTxtEnabled.Name = "chkFGTxtEnabled";
            this.chkFGTxtEnabled.UseVisualStyleBackColor = true;
            // 
            // radFGDark
            // 
            resources.ApplyResources(this.radFGDark, "radFGDark");
            this.radFGDark.Name = "radFGDark";
            this.radFGDark.UseVisualStyleBackColor = true;
            // 
            // lblFGText
            // 
            resources.ApplyResources(this.lblFGText, "lblFGText");
            this.lblFGText.Name = "lblFGText";
            // 
            // radFGLight
            // 
            this.radFGLight.Checked = true;
            resources.ApplyResources(this.radFGLight, "radFGLight");
            this.radFGLight.Name = "radFGLight";
            this.radFGLight.TabStop = true;
            this.radFGLight.UseVisualStyleBackColor = true;
            // 
            // cmbColour
            // 
            resources.ApplyResources(this.cmbColour, "cmbColour");
            this.cmbColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColour.FormattingEnabled = true;
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
            // 
            // ColorPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ColorPanel";
            this.Load += new System.EventHandler(this.ColorPanel_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlFGColour.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Eyedropper.EyedropColorPicker eyedropperColorPicker;
        private TileIconifier.Controls.SkinnableButton btnColourPicker;
        private System.Windows.Forms.Panel pnlFGColour;
        private TileIconifier.Controls.SkinnableRadioButton radFGDark;
        private TileIconifier.Controls.SkinnableCheckBox chkFGTxtEnabled;
        private System.Windows.Forms.Label lblFGText;
        private TileIconifier.Controls.SkinnableRadioButton radFGLight;
        private TileIconifier.Controls.SkinnableComboBox cmbColour;
        private System.Windows.Forms.Label lblBGColour;
        private TileIconifier.Controls.SkinnableTextBox txtBGColour;
        private System.Windows.Forms.ColorDialog clrDialog;
    }
}
