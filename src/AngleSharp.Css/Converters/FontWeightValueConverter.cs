#nullable enable
namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;

    sealed class FontWeightValueConverter : IValueConverter
    {
        private readonly IValueConverter _keywords = Map.FontWeights.ToConverter();

        public ICssValue? Convert(StringSource source)
        {
            var value = _keywords.Convert(source);

            if (value is CssConstantValue<FontWeight> keyword && (keyword.Value == FontWeight.Bolder || keyword.Value == FontWeight.Lighter))
            {
                return new CssRelativeFontWeightValue(keyword);
            }

            return value;
        }
    }
}
