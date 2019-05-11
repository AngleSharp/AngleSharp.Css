namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BorderBottomRightRadiusDeclaration
    {
        public static String Name = PropertyNames.BorderBottomRightRadius;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderRadius,
        };

        public static IValueConverter Converter = Or(BorderRadiusLonghandConverter, AssignInitial(InitialValues.BorderRadiusDecl));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
