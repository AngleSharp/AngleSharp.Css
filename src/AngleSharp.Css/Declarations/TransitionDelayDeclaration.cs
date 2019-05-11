namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TransitionDelayDeclaration
    {
        public static String Name = PropertyNames.TransitionDelay;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Transition,
        };

        public static IValueConverter Converter = TimeConverter.FromList();

        public static ICssValue InitialValue = InitialValues.TransitionDelayDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
