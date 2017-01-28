namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    public struct BorderImage : ICssValue
    {
        #region Fields

        private readonly IImageSource _image;
        private readonly BorderImageSlice? _slice;
        private readonly PeriodicValue<Length> _widths;
        private readonly PeriodicValue<Length> _outsets;
        private readonly BorderRepeat? _repeatX;
        private readonly BorderRepeat? _repeatY;

        #endregion

        #region ctor

        public BorderImage(IImageSource image, BorderImageSlice? slice, PeriodicValue<Length> widths, PeriodicValue<Length> outsets, BorderRepeat? repeatX, BorderRepeat? repeatY)
        {
            _image = image;
            _slice = slice;
            _widths = widths;
            _outsets = outsets;
            _repeatX = repeatX;
            _repeatY = repeatY;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get { return ToString(); }
        }

        public IImageSource Image
        {
            get { return _image; }
        }

        public BorderImageSlice? Slice
        {
            get { return _slice; }
        }

        public PeriodicValue<Length> Widths
        {
            get { return _widths; }
        }

        public PeriodicValue<Length> Outsets
        {
            get { return _outsets; }
        }

        public BorderRepeat? RepeatX
        {
            get { return _repeatX; }
        }

        public BorderRepeat? RepeatY
        {
            get { return _repeatY; }
        }

        #endregion

        #region Methods

        public override String ToString()
        {
            var sb = StringBuilderPool.Obtain();

            if (_image != null)
            {
                if (sb.Length > 0) sb.Append(Symbols.Space);
                sb.Append(_image.ToString());
            }

            if (_slice.HasValue)
            {
                if (sb.Length > 0) sb.Append(Symbols.Space);
                sb.Append(_slice.Value.ToString());
            }

            if (_widths != null)
            {
                sb.Append(" / ").Append(_widths.ToString());
            }

            if (_outsets != null)
            {
                if (_widths == null) sb.Append(" / ");
                sb.Append(" / ").Append(_outsets.ToString());
            }

            if (_repeatX.HasValue)
            {
                if (sb.Length > 0) sb.Append(Symbols.Space);
                sb.Append(_repeatX.Value.ToString(Map.BorderRepeats));
            }

            if (_repeatY.HasValue)
            {
                if (sb.Length > 0) sb.Append(Symbols.Space);
                sb.Append(_repeatY.Value.ToString(Map.BorderRepeats));
            }

            return sb.ToPool();
        }

        #endregion
    }
}
