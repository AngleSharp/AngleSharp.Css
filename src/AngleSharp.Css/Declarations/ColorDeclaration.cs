namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ColorDeclaration
    {
        public static String Name = PropertyNames.Color;

        public static IValueConverter Converter = Or(ColorConverter, AssignInitial(InitialValues.ColorDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Hashless | PropertyFlags.Animatable;
    }
}
