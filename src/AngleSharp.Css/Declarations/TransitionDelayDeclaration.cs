namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class TransitionDelayDeclaration
    {
        public static String Name = PropertyNames.TransitionDelay;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Transition,
        };

        public static IValueConverter Converter = Or(TimeConverter.FromList(), AssignInitial(Time.Zero));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
