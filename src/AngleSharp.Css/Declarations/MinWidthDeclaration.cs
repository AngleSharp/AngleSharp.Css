namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class MinWidthDeclaration
    {
        public static String Name = PropertyNames.MinWidth;

        public static IValueConverter Converter = Or(LengthOrPercentConverter, AssignInitial(Length.Zero));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
