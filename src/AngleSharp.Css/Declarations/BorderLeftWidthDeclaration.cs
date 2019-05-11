namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BorderLeftWidthDeclaration
    {
        public static String Name = PropertyNames.BorderLeftWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderWidth,
            PropertyNames.BorderLeft,
        };

        public static IValueConverter Converter = Or(LineWidthConverter, AssignInitial(InitialValues.BorderLeftWidthDecl));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
