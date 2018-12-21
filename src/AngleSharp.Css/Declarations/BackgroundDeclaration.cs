namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class BackgroundDeclaration
    {
        public static String Name = PropertyNames.Background;

        public static IValueConverter Converter = Or(new BackgroundValueConverter(), AssignInitial());

        public static IValueAggregator Aggregator = new BackgroundAggregator();

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BackgroundAttachment,
            PropertyNames.BackgroundClip,
            PropertyNames.BackgroundColor,
            PropertyNames.BackgroundImage,
            PropertyNames.BackgroundOrigin,
            PropertyNames.BackgroundPosition,
            PropertyNames.BackgroundSize,
            PropertyNames.BackgroundRepeat,
        };
    }
}
