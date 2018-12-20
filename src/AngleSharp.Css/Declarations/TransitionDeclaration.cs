namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class TransitionDeclaration
    {
        public static String Name = PropertyNames.Transition;

        public static IValueConverter Converter = Or(new TransitionValueConverter(), AssignInitial());

        public static IValueAggregator Aggregator = new TransitionAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Children = new[]
        {
            PropertyNames.TransitionDelay,
            PropertyNames.TransitionDuration,
            PropertyNames.TransitionProperty,
            PropertyNames.TransitionTimingFunction,
        };
    }
}
