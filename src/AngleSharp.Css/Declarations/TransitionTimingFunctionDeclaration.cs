namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TransitionTimingFunctionDeclaration
    {
        public static String Name = PropertyNames.TransitionTimingFunction;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Transition,
        };

        public static IValueConverter Converter = TransitionConverter.FromList();

        public static ICssValue InitialValue = InitialValues.TransitionTimingFunctionDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
