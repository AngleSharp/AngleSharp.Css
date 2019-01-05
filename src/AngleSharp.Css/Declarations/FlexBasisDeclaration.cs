namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class FlexBasisDeclaration
    {
        public static String Name = PropertyNames.FlexBasis;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Flex,
        };

        public static IValueConverter Converter = Or(Content, AutoLengthOrPercentConverter, AssignInitial(Length.Auto));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
