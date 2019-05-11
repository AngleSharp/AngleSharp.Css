namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ScrollbarArrowColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarArrowColor;

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(InitialValues.ScrollbarArrowColorDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
