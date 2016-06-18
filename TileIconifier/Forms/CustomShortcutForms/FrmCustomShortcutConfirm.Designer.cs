namespace TileIconifier.Forms.CustomShortcutForms
{
    partial class FrmCustomShortcutConfirm
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
            this.txtCustomShortcutName = new System.Windows.Forms.TextBox();
            this.lblCustomShortcutName = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCaption = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCustomShortcutName
            // 
            this.txtCustomShortcutName.Location = new System.Drawing.Point(12, 122);
            this.txtCustomShortcutName.Name = "txtCustomShortcutName";
            this.txtCustomShortcutName.Size = new System.Drawing.Size(261, 20);
            this.txtCustomShortcutName.TabIndex = 0;
            this.txtCustomShortcutName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomShortcutName_KeyDown);
            // 
            // lblCustomShortcutName
            // 
            this.lblCustomShortcutName.AutoSize = true;
            this.lblCustomShortcutName.Location = new System.Drawing.Point(9, 106);
            this.lblCustomShortcutName.Name = "lblCustomShortcutName";
            this.lblCustomShortcutName.Size = new System.Drawing.Size(116, 13);
            this.lblCustomShortcutName.TabIndex = 1;
            this.lblCustomShortcutName.Text = "Custom Shortcut Name";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(12, 148);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(198, 148);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblCaption
            // 
            this.lblCaption.Location = new System.Drawing.Point(12, 9);
            this.lblCaption.MaximumSize = new System.Drawing.Size(250, 0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(250, 97);
            this.lblCaption.TabIndex = 4;
            this.lblCaption.Text = "Placeholder Text";
            // 
            // FrmCustomShortcutConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 179);
            this.ControlBox = false;
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblCustomShortcutName);
            this.Controls.Add(this.txtCustomShortcutName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmCustomShortcutConfirm";
            this.Text = "Clone As Custom Shortcut?";
            this.Load += new System.EventHandler(this.FrmCustomShortcutConfirm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCustomShortcutName;
        private System.Windows.Forms.Label lblCustomShortcutName;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCaption;
    }
}