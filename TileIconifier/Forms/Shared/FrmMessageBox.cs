using System;
using System.Drawing;
using System.Media;

using System.Windows.Forms;

namespace TileIconifier.Forms.Shared
{
    public partial class FrmMessageBox : TileIconifier.Forms.SkinnableForm
    {
        private SystemSound _sound;

        private FrmMessageBox()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// Displays a message box.
        /// </summary>
        /// 
        /// <param name="owner">
        /// An implementation of <see cref="IWin32Window"/> that will 
        /// own the modal dialog box.</param>
        /// 
        /// <param name="text">
        /// The text to display in the message box.</param>
        /// 
        /// <param name="caption">
        /// The text to display in the title bar of the message box. 
        /// If the value is not specified or null, the product name 
        /// is used instead.</param>
        /// 
        /// <param name="buttons">
        /// One of the <see cref="MessageBoxButtons"/> values that 
        /// specifies which buttons to display in the message box.</param>
        /// 
        /// <param name="icon">
        /// One of the <see cref="MessageBoxIcon"/> values that specifies 
        /// which icon to display in the message box. </param>
        /// 
        /// <param name="defaultButton">
        /// One of the <see cref="MessageBoxDefaultButton"/> values that 
        /// specifies the default button for the message box. If the value 
        /// is null, the default button is determined by the 
        /// <see cref="MessageBoxButtons"/> value.</param>
        /// <returns></returns>
        public static DialogResult Show(
            IWin32Window owner,
            string text,
            string caption = null,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None,
            MessageBoxDefaultButton? defaultButton = null) //Use nullable, because there is no "default" value.
        {
            using (var frm = new FrmMessageBox())
            {
                //Ensure the title bar always have text, because otherwise,
                //it is totally hidden when ControlBox = false. Alternatively, 
                //we could override the CreateParams property.
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

                        frm.btn2.Visible = false;

                        frm.btn3.Visible = false;

                        frm.AcceptButton = frm.btn1;
                        frm.CancelButton = frm.btn1;
                        break;

                    case MessageBoxButtons.OKCancel:
                        frm.btn1.Text = "OK";
                        frm.btn1.DialogResult = DialogResult.OK;
                        
                        frm.btn2.Text = "Cancel";
                        frm.btn2.DialogResult = DialogResult.Cancel;                        

                        frm.btn3.Visible = false;

                        frm.AcceptButton = frm.btn1;
                        frm.CancelButton = frm.btn2;
                        break;                        

                    case MessageBoxButtons.AbortRetryIgnore:
                        frm.btn1.Text = "Abord";
                        frm.btn1.DialogResult = DialogResult.Abort;

                        frm.btn2.Text = "Retry";
                        frm.btn2.DialogResult = DialogResult.Retry;

                        frm.btn3.Text = "Ignore";
                        frm.btn3.DialogResult = DialogResult.Ignore;

                        frm.ControlBox = false;
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
                        
                        frm.btn3.Visible = false;

                        frm.AcceptButton = frm.btn1;
                        frm.ControlBox = false;
                        break;

                    case MessageBoxButtons.RetryCancel:
                        frm.btn1.Text = "Retry";
                        frm.btn1.DialogResult = DialogResult.Retry;

                        frm.btn2.Text = "Cancel";
                        frm.btn2.DialogResult = DialogResult.Cancel;                        

                        frm.btn3.Visible = false;

                        frm.AcceptButton = frm.btn1;
                        frm.CancelButton = frm.btn2;
                        break;

                    default:
                        throw new ArgumentException("Unkown combination of dialog buttons", nameof(buttons));
                }

                //Setup default button
                if (defaultButton != null)
                {
                    Button btn;
                    switch (defaultButton)
                    {
                        case MessageBoxDefaultButton.Button1:
                            btn = frm.btn1;
                            break;

                        case MessageBoxDefaultButton.Button2:
                            btn = frm.btn2;
                            break;

                        case MessageBoxDefaultButton.Button3:
                            btn = frm.btn3;
                            break;

                        default:
                            throw new ArgumentException("Unkown default buttons", nameof(defaultButton));
                    }
                    btn.NotifyDefault(true);
                    btn.Select();
                }
                

                //Setup icon and sound                
                //Cast enum to int because there are seveal enum members with the same value.
                switch((int)icon)
                {
                    case 16: //That red error icon...
                        frm.SetIcon(SystemIcons.Hand);
                        frm._sound = SystemSounds.Hand;
                        break;

                    case 48: //Warning
                        frm.SetIcon(SystemIcons.Exclamation);
                        frm._sound = SystemSounds.Exclamation;
                        break;

                    case 64: //Info
                        frm.SetIcon(SystemIcons.Asterisk);
                        frm._sound = SystemSounds.Asterisk;
                        break;

                    //Question message box icon is deprecated, so we purposely not implement it.

                    default:
                        frm.pctIcon.Visible = false;
                        break;
                }

                return frm.ShowDialog(owner);
            }
        }
        
        private void SetIcon(Icon icon)
        {
            //Attempt to find an icon that's the same size as the picture box.
            //If the correct size is not available, the picture box will stretch
            //the image.
            using (var ico = new Icon(icon, pctIcon.Size))
            {
                pctIcon.Image = ico.ToBitmap();
            }
        }

        protected override void OnShown(EventArgs e)
        {
            if (_sound != null)
            {
                _sound.Play();
            }

            base.OnShown(e);
        }
    }
}
