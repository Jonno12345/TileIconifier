namespace TileIconifier.Controls
{
    partial class AllOrCurrentUserRadios
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
            this.radAllUsers = new System.Windows.Forms.RadioButton();
            this.lblHeader = new System.Windows.Forms.Label();
            this.radCurrentUser = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // radAllUsers
            // 
            this.radAllUsers.AutoSize = false;
            this.radAllUsers.Checked = true;
            this.radAllUsers.Location = new System.Drawing.Point(6, 22);
            this.radAllUsers.Name = "radAllUsers";
            this.radAllUsers.Size = new System.Drawing.Size(66, 17);
            this.radAllUsers.TabIndex = 0;
            this.radAllUsers.TabStop = true;
            this.radAllUsers.Text = "All Users";
            this.radAllUsers.UseVisualStyleBackColor = true;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = false;
            this.lblHeader.Location = new System.Drawing.Point(3, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(68, 13);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Shortcut For:";
            // 
            // radCurrentUser
            // 
            this.radCurrentUser.AutoSize = false;
            this.radCurrentUser.Location = new System.Drawing.Point(6, 45);
            this.radCurrentUser.Name = "radCurrentUser";
            this.radCurrentUser.Size = new System.Drawing.Size(84, 17);
            this.radCurrentUser.TabIndex = 2;
            this.radCurrentUser.Text = "Current User";
            this.radCurrentUser.UseVisualStyleBackColor = true;
            // 
            // AllOrCurrentUserRadios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.radCurrentUser);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.radAllUsers);
            this.Name = "AllOrCurrentUserRadios";
            this.Size = new System.Drawing.Size(101, 66);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radAllUsers;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.RadioButton radCurrentUser;
    }
}
