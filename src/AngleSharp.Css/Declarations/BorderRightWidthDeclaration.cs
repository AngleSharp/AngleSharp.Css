namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BorderRightWidthDeclaration
    {
        public static String Name = PropertyNames.BorderRightWidth;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderWidth,
            PropertyNames.BorderRight,
        };

        public static IValueConverter Converter = Or(LineWidthConverter, AssignInitial(InitialValues.BorderRightWidthDecl));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
