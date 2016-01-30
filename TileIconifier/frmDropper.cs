using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TileIconifier.Utilities;

namespace TileIconifier
{
    public partial class frmDropper : Form
    {
        public frmDropper()
        {
            InitializeComponent();
        }

        //
        //
        //Code for drag and drop. Doesn't work unless you're dragging from an
        //elevated explorer window though, so left out for now.
        //

        //private void frmDropper_DragEnter(object sender, DragEventArgs e)
        //{
        //    e.Effect = DragDropEffects.Copy;
        //}

        //private void frmDropper_DragDrop(object sender, DragEventArgs e)
        //{
        //    string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        //    var lnkFiles = FileList.Where(f => Path.GetExtension(f).ToUpper() == ".LNK");

        //    try
        //    {
        //        foreach (var lnkFile in lnkFiles)
        //        {

        //            new TileIcon(BuildParameters());
        //        }
        //    }
        //    catch (UserCancellationException)
        //    {
        //        MessageBox.Show("Process cancelled.");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message + " - " + ex.ToString());
        //    }
        //}

        private TileIconParameters BuildParameters()
        {
            return new TileIconParameters()
            {
                BgColour = txtBGColour.Text,
                FgText = txtFGText.Text,
                ShowNameOnSquare150x150Logo = chkNameOnSquare.Checked,
                LnkFilePath = txtLnkPath.Text
            };
        }

        private void btnIconify_Click(object sender, EventArgs e)
        {
            if (!DoValidation())
                return;

            try
            {
                var TileIconify = new TileIcon(BuildParameters());
                TileIconify.RunIconify();
            }
            catch (UserCancellationException)
            {
                MessageBox.Show("Process cancelled.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - " + ex.ToString());
            }
        }


        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (!DoValidation(true))
                return;

            if (MessageBox.Show("Are you sure you wish to remove iconification?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var TileDeIconify = new TileIcon(BuildParameters());
                TileDeIconify.DeIconify();
            }
        }




        private bool DoValidation(bool pathOnly = false)
        {
            ResetValidation();
            CleanFields();

            return ValidateFields(pathOnly);
        }

        private void CleanFields()
        {
            txtLnkPath.Text = txtLnkPath.Text.Replace("\"", "");
        }

        private void ResetValidation()
        {
            txtLnkPath.BackColor = Color.White;
            txtFGText.BackColor = Color.White;
            txtBGColour.BackColor = Color.White;
        }

        private bool ValidateFields(bool pathOnly = false)
        {
            bool valid = true;

            Action<Control> controlInvalid = (c => { c.BackColor = Color.Red; valid = false; });

            if (!File.Exists(txtLnkPath.Text))
                controlInvalid(txtLnkPath);

            if (Path.GetExtension(txtLnkPath.Text).ToUpper() != ".LNK")
                controlInvalid(txtLnkPath);

            if (!pathOnly)
            {
                if (!Regex.Match(txtBGColour.Text, @"#\d{6}").Success)
                    controlInvalid(txtBGColour);
            }

            return valid;
        }
    }
}
