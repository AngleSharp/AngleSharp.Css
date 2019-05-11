namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class PaddingLeftDeclaration
    {
        public static String Name = PropertyNames.PaddingLeft;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Padding,
        };

        public static IValueConverter Converter = Or(LengthOrPercentConverter, AssignInitial(InitialValues.PaddingLeftDecl));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
