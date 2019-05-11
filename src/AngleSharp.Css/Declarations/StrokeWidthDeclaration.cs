namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class StrokeWidthDeclaration
    {
        public static String Name = PropertyNames.StrokeWidth;

        public static IValueConverter Converter = Or(
            LengthOrPercentConverter,
            NumberConverter);

        public static ICssValue InitialValue = InitialValues.StrokeWidthDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
