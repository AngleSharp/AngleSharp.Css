namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class WidthDeclaration
    {
        public static String Name = PropertyNames.Width;

        public static IValueConverter Converter = Or(AutoLengthOrPercentConverter, AssignInitial(Length.Auto));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
