namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class FlexDeclaration
    {
        public static String Name = PropertyNames.Flex;

        public static String[] Longhands = new[]
        {
            PropertyNames.FlexGrow,
            PropertyNames.FlexShrink,
            PropertyNames.FlexBasis,
        };

        public static IValueConverter Converter = Or(NumberConverter, AssignInitial(0));

        public static PropertyFlags Flags = PropertyFlags.Shorthand;
    }
}
