namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Aggregators;
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class BorderImageDeclaration
    {
        public static String Name = PropertyNames.BorderImage;

        public static IValueConverter Converter = Or(None, new BorderImageValueConverter(), AssignInitial());

        public static IValueAggregator Aggregator = new BorderImageAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.BorderImageOutset,
            PropertyNames.BorderImageRepeat,
            PropertyNames.BorderImageSlice,
            PropertyNames.BorderImageSource,
            PropertyNames.BorderImageWidth,
        };
    }
}
