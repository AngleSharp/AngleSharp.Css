namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ScrollbarTrackColorDeclaration
    {
        public static String Name = PropertyNames.ScrollbarTrackColor;

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(InitialValues.ScrollbarTrackColorDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
