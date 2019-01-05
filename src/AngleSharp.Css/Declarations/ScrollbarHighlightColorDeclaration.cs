namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class ScrollbarHighlightColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarHighlightColor;

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(Colors.GetColor("threedhighlight")));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
