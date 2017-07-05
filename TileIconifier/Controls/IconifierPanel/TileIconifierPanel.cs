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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TileIconifier.Controls.IconifierPanel.PictureBox;
using TileIconifier.Core;
using TileIconifier.Core.Custom;
using TileIconifier.Core.Shortcut;
using TileIconifier.Core.Utilities;
using TileIconifier.Forms.Shared;
using TileIconifier.Skinning.Skins;
using TileIconifier.Utilities;

namespace TileIconifier.Controls.IconifierPanel
{
    public partial class TileIconifierPanel : UserControl
    {
        private readonly List<PannablePictureBoxMetaData> _pannablePictureBoxMetaDatas =
            new List<PannablePictureBoxMetaData>();

        private BaseSkin _currentBaseSkin;
        private PannablePictureBox _panPctMediumIcon;
        private PannablePictureBox _panPctSmallIcon;

        public TileIconifierPanel()
        {
            InitializeComponent();

            //Keep a reference to the pictureboxes in a field just for convenience
            _panPctMediumIcon = pannablePictureBoxControlPanelMedium.PannablePictureBox;
            _panPctSmallIcon = pannablePictureBoxControlPanelSmall.PannablePictureBox;

            AddEventHandlers();
        }

        public ShortcutItem CurrentShortcutItem { get; set; }

        public Size MediumPictureBoxSize => _panPctMediumIcon.Size;
        public Size SmallPictureBoxSize => _panPctSmallIcon.Size;

        public event EventHandler OnIconifyPanelUpdate;

        public void UpdateControlsToShortcut()
        {
            if (CurrentShortcutItem == null)
            {
                return;
            }

            //disable event handlers whilst updating things programatically
            RemoveEventHandlers();

            //check if unsaved once per update
            var hasUnsavedChanges = CurrentShortcutItem.Properties.HasUnsavedChanges;

            //update color panel
            UpdateColorPanelControlsToCurrentShortcut();
            
            //update the picture boxes to show the relevant images
            UpdatePictureBoxImage(_panPctMediumIcon, CurrentShortcutItem.Properties.CurrentState.MediumImage);
            UpdatePictureBoxImage(_panPctSmallIcon, CurrentShortcutItem.Properties.CurrentState.SmallImage);
            UpdatePictureBoxOverlay(_panPctMediumIcon, CurrentShortcutItem);

            UpdatePictureBoxBackColors();


            //set the associatedShortcutItemImages for each picturebox
            GetSenderPictureBoxToMetaData(_panPctMediumIcon).ShortcutItemImage =
                CurrentShortcutItem.Properties.CurrentState.MediumImage;
            GetSenderPictureBoxToMetaData(_panPctSmallIcon).ShortcutItemImage =
                CurrentShortcutItem.Properties.CurrentState.SmallImage;

            //update the picture box control panels
            pannablePictureBoxControlPanelMedium.UpdateControls();
            pannablePictureBoxControlPanelSmall.UpdateControls();

            //set relevant unsaved changes controls to required visibility/enabled states
            lblUnsaved.Visible = hasUnsavedChanges;
            btnReset.Enabled = hasUnsavedChanges;

            //reset any validation failures
            ResetValidation();

            //re-add the event handlers now we've finished updating
            AddEventHandlers();
        }

        private void UpdatePictureBoxBackColors()
        {
            var result = colorPanel.GetColorPanelResult();
            if (result != null)
            {
                SetPictureBoxesBackColor(result.BackgroundColor);
            }
            else
            {
                SetPictureBoxesBackColor();
            }
        }

        public void SetPictureBoxesBackColor(string color = null)
        {
            Action<PannablePictureBox> setBackColor = b =>
            {
                b.ImageBackColor = b.PannablePictureBoxImage.Image == null
                    ? _currentBaseSkin.BackColor
                    : color == null ? _currentBaseSkin.BackColor : ColorUtils.HexOrNameToColor(color);
                b.Refresh();
            };
            setBackColor(_panPctMediumIcon);
            setBackColor(_panPctSmallIcon);
        }


        public bool DoValidation()
        {
            ResetValidation();

            return ValidateControls();
        }

        public void UpdateSkinColors(BaseSkin currentBaseSkin)
        {
            _currentBaseSkin = currentBaseSkin;
            lblUnsaved.ForeColor = _currentBaseSkin.ErrorForeColor;
            UpdatePictureBoxBackColors();
        }

        private void UpdateColorPanelControlsToCurrentShortcut()
        {
            colorPanel.SetBackgroundColor(
                CurrentShortcutItem.Properties.CurrentState.BackgroundColor);
            colorPanel.SetForegroundColorRadio(CurrentShortcutItem.Properties.CurrentState.ForegroundText == "light");
            colorPanel.SetForegroundTextShow(CurrentShortcutItem.Properties.CurrentState.ShowNameOnSquare150X150Logo);
        }

        private void UpdatePictureBoxOverlay(PannablePictureBox pannablePictureBox, ShortcutItem currentShortcutItem)
        {
            pannablePictureBox.ShowTextOverlay = currentShortcutItem.Properties.CurrentState.ShowNameOnSquare150X150Logo;
            pannablePictureBox.TextOverlayColor = currentShortcutItem.Properties.CurrentState.ForegroundText == "light"
                ? Color.White
                : Color.Black;
            pannablePictureBox.TextOverlay = Path.GetFileNameWithoutExtension(currentShortcutItem.ShortcutFileInfo.Name);
        }

        private void UpdatePictureBoxImage(PannablePictureBox pannablePictureBox, ShortcutItemImage shortcutItemImage)
        {
            pannablePictureBox.SetImage(shortcutItemImage.CachedImage(),
                shortcutItemImage.Width,
                shortcutItemImage.Height,
                shortcutItemImage.X,
                shortcutItemImage.Y);
        }

        private void RunUpdate()
        {
            UpdateControlsToShortcut();
            OnIconifyPanelUpdate?.Invoke(this, null);
        }

        private void IconSet(object sender)
        {
            IconSelectorResult selectedImage;
            try
            {
                var imagePath = GetSenderPictureBoxToMetaData(sender).ShortcutItemImage.Path;
                //if we haven't got a valid file from previously, try and get the default icon
                if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
                {
                    //if it's a custom shortcut, try and get the target path from the VBS file
                    if (CurrentShortcutItem.IsTileIconifierCustomShortcut)
                    {
                        try
                        {
                            var customShortcutExecutionTarget = CustomShortcut.Load(CurrentShortcutItem.TargetFilePath);
                            imagePath = customShortcutExecutionTarget.TargetPath.UnQuoteWrap();
                        }
                        catch (InvalidCustomShortcutException)
                        {
                            //corrupted custom shortcut?
                            imagePath = CurrentShortcutItem.TargetFilePath;
                        }
                    }
                    else
                    {
                        //otherwise we just use the target file path from the shortcut
                        imagePath = CurrentShortcutItem.TargetFilePath;
                    }
                }
                selectedImage = FrmIconSelector.GetImage(this, imagePath);
            }
            catch (UserCancellationException)
            {
                return;
            }

            var pictureBoxMetaDataToUse = chkUseSameImg.Checked
                ? _pannablePictureBoxMetaDatas
                : new List<PannablePictureBoxMetaData> {GetSenderPictureBoxToMetaData(sender)};

            foreach (var pictureBoxMetaData in pictureBoxMetaDataToUse)
            {
                pictureBoxMetaData.ShortcutItemImage.Path = selectedImage.ImagePath;
                pictureBoxMetaData.ShortcutItemImage.SetImage(selectedImage.ImageBytes, pictureBoxMetaData.Size);
                UpdatePictureBoxImage(pictureBoxMetaData.PannablePictureBox, pictureBoxMetaData.ShortcutItemImage);
                pictureBoxMetaData.PannablePictureBox.ResetImage();
            }

            RunUpdate();
        }

        private PannablePictureBoxMetaData GetSenderPictureBoxToMetaData(object sender)
        {
            PannablePictureBox senderPictureBox = null;
            if (sender.GetType() == typeof (PannablePictureBoxControlPanel))
            {
                senderPictureBox = ((PannablePictureBoxControlPanel) sender).PannablePictureBox;
            }
            if (sender.GetType() == typeof (PannablePictureBox))
            {
                senderPictureBox = (PannablePictureBox) sender;
            }
            if (senderPictureBox == null)
            {
                throw new InvalidCastException($@"Sender not valid type! Received {sender.GetType()}");
            }

            return _pannablePictureBoxMetaDatas.Single(p => p.PannablePictureBox == senderPictureBox);
        }

        private void BuildPannableShortcutBoxControlPanels()
        {
            _panPctMediumIcon.ContextMenuStrip = cmsPicBox;
            _panPctSmallIcon.ContextMenuStrip = cmsPicBox;
            pannablePictureBoxControlPanelMedium.ChangeImageClick += (sender, args) => { IconSet(sender); };
            pannablePictureBoxControlPanelSmall.ChangeImageClick += (sender, args) => { IconSet(sender); };
            pannablePictureBoxControlPanelMedium.UpdateTrackBarAndZoom();
            pannablePictureBoxControlPanelSmall.UpdateTrackBarAndZoom();
        }

        private void TileIconifierPanel_Load(object sender, EventArgs e)
        {
            SetupPannablePictureBoxes();
            BuildPannableShortcutBoxControlPanels();
        }

        private void SetupPannablePictureBoxes()
        {
            //TODO: Set this with the output size
            _panPctMediumIcon.TextOverlayLocation = new Point(6, 78);

            _pannablePictureBoxMetaDatas.Add(new PannablePictureBoxMetaData
            {
                PannablePictureBox = _panPctMediumIcon,
                Size = ShortcutConstantsAndEnums.MediumShortcutOutputSize
            });
            _pannablePictureBoxMetaDatas.Add(new PannablePictureBoxMetaData
            {
                PannablePictureBox = _panPctSmallIcon,
                Size = ShortcutConstantsAndEnums.SmallShortcutOutputSize
            });
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            CurrentShortcutItem.Properties.UndoChanges();

            RunUpdate();
        }

        private void ResetValidation()
        {
            //TODO
            colorPanel.ResetValidation();
        }

        private bool ValidateControls()
        {
            var valid = true;

            Action<Control> controlInvalid = c =>
            {
                c.BackColor = Color.Red;
                valid = false;
            };

            if (CurrentShortcutItem.Properties.CurrentState.MediumImage.Bytes == null)
            {
                controlInvalid(_panPctMediumIcon);
            }

            if (CurrentShortcutItem.Properties.CurrentState.SmallImage.Bytes == null)
            {
                controlInvalid(_panPctSmallIcon);
            }

            var colorPanelValid = colorPanel.ValidateControls();

            return valid && colorPanelValid;
        }

        //TODO
        private void AddEventHandlers()
        {
            colorPanel.ColorUpdate += ColorPanelColorUpdate;
            _panPctMediumIcon.OnPannablePictureImagePropertyChange +=
                PanPctMediumIcon_OnPannablePictureImagePropertyChange;
            _panPctSmallIcon.OnPannablePictureImagePropertyChange += PanPctSmallIcon_OnPannablePictureImagePropertyChange;
            _panPctMediumIcon.DoubleClick += _panPctMediumIcon_DoubleClick;
            _panPctSmallIcon.DoubleClick += _panPctSmallIcon_DoubleClick;
        }        

        private void ColorPanelColorUpdate(object sender, EventArgs eventArgs)
        {
            UpdateFromColorPanel((ColorPanel)sender);
            RunUpdate();
        }

        private void UpdateFromColorPanel(ColorPanel usedColorPanel)
        {
            var result = usedColorPanel.GetColorPanelResult();
            if (result == null || CurrentShortcutItem == null)
            {
                return;
            }

            CurrentShortcutItem.Properties.CurrentState.BackgroundColor = result.BackgroundColor;
            CurrentShortcutItem.Properties.CurrentState.ShowNameOnSquare150X150Logo = result.DisplayForegroundText;
            CurrentShortcutItem.Properties.CurrentState.ForegroundText = result.ForegroundColor;
        }

        private void RemoveEventHandlers()
        {
            colorPanel.ColorUpdate -= ColorPanelColorUpdate;
            _panPctMediumIcon.OnPannablePictureImagePropertyChange -=
                PanPctMediumIcon_OnPannablePictureImagePropertyChange;
            _panPctSmallIcon.OnPannablePictureImagePropertyChange -=
                PanPctSmallIcon_OnPannablePictureImagePropertyChange;
            _panPctMediumIcon.DoubleClick -= _panPctMediumIcon_DoubleClick;
            _panPctSmallIcon.DoubleClick -= _panPctSmallIcon_DoubleClick;
        }        

        private void _panPctMediumIcon_DoubleClick(object sender, EventArgs e)
        {
            IconSet(sender);
        }

        private void _panPctSmallIcon_DoubleClick(object sender, EventArgs e)
        {
            IconSet(sender);
        }

        private void tmiChangeImage_Click(object sender, EventArgs e)
        {
            var panPctBox = ControlUtils.GetToolStripSourceControl(sender);
            if (panPctBox != null)
            {
                IconSet(panPctBox);
            }
        }

        private void tmiCentreImage_Click(object sender, EventArgs e)
        {
            var panPctBox = ControlUtils.GetToolStripSourceControl(sender) as PannablePictureBox;
            if (panPctBox != null)
            {
                panPctBox.CenterImage();
            }
        }

        private void PanPctMediumIcon_OnPannablePictureImagePropertyChange(object sender, EventArgs e)
        {
            var item = (PannablePictureBoxImage) sender;
            CurrentShortcutItem.Properties.CurrentState.MediumImage.X = item.X;
            CurrentShortcutItem.Properties.CurrentState.MediumImage.Y = item.Y;
            CurrentShortcutItem.Properties.CurrentState.MediumImage.Width = item.Width;
            CurrentShortcutItem.Properties.CurrentState.MediumImage.Height = item.Height;

            RunUpdate();
        }

        private void PanPctSmallIcon_OnPannablePictureImagePropertyChange(object sender, EventArgs e)
        {
            var item = (PannablePictureBoxImage) sender;
            CurrentShortcutItem.Properties.CurrentState.SmallImage.X = item.X;
            CurrentShortcutItem.Properties.CurrentState.SmallImage.Y = item.Y;
            CurrentShortcutItem.Properties.CurrentState.SmallImage.Width = item.Width;
            CurrentShortcutItem.Properties.CurrentState.SmallImage.Height = item.Height;

            RunUpdate();
        }
    }
}