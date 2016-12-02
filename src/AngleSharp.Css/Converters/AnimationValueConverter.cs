namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using static ValueConverters;

    sealed class AnimationValueConverter : IValueConverter
    {
        private static readonly IValueConverter ListConverter = WithAny(
            TimeConverter.Option().For(PropertyNames.AnimationDuration),
            TransitionConverter.Option().For(PropertyNames.AnimationTimingFunction),
            TimeConverter.Option().For(PropertyNames.AnimationDelay),
            PositiveOrInfiniteNumberConverter.Option().For(PropertyNames.AnimationIterationCount),
            AnimationDirectionConverter.Option().For(PropertyNames.AnimationDirection),
            AnimationFillStyleConverter.Option().For(PropertyNames.AnimationFillMode),
            PlayStateConverter.Option().For(PropertyNames.AnimationPlayState),
            IdentifierConverter.Option().For(PropertyNames.AnimationName)).FromList();

        public ICssValue Convert(StringSource source)
        {
            return ListConverter.Convert(source);
        }
    }
}
