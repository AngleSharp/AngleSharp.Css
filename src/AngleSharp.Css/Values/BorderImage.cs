namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS border image definition.
    /// </summary>
    struct BorderImage : ICssValue
    {
        #region Fields

        private readonly ICssValue _image;
        private readonly ICssValue _slice;
        private readonly ICssValue _widths;
        private readonly ICssValue _outsets;
        private readonly ICssValue _repeat;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new border image definition.
        /// </summary>
        /// <param name="image">The image source to use.</param>
        /// <param name="slice">The image slice portion.</param>
        /// <param name="widths">The image width definitions.</param>
        /// <param name="outsets">The image outset declarations.</param>
        /// <param name="repeat">The image repeat settings.</param>
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
            get
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
                    sb.Append(" / ").Append(_widths.CssText);
                }

                if (_outsets != null)
                {
                    if (_widths == null) sb.Append(" / ");
                    sb.Append(" / ").Append(_outsets.CssText);
                }

                if (_repeat != null)
                {
                    if (sb.Length > 0) sb.Append(Symbols.Space);
                    sb.Append(_repeat.CssText);
                }

                return sb.ToPool();
            }
        }

        /// <summary>
        /// Gets the associated image value.
        /// </summary>
        public ICssValue Image
        {
            get { return _image; }
        }

        /// <summary>
        /// Gets the associated slice value.
        /// </summary>
        public ICssValue Slice
        {
            get { return _slice; }
        }

        /// <summary>
        /// Gets the associated width value.
        /// </summary>
        public ICssValue Widths
        {
            get { return _widths; }
        }

        /// <summary>
        /// Gets the associated outset value.
        /// </summary>
        public ICssValue Outsets
        {
            get { return _outsets; }
        }

        /// <summary>
        /// Gets the associated repeat value.
        /// </summary>
        public ICssValue Repeat
        {
            get { return _repeat; }
        }

        #endregion
    }
}
