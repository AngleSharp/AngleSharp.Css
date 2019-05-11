namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class TransitionTimingFunctionDeclaration
    {
        public static String Name = PropertyNames.TransitionTimingFunction;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Transition,
        };

        public static IValueConverter Converter = Or(TransitionConverter.FromList(), AssignInitial(InitialValues.TransitionTimingFunctionDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
