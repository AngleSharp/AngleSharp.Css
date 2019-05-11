namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BorderTopWidthDeclaration
    {
        public static String Name = PropertyNames.BorderTopWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderWidth,
            PropertyNames.BorderTop,
        };

        public static IValueConverter Converter = Or(LineWidthConverter, AssignInitial(InitialValues.BorderTopWidthDecl));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
