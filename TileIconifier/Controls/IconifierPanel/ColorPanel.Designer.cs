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
            this.cmbColour = new TileIconifier.Controls.SkinnableComboBox();
            this.lblBGColour = new System.Windows.Forms.Label();
            this.txtBGColour = new TileIconifier.Controls.SkinnableTextBox();
            this.chkFGTxtEnabled = new TileIconifier.Controls.SkinnableCheckBox();
            this.radFGDark = new TileIconifier.Controls.SkinnableRadioButton();
            this.lblFGText = new System.Windows.Forms.Label();
            this.radFGLight = new TileIconifier.Controls.SkinnableRadioButton();
            this.clrDialog = new System.Windows.Forms.ColorDialog();
            this.tlpFGColour = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.tlpFGColour.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.eyedropperColorPicker);
            this.panel1.Controls.Add(this.btnColourPicker);
            this.panel1.Controls.Add(this.cmbColour);
            this.panel1.Controls.Add(this.lblBGColour);
            this.panel1.Controls.Add(this.txtBGColour);
            this.panel1.Name = "panel1";
            // 
            // eyedropperColorPicker
            // 
            resources.ApplyResources(this.eyedropperColorPicker, "eyedropperColorPicker");
            this.eyedropperColorPicker.Name = "eyedropperColorPicker";
            this.eyedropperColorPicker.Zoom = 6;
            this.eyedropperColorPicker.SelectedColorChanged += new System.EventHandler(this.eyedropperColorPicker_SelectedColorChanged);
            // 
            // btnColourPicker
            // 
            this.btnColourPicker.BackgroundImage = global::TileIconifier.Properties.Resources.colorsquare;
            resources.ApplyResources(this.btnColourPicker, "btnColourPicker");
            this.btnColourPicker.FlatAppearance.BorderSize = 0;
            this.btnColourPicker.Name = "btnColourPicker";
            this.btnColourPicker.UseVisualStyleBackColor = true;
            this.btnColourPicker.Click += new System.EventHandler(this.btnColourPicker_Click);
            // 
            // cmbColour
            // 
            this.cmbColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColour.FormattingEnabled = true;
            resources.ApplyResources(this.cmbColour, "cmbColour");
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
            // chkFGTxtEnabled
            // 
            resources.ApplyResources(this.chkFGTxtEnabled, "chkFGTxtEnabled");
            this.chkFGTxtEnabled.Checked = true;
            this.chkFGTxtEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
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
            this.tlpFGColour.SetColumnSpan(this.lblFGText, 2);
            this.lblFGText.Name = "lblFGText";
            // 
            // radFGLight
            // 
            resources.ApplyResources(this.radFGLight, "radFGLight");
            this.radFGLight.Checked = true;
            this.radFGLight.Name = "radFGLight";
            this.radFGLight.TabStop = true;
            this.radFGLight.UseVisualStyleBackColor = true;
            // 
            // tlpFGColour
            // 
            resources.ApplyResources(this.tlpFGColour, "tlpFGColour");
            this.tlpFGColour.Controls.Add(this.lblFGText, 0, 0);
            this.tlpFGColour.Controls.Add(this.radFGDark, 1, 1);
            this.tlpFGColour.Controls.Add(this.radFGLight, 0, 1);
            this.tlpFGColour.Controls.Add(this.chkFGTxtEnabled, 2, 0);
            this.tlpFGColour.Name = "tlpFGColour";
            // 
            // ColorPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpFGColour);
            this.Controls.Add(this.panel1);
            this.Name = "ColorPanel";
            this.Load += new System.EventHandler(this.ColorPanel_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tlpFGColour.ResumeLayout(false);
            this.tlpFGColour.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Eyedropper.EyedropColorPicker eyedropperColorPicker;
        private TileIconifier.Controls.SkinnableButton btnColourPicker;
        private TileIconifier.Controls.SkinnableRadioButton radFGDark;
        private SkinnableCheckBox chkFGTxtEnabled;
        private System.Windows.Forms.Label lblFGText;
        private TileIconifier.Controls.SkinnableRadioButton radFGLight;
        private TileIconifier.Controls.SkinnableComboBox cmbColour;
        private System.Windows.Forms.Label lblBGColour;
        private SkinnableTextBox txtBGColour;
        private System.Windows.Forms.ColorDialog clrDialog;
        private System.Windows.Forms.TableLayoutPanel tlpFGColour;
    }
}
