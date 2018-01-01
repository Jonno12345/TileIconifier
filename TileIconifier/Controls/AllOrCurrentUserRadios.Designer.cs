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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllOrCurrentUserRadios));
            this.radAllUsers = new TileIconifier.Controls.SkinnableRadioButton();
            this.lblHeader = new System.Windows.Forms.Label();
            this.radCurrentUser = new TileIconifier.Controls.SkinnableRadioButton();
            this.SuspendLayout();
            // 
            // radAllUsers
            // 
            this.radAllUsers.Checked = true;
            resources.ApplyResources(this.radAllUsers, "radAllUsers");
            this.radAllUsers.Name = "radAllUsers";
            this.radAllUsers.TabStop = true;
            this.radAllUsers.UseVisualStyleBackColor = true;
            // 
            // lblHeader
            // 
            resources.ApplyResources(this.lblHeader, "lblHeader");
            this.lblHeader.Name = "lblHeader";
            // 
            // radCurrentUser
            // 
            resources.ApplyResources(this.radCurrentUser, "radCurrentUser");
            this.radCurrentUser.Name = "radCurrentUser";
            this.radCurrentUser.UseVisualStyleBackColor = true;
            // 
            // AllOrCurrentUserRadios
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radCurrentUser);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.radAllUsers);
            this.Name = "AllOrCurrentUserRadios";
            this.ResumeLayout(false);

        }

        #endregion

        private TileIconifier.Controls.SkinnableRadioButton radAllUsers;
        private System.Windows.Forms.Label lblHeader;
        private TileIconifier.Controls.SkinnableRadioButton radCurrentUser;
    }
}
