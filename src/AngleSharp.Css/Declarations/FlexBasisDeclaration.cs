namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FlexBasisDeclaration
    {
        public static String Name = PropertyNames.FlexBasis;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Flex,
        };

        public static IValueConverter Converter = Or(Content, WidthConverter);

        public static ICssValue InitialValue = InitialValues.FlexBasisDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
