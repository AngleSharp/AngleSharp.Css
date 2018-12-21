namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class MarginRightDeclaration
    {
        public static String Name = PropertyNames.MarginRight;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Margin,
        };

        public static IValueConverter Converter = Or(AutoLengthOrPercentConverter, AssignInitial(Length.Zero));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
