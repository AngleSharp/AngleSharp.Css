namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class ScrollbarShadowColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarShadowColor;

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(Colors.GetColor("threeddarkshadow")));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
