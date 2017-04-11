#region LICENCE

// /*
//         The MIT License (MIT)
// 
//         Copyright (c) 2017 Johnathon M
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
using System.Windows.Forms;
using TileIconifier.Properties;
using TileIconifier.Skinning;
using TileIconifier.Skinning.Skins;
using TileIconifier.Utilities;

namespace TileIconifier.Forms
{
    public class SkinnableForm : Form
    {
        protected BaseSkin FormSkin => SkinHandler.GetCurrentSkin();
        
        protected virtual void OnSkinChanged(object sender, EventArgs e)
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
            {
                ContainerUtils.ApplySkinToControl(FormSkin, e.Control);
            }
        }

        protected override void OnLoad(EventArgs e)
        {

            Icon = Resources.tiles2_shadow_lyk_icon;

            SkinHandler.SkinChanged += OnSkinChanged;

            //We only apply the skin on the form, not on its controls
            //because control skins are applied when controls are added
            //to the form.
            if (!DesignMode)
            {
                ApplyFormSkin();
            }

            base.OnLoad(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            //FormClosed is a good place to remove an handler that was added when Load, because
            //if the form is ever shown again, it will raise the Load event, which will add the
            //handler back (even if the form was just hidden).
            SkinHandler.SkinChanged -= OnSkinChanged;
        }

        /// <summary>
        ///     Applies the skin on the form only.
        /// </summary>
        private void ApplyFormSkin()
        {
            ForeColor = FormSkin.ForeColor;
            BackColor = FormSkin.BackColor;
            Font = FormSkin.Font;
        }

        /// <summary>
        ///     Applies the skin on all controls inside the form.
        /// </summary>
        private void ApplyAllControlsSkin()
        {
            foreach (Control c in Controls)
            {
                ContainerUtils.ApplySkinToControl(FormSkin, c);
            }
        }        
    }
}