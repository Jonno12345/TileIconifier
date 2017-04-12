using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TileIconifier.Forms.Shared
{
    public partial class FrmMessageBox : TileIconifier.Forms.SkinnableForm
    {
        private FrmMessageBox()
        {
            InitializeComponent();

            Font = SystemFonts.MessageBoxFont;            
        }

        public static DialogResult Show(
            IWin32Window owner,
            string text,
            string caption = null,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            using (var frm = new FrmMessageBox())
            {
                //Ensure the title bar always have text, because otherwise,
                //it is totally hidden. Alternatively, we could override 
                //the CreateParams property.
                if (caption == null || caption == "")
                {
                    caption = Application.ProductName;
                }

                frm.lblMsg.Text = text;
                frm.Text = caption;

                //Setup buttons
                switch(buttons)
                {
                    case MessageBoxButtons.OK:
                        frm.btn1.Text = "OK";
                        frm.btn1.DialogResult = DialogResult.OK;

                        frm.AcceptButton = frm.btn1;
                        frm.CancelButton = frm.btn1;

                        frm.btn2.Visible = false;
                        frm.btn3.Visible = false;
                        break;

                    case MessageBoxButtons.OKCancel:
                        frm.btn1.Text = "OK";
                        frm.btn1.DialogResult = DialogResult.OK;

                        frm.AcceptButton = frm.btn1;
                        frm.CancelButton = frm.btn2;

                        frm.btn2.Text = "Cancel";
                        frm.btn2.DialogResult = DialogResult.Cancel;

                        frm.btn3.Visible = false;
                        break;                        

                    case MessageBoxButtons.AbortRetryIgnore:
                        frm.btn1.Text = "Abord";
                        frm.btn1.DialogResult = DialogResult.Abort;

                        frm.btn2.Text = "Retry";
                        frm.btn2.DialogResult = DialogResult.Retry;

                        frm.btn3.Text = "Ignore";
                        frm.btn3.DialogResult = DialogResult.Ignore;

                        frm.ControlBox = false;// to fix
                        break;

                    case MessageBoxButtons.YesNoCancel:
                        frm.btn1.Text = "Yes";
                        frm.btn1.DialogResult = DialogResult.Yes;

                        frm.btn2.Text = "No";
                        frm.btn2.DialogResult = DialogResult.No;

                        frm.btn3.Text = "Cancel";
                        frm.btn3.DialogResult = DialogResult.Cancel;

                        frm.AcceptButton = frm.btn1;
                        frm.CancelButton = frm.btn3;
                        break;

                    case MessageBoxButtons.YesNo:
                        frm.btn1.Text = "Yes";
                        frm.btn1.DialogResult = DialogResult.Yes;

                        frm.btn2.Text = "No";
                        frm.btn2.DialogResult = DialogResult.No;

                        frm.AcceptButton = frm.btn1;

                        frm.ControlBox = false;// to fix
                        frm.btn3.Visible = false;
                        break;

                    case MessageBoxButtons.RetryCancel:
                        frm.btn1.Text = "Retry";
                        frm.btn1.DialogResult = DialogResult.Retry;

                        frm.btn2.Text = "Cancel";
                        frm.btn2.DialogResult = DialogResult.Cancel;

                        frm.AcceptButton = frm.btn1;
                        frm.CancelButton = frm.btn2;

                        frm.btn3.Visible = false;
                        break;

                    default:
                        throw new ArgumentException("Unkown combination of dialog buttons", nameof(buttons));
                }

                //Setup default button
                switch(defaultButton)
                {
                    case MessageBoxDefaultButton.Button1:
                        frm.btn1.NotifyDefault(true);
                        break;

                    case MessageBoxDefaultButton.Button2:
                        frm.btn2.NotifyDefault(true);
                        break;

                    case MessageBoxDefaultButton.Button3:
                        frm.btn3.NotifyDefault(true);
                        break;

                    default:
                        throw new ArgumentException("Unkown default buttons", nameof(defaultButton));
                }

                //Setup icon
                //Cast enum because there are seveal enum members with the same value.
                switch((int)icon)
                {
                    case 16: //That red error icon...
                        frm.SetIcon(SystemIcons.Error);
                        break;

                    case 48: //Warning
                        frm.SetIcon(SystemIcons.Exclamation);
                        break;

                    case 64: //Info
                        frm.SetIcon(SystemIcons.Information);
                        break;

                    default:
                        frm.pctIcon.Visible = false;
                        break;
                }

                return frm.ShowDialog(owner);
            }
        }
        
        public void SetIcon(Icon icon)
        {
            //Attempt to find an icon that's the same size as the picture box.
            //If the correct size is not available, the picture box will stretch
            //the image.
            using (var ico = new Icon(icon, pctIcon.Size))
            {
                pctIcon.Image = ico.ToBitmap();
            }
        }
    }
}
