namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderImageOutsetDeclaration
    {
        public static String Name = PropertyNames.BorderImageOutset;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderImage,
        };

        public static IValueConverter Converter = LengthOrPercentConverter.Periodic();

        public static ICssValue InitialValue = InitialValues.BorderImageOutsetDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
