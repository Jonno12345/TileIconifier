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
            this.btnColourPicker = new System.Windows.Forms.Button();
            this.pnlFGColour = new System.Windows.Forms.Panel();
            this.chkFGTxtEnabled = new System.Windows.Forms.CheckBox();
            this.radFGDark = new System.Windows.Forms.RadioButton();
            this.lblFGText = new System.Windows.Forms.Label();
            this.radFGLight = new System.Windows.Forms.RadioButton();
            this.cmbColour = new System.Windows.Forms.ComboBox();
            this.lblBGColour = new System.Windows.Forms.Label();
            this.txtBGColour = new System.Windows.Forms.TextBox();
            this.clrDialog = new System.Windows.Forms.ColorDialog();
            this.panel1.SuspendLayout();
            this.pnlFGColour.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.eyedropperColorPicker);
            this.panel1.Controls.Add(this.btnColourPicker);
            this.panel1.Controls.Add(this.pnlFGColour);
            this.panel1.Controls.Add(this.cmbColour);
            this.panel1.Controls.Add(this.lblBGColour);
            this.panel1.Controls.Add(this.txtBGColour);
            this.panel1.Name = "panel1";
            // 
            // eyedropperColorPicker
            // 
            resources.ApplyResources(this.eyedropperColorPicker, "eyedropperColorPicker");
            this.eyedropperColorPicker.Name = "eyedropperColorPicker";
            this.eyedropperColorPicker.SelectedColor = System.Drawing.Color.Empty;
            this.eyedropperColorPicker.Zoom = 4;
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
            // 
            // ColorPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ColorPanel";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlFGColour.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Eyedropper.EyedropColorPicker eyedropperColorPicker;
        private System.Windows.Forms.Button btnColourPicker;
        private System.Windows.Forms.Panel pnlFGColour;
        private System.Windows.Forms.RadioButton radFGDark;
        private System.Windows.Forms.CheckBox chkFGTxtEnabled;
        private System.Windows.Forms.Label lblFGText;
        private System.Windows.Forms.RadioButton radFGLight;
        private System.Windows.Forms.ComboBox cmbColour;
        private System.Windows.Forms.Label lblBGColour;
        private System.Windows.Forms.TextBox txtBGColour;
        private System.Windows.Forms.ColorDialog clrDialog;
    }
}
