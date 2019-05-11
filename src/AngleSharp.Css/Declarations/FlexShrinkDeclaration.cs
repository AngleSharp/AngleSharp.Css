namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FlexShrinkDeclaration
    {
        public static String Name = PropertyNames.FlexShrink;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Flex,
        };

        public static IValueConverter Converter = NumberConverter;

        public static ICssValue InitialValue = InitialValues.FlexShrinkDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
