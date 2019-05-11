namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BorderTopRightRadiusDeclaration
    {
        public static String Name = PropertyNames.BorderTopRightRadius;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderRadius,
        };

        public static IValueConverter Converter = Or(BorderRadiusLonghandConverter, AssignInitial(InitialValues.BorderRadiusDecl));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
