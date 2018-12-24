namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using static ValueConverters;

    sealed class TransitionValueConverter : IValueConverter
    {
        private static readonly IValueConverter TheConverter = WithAny(
            AnimatableConverter.Option(),
            TimeConverter.Option(),
            TransitionConverter.Option(),
            TimeConverter.Option()).FromList();

        public ICssValue Convert(StringSource source)
        {
            return TheConverter.Convert(source);
        }
    }
}
