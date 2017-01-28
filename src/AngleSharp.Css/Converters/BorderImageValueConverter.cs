namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    sealed class BorderImageValueConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var image = source.ParseBorderImage();

            if (image.HasValue)
            {
                return new BorderImageValue(image.Value);
            }

            return null;
        }

        sealed class BorderImageValue : ICssValue
        {
            private readonly BorderImage _image;

            public BorderImageValue(BorderImage image)
            {
                _image = image;
            }

            public String CssText
            {
                get { return _image.ToString(); }
            }
        }
    }
}
