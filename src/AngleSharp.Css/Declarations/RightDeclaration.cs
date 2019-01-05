namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class RightDeclaration
    {
        public static String Name = PropertyNames.Right;

        public static IValueConverter Converter = Or(AutoLengthOrPercentConverter, AssignInitial(Length.Auto));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
