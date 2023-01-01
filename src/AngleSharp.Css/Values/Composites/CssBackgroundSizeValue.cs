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
        private readonly string _cssText;

        /// <summary>
        /// Used to declare cover background size.
        /// </summary>
        public static readonly CssBackgroundSizeValue Cover = new CssBackgroundSizeValue(ValueMode.Cover);

        /// <summary>
        /// Used to declare contain background size.
        /// </summary>
        public static readonly CssBackgroundSizeValue Contain = new CssBackgroundSizeValue(ValueMode.Contain);

        /// <summary>
        /// Used to declare contain background size.
        /// </summary>
        public static readonly CssBackgroundSizeValue Text = new CssBackgroundSizeValue(ValueMode.Text);

        #endregion

        #region ctor

        private CssBackgroundSizeValue(ValueMode mode)
        {
            _width = Length.Zero;
            _height = Length.Zero;
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

        public CssBackgroundSizeValue(string value) : this(ValueMode.Text)
        {
            _cssText = value;
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
                else if (Equals(Text))
                {
                    return _cssText;
                }
                else if (_height.Equals(Length.Auto))
                {
                    return _width.CssText;
                }

                return String.Concat(_width.CssText, " ", _height.CssText);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares to another background size.
        /// </summary>
        /// <param name="other">The other background size.</param>
        /// <returns>True if both are equivalent, otherwise false.</returns>
        public Boolean Equals(CssBackgroundSizeValue other)
        {
            if (_mode == ValueMode.Text && other._mode == ValueMode.Text)
            {
                return true;
            }
            return _mode == other._mode && _height == other._height && _width == other._width;
        }

        #endregion

        #region Value Mode

        private enum ValueMode
        {
            Explicit,
            Cover,
            Contain,
            Text,
        }

        #endregion
    }
}
