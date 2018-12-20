namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class PaddingLeftDeclaration
    {
        public static String Name = PropertyNames.PaddingLeft;

        public static String Parent = PropertyNames.Padding;

        public static IValueConverter Converter = Or(LengthOrPercentConverter, AssignInitial(Length.Zero));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
