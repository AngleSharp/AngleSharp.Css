namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents a CSS background size definition.
    /// </summary>
    public sealed class CssBackgroundSizeValue : IEquatable<CssBackgroundSizeValue>, ICssPrimitiveValue
    {
        #region Fields

        private readonly ICssValue _width;
        private readonly ICssValue _height;
        private readonly ValueMode _mode;

        /// <summary>
        /// Used to declare cover background size.
        /// </summary>
        public static readonly CssBackgroundSizeValue Cover = new(ValueMode.Cover);

        /// <summary>
        /// Used to declare contain background size.
        /// </summary>
        public static readonly CssBackgroundSizeValue Contain = new(ValueMode.Contain);

        #endregion

        #region ctor

        private CssBackgroundSizeValue(ValueMode mode)
        {
            _width = CssLengthValue.Auto;
            _height = CssLengthValue.Auto;
            _mode = mode;
        }

        /// <summary>
        /// Creates a new CSS background size definition.
        /// </summary>
        /// <param name="width">The width to use.</param>
        /// <param name="height">The height to use.</param>
        public CssBackgroundSizeValue(ICssValue width, ICssValue height)
        {
            _width = width;
            _height = height;
            _mode = ValueMode.Explicit;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width of the background.
        /// </summary>
        public ICssValue Width => _width;

        /// <summary>
        /// Gets the height of the background.
        /// </summary>
        public ICssValue Height => _height;

        /// <summary>
        /// Gets the CSS text representation.
        /// </summary>
        public String CssText
        {
            get
            {
                if (Equals(Cover))
                {
                    return CssKeywords.Cover;
                }
                else if (Equals(Contain))
                {
                    return CssKeywords.Contain;
                }
                else if (_height.Equals(CssLengthValue.Auto))
                {
                    return _width.CssText;
                }

                return String.Concat(_width.CssText, " ", _height.CssText);
            }
        }

        #endregion

        #region Methods

        ICssValue ICssValue.Compute(ICssComputeContext context)
        {
            if (_mode == ValueMode.Explicit)
            {
                var w = _width.Compute(context);
                var h = _height.Compute(context);
                return new CssBackgroundSizeValue(w, h);
            }

            return this;
        }

        /// <summary>
        /// Compares to another background size.
        /// </summary>
        /// <param name="other">The other background size.</param>
        /// <returns>True if both are equivalent, otherwise false.</returns>
        public Boolean Equals(CssBackgroundSizeValue other) => _mode == other._mode && _height == other._height && _width == other._width;

        #endregion

        #region Value Mode

        /// <summary>
        /// Represents the value modes for the vertical and horizontal axis.
        /// </summary>
        private enum ValueMode
        {
            /// <summary>
            /// The size is explicitly specified.
            /// </summary>
            Explicit,
            /// <summary>
            /// The size should cover the axis.
            /// </summary>
            Cover,
            /// <summary>
            /// The size should contain the axis.
            /// </summary>
            Contain,
        }

        #endregion
    }
}
