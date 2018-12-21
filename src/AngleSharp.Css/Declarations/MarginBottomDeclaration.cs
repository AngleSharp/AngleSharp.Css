namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class MarginBottomDeclaration
    {
        public static String Name = PropertyNames.MarginBottom;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Margin,
        };

        public static IValueConverter Converter = Or(AutoLengthOrPercentConverter, AssignInitial(Length.Zero));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
