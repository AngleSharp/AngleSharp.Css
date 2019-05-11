namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ScrollbarHighlightColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarHighlightColor;

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(InitialValues.ScrollbarHighlightColorDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
