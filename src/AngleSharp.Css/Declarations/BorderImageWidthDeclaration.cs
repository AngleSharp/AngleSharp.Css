namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class BorderImageWidthDeclaration
    {
        public static String Name = PropertyNames.BorderImageWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderImage,
        };

        public static IValueConverter Converter = Or(BorderImageWidthConverter, AssignInitial(Length.Full));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
