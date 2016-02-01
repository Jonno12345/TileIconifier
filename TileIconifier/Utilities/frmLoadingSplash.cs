using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileIconifier.Utilities
{
    public partial class frmLoadingSplash : Form
    {
        private string _title = "Loading";

        public frmLoadingSplash()
        {
            InitializeComponent();
        }

        private void tmrIncrement_Tick(object sender, EventArgs e)
        {
            int maxPeriods = 5;

            if (lblLoading.Text.Split('.').Count() > maxPeriods)
                lblLoading.Text = _title;

            if (Text.Split('.').Count() > maxPeriods)
                Text = _title;

            lblLoading.Text += ".";
            Text += ".";
        }

        public void SetTitle(string title)
        {
            _title = title;
        }

        public void WorkCompleted()
        {
            Close();
        }

        private void frmLoadingSplash_Load(object sender, EventArgs e)
        {

            tmrIncrement_Tick(null, null);
        }
    }
}
