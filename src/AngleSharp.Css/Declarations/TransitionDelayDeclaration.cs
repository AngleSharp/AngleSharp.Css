namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class TransitionDelayDeclaration
    {
        public static String Name = PropertyNames.TransitionDelay;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Transition,
        };

        public static IValueConverter Converter = Or(TimeConverter.FromList(), AssignInitial(InitialValues.TransitionDelayDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
