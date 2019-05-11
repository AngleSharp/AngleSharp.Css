namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AnimationIterationCountDeclaration
    {
        public static String Name = PropertyNames.AnimationIterationCount;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Animation,
        };

        public static IValueConverter Converter = PositiveOrInfiniteNumberConverter.FromList();

        public static ICssValue InitialValue = InitialValues.AnimationIterationCountDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
