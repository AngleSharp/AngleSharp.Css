namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class BackgroundSizeDeclaration
    {
        public static String Name = PropertyNames.BackgroundSize;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Background,
        };

        public static IValueConverter Converter = Or(BackgroundSizeConverter.FromList(), AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
