namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class TransitionDurationDeclaration
    {
        public static String Name = PropertyNames.TransitionDuration;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Transition,
        };

        public static IValueConverter Converter = Or(TimeConverter.FromList(), AssignInitial(Time.Zero));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
