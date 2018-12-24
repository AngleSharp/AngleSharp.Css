namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BackgroundRepeatDeclaration
    {
        public static String Name = PropertyNames.BackgroundRepeat;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Background,
        };

        public static String[] Longhands = new[]
        {
            PropertyNames.BackgroundRepeatX,
            PropertyNames.BackgroundRepeatY,
        };

        public static IValueConverter Converter = Or(BackgroundRepeatsConverter.FromList(), AssignInitial(BackgroundRepeat.Repeat));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
