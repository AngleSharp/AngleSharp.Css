namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BorderImageSourceDeclaration
    {
        public static String Name = PropertyNames.BorderImageSource;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderImage,
        };

        public static IValueConverter Converter = Or(OptionalImageSourceConverter, AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
