namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ScrollbarBaseColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarBaseColor;

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(InitialValues.ScrollbarBaseColorDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
