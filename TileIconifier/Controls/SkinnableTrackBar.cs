using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TileIconifier.Skinning.Skins;

namespace TileIconifier.Controls
{
    class SkinnableTrackBar : TrackBar, ISkinnableControl
    {
        private const string UNSUPPORTED_PROPERTY_ERROR =
            "This property is not currently supported by this control.";

        //Normally, we would just calclulate these when we paint, which would avoid 
        //the need to reimplement tons of properties, but the needed Windows message 
        //does not work when painting, so we calculate them whenever they change and
        //store the result for when we need it.
        private int[] _tickLocations;

        /// <summary>
        ///     Collection of <see cref="ControlStyles"/> that should be set to true 
        ///     when we want to draw ourselves. 
        /// </summary>
        private static readonly ReadOnlyCollection<ControlStyles> _customPaintingFlags = new List<ControlStyles>
        {
            ControlStyles.UserPaint,
            ControlStyles.AllPaintingInWmPaint,
            ControlStyles.OptimizedDoubleBuffer
        }.AsReadOnly();

        /// <summary>
        ///     Dictionary of the default values for the <see cref="ControlStyles"/>
        ///     that we change when we are drawing ourselves. 
        /// </summary>
        private readonly ReadOnlyDictionary<ControlStyles, bool> _defaultPaintingFlags;

        public SkinnableTrackBar()
        {
            //Backup the initial ControlStyles in case we need to reset them.
            var defaultsFlags = new Dictionary<ControlStyles, bool>();
            foreach (var f in _customPaintingFlags)
            {
                defaultsFlags.Add(f, GetStyle(f));
            }
            _defaultPaintingFlags = new ReadOnlyDictionary<ControlStyles, bool>(defaultsFlags);

            CreateTickPositions();
        }

        #region "Properties"
        /// <summary>
        ///     Indicates whether or not we should draw the control ourselves.
        /// </summary>
        private bool HandleDrawing
        {
            get { return (FlatStyle == FlatStyle.Flat); }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Obsolete(UNSUPPORTED_PROPERTY_ERROR)]
        public new Orientation Orientation
        {
            get { return base.Orientation; }
            set { throw new NotSupportedException(UNSUPPORTED_PROPERTY_ERROR); }
        }

        public new int Maximum
        {
            get { return base.Maximum; }
            set
            {
                base.Maximum = value;
                CreateTickPositions();
            }
        }

        public new int Minimum
        {
            get { return base.Minimum; }
            set
            {
                base.Minimum = value;
                CreateTickPositions();
            }
        }

        public new int TickFrequency
        {
            get { return base.TickFrequency; }
            set
            {
                base.TickFrequency = value;
                CreateTickPositions();
            }
        }

        public new TickStyle TickStyle
        {
            get { return base.TickStyle; }
            set
            {
                base.TickStyle = value;
                CreateTickPositions();
            }
        }

        protected int TickCount
        {
            get { return _tickLocations.Length; }
        }

        private FlatStyle _flatStyle = FlatStyle.Standard;
        [DefaultValue(FlatStyle.Standard)]
        public FlatStyle FlatStyle
        {
            get { return _flatStyle; }
            set
            {
                if (_flatStyle != value)
                {
                    _flatStyle = value;

                    if (HandleDrawing)
                    {
                        EnableOwnerDrawing();
                    }
                    else
                    {
                        ResetOwnerDrawing();
                    }

                    Invalidate();
                }
            }
        }

        private Color _flatThumbBackColor = SystemColors.Control;
        [DefaultValue(typeof(Color), nameof(SystemColors.Control))]
        public Color FlatThumbBackColor
        {
            get { return _flatThumbBackColor; }
            set
            {
                if (_flatThumbBackColor != value)
                {
                    _flatThumbBackColor = value;
                    if (HandleDrawing && Enabled)
                    {
                        Invalidate();
                    }
                }
            }
        }

        private Color _flatThumbDisabledBackColor = SystemColors.ControlLight;
        [DefaultValue(typeof(Color), nameof(SystemColors.ControlLight))]
        public Color FlatThumbDisabledBackColor
        {
            get { return _flatThumbDisabledBackColor; }
            set
            {
                if (_flatThumbDisabledBackColor != value)
                {
                    _flatThumbDisabledBackColor = value;
                    if (HandleDrawing && !Enabled)
                    {
                        Invalidate();
                    }
                }
            }
        }

        private Color _flatThumbBorderColor = SystemColors.WindowFrame;
        [DefaultValue(typeof(Color), nameof(SystemColors.WindowFrame))]
        public Color FlatThumbBorderColor
        {
            get { return _flatThumbBorderColor; }
            set
            {
                if (_flatThumbBorderColor != value)
                {
                    _flatThumbBorderColor = value;
                    if (HandleDrawing && Enabled)
                    {
                        Invalidate();
                    }
                }
            }
        }

        private Color _flatThumbDisabledBorderColor = SystemColors.ControlDark;
        [DefaultValue(typeof(Color), nameof(SystemColors.ControlDark))]
        public Color FlatThumbDisabledBorderColor
        {
            get { return _flatThumbDisabledBorderColor; }
            set
            {
                if (_flatThumbDisabledBorderColor != value)
                {
                    _flatThumbDisabledBorderColor = value;
                    if (HandleDrawing && !Enabled)
                    {
                        Invalidate();
                    }
                }
            }
        }

        private Color _flatTrackColor = SystemColors.ControlDark;
        [DefaultValue(typeof(Color), nameof(SystemColors.ControlDark))]
        public Color FlatTrackColor
        {
            get { return _flatTrackColor; }
            set
            {
                if (_flatTrackColor != value)
                {
                    _flatTrackColor = value;
                    if (HandleDrawing)
                    {
                        Invalidate();
                    }
                }
            }
        }

        private int _flatTrackThickness = 2;
        [DefaultValue(2)]
        public int FlatTrackThickness
        {
            get { return _flatTrackThickness; }
            set
            {
                if (_flatTrackThickness != value)
                {
                    _flatTrackThickness = value;
                    if (HandleDrawing)
                    {
                        Invalidate();
                    }
                }
            }
        }
        #endregion

        /// <summary>
        ///     Returns the bounding rectangle for the track of this trackbar.
        /// </summary>
        protected Rectangle GetTrackRect()
        {
            NativeMethods.RECT r = new NativeMethods.RECT();
            NativeMethods.SendMessage(Handle, NativeMethods.TBM_GETCHANNELRECT, IntPtr.Zero, ref r);
            return Rectangle.FromLTRB(r.left, r.top, r.right, r.bottom);
        }

        /// <summary>
        ///     Returns the bounding rectangle for the thumb of this trackbar.
        /// </summary>
        protected Rectangle GetThumbRect()
        {
            NativeMethods.RECT r = new NativeMethods.RECT();
            NativeMethods.SendMessage(Handle, NativeMethods.TBM_GETTHUMBRECT, IntPtr.Zero, ref r);
            return Rectangle.FromLTRB(r.left, r.top, r.right, r.bottom);
        }

        /// <summary>
        ///     Returns the bounding rectangles of the ticks at the specified index.
        /// </summary>        
        protected Rectangle[] GetTickRectangles(int index)
        {
            if (index < 0 || index >= TickCount)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var x = _tickLocations[index];
            var tickWidth = 1;
            var tickHeight = 4;

            switch (TickStyle)
            {
                case TickStyle.TopLeft:
                    return new Rectangle[] { new Rectangle(x, 4, tickWidth, tickHeight) };

                case TickStyle.BottomRight:
                    return new Rectangle[] { new Rectangle(x, 23, tickWidth, tickHeight) };

                case TickStyle.Both:
                    return new Rectangle[] { new Rectangle(x, 4, tickWidth, tickHeight), new Rectangle(x, 34, tickWidth, tickHeight) };

                default:
                    return new Rectangle[] { Rectangle.Empty };
            }
        }

        /// <summary>
        ///     Returns the number of ticks for this trackbar, whether or not we are ready to deal with 
        ///     that number. For most scenarios, use the <see cref="TickCount"/> property instead.
        /// </summary>        
        private int GetTickCountInternal()
        {
            return NativeMethods.SendMessage(Handle, NativeMethods.TBM_GETNUMTICS, IntPtr.Zero, IntPtr.Zero).ToInt32();
        }

        /// <summary>
        ///     Initialize the _tickLocation array and calculate its values.
        /// </summary>
        private void CreateTickPositions()
        {
            //We must do all sort of tricks here because of how the native track bar control is implemented:
            //0 1 2 3 4 5 6 7 8 9    // Tick positions seen on the trackbar.
            //  1 2 3 4 5 6 7 8      // Tick positions whose position can be identified.
            //  0 1 2 3 4 5 6 7      // Index numbers for the identifiable positions.
            //https://msdn.microsoft.com/en-us/library/windows/desktop/bb760207

            int tickCount = GetTickCountInternal();

            if (tickCount < 1)
            {
                //Must not be null, because we use the length of this array for the TickCount property.
                _tickLocations = new int[0];
                return;
            }

            int[] ticksLoc = new int[tickCount];

            //Get the location of the ticks whose position can be obtained
            for (int i = 1; i < tickCount - 1; i++)
            {
                ticksLoc[i] = NativeMethods.SendMessage(Handle, NativeMethods.TBM_GETTICPOS, (IntPtr)i - 1, IntPtr.Zero).ToInt32();
            }

            //Get the location of the first and last ticks
            if (tickCount >= 4)
            {
                //We have the location of at least two ticks, so we can 
                //calculate the spacing between each tick and guess from there.
                int ticksSpacing = ticksLoc[2] - ticksLoc[1] - 1;
                ticksLoc[0] = ticksLoc[1] - ticksSpacing;
                ticksLoc[ticksLoc.Length - 1] = ticksLoc[ticksLoc.Length - 2] + ticksSpacing;
            }
            else if (tickCount >= 2)
            {
                //We don't have enough info, so we just hard-code empirical values.
                ticksLoc[0] = ClientRectangle.Left + 13;
                ticksLoc[ticksLoc.Length - 1] = ClientRectangle.Right - 13;
            }

            _tickLocations = ticksLoc;
        }

        /// <summary>
        ///     Sets the appropriate flags to the control so that it can be owner drawn.
        /// </summary>
        private void EnableOwnerDrawing()
        {
            var flags = new ControlStyles();
            foreach (ControlStyles s in _customPaintingFlags)
            {
                flags |= s;
            }

            SetStyle(flags, true);
        }

        /// <summary>
        ///     Reset the owner drawing flags.
        /// </summary>
        private void ResetOwnerDrawing()
        {
            foreach (var k in _defaultPaintingFlags.Keys)
            {
                SetStyle(k, _defaultPaintingFlags[k]);
            }
        }

        /// <summary>
        ///     Returns an array of <see cref="Point"/> defining the outline of the thumb.
        /// </summary>
        /// <param name="rect"><see cref="Rectangle"/> where the thumb can fit.</param>
        /// <returns></returns>
        private Point[] GetThumbPoints(Rectangle rect)
        {
            Point[] pts;

            if (TickStyle == TickStyle.Both)
            {
                //The thumb is rectangular
                pts = new Point[4];
                pts[0] = rect.Location;
                pts[1] = new Point(rect.Left, rect.Bottom);
                pts[2] = new Point(rect.Right, rect.Bottom);
                pts[3] = new Point(rect.Right, rect.Top);

            }
            else
            {
                //The thumb is an irregular polygon pointing up or down
                int arrowHeight = (int)(rect.Height * 0.3);
                int middleX = rect.Left + rect.Width / 2;
                pts = new Point[5];

                if (TickStyle == TickStyle.TopLeft)
                {
                    //Thumb pointing up
                    pts[0] = new Point(middleX, rect.Top);
                    pts[1] = new Point(rect.Left, rect.Top + arrowHeight);
                    pts[2] = new Point(rect.Left, rect.Bottom);
                    pts[3] = new Point(rect.Right, rect.Bottom);
                    pts[4] = new Point(rect.Right, rect.Top + arrowHeight);
                }
                else
                {
                    //Thumb pointing down
                    pts[0] = rect.Location;
                    pts[1] = new Point(rect.Left, rect.Bottom - arrowHeight);
                    pts[2] = new Point(middleX, rect.Bottom);
                    pts[3] = new Point(rect.Right, rect.Bottom - arrowHeight);
                    pts[4] = new Point(rect.Right, rect.Top);
                }
            }

            return pts;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            CreateTickPositions();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(BackColor);

            //Draw the track
            Rectangle trackRect = GetTrackRect();
            //We can't use Inflate here, because it can only change the height two pixels
            //at the time (one on each side).
            Rectangle usrTrackRect = new Rectangle(
                trackRect.X, trackRect.Y + (trackRect.Height - FlatTrackThickness) / 2, trackRect.Width, FlatTrackThickness);

            using (var b = new SolidBrush(FlatTrackColor))
            {
                g.FillRectangle(b, usrTrackRect);
            }

            //Prepare to draw the thumb
            Rectangle thumbRect = GetThumbRect();
            Point[] ptsBorder = GetThumbPoints(new Rectangle(thumbRect.X, thumbRect.Y, thumbRect.Width - 1, thumbRect.Height - 1)); //-1 is the typical GDI+ compensation.
            thumbRect.Inflate(-1, -1); //exclude border
            Point[] ptsBack = GetThumbPoints(thumbRect);
            Color borderCol;
            Color backCol;
            if (Enabled)
            {
                borderCol = FlatThumbBorderColor;
                backCol = FlatThumbBackColor;
            }
            else
            {
                borderCol = FlatThumbDisabledBorderColor;
                backCol = FlatThumbDisabledBackColor;
            }

            //Draw thumb border
            using (var p = new Pen(borderCol))
            {
                g.DrawPolygon(p, ptsBorder);
            }

            //Draw thumb background
            using (var b = new SolidBrush(backCol))
            {
                g.FillPolygon(b, ptsBack);
            }

            //Draw ticks            
            using (var b = new SolidBrush(ForeColor))
            {
                for (int i = 0; i < TickCount; i++)
                {
                    g.FillRectangles(b, GetTickRectangles(i));
                }
            }

            //Draw focus rectangle, if needed
            if (Focused && ShowFocusCues)
            {
                ControlPaint.DrawFocusRectangle(g, ClientRectangle, ForeColor, Color.Transparent);
            }

            base.OnPaint(e);
        }

        public void ApplySkin(BaseSkin skin)
        {
            FlatStyle = skin.TrackBarFlatStyle;
            FlatThumbBackColor = skin.TrackBarThumbBackColor;
            FlatThumbDisabledBackColor = skin.TrackBarThumbDisabledBackColor;
            FlatThumbBorderColor = skin.TrackBarThumbBorderColor;
            FlatThumbDisabledBorderColor = skin.TrackBarThumbDisabledBorderColor;
            FlatTrackColor = skin.TrackBarTrackColor;
        }
    }
}
