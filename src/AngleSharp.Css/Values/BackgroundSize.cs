namespace AngleSharp.Css.Values
{
    using System;
    using System.Globalization;

    public struct BackgroundSize : IEquatable<BackgroundSize>, IFormattable
    {
        private readonly Length _width;
        private readonly Length _height;
        private readonly ValueMode _mode;

        public static readonly BackgroundSize Cover = new BackgroundSize(ValueMode.Cover);

        public static readonly BackgroundSize Contain = new BackgroundSize(ValueMode.Contain);

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

        public Boolean Equals(BackgroundSize other)
        {
            return _mode == other._mode && _height == other._height && _width == other._width;
        }

        public override String ToString()
        {
            return ToString(null, CultureInfo.InvariantCulture);
        }

        public String ToString(String format, IFormatProvider formatProvider)
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
                return _width.ToString(format, formatProvider);
            }

            return String.Concat(_width.ToString(format, formatProvider), " ", _height.ToString(format, formatProvider));
        }

        private enum ValueMode
        {
            Explicit,
            Cover,
            Contain
        }
    }
}
