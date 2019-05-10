namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class FontStretchDeclaration
    {
        public static String Name = PropertyNames.FontStretch;

        public static IValueConverter Converter = Or(FontStretchConverter, AssignInitial(InitialValues.FontStretchDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}
