namespace TileIconifier.Forms.Shared
{
    partial class FrmMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMessageBox));
            this.tlpBody = new System.Windows.Forms.TableLayoutPanel();
            this.flpCommands = new System.Windows.Forms.FlowLayoutPanel();
            this.btn3 = new TileIconifier.Controls.SkinnableButton();
            this.btn2 = new TileIconifier.Controls.SkinnableButton();
            this.btn1 = new TileIconifier.Controls.SkinnableButton();
            this.pctIcon = new System.Windows.Forms.PictureBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.tlpBody.SuspendLayout();
            this.flpCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpBody
            // 
            this.tlpBody.AutoSize = true;
            this.tlpBody.ColumnCount = 2;
            this.tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBody.Controls.Add(this.flpCommands, 1, 1);
            this.tlpBody.Controls.Add(this.pctIcon, 0, 0);
            this.tlpBody.Controls.Add(this.lblMsg, 1, 0);
            this.tlpBody.Location = new System.Drawing.Point(13, 13);
            this.tlpBody.MaximumSize = new System.Drawing.Size(400, 0);
            this.tlpBody.Name = "tlpBody";
            this.tlpBody.RowCount = 2;
            this.tlpBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBody.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpBody.Size = new System.Drawing.Size(323, 104);
            this.tlpBody.TabIndex = 0;
            // 
            // flpCommands
            // 
            this.flpCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flpCommands.AutoSize = true;
            this.flpCommands.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpCommands.Controls.Add(this.btn3);
            this.flpCommands.Controls.Add(this.btn2);
            this.flpCommands.Controls.Add(this.btn1);
            this.flpCommands.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpCommands.Location = new System.Drawing.Point(80, 75);
            this.flpCommands.Margin = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.flpCommands.Name = "flpCommands";
            this.flpCommands.Size = new System.Drawing.Size(243, 29);
            this.flpCommands.TabIndex = 1;
            this.flpCommands.WrapContents = false;
            // 
            // btn3
            // 
            this.btn3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn3.AutoSize = true;
            this.btn3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn3.Location = new System.Drawing.Point(165, 3);
            this.btn3.MinimumSize = new System.Drawing.Size(75, 23);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(75, 23);
            this.btn3.TabIndex = 2;
            this.btn3.Text = "Button3";
            this.btn3.UseVisualStyleBackColor = true;
            // 
            // btn2
            // 
            this.btn2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn2.AutoSize = true;
            this.btn2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn2.Location = new System.Drawing.Point(84, 3);
            this.btn2.MinimumSize = new System.Drawing.Size(75, 23);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(75, 23);
            this.btn2.TabIndex = 1;
            this.btn2.Text = "Button 2";
            this.btn2.UseVisualStyleBackColor = true;
            // 
            // btn1
            // 
            this.btn1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn1.AutoSize = true;
            this.btn1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn1.Location = new System.Drawing.Point(3, 3);
            this.btn1.MinimumSize = new System.Drawing.Size(75, 23);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(75, 23);
            this.btn1.TabIndex = 0;
            this.btn1.Text = "Button 1";
            this.btn1.UseVisualStyleBackColor = true;
            // 
            // pctIcon
            // 
            this.pctIcon.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pctIcon.Location = new System.Drawing.Point(14, 14);
            this.pctIcon.Margin = new System.Windows.Forms.Padding(14);
            this.pctIcon.Name = "pctIcon";
            this.pctIcon.Size = new System.Drawing.Size(32, 32);
            this.pctIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pctIcon.TabIndex = 3;
            this.pctIcon.TabStop = false;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(63, 23);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(108, 13);
            this.lblMsg.TabIndex = 4;
            this.lblMsg.Text = "Placeholder message";
            // 
            // FrmMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(428, 222);
            this.Controls.Add(this.tlpBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMessageBox";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.tlpBody.ResumeLayout(false);
            this.tlpBody.PerformLayout();
            this.flpCommands.ResumeLayout(false);
            this.flpCommands.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpBody;
        private Controls.SkinnableButton btn3;
        private Controls.SkinnableButton btn1;
        private Controls.SkinnableButton btn2;
        private System.Windows.Forms.PictureBox pctIcon;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.FlowLayoutPanel flpCommands;
    }
}
