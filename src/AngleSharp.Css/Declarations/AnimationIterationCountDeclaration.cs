namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class AnimationIterationCountDeclaration
    {
        public static String Name = PropertyNames.AnimationIterationCount;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Animation,
        };

        public static IValueConverter Converter = Or(PositiveOrInfiniteNumberConverter.FromList(), AssignInitial(InitialValues.AnimationIterationCountDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
