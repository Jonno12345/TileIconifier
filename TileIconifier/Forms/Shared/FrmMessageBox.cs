using System;
using System.Drawing;
using System.Media;
using System.Text;
using System.Windows.Forms;
using TileIconifier.Properties;

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
        /// If the value is null, the product name is used instead.</param>
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
        /// is null, the default button is the first one from the left</param>
        /// <returns></returns>
        public static DialogResult Show(
            IWin32Window owner,
            string text,
            string caption = null,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.None,

            //Use nullable for this parameter, because there is no 
            //"default" enum member.
            MessageBoxDefaultButton? defaultButton = null)
        {
            using (var frm = new FrmMessageBox())
            {
                frm.SetText(text, caption);
                frm.SetButtons(buttons);
                frm.SetDefaultButton(defaultButton);
                frm.SetDecoration(icon);

                return frm.ShowDialog(owner);
            }
        }

        #region "Setup methods"
        private void SetText(string text, string caption)
        {
            //Ensure the title bar always have text, because otherwise,
            //it is totally hidden when ControlBox = false. Alternatively, 
            //we could override the CreateParams property.
            if (string.IsNullOrEmpty(caption))
            {
                caption = Application.ProductName;
            }

            lblMsg.Text = text;
            Text = caption;
        }

        private void SetButtons(MessageBoxButtons buttons)
        {
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    btn1.Text = Strings.Ok;
                    btn1.DialogResult = DialogResult.OK;

                    btn2.Visible = false;

                    btn3.Visible = false;

                    AcceptButton = btn1;
                    CancelButton = btn1;
                    break;

                case MessageBoxButtons.OKCancel:
                    btn1.Text = Strings.Ok;
                    btn1.DialogResult = DialogResult.OK;

                    btn2.Text = Strings.Cancel;
                    btn2.DialogResult = DialogResult.Cancel;

                    btn3.Visible = false;

                    AcceptButton = btn1;
                    CancelButton = btn2;
                    break;

                case MessageBoxButtons.AbortRetryIgnore:
                    btn1.Text = Strings.Abort;
                    btn1.DialogResult = DialogResult.Abort;

                    btn2.Text = Strings.Retry;
                    btn2.DialogResult = DialogResult.Retry;

                    btn3.Text = Strings.Ignore;
                    btn3.DialogResult = DialogResult.Ignore;

                    ControlBox = false;
                    break;

                case MessageBoxButtons.YesNoCancel:
                    btn1.Text = Strings.Yes;
                    btn1.DialogResult = DialogResult.Yes;

                    btn2.Text = Strings.No;
                    btn2.DialogResult = DialogResult.No;

                    btn3.Text = Strings.Cancel;
                    btn3.DialogResult = DialogResult.Cancel;

                    AcceptButton = btn1;
                    CancelButton = btn3;
                    break;

                case MessageBoxButtons.YesNo:
                    btn1.Text = Strings.Yes;
                    btn1.DialogResult = DialogResult.Yes;

                    btn2.Text = Strings.No;
                    btn2.DialogResult = DialogResult.No;

                    btn3.Visible = false;

                    AcceptButton = btn1;
                    ControlBox = false;
                    break;

                case MessageBoxButtons.RetryCancel:
                    btn1.Text = Strings.Retry;
                    btn1.DialogResult = DialogResult.Retry;

                    btn2.Text = Strings.Cancel;
                    btn2.DialogResult = DialogResult.Cancel;

                    btn3.Visible = false;

                    AcceptButton = btn1;
                    CancelButton = btn2;
                    break;

                default:
                    throw new ArgumentException("Unkown combination of dialog buttons", nameof(buttons));
            }
        }

        private void SetDefaultButton(MessageBoxDefaultButton? defaultButton)
        {            
            if (defaultButton != null)
            {
                Button btn;
                switch (defaultButton)
                {
                    case MessageBoxDefaultButton.Button1:
                        btn = btn1;
                        break;

                    case MessageBoxDefaultButton.Button2:
                        btn = btn2;
                        break;

                    case MessageBoxDefaultButton.Button3:
                        btn = btn3;
                        break;

                    default:
                        throw new ArgumentException("Unkown default buttons", nameof(defaultButton));
                }
                btn.NotifyDefault(true);
                btn.Select();
            }
        }

        private void SetDecoration(MessageBoxIcon icon)
        {
            Action<Icon> setIcon = i =>
            {
                //Attempt to find an icon that's the same size as the picture box.
                //If the correct size is not available, the picture box will stretch
                //the image.
                using (var ico = new Icon(i, pctIcon.Size))
                {
                    pctIcon.Image = ico.ToBitmap();
                }
            };
            
            //Cast enum to int because there are seveal enum members with the same value.
            switch ((int)icon)
            {
                case 16: //That red error icon...
                    setIcon(SystemIcons.Hand);
                    _sound = SystemSounds.Hand;
                    break;

                case 48: //Warning
                    setIcon(SystemIcons.Exclamation);
                    _sound = SystemSounds.Exclamation;
                    break;

                case 64: //Info
                    setIcon(SystemIcons.Asterisk);
                    _sound = SystemSounds.Asterisk;
                    break;

                //Question message box icon is deprecated, so we purposely not implement it.

                default:
                    pctIcon.Visible = false;
                    break;
            }
        }
        #endregion

        protected override void OnShown(EventArgs e)
        {
            if (_sound != null)
            {
                _sound.Play();
            }

            base.OnShown(e);
        }

        //Re-implements an obscure feature of the standard Windows message box where 
        //its content is copied to the clipboard when it is active and Ctrl+C is pressed.
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Control && e.KeyCode == Keys.C)
            {
                var sb = new StringBuilder();
                Action<Button> handleButton = b =>
                {
                    if (b.Visible)
                    {
                        sb.AppendFormat("[{0}] ", b.Text);
                    }
                };

                //Add text from the caption and the message
                var common = string.Format(
                    "[Title]" +
                    Environment.NewLine +
                    "{0}" +
                    Environment.NewLine +
                    Environment.NewLine +
                    "[Message]" +
                    Environment.NewLine +
                    "{1}" +
                    Environment.NewLine + 
                    Environment.NewLine, Text, lblMsg.Text);
                sb.Append(common);

                //Add the text from the buttons.
                //Note that the order of a button in the ControlCollection is determined by its z-index.
                var ctrls = flpCommands.Controls;
                if (RightToLeftLayout)
                {
                    for (var i = 0; i < ctrls.Count; i++)
                    {
                        handleButton((Button)ctrls[i]);
                    }
                }
                else
                {
                    for (var i = ctrls.Count - 1; i >= 0; i--)
                    {
                        handleButton((Button)ctrls[i]);
                    }
                }                

                //Remove the last space that was added in case there would be another button.
                sb.Remove(sb.Length - 1, 1);

                Clipboard.SetText(sb.ToString());
            }
        }
    }
}
