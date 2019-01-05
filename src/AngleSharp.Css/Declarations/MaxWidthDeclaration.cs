namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class MaxWidthDeclaration
    {
        public static String Name = PropertyNames.MaxWidth;

        public static IValueConverter Converter = Or(OptionalLengthOrPercentConverter, AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
