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
                ApplyFormSkin();
        }        

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            //Apply the skin to any control newly added to the form.
            if (FormSkin != null)
                ApplyControlSkin(e.Control);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Icon = Resources.tiles2_shadow_lyk_icon;
            SkinHandler.SkinChanged += SkinHandler_SkinChanged;
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
        /// Applies the skin on the form and each of its child controls.
        /// </summary>
        private void ApplyFormSkin()
        {
            ForeColor = FormSkin.ForeColor;
            BackColor = FormSkin.BackColor;
            Font = FormSkin.Font;

            foreach (Control c in Controls)
                ApplyControlSkin(c);
        }

        /// <summary>
        /// Applies the skin on the specified control.
        /// </summary>
        /// <param name="control"></param>
        private void ApplyControlSkin(Control control)
        {
            Type t = control.GetType();
            if (typeof(ISkinnableCheckableButton).IsAssignableFrom(t))
            {   //ChechBox or RadioButton
                ISkinnableCheckableButton checkableCtrl = (ISkinnableCheckableButton)control;
                if (checkableCtrl.Appearance == Appearance.Button)
                {
                    ApplyButtonSkin(checkableCtrl);
                }
                else
                {
                    //We treat checkboxes and radiobutton like labels (their Fore/Back/Disabled colors
                    //are ambiant) so there is no need to apply any skin properties manually. The
                    //only exception is FlatStyle, which is not ambiant and specific to Buttons/Checkboxes, etc.
                    checkableCtrl.FlatStyle = FormSkin.ButtonFlatStyle;
                }
                
            }
            else if (t == typeof(SkinnableButton))
            {   //Button
                //We MUST evaluate this condition AFTER typeof(ISkinnableCheckableButton).IsAssignableFrom(t)
                //because ISkinnableCheckableButton descends from ISkinnableButton, so we would otherwise
                //be unable to differenciate Buttons from Checkboxes and RadioButtons, because this condition would
                //be always true and the other would not be evaluated.
                ISkinnableButton btn = (ISkinnableButton)control;
                ApplyButtonSkin(btn);                         
            }            
            else if (t == typeof(SkinnableTextBox))
            {
                SkinnableTextBox txt = (SkinnableTextBox)control;
                txt.BorderStyle = FormSkin.TextBoxBorderStyle;
                txt.BackColor = FormSkin.TextBoxBackColor;
                txt.ReadOnlyBackColor = FormSkin.TextBoxReadOnlyBackColor;
                txt.BorderColor = FormSkin.TextBoxBorderColor;
                txt.BorderFocusedColor = FormSkin.TextBoxBorderFocusedColor;
                txt.BorderDisabledColor = FormSkin.TextBoxBorderDisabledColor;
                txt.ForeColor = FormSkin.TextBoxForeColor;                
            }
            else if (t.IsSubclassOf(typeof(SkinnableListView)))
            {
                SkinnableListView lvw = (SkinnableListView)control;
                lvw.HeadersUseVisualStyleColors = FormSkin.ListViewHeadersUseVisualStyleColors;
                lvw.HeaderBackColor = FormSkin.ListViewHeaderBackColor;
                lvw.HeaderForeColor = FormSkin.ListViewHeaderForeColor;                
                lvw.BackColor = FormSkin.ListViewBackColor;
                lvw.ForeColor = FormSkin.ListViewForeColor;
                lvw.BorderStyle = FormSkin.ListViewBorderStyle;
                lvw.BorderColor = FormSkin.ListViewBorderColor;
                lvw.BorderFocusedColor = FormSkin.ListViewBorderFocusedColor;
                lvw.BorderDisabledColor = FormSkin.ListViewBorderDisabledColor;
            }
            else if (t == typeof(SkinnableComboBox))
            {
                SkinnableComboBox cbo = (SkinnableComboBox)control;
                cbo.FlatStyle = FormSkin.ComboBoxFlatStyle;
                cbo.BackColor = FormSkin.ComboBoxBackColor;
                cbo.ForeColor = FormSkin.ComboBoxForeColor;
                cbo.FlatButtonBackColor = FormSkin.ComboBoxButtonBackColor;
                cbo.FlatButtonForeColor = FormSkin.ComboboxButtonForeColor;
                cbo.FlatButtonDisabledForeColor = FormSkin.ComboBoxDisabledForeColor;
                cbo.FlatButtonBorderColor = FormSkin.ComboBoxButtonBorderColor;
                cbo.FlatButtonBorderFocusedColor = FormSkin.ComboBoxButtonBorderFocusedColor;
            }
            else if (t.IsSubclassOf(typeof(ToolStrip)))
            {
                ToolStrip toolstrip = ((ToolStrip)control);
                toolstrip.Renderer = FormSkin.ToolStripRenderer;
            }
            else if (control.Controls.Count > 0) //Recursive loop that applies the skin to controls inside controls.
                foreach (Control c in control.Controls)
                    ApplyControlSkin(c);
        }


        /// <summary>
        /// Applies the part of the FormSkin that defines the appearance 
        /// of a button on the specified control.
        /// </summary>
        /// <param name="pButton"></param>
        private void ApplyButtonSkin(ISkinnableButton pButton)
        {
            pButton.FlatStyle = FormSkin.ButtonFlatStyle;
            pButton.ForeColor = FormSkin.ButtonForeColor;
            pButton.BackColor = FormSkin.ButtonBackColor;
            pButton.DisabledForeColor = FormSkin.ButtonDisabledForeColor;
            pButton.FlatAppearance.BorderColor = FormSkin.ButtonFlatBorderColor;
        }
    }
}