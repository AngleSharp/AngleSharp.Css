namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class AnimationDelayDeclaration
    {
        public static String Name = PropertyNames.AnimationDelay;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Animation,
        };

        public static IValueConverter Converter = Or(TimeConverter.FromList(), AssignInitial(InitialValues.AnimationDelayDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
