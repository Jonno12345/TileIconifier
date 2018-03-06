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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TileIconifier.Skinning;
using TileIconifier.Utilities;

namespace TileIconifier.Controls.IconifierPanel.PictureBox
{
    [SkinIgnore]
    public class PannablePictureBox : Control
    {
        //Read only fields
        private readonly int _maxHeight = 400; //4*pctBox.Height;
        private readonly int _maxWidth = 400; //4*pctBox.Width;
        private readonly int _minHeight = -200; //-2*pctBox.Height;
        private readonly int _minWidth = -200; //-2*pctBox.Width;
        private readonly Font _overlayFont = new Font(FontUtils.GetSystemFontFamily(), 9f, FontStyle.Regular);

        //Modifiable fields
        private Point _movingPoint = Point.Empty;
        private bool _panning = false;
        private Point _startingPoint = Point.Empty;
        private PannableImageContinuousAdjustement _adjustementInProgress = PannableImageContinuousAdjustement.None;
        private Container _components;
        private Timer _tmrScrollDelay;
        private Timer _tmrNudge;

        //Read-only properties
        [Browsable(false)]
        public PannablePictureBoxImage PannablePictureBoxImage { get; } = new PannablePictureBoxImage();

        [Browsable(false)]
        public Rectangle ImageRectangle => Rectangle.Round(GetImageBounds());

        //Modifiable properties with their field, if needed
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set
            {
                SetStyle(ControlStyles.FixedWidth | ControlStyles.FixedHeight, value);
                base.AutoSize = value;
            }
        }

        private Color _textOverlayColor = Color.White;
        [DefaultValue(typeof(Color), nameof(Color.White))]
        public Color TextOverlayColor
        {
            get { return _textOverlayColor; }
            set
            {
                if (_textOverlayColor != value)
                {
                    _textOverlayColor = value;
                    Invalidate(); //We could only invalidate text area.
                }
            }
        }

        private bool _showTextOverlay = false;
        [DefaultValue(false)]
        public bool ShowTextOverlay
        {
            get { return _showTextOverlay; }
            set
            {
                if (_showTextOverlay != value)
                {
                    _showTextOverlay = value;
                    Invalidate(); //We could only invalidate text area.
                }
            }
        }

        private string _textOverlay = null;
        [DefaultValue(null)]
        public string TextOverlay
        {
            get { return _textOverlay; }
            set
            {
                if (_textOverlay != value)
                {
                    _textOverlay = value;
                    Invalidate(); //We could only invalidate text area.
                }
            }
        }

        private Point _textOverlayLocation = Point.Empty;
        [DefaultValue(typeof(Point), "0, 0")]
        public Point TextOverlayLocation
        {
            get { return _textOverlayLocation; }
            set
            {
                if (_textOverlayLocation != value)
                {
                    _textOverlayLocation = value;
                    Invalidate();
                }
            }
        }

        private int _borderThickness = 1;
        [DefaultValue(1)]
        public int BorderThickness
        {
            get { return _borderThickness; }
            set
            {
                if (_borderThickness != value)
                {
                    if (value < 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(BorderThickness), value, "Border thickness must be positive.");
                    }
                    _borderThickness = value;
                    Invalidate();
                }
            }
        }

        private Color _borderColor = SystemColors.WindowFrame;
        [DefaultValue(typeof(Color), nameof(SystemColors.WindowFrame))]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                if (_borderColor != value)
                {
                    _borderColor = value;
                    if (!Focused)
                    {
                        Invalidate(); //We could only invalidate the border region
                    }
                }
            }
        }

        private Color _borderFocusedColor = SystemColors.Highlight;
        [DefaultValue(typeof(Color), nameof(SystemColors.Highlight))]
        public Color BorderFocusedColor
        {
            get { return _borderFocusedColor; }
            set
            {
                if (_borderFocusedColor != value)
                {
                    _borderFocusedColor = value;
                    if (Focused)
                    {
                        Invalidate(); //We could only invalidate the border region
                    }
                }
            }
        }

        private Color _imageBackColor = Color.Gray;
        [DefaultValue(typeof(Color), nameof(Color.Gray))]
        public Color ImageBackColor
        {
            get { return _imageBackColor; }
            set
            {
                if (_imageBackColor != value)
                {
                    _imageBackColor = value;
                    Invalidate(); //We could only invalidate text area.
                }
            }
        }

        private string _placeholderText;
        [DefaultValue(null)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor))]
        public string PlaceholderText
        {
            get { return _placeholderText; }
            set
            {
                if (_placeholderText != value)
                {
                    _placeholderText = value;
                    if (PannablePictureBoxImage.Image == null)
                    {
                        Invalidate();
                    }
                }
            }
        }

        private Size _outputSize = new Size(25, 25);
        [DefaultValue(typeof(Size), "25, 25")]
        public Size OutputSize
        {
            get { return _outputSize; }
            set
            {
                if (_outputSize != value)
                {
                    if (value.Width < 1 || value.Height < 1)
                    {
                        throw new ArgumentOutOfRangeException(nameof(OutputSize), value, "The width and height of the output size must be greater or equal to 1.");
                    }
                    _outputSize = value;
                    if (AutoSize && Parent != null)
                    {
                        var oldSize = Size;
                        Parent.PerformLayout(this, nameof(Bounds));
                        //If performing the layout has changed the size of the control, the latter 
                        //has been already invalidated, so there is no need to do it ourselves.
                        if (Size == oldSize)
                        {
                            Invalidate();
                        }
                    }
                    else
                    {
                        Invalidate();
                    }
                }
            }
        }

        public PannablePictureBox()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
            
            //Components initialization
            _components = new Container();
            _tmrScrollDelay = new Timer(_components);
            _tmrNudge = new Timer(_components);
            _tmrScrollDelay.Interval = SystemInformation.DoubleClickTime;
            _tmrNudge.Interval = 50;
            _tmrScrollDelay.Tick += _tmrScrollDelay_Tick;
            _tmrNudge.Tick += _tmrNudge_Tick;

            PannablePictureBoxImage.OnPannablePictureNewImageSet += Image_OnPannablePictureNewImageSet;
        }

        public event PannablePictureBoxImage.PannablePictureImagePropertyChanges OnPannablePictureImagePropertyChange;

        public void ShrinkImage()
        {
            SetZoom(GetZoomPercentage() - 0.5m);
        }

        public void EnlargeImage()
        {
            SetZoom(GetZoomPercentage() + 0.5m);
        }

        public void ResetImage()
        {
            ResetZoom(false);
            CenterImage(false);
            Invalidate(ImageRectangle);
        }

        public void ResetZoom(bool invalidateImage = true)
        {
            PannablePictureBoxImage.Width = OutputSize.Width;
            PannablePictureBoxImage.Height = (int)(PannablePictureBoxImage.Width / PannablePictureBoxImage.AspectRatio);
            TriggerUpdate(invalidateImage);
        }

        public void CenterImage(bool invalidateImage = true)
        {
            if (PannablePictureBoxImage.Image == null) return;
            PannablePictureBoxImage.X = (OutputSize.Width - PannablePictureBoxImage.Width) / 2;
            PannablePictureBoxImage.Y = (OutputSize.Height - PannablePictureBoxImage.Height) / 2;
            TriggerUpdate(invalidateImage);
        }

        public void SetImage(Image image, int width, int height, int x, int y)
        {
            PannablePictureBoxImage.SetImage(image, width, height, x, y);
            _movingPoint = new Point(x, y);
        }

        public void SetZoom(decimal value)
        {
            if (value < 1) value = 1;
            if (value > 100) value = 100;
            var previousWidth = PannablePictureBoxImage.Width;
            var previousHeight = PannablePictureBoxImage.Height;

            PannablePictureBoxImage.Width = (int)((decimal)_maxWidth / 100 * value);
            PannablePictureBoxImage.Height = (int)(PannablePictureBoxImage.Width / PannablePictureBoxImage.AspectRatio);

            PannablePictureBoxImage.X += (previousWidth - PannablePictureBoxImage.Width) / 2;
            PannablePictureBoxImage.Y += (previousHeight - PannablePictureBoxImage.Height) / 2;

            TriggerUpdate();
        }

        public void AlignTop()
        {
            Align(y: 0);
        }

        public void AlignLeft()
        {
            Align(0);
        }

        public void AlignRight()
        {
            Align(OutputSize.Width - PannablePictureBoxImage.Width);
        }

        public void AlignBottom()
        {
            Align(y: OutputSize.Height - PannablePictureBoxImage.Height);
        }

        public void AlignXMiddle()
        {
            Align((OutputSize.Width - PannablePictureBoxImage.Width) / 2);
        }

        public void AlignYMiddle()
        {
            Align(y: (OutputSize.Height - PannablePictureBoxImage.Height) / 2);
        }

        public void Nudge(int? x = null, int? y = null)
        {
            if (x != null)
                PannablePictureBoxImage.X += (int)x;
            if (y != null)
                PannablePictureBoxImage.Y += (int)y;
            TriggerUpdate();
        }

        private void DoContinuousAdjustment(PannableImageContinuousAdjustement adjustmentType)
        {
            switch (adjustmentType)
            {
                case PannableImageContinuousAdjustement.None:
                    return;                
                case PannableImageContinuousAdjustement.NudgeUp:
                    Nudge(y: -1);
                    break;
                case PannableImageContinuousAdjustement.NudgeDown:
                    Nudge(y: 1);
                    break;
                case PannableImageContinuousAdjustement.NudgeLeft:
                    Nudge(-1);
                    break;
                case PannableImageContinuousAdjustement.NudgeRight:
                    Nudge(1);
                    break;                
                case PannableImageContinuousAdjustement.Enlarge:
                    EnlargeImage();
                    break;
                case PannableImageContinuousAdjustement.Shrink:
                    ShrinkImage();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(adjustmentType), adjustmentType, null);
            }
        }

        internal void BeginContinuousAdjustment(PannableImageContinuousAdjustement adjustmentType)
        {
            if (_adjustementInProgress != PannableImageContinuousAdjustement.None)
            {
                throw new InvalidOperationException("A continuous adjustement is already in progress.");
            }

            //First adjustement immediatly when the button is down, before the delay is considered.
            //This also checks if the adjustement type is valid.
            DoContinuousAdjustment(adjustmentType);
            _adjustementInProgress = adjustmentType;
            _tmrScrollDelay.Start();

        }

        internal void EndContinuousAdjustment()
        {
            //Don't forget to stop _tmrScrollDelay to prevent it from starting tmrNudge if the delay is not reached yet.
            _tmrScrollDelay.Stop();
            _tmrNudge.Stop();
            _adjustementInProgress = PannableImageContinuousAdjustement.None;
        }

        internal decimal GetZoomPercentage()
        {
            var zoomPercentage = (decimal)PannablePictureBoxImage.Width / _maxWidth *
                                 100;
            return zoomPercentage >= 1 ? zoomPercentage : 1;
        }

        private void _tmrScrollDelay_Tick(object sender, EventArgs e)
        {
            _tmrScrollDelay.Stop();
            //Do an adjustement right now
            DoContinuousAdjustment(_adjustementInProgress);
            //Start the continuous adjustement
            _tmrNudge.Start();
        }

        private void _tmrNudge_Tick(object sender, EventArgs e)
        {
            DoContinuousAdjustment(_adjustementInProgress);
        }

        private void Image_OnPannablePictureNewImageSet(object sender, EventArgs e)
        {
            Invalidate(ImageRectangle);
        }
        
        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.Add:
                case Keys.Subtract:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (PannablePictureBoxImage.Image != null)
            {
                PannableImageContinuousAdjustement adjustment = PannableImageContinuousAdjustement.None;
                switch (e.KeyData)
                {
                    case Keys.Up:
                        adjustment = PannableImageContinuousAdjustement.NudgeUp;
                        break;
                    case Keys.Down:
                        adjustment = PannableImageContinuousAdjustement.NudgeDown;
                        break;
                    case Keys.Left:
                        adjustment = PannableImageContinuousAdjustement.NudgeLeft;
                        break;
                    case Keys.Right:
                        adjustment = PannableImageContinuousAdjustement.NudgeRight;
                        break;
                    case Keys.Add:
                        adjustment = PannableImageContinuousAdjustement.Enlarge;
                        break;
                    case Keys.Subtract:
                        adjustment = PannableImageContinuousAdjustement.Shrink;
                        break;
                }
                if (adjustment != PannableImageContinuousAdjustement.None)
                {
                    DoContinuousAdjustment(adjustment);
                }
            }

            base.OnKeyDown(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (PannablePictureBoxImage.Image != null)
            {
                if (e.Delta < 0)
                {
                    ShrinkImage();
                }
                else
                {
                    EnlargeImage();
                }
            }

            base.OnMouseWheel(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Focus();

                _panning = true;

                //"Unscale" the provided point so that the panning follow the mouse.
                var scaleFactor = GetControlScaleFactor();
                var location = e.Location;
                LayoutAndPaintUtils.ScalePoint(ref location, new SizeF(1 / scaleFactor, 1 / scaleFactor));

                _startingPoint = new Point(location.X - _movingPoint.X,
                    location.Y - _movingPoint.Y);
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _panning = false;
                TriggerUpdate();
            }

            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_panning && PannablePictureBoxImage.Image != null && e.Button == MouseButtons.Left)
            {
                //"Unscale" the provided point so that the panning follow the mouse.
                var scaleFactor = GetControlScaleFactor();
                var location = e.Location;
                LayoutAndPaintUtils.ScalePoint(ref location, new SizeF(1 / scaleFactor, 1 / scaleFactor));

                _movingPoint = new Point(location.X - _startingPoint.X,
                    location.Y - _startingPoint.Y);

                if (_movingPoint.X + PannablePictureBoxImage.Width > _maxWidth * 2)
                    _movingPoint.X = _maxWidth * 2 - PannablePictureBoxImage.Width;
                if (_movingPoint.Y + PannablePictureBoxImage.Height > _maxHeight * 2)
                    _movingPoint.Y = _maxHeight * 2 - PannablePictureBoxImage.Height;
                if (_movingPoint.X < _minWidth * 2)
                    _movingPoint.X = _minWidth * 2;
                if (_movingPoint.Y < _minHeight * 2)
                    _movingPoint.Y = _minHeight * 2;

                PannablePictureBoxImage.X = _movingPoint.X;
                PannablePictureBoxImage.Y = _movingPoint.Y;

                Invalidate(ImageRectangle);
            }

            base.OnMouseMove(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            Invalidate(); //We could only invalidate the border region
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            Invalidate(); //We could only invalidate the border region
            base.OnLeave(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _components != null)
            {
                _components.Dispose();
            }
            base.Dispose(disposing);
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            //Calculate the perfect size to accomodate the image at its DPI scaled output size + the border.
            //Don't use GetImageSize() or the image rectangle since that relies on the control size and 
            //that's exactly what we are trying to calculate here.
            var scaleFactor = GetDPIScaleFactor();
            var prefSizeF = new SizeF();

            prefSizeF.Width = OutputSize.Width * scaleFactor.Width + 2 * BorderThickness;
            prefSizeF.Height = OutputSize.Height * scaleFactor.Height + 2 * BorderThickness;

            var prefSize = Size.Round(prefSizeF);

            //Enforce maximum size
            //0 means no maximum
            if (MaximumSize.Width > 0)
            {
                prefSize.Width = Math.Min(MaximumSize.Width, prefSize.Width);
            }

            if (MaximumSize.Height > 0)
            {
                prefSize.Height = Math.Min(MaximumSize.Height, prefSize.Height);
            }

            //Enforce minimum size
            //0 means no minimum
            if (MinimumSize.Width > 0)
            {
                prefSize.Width = Math.Max(MinimumSize.Width, prefSize.Width);
            }

            if (MinimumSize.Height > 0)
            {
                prefSize.Height = Math.Max(MinimumSize.Height, prefSize.Height);
            }

            return prefSize;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (PannablePictureBoxImage.Width < 1 ||
                PannablePictureBoxImage.Height < 1)
                return;

            e.Graphics.Clear(BackColor);

            var scaleFactor = GetControlScaleFactor();
            var imgBounds = ImageRectangle;

            if (PannablePictureBoxImage.Image != null)
            {
                //Set a clip with the image bounds so that when the PannablePictureBox image is offset
                //or larger than the control can accomodate while the control is not exactly the right 
                //aspect ratio, the overflow is not visible.
                var oldClip = e.Graphics.Clip;
                using (var imgClip = oldClip.Clone())
                {
                    imgClip.Intersect(imgBounds);
                    e.Graphics.Clip = imgClip;

                    //Draw image back color
                    using (var b = new SolidBrush(ImageBackColor))
                    {
                        e.Graphics.FillRectangle(b, imgBounds);
                    }

                    //Draw image
                    e.Graphics.DrawImage(
                        PannablePictureBoxImage.Image,
                        imgBounds.X + PannablePictureBoxImage.X * scaleFactor,
                        imgBounds.Y + PannablePictureBoxImage.Y * scaleFactor,
                        PannablePictureBoxImage.Width * scaleFactor,
                        PannablePictureBoxImage.Height * scaleFactor);

                    //Restore the old clip for future drawing, especially by the potential external 
                    //Paint event listeners that may want to draw outside of the image bounds.
                    e.Graphics.Clip = oldClip;
                }

                if (ShowTextOverlay)
                {
                    DrawTextOverlay(e);
                }

                //debug mode overlay
#if DEBUG
                e.Graphics.DrawString(PannablePictureBoxImage.Width + ", " + PannablePictureBoxImage.Height, DefaultFont,
                    new SolidBrush(Color.Red), 0, 0);
                e.Graphics.DrawString(PannablePictureBoxImage.X + ", " + PannablePictureBoxImage.Y, DefaultFont,
                    new SolidBrush(Color.Red), 0, 20);
                e.Graphics.DrawString(Width + ", " + Height, DefaultFont, new SolidBrush(Color.Red), 0, 40);
                e.Graphics.DrawString(_minWidth + "_" + _maxWidth + ", " + _minHeight + "_" + _maxHeight, DefaultFont,
                    new SolidBrush(Color.Red), 0, 60);
#endif
            }
            else if (!string.IsNullOrEmpty(PlaceholderText))
            {
                using (var sf = new StringFormat())
                {
                    sf.Trimming = StringTrimming.EllipsisCharacter;
                    var bounds = imgBounds;
                    bounds.Inflate(-2, -2); //Some margins
                    e.Graphics.DrawString(PlaceholderText, Font, Brushes.Red, bounds);
                }
            }

            //Draw the border delimiting the image area and the rest of the control
            if (BorderThickness > 0)
            {
                var c = Focused ? BorderFocusedColor : BorderColor;

                imgBounds.Inflate(BorderThickness, BorderThickness);

                ControlPaint.DrawBorder(e.Graphics, imgBounds,
                    c, BorderThickness, ButtonBorderStyle.Solid,
                    c, BorderThickness, ButtonBorderStyle.Solid,
                    c, BorderThickness, ButtonBorderStyle.Solid,
                    c, BorderThickness, ButtonBorderStyle.Solid);
            }

            base.OnPaint(e);
        }

        //not a fun mass of parsing. Attempts to closely match the label behaviour of the Windows 10 Start Menu. Haven't tested against Windows 8.1
        //probably fails in some instances...
        private void DrawTextOverlay(PaintEventArgs e)
        {
            var scaleFactor = GetControlScaleFactor();
            var fontScaleFactor = GetControlScaleFactorForFont(e.Graphics);
            if (scaleFactor == 0 || fontScaleFactor == 0)
            {
                return;
            }
            var imgBounds = ImageRectangle;
            var overlayBrush = new SolidBrush(TextOverlayColor);
            var overlayLoc = new PointF(imgBounds.X + TextOverlayLocation.X * scaleFactor, imgBounds.Y + TextOverlayLocation.Y * scaleFactor);
            var overlayFont = new Font(_overlayFont.FontFamily, _overlayFont.Size * fontScaleFactor, _overlayFont.Style);

            try
            {
                //get the width of the text before any manipulation
                var textWidth = e.Graphics.MeasureString(TextOverlay, overlayFont).Width;
                //maximum length for a string, without spaces, to fit on the initial line - TODO: Pass these values in if there is ever a new tile type available
                var maxSingleLineLength = 90 * scaleFactor;
                //maximum length of a line before truncating with ellipsis
                var ellipsisLength = 91 * scaleFactor;

                //function to loop through a line, removing a char at a time until the length satisfies the requirement
                Func<string, float, string> getCharsToMaxLength = (inputString, length) =>
                {
                    var textChunk = inputString;
                    do
                    {
                        textChunk = textChunk.Substring(0, textChunk.Length - 1);
                    } while (e.Graphics.MeasureString(textChunk, overlayFont).Width > length);
                    return textChunk;
                };

                //if we have a space somewhere in the string and it's over the max single line length it will be split over two lines
                if (textWidth >= maxSingleLineLength && TextOverlay.Contains(" "))
                {
                    //get the first chunk of data to process
                    var firstChunk = getCharsToMaxLength(TextOverlay, maxSingleLineLength);

                    string firstLine;
                    string secondLine;

                    //different handling whether there is a space in the first chunk or not. If there *is* a space, we will grab up until the last occurrance of a space character
                    //anything after that space character is put on the second line
                    //
                    //if there isn't a space in the first line, the entire chunk can be made the first line. The second line will start from where the first space character after this chunk is
                    //EXAMPLES
                    //1- XXXXX XXX XXXXXXXXXXXXXXXXXXXXXXXXXX
                    //This should place XXXXX XXX on line one, and XXXXXXXXXXXXXXXXXXXXXXXXXX on line two
                    //
                    //2- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX X
                    //This should place XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX on line one, and X on line two
                    if (firstChunk.Contains(" "))
                    {
                        var lastSpaceIndex = firstChunk.LastIndexOf(" ", StringComparison.Ordinal);
                        firstLine = firstChunk.Substring(0, lastSpaceIndex).Trim();
                        secondLine = TextOverlay.Substring(lastSpaceIndex, TextOverlay.Length - lastSpaceIndex).Trim();
                    }
                    else
                    {
                        firstLine = firstChunk;
                        var tempSecondLine =
                            TextOverlay.Substring(firstChunk.Length, TextOverlay.Length - firstChunk.Length).Trim();
                        var firstSpaceIndex = tempSecondLine.IndexOf(" ", StringComparison.Ordinal);
                        secondLine =
                            tempSecondLine.Substring(firstSpaceIndex, tempSecondLine.Length - firstSpaceIndex).Trim();
                    }

                    //compare each line to the ellipsis length - if exceeds, truncate with ...
                    //from previous examples above, we should now have:
                    //1. XXXXX XXX
                    //   XXXXXXXXXX...
                    //
                    //2. XXXXXXXXXX...
                    //   X
                    if (e.Graphics.MeasureString(firstLine, overlayFont).Width > ellipsisLength)
                        firstLine = getCharsToMaxLength(firstLine, ellipsisLength) + "...";
                    if (e.Graphics.MeasureString(secondLine, overlayFont).Width > ellipsisLength)
                    {
                        var tempSecondLine = getCharsToMaxLength(secondLine, ellipsisLength);

                        if (tempSecondLine.Contains(" "))
                        {
                            //Another fun quirk - we only keep the parts of the second line that can be fully displayed?
                            //Example:
                            //XXXXXXXX XXXXXXXXXXX
                            //becomes
                            //XXXXXXXX...
                            //NOT
                            //XXXXXXXX X...
                            var lastSpaceIndex = tempSecondLine.LastIndexOf(" ", StringComparison.Ordinal);
                            secondLine = tempSecondLine.Substring(0, lastSpaceIndex).Trim() + "...";
                        }
                        else
                        {
                            secondLine = tempSecondLine + "...";
                        }
                    }

                    //draw our lines, first line 16px higher than the default (?) TODO: 16 should really be passed in if new tile types ever become available
                    e.Graphics.DrawString(firstLine, overlayFont, overlayBrush,
                        new PointF(overlayLoc.X, overlayLoc.Y - 16 * scaleFactor));
                    e.Graphics.DrawString(secondLine, overlayFont, overlayBrush, overlayLoc);
                }
                else
                {
                    //if we have no spaces, it'll always be on a single line. Check if the line needs truncating and display in default position
                    var renderLine = TextOverlay;
                    if (e.Graphics.MeasureString(renderLine, overlayFont).Width > ellipsisLength)
                        renderLine = getCharsToMaxLength(renderLine, ellipsisLength) + "...";
                    e.Graphics.DrawString(renderLine, overlayFont, overlayBrush, overlayLoc);
                }
            }
            catch
            {
                //ignore a failure
            }
            finally
            {
                overlayBrush.Dispose();
                overlayFont.Dispose();
            }
        }

        private void TriggerUpdate(bool invalidate = true)
        {
            OnPannablePictureImagePropertyChange?.Invoke(PannablePictureBoxImage, null);
            if (invalidate) Invalidate(ImageRectangle);
        }

        private void Align(int? x = null, int? y = null)
        {
            if (PannablePictureBoxImage.Image == null) return;
            if (x != null)
                PannablePictureBoxImage.X = (int)x;
            if (y != null)
                PannablePictureBoxImage.Y = (int)y;
            TriggerUpdate();
        }
        
        /// <summary>
        ///     Returns the bounding rectangle of the region where the image can be drawn.
        /// </summary>        
        private RectangleF GetImageBounds()
        {
            var imgSize = GetImageSize();
            var imgRect = new RectangleF();

            imgRect.Size = imgSize;
            //Center the rectangle
            imgRect.X = ClientRectangle.X + (ClientSize.Width - imgRect.Width) / 2F;
            imgRect.Y = ClientRectangle.Y + (ClientSize.Height - imgRect.Height) / 2F;

            return imgRect;
        }

        /// <summary>
        ///     Returns the physical size, in pixel, of the image.
        /// </summary>        
        private SizeF GetImageSize()
        {
            var scale = GetControlScaleFactor();
            var imgSize = new SizeF();

            imgSize.Width = (OutputSize.Width * scale);
            imgSize.Height = (OutputSize.Height * scale);

            return imgSize;
        }

        /// <summary>
        ///     Returns the scaling factor to apply on the <see cref="PannablePictureBoxImage"/> data
        ///     considering the image size on the control.
        /// </summary>
        private float GetControlScaleFactor()
        {
            var scale = GetRawControlScaleFactor();

            //Use the minimum scaling factor to make the image as 
            //big as possible without cropping any of its size and 
            //preserving its aspect ratio.
            return Math.Min(scale.Width, scale.Height);
        }

        /// <summary>
        ///     Returns the scaling factor to apply on a font considering the image
        ///     size on the control, but undoing the DPI scaling since fonts are
        ///     automatically scaled to the Graphics on which they are drawn.
        /// </summary>        
        private float GetControlScaleFactorForFont(Graphics g = null)
        {
            var ownGraphics = false;
            try
            {
                if (g == null)
                {
                    g = CreateGraphics();
                    ownGraphics = true;
                }

                var scale = GetRawControlScaleFactor();

                scale.Width *= 96F / g.DpiX;
                scale.Height *= 96F / g.DpiY;

                //Use the minimum scaling factor to make the image as 
                //big as possible without cropping any of its size and 
                //preserving its aspect ratio.
                return Math.Min(scale.Width, scale.Height);
            }
            finally
            {
                if (ownGraphics)
                {
                    g.Dispose();
                }
            }
        }

        /// <summary>
        ///     Return the horizontal and vertical scaling factor of the control
        /// </summary>        
        private SizeF GetRawControlScaleFactor()
        {
            var scale = new SizeF();

            scale.Width = (float)(ClientSize.Width - 2 * BorderThickness) / OutputSize.Width; //Don't scale the border size! Apparently GDI+ does not scale Pens with DPI.
            scale.Height = (float)(ClientSize.Height - 2 * BorderThickness) / OutputSize.Height;

            return scale;
        }

        private SizeF GetDPIScaleFactor(Graphics g = null)
        {
            var ownGraphics = false;
            try
            {
                if (g == null)
                {
                    g = CreateGraphics();
                    ownGraphics = true;
                }
                return new SizeF(g.DpiX / 96F, g.DpiY / 96F);
            }
            finally
            {
                if (ownGraphics)
                {
                    g.Dispose();
                }
            }
        }
    }

    internal enum PannableImageContinuousAdjustement
    {
        None,
        //Pan
        NudgeUp,
        NudgeDown,
        NudgeLeft,
        NudgeRight,        
        //Zoom
        Enlarge,
        Shrink
    }
}