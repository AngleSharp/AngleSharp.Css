namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using static ValueConverters;

    sealed class AnimationValueConverter : IValueConverter
    {
        private static readonly IValueConverter ListConverter = WithAny(
            TimeConverter.Option(),
            TransitionConverter.Option(),
            TimeConverter.Option(),
            PositiveOrInfiniteNumberConverter.Option(),
            AnimationDirectionConverter.Option(),
            AnimationFillStyleConverter.Option(),
            PlayStateConverter.Option(),
            IdentifierConverter.Option()).FromList();

        public ICssValue Convert(StringSource source)
        {
            return ListConverter.Convert(source);
        }
    }
}
