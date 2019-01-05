namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class AnimationDelayDeclaration
    {
        public static String Name = PropertyNames.AnimationDelay;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Animation,
        };

        public static IValueConverter Converter = Or(TimeConverter.FromList(), AssignInitial(Time.Zero));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
