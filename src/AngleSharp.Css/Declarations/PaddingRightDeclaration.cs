namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class PaddingRightDeclaration
    {
        public static String Name = PropertyNames.PaddingRight;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Padding,
        };

        public static IValueConverter Converter = Or(LengthOrPercentConverter, AssignInitial(InitialValues.PaddingRightDecl));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
