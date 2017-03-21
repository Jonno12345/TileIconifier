#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2016 Johnathon M
// 
//         Permission is hereby granted, free of charge, to any person obtaining a copy
//         of this software and associated documentation files (the "Software"), to deal
//         in the Software without restriction, including without limitation the rights
//         to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//         copies of the Software, and to permit persons to whom the Software is
//         furnished to do so, subject to the following conditions:
// 
//         The above copyright notice and this permission notice shall be included in
//         all copies or substantial portions of the Software.
// 
//         THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//         IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//         FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//         AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//         LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//         OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//         THE SOFTWARE.
// 
// */

#endregion

using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TileIconifier.Controls;
using TileIconifier.Properties;
using TileIconifier.Skinning;
using TileIconifier.Skinning.Skins;

namespace TileIconifier.Forms
{
    public class SkinnableForm : Form
    {
        private BaseSkin formSkin = SkinHandler.GetCurrentSkin();
        public BaseSkin FormSkin
        {
            get { return formSkin; }
            set
            {
                if (formSkin != value)
                {
                    formSkin = value;
                    OnSkinChanged(EventArgs.Empty);
                }
            }
        }

        protected virtual void OnSkinChanged(EventArgs e)
        {
            if (FormSkin != null)
            {
                ApplyFormSkin();
                ApplyAllControlsSkin();
            }
        }        

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            //Apply the skin to any control newly added to the form.
            if (FormSkin != null && !DesignMode)
                ApplyControlSkin(e.Control);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Icon = Resources.tiles2_shadow_lyk_icon;
            SkinHandler.SkinChanged += SkinHandler_SkinChanged;
            
            //We only apply the skin on the form, not on its controls
            //because control skins are applied when controls are added
            //to the form.
            if (!DesignMode)
                ApplyFormSkin();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            //FormClosed is a good place to remove an handler that was added when Load, because
            //if the form is ever shown again, it will raise the Load event, which will add the
            //handler back (even if the form was just hidden).
            SkinHandler.SkinChanged -= SkinHandler_SkinChanged;
        }

        private void SkinHandler_SkinChanged(object sender, EventArgs e)
        {
            FormSkin = SkinHandler.GetCurrentSkin();
        }

        /// <summary>
        /// Applies the skin on the form only.
        /// </summary>
        private void ApplyFormSkin()
        {
            ForeColor = FormSkin.ForeColor;
            BackColor = FormSkin.BackColor;
            Font = FormSkin.Font;            
        }

        /// <summary>
        /// Applies the skin on all controls inside the form.
        /// </summary>
        private void ApplyAllControlsSkin()
        {
            foreach (Control c in Controls)
                ApplyControlSkin(c);
        }

        /// <summary>
        /// Applies the skin on the specified control.
        /// </summary>
        /// <param name="control"></param>
        private void ApplyControlSkin(Control control)
        {
            //Maybe not the most efficient way to do this. Something to think about.

            //Checkbox or RadioButton
            ISkinnableCheckableButton checkableBtn = control as ISkinnableCheckableButton;
            if (checkableBtn != null)
            {                   
                if (checkableBtn.Appearance == Appearance.Button)
                {
                    ApplyButtonSkin(checkableBtn);
                }
                else
                {
                    //We treat checkboxes and radiobutton like labels (their Fore/Back colors
                    //are ambiant) so there is no need to apply any skin properties manually. The
                    //only exceptions are FlatStyle and DisabledForeColor, which are not ambiant 
                    //and specific to ISkinnableButton in the case of FlatStyle.
                    checkableBtn.FlatStyle = FormSkin.ButtonFlatStyle;                    
                }
                return;
            }

            //Button
            ISkinnableButton btn = control as ISkinnableButton;
            if (btn != null)
            {
                //We MUST evaluate this condition AFTER ISkinnableCheckableButton
                //because ISkinnableCheckableButton descends from ISkinnableButton, so we would otherwise
                //be unable to differenciate Buttons from Checkboxes and RadioButtons, because this condition would
                //be always true and the other would not be evaluated.
                ApplyButtonSkin(btn);
                return;
            }

            //TextBox or RichTextBox
            ISkinnableTextBox txt = control as ISkinnableTextBox;
            if (txt != null)
            {
                txt.BorderStyle = FormSkin.TextBoxBorderStyle;
                txt.BackColor = FormSkin.TextBoxBackColor;
                txt.ReadOnlyBackColor = FormSkin.TextBoxReadOnlyBackColor;
                txt.BorderColor = FormSkin.TextBoxBorderColor;
                txt.BorderFocusedColor = FormSkin.TextBoxBorderFocusedColor;
                txt.BorderDisabledColor = FormSkin.TextBoxBorderDisabledColor;
                txt.ForeColor = FormSkin.TextBoxForeColor;
                return;
            }            

            //ListView
            SkinnableListView lvw = control as SkinnableListView;
            if (lvw != null)
            {
                lvw.FlatStyle = FormSkin.ListViewFlatStyle;
                lvw.FlatHeaderBackColor = FormSkin.ListViewHeaderBackColor;
                lvw.FlatHeaderForeColor = FormSkin.ListViewHeaderForeColor;
                lvw.BackColor = FormSkin.ListViewBackColor;
                lvw.ForeColor = FormSkin.ListViewForeColor;                
                lvw.FlatBorderColor = FormSkin.ListViewBorderColor;
                lvw.FlatBorderFocusedColor = FormSkin.ListViewBorderFocusedColor;
                lvw.FlatBorderDisabledColor = FormSkin.ListViewBorderDisabledColor;
                return;
            }

            //ComboBox
            SkinnableComboBox cmb = control as SkinnableComboBox;
            if (cmb != null)
            {
                cmb.FlatStyle = FormSkin.ComboBoxFlatStyle;
                cmb.BackColor = FormSkin.ComboBoxBackColor;
                cmb.ForeColor = FormSkin.ComboBoxForeColor;
                cmb.FlatButtonBackColor = FormSkin.ComboBoxButtonBackColor;
                cmb.FlatButtonForeColor = FormSkin.ComboboxButtonForeColor;
                cmb.FlatButtonDisabledForeColor = FormSkin.ComboBoxDisabledForeColor;
                cmb.FlatButtonBorderColor = FormSkin.ComboBoxButtonBorderColor;
                cmb.FlatButtonBorderFocusedColor = FormSkin.ComboBoxButtonBorderFocusedColor;
                return;
            }

            //ToolStrip
            ToolStrip tsp = control as ToolStrip;
            if (tsp != null)
            {
                tsp.Renderer = FormSkin.ToolStripRenderer;
                return;
            }

            //Recursive loop that applies the skin to controls inside controls. At this
            //point, the control is not a button, a listview, etc. so it is likely to be just
            //a container that contains more controls.
            if (control.Controls.Count > 0)
                foreach (Control c in control.Controls)
                    ApplyControlSkin(c);
        }


        /// <summary>
        /// Applies the part of the FormSkin that defines the appearance 
        /// of a button on the specified control.
        /// </summary>
        /// <param name="button"></param>
        private void ApplyButtonSkin(ISkinnableButton button)
        {
            button.FlatStyle = FormSkin.ButtonFlatStyle;
            button.ForeColor = FormSkin.ButtonForeColor;
            button.BackColor = FormSkin.ButtonBackColor;
            button.DisabledForeColor = FormSkin.ButtonDisabledForeColor;
            button.FlatAppearance.BorderColor = FormSkin.ButtonFlatBorderColor;
        }
    }
}