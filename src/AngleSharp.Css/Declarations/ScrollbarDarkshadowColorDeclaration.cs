namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ScrollbarDarkshadowColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarDarkShadowColor;

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(InitialValues.ScrollbarDarkshadowColorDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
