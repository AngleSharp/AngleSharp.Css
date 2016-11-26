namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Extensions;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.IO;

    sealed class OriginConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var pt = source.ToPoint();
            source.SkipSpacesAndComments();
            var z = source.ToLength();

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
                get { return String.Concat(_x.ToString(), " ", _y.ToString(), " ", _z.ToString()); }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }
    }
}
