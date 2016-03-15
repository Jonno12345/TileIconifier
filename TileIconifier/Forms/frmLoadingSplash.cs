using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileIconifier.Forms
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

        /// <summary>
        /// Stops user from moving this window
        /// </summary>
        /// <param name="message"></param>
        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }

            base.WndProc(ref message);
        }
    }
}
