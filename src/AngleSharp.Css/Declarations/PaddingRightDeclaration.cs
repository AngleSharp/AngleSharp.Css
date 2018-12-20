namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class PaddingRightDeclaration
    {
        public static String Name = PropertyNames.PaddingRight;

        public static String Parent = PropertyNames.Padding;

        public static IValueConverter Converter = Or(LengthOrPercentConverter, AssignInitial(Length.Zero));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
