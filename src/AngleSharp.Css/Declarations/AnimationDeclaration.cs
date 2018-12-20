namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class AnimationDeclaration
    {
        public static String Name = PropertyNames.Animation;

        public static IValueConverter Converter = Or(new AnimationValueConverter(), AssignInitial());

        public static IValueAggregator Aggregator = new AnimationAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Children = new[]
        {
            PropertyNames.AnimationName,
            PropertyNames.AnimationDuration,
            PropertyNames.AnimationTimingFunction,
            PropertyNames.AnimationDelay,
            PropertyNames.AnimationIterationCount,
            PropertyNames.AnimationDirection,
            PropertyNames.AnimationFillMode,
            PropertyNames.AnimationPlayState,
        };
    }
}
