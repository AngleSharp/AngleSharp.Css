namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class TransitionPropertyDeclaration
    {
        public static String Name = PropertyNames.TransitionProperty;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Transition,
        };

        public static IValueConverter Converter = Or(AnimatableConverter.FromList(), None, AssignInitial(CssKeywords.All));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
