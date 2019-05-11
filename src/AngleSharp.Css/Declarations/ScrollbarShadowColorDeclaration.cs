namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ScrollbarShadowColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarShadowColor;

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(InitialValues.ScrollbarShadowColorDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
