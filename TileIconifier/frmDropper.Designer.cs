namespace TileIconifier
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
            this.chkNameOnSquare = new System.Windows.Forms.CheckBox();
            this.lblBGColour = new System.Windows.Forms.Label();
            this.txtBGColour = new System.Windows.Forms.TextBox();
            this.lblFGText = new System.Windows.Forms.Label();
            this.txtFGText = new System.Windows.Forms.TextBox();
            this.btnIconify = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLnkPath
            // 
            this.txtLnkPath.Location = new System.Drawing.Point(12, 38);
            this.txtLnkPath.Name = "txtLnkPath";
            this.txtLnkPath.Size = new System.Drawing.Size(370, 26);
            this.txtLnkPath.TabIndex = 1;
            // 
            // lblLnkPath
            // 
            this.lblLnkPath.AutoSize = true;
            this.lblLnkPath.Location = new System.Drawing.Point(8, 15);
            this.lblLnkPath.Name = "lblLnkPath";
            this.lblLnkPath.Size = new System.Drawing.Size(76, 20);
            this.lblLnkPath.TabIndex = 2;
            this.lblLnkPath.Text = "LNK Path";
            // 
            // chkNameOnSquare
            // 
            this.chkNameOnSquare.AutoSize = true;
            this.chkNameOnSquare.Checked = true;
            this.chkNameOnSquare.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNameOnSquare.Location = new System.Drawing.Point(12, 222);
            this.chkNameOnSquare.Name = "chkNameOnSquare";
            this.chkNameOnSquare.Size = new System.Drawing.Size(287, 24);
            this.chkNameOnSquare.TabIndex = 3;
            this.chkNameOnSquare.Text = "ShowNameOnSquare150x150Logo";
            this.chkNameOnSquare.UseVisualStyleBackColor = true;
            // 
            // lblBGColour
            // 
            this.lblBGColour.AutoSize = true;
            this.lblBGColour.Location = new System.Drawing.Point(8, 80);
            this.lblBGColour.Name = "lblBGColour";
            this.lblBGColour.Size = new System.Drawing.Size(145, 20);
            this.lblBGColour.TabIndex = 5;
            this.lblBGColour.Text = "Background Colour";
            // 
            // txtBGColour
            // 
            this.txtBGColour.Location = new System.Drawing.Point(12, 103);
            this.txtBGColour.Name = "txtBGColour";
            this.txtBGColour.Size = new System.Drawing.Size(141, 26);
            this.txtBGColour.TabIndex = 4;
            this.txtBGColour.Text = "#323232";
            // 
            // lblFGText
            // 
            this.lblFGText.AutoSize = true;
            this.lblFGText.Location = new System.Drawing.Point(8, 149);
            this.lblFGText.Name = "lblFGText";
            this.lblFGText.Size = new System.Drawing.Size(126, 20);
            this.lblFGText.TabIndex = 7;
            this.lblFGText.Text = "Foreground Text";
            // 
            // txtFGText
            // 
            this.txtFGText.Location = new System.Drawing.Point(12, 172);
            this.txtFGText.Name = "txtFGText";
            this.txtFGText.Size = new System.Drawing.Size(141, 26);
            this.txtFGText.TabIndex = 6;
            this.txtFGText.Text = "light";
            // 
            // btnIconify
            // 
            this.btnIconify.Location = new System.Drawing.Point(24, 314);
            this.btnIconify.Name = "btnIconify";
            this.btnIconify.Size = new System.Drawing.Size(129, 31);
            this.btnIconify.TabIndex = 8;
            this.btnIconify.Text = "Tile Iconify!";
            this.btnIconify.UseVisualStyleBackColor = true;
            this.btnIconify.Click += new System.EventHandler(this.btnIconify_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(238, 314);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(144, 31);
            this.btnRemove.TabIndex = 9;
            this.btnRemove.Text = "Remove Iconify";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // frmDropper
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 357);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnIconify);
            this.Controls.Add(this.lblFGText);
            this.Controls.Add(this.txtFGText);
            this.Controls.Add(this.lblBGColour);
            this.Controls.Add(this.txtBGColour);
            this.Controls.Add(this.chkNameOnSquare);
            this.Controls.Add(this.lblLnkPath);
            this.Controls.Add(this.txtLnkPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmDropper";
            this.Text = "Tile Iconifier";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmDropper_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.frmDropper_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtLnkPath;
        private System.Windows.Forms.Label lblLnkPath;
        private System.Windows.Forms.CheckBox chkNameOnSquare;
        private System.Windows.Forms.Label lblBGColour;
        private System.Windows.Forms.TextBox txtBGColour;
        private System.Windows.Forms.Label lblFGText;
        private System.Windows.Forms.TextBox txtFGText;
        private System.Windows.Forms.Button btnIconify;
        private System.Windows.Forms.Button btnRemove;
    }
}

