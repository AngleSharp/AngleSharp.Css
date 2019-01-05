namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class FlexShrinkDeclaration
    {
        public static String Name = PropertyNames.FlexShrink;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Flex,
        };

        public static IValueConverter Converter = Or(NumberConverter, AssignInitial(1));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
