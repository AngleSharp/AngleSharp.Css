#nullable enable
namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Converts the font-style property, including `oblique &lt;angle&gt;` (CSS Fonts 4).
    /// </summary>
    sealed class FontStyleValueConverter : IValueConverter
    {
        private readonly IValueConverter _keywords = Map.FontStyles.ToConverter();

        public ICssValue? Convert(StringSource source)
        {
            var pos = source.Index;

            if (source.IsIdentifier(CssKeywords.Oblique))
            {
                var keyword = source.Index;
                source.SkipSpacesAndComments();
                var angle = source.ParseAngle();

                if (angle.HasValue)
                {
                    return new ObliqueValue(angle.Value);
                }

                source.BackTo(keyword);
            }

            source.BackTo(pos);
            return _keywords.Convert(source);
        }

        sealed class ObliqueValue : ICssValue
        {
            private readonly CssAngleValue _angle;

            public ObliqueValue(CssAngleValue angle)
            {
                _angle = angle;
            }

            public String CssText => String.Concat(CssKeywords.Oblique, " ", _angle.CssText);

            // The computed angle is given in degrees.
            public ICssValue Compute(ICssComputeContext context) =>
                new ObliqueValue(new CssAngleValue(_angle.ToDegree(), CssAngleValue.Unit.Deg));

            public Boolean Equals(ICssValue? other) => other is ObliqueValue value && value._angle.Equals(_angle);
        }
    }
}
