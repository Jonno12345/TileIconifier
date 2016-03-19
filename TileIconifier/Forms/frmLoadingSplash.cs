using System;
using System.Windows.Forms;

namespace TileIconifier.Forms
{
    public partial class FrmLoadingSplash : Form
    {
        private string _title = "Loading";

        public FrmLoadingSplash()
        {
            InitializeComponent();
        }

        private void tmrIncrement_Tick(object sender, EventArgs e)
        {
            const int maxPeriods = 5;

            if (lblLoading.Text.Split('.').Length > maxPeriods)
                lblLoading.Text = _title;

            if (Text.Split('.').Length > maxPeriods)
                Text = _title;

            lblLoading.Text += @".";
            Text += @".";
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
        ///     Stops user from moving this window
        /// </summary>
        /// <param name="message"></param>
        protected override void WndProc(ref Message message)
        {
            const int wmSyscommand = 0x0112;
            const int scMove = 0xF010;

            switch (message.Msg)
            {
                case wmSyscommand:
                    var command = message.WParam.ToInt32() & 0xfff0;
                    if (command == scMove)
                        return;
                    break;
            }

            base.WndProc(ref message);
        }
    }
}