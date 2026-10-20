#nullable disable
namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;

    /// <summary>
    /// Marks the values of a property as relative to the element's own box,
    /// e.g., the percentages of a background or mask position. Such percentages
    /// are only resolved during layout, i.e., they stay percentages when computed.
    /// </summary>
    sealed class BoxRelativeValueConverter : IValueConverter
    {
        private readonly IValueConverter _converter;

        public BoxRelativeValueConverter(IValueConverter converter)
        {
            _converter = converter;
        }

        public ICssValue Convert(StringSource source) => _converter.Convert(source);
    }
}
