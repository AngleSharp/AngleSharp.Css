namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class BorderImageOutsetDeclaration
    {
        public static String Name = PropertyNames.BorderImageOutset;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderImage,
        };

        public static IValueConverter Converter = Or(LengthOrPercentConverter.Periodic(), AssignInitial(Length.Zero));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
