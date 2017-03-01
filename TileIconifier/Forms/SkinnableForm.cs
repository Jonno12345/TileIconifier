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
            if (t == typeof(SkinnableButton))
            {   //Button
                SkinnableButton btn = (SkinnableButton)control;
                btn.FlatStyle = FormSkin.ButtonFlatStyle;
                btn.ForeColor = FormSkin.ButtonForeColor;
                btn.BackColor = FormSkin.ButtonBackColor;
                btn.ForeColorDisabled = FormSkin.ButtonForeColorDisabled;
                btn.FlatAppearance.BorderColor = FormSkin.ButtonFlatBorderColor;
            }
            else if (t == typeof(SkinnableCheckBox))
            {   //Checkbox
                SkinnableCheckBox chk = (SkinnableCheckBox)control;
                
                //A checkbox can have the appearance of a button, so we apply the skin accordingly.
                if (chk.Appearance == Appearance.Button)
                {   chk.ForeColor = FormSkin.ButtonForeColor;
                    chk.BackColor = FormSkin.ButtonBackColor;
                    chk.ForeColorDisabled = FormSkin.ButtonForeColorDisabled;
                    chk.FlatAppearance.BorderColor = FormSkin.ButtonFlatBorderColor;
                }
                //else
                //{
                //    //We treat the checkbox like a label, which means that we don't need
                //    //to set any color since they are ambiant.
                //}

                //Button and Checkboxes have this property in common, which we apply in both cases.
                chk.FlatStyle = FormSkin.ButtonFlatStyle;                
            }
            else if (t == typeof(SkinnableRadioButton))
            {
                SkinnableRadioButton rbt = (SkinnableRadioButton)control;

                //A radiobutton can have the appearance of a button, so we apply the skin accordingly.
                if (rbt.Appearance == Appearance.Button)
                {
                    rbt.ForeColor = FormSkin.ButtonForeColor;
                    rbt.BackColor = FormSkin.ButtonBackColor;
                    rbt.ForeColorDisabled = FormSkin.ButtonForeColorDisabled;
                    rbt.FlatAppearance.BorderColor = FormSkin.ButtonFlatBorderColor;
                }
                //else
                //{
                //    //We treat the checkbox like a label, which means that we don't need
                //    //to set any color since they are ambiant.
                //}

                //Button and RadioButtons have this property in common, which we apply in both cases.
                rbt.FlatStyle = FormSkin.ButtonFlatStyle;
            }
            else if (t == typeof(SkinnableTextBox))
            {
                SkinnableTextBox txt = (SkinnableTextBox)control;
                txt.BorderStyle = FormSkin.TextBoxBorderStyle;
                txt.BorderColor = FormSkin.TextBoxBorderColor;
                txt.ForeColor = FormSkin.TextBoxForeColor;
                if (txt.ReadOnly)
                {
                    txt.BackColor = FormSkin.TextBoxBackColorReadOnly;
                }
                else
                {
                    txt.BackColor = FormSkin.TextBoxBackColor;
                }
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
    }
}