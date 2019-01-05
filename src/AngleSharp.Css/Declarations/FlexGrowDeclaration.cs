namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class FlexGrowDeclaration
    {
        public static String Name = PropertyNames.FlexGrow;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Flex,
        };

        public static IValueConverter Converter = Or(NumberConverter, AssignInitial(0));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
