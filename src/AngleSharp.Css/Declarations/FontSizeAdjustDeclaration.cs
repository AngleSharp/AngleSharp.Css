namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class FontSizeAdjustDeclaration
    {
        public static String Name = PropertyNames.FontSizeAdjust;

        public static IValueConverter Converter = Or(OptionalNumberConverter, AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}
