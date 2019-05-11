namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ScrollbarFaceColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarFaceColor;

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(InitialValues.ScrollbarFaceColorDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
