namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    sealed class OriginConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var pt = source.ParsePoint();
            source.SkipSpacesAndComments();
            var z = source.ParseLength();

            if (pt.HasValue)
            {
                return new OriginValue(pt.Value.X, pt.Value.Y, z ?? Length.Zero);
            }

            return null;
        }

        sealed class OriginValue : ICssValue
        {
            private readonly Length _x;
            private readonly Length _y;
            private readonly Length _z;

            public OriginValue(Length x, Length y, Length z)
            {
                _x = x;
                _y = y;
                _z = z;
            }

            public String CssText
            {
                get
                {
                    var pt = new Point(_x, _y).ToString();
                    return _z != Length.Zero ? String.Concat(pt, " ", _z.ToString()) : pt;
                }
            }
        }
    }
}
