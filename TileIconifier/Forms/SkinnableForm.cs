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
        protected BaseSkin CurrentBaseSkin = SkinHandler.GetCurrentSkin();

        public SkinnableForm()
        {
            SkinHandler.SkinChanged += ApplySkin;
            Load += OnLoad;
            Move += RedrawAllButtons;
            Resize += RedrawAllButtons;
        }

        protected virtual void ApplySkin(object sender, EventArgs e)
        {
            CurrentBaseSkin = SkinHandler.GetCurrentSkin();
            ApplyControlSkins(Controls);

            BackColor = CurrentBaseSkin.BackColor;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    SkinHandler.SkinChanged -= ApplySkin;
                }
                catch
                {
                    // ignored
                }
            }

            base.Dispose(disposing);
        }

        private void RedrawAllButtons(object sender, EventArgs e)
        {
            RedrawButtons(Controls);
        }

        private static void RedrawButtons(IEnumerable controls)
        {
            foreach (Control control in controls)
            {
                if (control.GetType() == typeof (Button))
                {
                    control.Refresh();
                }
                if (control.Controls.Count > 0)
                    RedrawButtons(control.Controls);
            }
        }

        private void OnLoad(object sender, EventArgs eventArgs)
        {
            AddControlEvents(Controls);

            ApplySkin(this, null);
            Icon = Resources.tiles2_shadow_lyk_icon;
        }

        private void AddControlEvents(IEnumerable baseControl)
        {
            foreach (Control control in baseControl)
            {
                if (control.GetType() == typeof (Button))
                {
                    var button = control as Button;
                    if (button != null)
                    {
                        button.Tag = button.Text;
                        button.Text = string.Empty;
                        button.Paint += ButtonOnPaint;
                        button.EnabledChanged += ButtonOnEnabledChanged;
                    }
                }
                if (control.GetType() == typeof (SortableListView))
                {
                    var listView = control as ListView;
                    if (listView != null)
                    {
                        listView.OwnerDraw = true;
                        listView.DrawColumnHeader += ListViewOnDrawColumnHeader;
                        listView.DrawItem += ListViewOnDrawItem;
                        listView.Invalidate(true);
                        listView.Refresh();
                        listView.Sort();
                    }
                }

                if (control.Controls.Count > 0)
                    AddControlEvents(control.Controls);
            }
        }

        private void ApplyControlSkins(IEnumerable baseControls)
        {
            foreach (
                var control in
                    baseControls.Cast<Control>()
                        .Where(control => control.GetType().GetCustomAttributes(typeof (SkinIgnore), true).Length == 0))
            {
                control.BackColor = CurrentBaseSkin.BackColor;
                if (control.GetType() == typeof (SortableListView))
                    control.BackColor = CurrentBaseSkin.SortableListViewBackColor;

                control.ForeColor = CurrentBaseSkin.ForeColor;
                //control.Font = CurrentBaseSkin.Font;

                if (control.GetType() == typeof (Button))
                    ButtonOnEnabledChanged(control, null);

                if (control.GetType() == typeof (MenuStrip))
                    ApplyToolStripMenuItemSkins((control as MenuStrip)?.Items);

                control.Refresh();

                if (control.Controls.Count <= 0) continue;

                ApplyControlSkins(control.Controls);
            }
        }

        private void ApplyToolStripMenuItemSkins(IEnumerable menuItems)
        {
            foreach (var toolStripMenuItem in from object menuItem in menuItems
                where menuItem.GetType() == typeof (ToolStripMenuItem)
                select menuItem as ToolStripMenuItem)
            {
                toolStripMenuItem.BackColor = CurrentBaseSkin.BackColor;

                toolStripMenuItem.ForeColor = CurrentBaseSkin.ForeColor;
                toolStripMenuItem.Font = CurrentBaseSkin.Font;

                if (toolStripMenuItem.DropDownItems.Count <= 0) continue;

                ApplyToolStripMenuItemSkins(toolStripMenuItem.DropDownItems);
            }
        }

        private static void ListViewOnDrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void ListViewOnDrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.FillRectangle(new SolidBrush(CurrentBaseSkin.BackColor), e.Bounds);
            e.Graphics.DrawLine(new Pen(CurrentBaseSkin.ForeColor, 1), e.Bounds.Left, e.Bounds.Bottom - 2,
                e.Bounds.Right,
                e.Bounds.Bottom - 2);
            e.Graphics.DrawLine(new Pen(CurrentBaseSkin.ForeColor, 1), e.Bounds.Left - 1, e.Bounds.Bottom - 2,
                e.Bounds.Left - 1,
                0);

            using (var sf = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            })
                e.Graphics.DrawString(e.Header.Text, CurrentBaseSkin.Font, new SolidBrush(CurrentBaseSkin.ForeColor),
                    e.Bounds, sf);
        }

        private void ButtonOnEnabledChanged(object sender, EventArgs eventArgs)
        {
            var btn = sender as Button;
            if (btn == null) return;
            btn.ForeColor = btn.Enabled ? CurrentBaseSkin.ForeColor : CurrentBaseSkin.DisabledForeColor;
            btn.BackColor = btn.Enabled ? CurrentBaseSkin.BackColor : CurrentBaseSkin.DisabledBackColor;
        }

        private void ButtonOnPaint(object sender, PaintEventArgs e)
        {
            var btn = (Button) sender;

            using (var drawBrush = new SolidBrush(btn.ForeColor))
            {
                using (var sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                {
                    e.Graphics.DrawString(btn.Tag?.ToString(), btn.Font, drawBrush, e.ClipRectangle, sf);
                }
            }
        }
    }
}