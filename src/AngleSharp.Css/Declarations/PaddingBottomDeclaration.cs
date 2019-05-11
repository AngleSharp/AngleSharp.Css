namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class PaddingBottomDeclaration
    {
        public static String Name = PropertyNames.PaddingBottom;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Padding,
        };

        public static IValueConverter Converter = Or(LengthOrPercentConverter, AssignInitial(InitialValues.PaddingBottomDecl));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
