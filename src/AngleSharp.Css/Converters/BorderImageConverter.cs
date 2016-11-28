namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Extensions;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.IO;

    sealed class BorderImageConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var value = new BorderImage();
            var pos = 0;

            do
            {
                pos = source.Index;

                if (value.Image == null)
                {
                    value.Image = source.ToImageSource();
                    source.SkipSpacesAndComments();
                }

                if (!value.Slice.HasValue)
                {
                    value.Slice = source.ToBorderImageSlice();
                    var c = source.SkipSpacesAndComments();

                    if (value.Slice.HasValue && c == Symbols.Solidus)
                    {
                        source.SkipSpacesAndComments();
                        //BorderImageWidthConverter
                        c = source.SkipSpacesAndComments();

                        if (false && c == Symbols.Solidus)
                        {
                            source.SkipSpacesAndComments();
                            //BorderImageOutsetConverter
                            source.SkipSpacesAndComments();
                        }
                    }
                }

                if (!value.RepeatX.HasValue)
                {
                    value.RepeatX = source.ToConstant(Map.BorderRepeats);
                    source.SkipSpacesAndComments();
                }

                if (!value.RepeatY.HasValue)
                {
                    value.RepeatY = source.ToConstant(Map.BorderRepeats);
                    source.SkipSpacesAndComments();
                }
            }
            while (pos != source.Index);
            
            return new BorderImageValue(value);
        }

        struct BorderImage
        {
            public IImageSource Image;
            public BorderImageSlice? Slice;
            public BorderRepeat? RepeatX;
            public BorderRepeat? RepeatY;
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

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }
    }
}
