namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AnimationDelayDeclaration
    {
        public static String Name = PropertyNames.AnimationDelay;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Animation,
        };

        public static IValueConverter Converter = TimeConverter.FromList();

        public static ICssValue InitialValue = InitialValues.AnimationDelayDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
