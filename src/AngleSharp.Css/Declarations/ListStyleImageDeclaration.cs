namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ListStyleImageDeclaration
    {
        public static String Name = PropertyNames.ListStyleImage;

        public static String[] Shorthands = new[]
        {
            PropertyNames.ListStyle,
        };

        public static IValueConverter Converter = Or(OptionalImageSourceConverter, AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
