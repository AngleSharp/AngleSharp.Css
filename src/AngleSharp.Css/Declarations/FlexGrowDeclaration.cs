namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FlexGrowDeclaration
    {
        public static String Name = PropertyNames.FlexGrow;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Flex,
        };

        public static IValueConverter Converter = NumberConverter;

        public static ICssValue InitialValue = InitialValues.FlexGrowDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
