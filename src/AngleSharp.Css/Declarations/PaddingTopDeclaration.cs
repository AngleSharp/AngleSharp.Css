namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class PaddingTopDeclaration
    {
        public static String Name = PropertyNames.PaddingTop;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Padding,
        };

        public static IValueConverter Converter = Or(LengthOrPercentConverter, AssignInitial(InitialValues.PaddingTopDecl));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
