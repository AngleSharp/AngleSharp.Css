namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class MarginTopDeclaration
    {
        public static String Name = PropertyNames.MarginTop;

        public static String Parent = PropertyNames.Margin;

        public static IValueConverter Converter = Or(AutoLengthOrPercentConverter, AssignInitial(Length.Zero));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
