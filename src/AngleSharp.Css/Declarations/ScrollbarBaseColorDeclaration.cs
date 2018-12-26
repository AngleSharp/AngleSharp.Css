namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class ScrollbarBaseColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarBaseColor;

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(Color.Transparent));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
