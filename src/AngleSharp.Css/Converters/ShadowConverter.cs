using AngleSharp.Css.Dom;
using AngleSharp.Css.Extensions;
using AngleSharp.Css.Parser;
using AngleSharp.Css.Values;
using AngleSharp.Text;
using System;
using System.IO;

namespace AngleSharp.Css.Converters
{
    sealed class ShadowConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var inset = false;
            var offsetX = default(Length?);
            var offsetY = default(Length?);
            var blurRadius = default(Length?);
            var spreadRadius = default(Length?);
            var color = default(Color?);
            var pos = 0;

            do
            {
                pos = source.Index;

                if (!inset)
                {
                    inset = source.IsIdentifier(CssKeywords.Inset);
                    source.SkipSpacesAndComments();
                }

                if (!offsetX.HasValue)
                {
                    offsetX = source.ToLength();
                    source.SkipSpacesAndComments();
                }

                if (!offsetY.HasValue)
                {
                    offsetY = source.ToLength();
                    source.SkipSpacesAndComments();
                }

                if (!blurRadius.HasValue)
                {
                    blurRadius = source.ToLength();
                    source.SkipSpacesAndComments();
                }

                if (!spreadRadius.HasValue)
                {
                    spreadRadius = source.ToLength();
                    source.SkipSpacesAndComments();
                }

                if (!color.HasValue)
                {
                    color = source.ParseColor();
                    source.SkipSpacesAndComments();
                }
            }
            while (pos != source.Index);

            if (offsetX.HasValue && offsetY.HasValue)
            {
                var shadow = new Shadow(
                    inset,
                    offsetX ?? Length.Zero,
                    offsetY ?? Length.Zero,
                    blurRadius ?? Length.Zero,
                    spreadRadius ?? Length.Zero,
                    color ?? Color.Black);
                return new ShadowValue(shadow);
            }

            return null;
        }

        sealed class ShadowValue : ICssValue
        {
            private readonly Shadow _shadow;

            public ShadowValue(Shadow shadow)
            {
                _shadow = shadow;
            }

            public String CssText
            {
                get { return _shadow.ToString(); }
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                writer.Write(CssText);
            }
        }
    }
}
