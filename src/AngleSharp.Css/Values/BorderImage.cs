namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    public struct BorderImage : ICssValue
    {
        #region Fields

        private readonly ICssValue _image;
        private readonly ICssValue _slice;
        private readonly ICssValue _widths;
        private readonly ICssValue _outsets;
        private readonly ICssValue _repeat;

        #endregion

        #region ctor

        public BorderImage(ICssValue image, ICssValue slice, ICssValue widths, ICssValue outsets, ICssValue repeat)
        {
            _image = image;
            _slice = slice;
            _widths = widths;
            _outsets = outsets;
            _repeat = repeat;
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

        public ICssValue Image
        {
            get { return _image; }
        }

        public ICssValue Slice
        {
            get { return _slice; }
        }

        public ICssValue Widths
        {
            get { return _widths; }
        }

        public ICssValue Outsets
        {
            get { return _outsets; }
        }

        public ICssValue Repeat
        {
            get { return _repeat; }
        }

        #endregion

        #region Methods

        public override String ToString()
        {
            var sb = StringBuilderPool.Obtain();

            if (_image != null)
            {
                if (sb.Length > 0) sb.Append(Symbols.Space);
                sb.Append(_image.CssText);
            }

            if (_slice != null)
            {
                if (sb.Length > 0) sb.Append(Symbols.Space);
                sb.Append(_slice.CssText);
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

            if (_repeat != null)
            {
                if (sb.Length > 0) sb.Append(Symbols.Space);
                sb.Append(_repeat.CssText);
            }

            return sb.ToPool();
        }

        #endregion
    }
}
