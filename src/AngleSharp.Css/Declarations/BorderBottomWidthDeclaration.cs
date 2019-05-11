namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BorderBottomWidthDeclaration
    {
        public static String Name = PropertyNames.BorderBottomWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderWidth,
            PropertyNames.BorderBottom,
        };

        public static IValueConverter Converter = Or(LineWidthConverter, AssignInitial(InitialValues.BorderBottomWidthDecl));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
