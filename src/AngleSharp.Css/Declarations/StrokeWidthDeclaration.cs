namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class StrokeWidthDeclaration
    {
        public static String Name = PropertyNames.StrokeWidth;

        public static IValueConverter Converter = Or(
            LengthOrPercentConverter,
            NumberConverter,
            AssignInitial(InitialValues.StrokeWidthDecl));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
