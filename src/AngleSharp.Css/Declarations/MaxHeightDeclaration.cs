namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class MaxHeightDeclaration
    {
        public static String Name = PropertyNames.MaxHeight;

        public static IValueConverter Converter = Or(OptionalLengthOrPercentConverter, AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
