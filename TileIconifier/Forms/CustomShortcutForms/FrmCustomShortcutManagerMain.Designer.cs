using TileIconifier.Controls;

namespace TileIconifier.Forms.CustomShortcutForms
{
    partial class FrmCustomShortcutManagerMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCustomShortcutManagerMain));
            this.btnCreateNewShortcut = new TileIconifier.Controls.SkinnableButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteCustomShortcut = new TileIconifier.Controls.SkinnableButton();
            this.lstCustomShortcuts = new TileIconifier.Controls.SortableListView();
            this.btnGotoShortcut = new TileIconifier.Controls.SkinnableButton();
            this.ilsCustomShortcutsSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateNewShortcut
            // 
            resources.ApplyResources(this.btnCreateNewShortcut, "btnCreateNewShortcut");
            this.btnCreateNewShortcut.BackColor = System.Drawing.SystemColors.Control;
            this.btnCreateNewShortcut.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCreateNewShortcut.Name = "btnCreateNewShortcut";
            this.btnCreateNewShortcut.UseVisualStyleBackColor = true;
            this.btnCreateNewShortcut.Click += new System.EventHandler(this.btnCreateNewShortcut_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // btnDeleteCustomShortcut
            // 
            resources.ApplyResources(this.btnDeleteCustomShortcut, "btnDeleteCustomShortcut");
            this.btnDeleteCustomShortcut.BackColor = System.Drawing.SystemColors.Control;
            this.btnDeleteCustomShortcut.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDeleteCustomShortcut.Name = "btnDeleteCustomShortcut";
            this.btnDeleteCustomShortcut.UseVisualStyleBackColor = true;
            this.btnDeleteCustomShortcut.Click += new System.EventHandler(this.btnDeleteCustomShortcut_Click);
            // 
            // lstCustomShortcuts
            // 
            resources.ApplyResources(this.lstCustomShortcuts, "lstCustomShortcuts");
            this.lstCustomShortcuts.BackColor = System.Drawing.SystemColors.Window;
            this.lstCustomShortcuts.BorderColor = System.Drawing.Color.Empty;
            this.lstCustomShortcuts.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lstCustomShortcuts.FullRowSelect = true;
            this.lstCustomShortcuts.HideSelection = false;
            this.lstCustomShortcuts.Name = "lstCustomShortcuts";
            this.lstCustomShortcuts.SmallImageList = this.ilsCustomShortcutsSmallIcons;
            this.lstCustomShortcuts.UseCompatibleStateImageBehavior = false;
            this.lstCustomShortcuts.View = System.Windows.Forms.View.Details;
            // 
            // btnGotoShortcut
            // 
            resources.ApplyResources(this.btnGotoShortcut, "btnGotoShortcut");
            this.btnGotoShortcut.BackColor = System.Drawing.SystemColors.Control;
            this.btnGotoShortcut.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGotoShortcut.Name = "btnGotoShortcut";
            this.btnGotoShortcut.UseVisualStyleBackColor = true;
            this.btnGotoShortcut.Click += new System.EventHandler(this.btnGotoShortcut_Click);
            // 
            // ilsCustomShortcutsSmallIcons
            // 
            this.ilsCustomShortcutsSmallIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.ilsCustomShortcutsSmallIcons, "ilsCustomShortcutsSmallIcons");
            this.ilsCustomShortcutsSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FrmCustomShortcutManagerMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnGotoShortcut);
            this.Controls.Add(this.lstCustomShortcuts);
            this.Controls.Add(this.btnDeleteCustomShortcut);
            this.Controls.Add(this.btnCreateNewShortcut);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmCustomShortcutManagerMain";
            this.Resize += new System.EventHandler(this.FrmCustomShortcutManagerMain_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TileIconifier.Controls.SkinnableButton btnCreateNewShortcut;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private TileIconifier.Controls.SkinnableButton btnDeleteCustomShortcut;
        private SortableListView lstCustomShortcuts;
        private TileIconifier.Controls.SkinnableButton btnGotoShortcut;
        private System.Windows.Forms.ImageList ilsCustomShortcutsSmallIcons;
    }
}