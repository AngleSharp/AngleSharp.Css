namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using System;

    public struct BackgroundSize : IEquatable<BackgroundSize>, ICssValue
    {
        #region Fields

        private readonly Length _width;
        private readonly Length _height;
        private readonly ValueMode _mode;

        public static readonly BackgroundSize Cover = new BackgroundSize(ValueMode.Cover);

        public static readonly BackgroundSize Contain = new BackgroundSize(ValueMode.Contain);

        #endregion

        #region ctor

        private BackgroundSize(ValueMode mode)
        {
            _width = Length.Zero;
            _height = Length.Zero;
            _mode = mode;
        }

        public BackgroundSize(Length width, Length height)
        {
            _width = width;
            _height = height;
            _mode = ValueMode.Explicit;
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

        #endregion

        #region Methods

        public Boolean Equals(BackgroundSize other)
        {
            return _mode == other._mode && _height == other._height && _width == other._width;
        }

        public override String ToString()
        {
            if (Equals(Cover))
            {
                return CssKeywords.Cover;
            }
            else if (Equals(Contain))
            {
                return CssKeywords.Contain;
            }
            else if (_height.Equals(Length.Auto))
            {
                return _width.ToString();
            }

            return String.Concat(_width.ToString(), " ", _height.ToString());
        }

        #endregion

        #region Value Mode

        private enum ValueMode
        {
            Explicit,
            Cover,
            Contain
        }

        #endregion
    }
}
