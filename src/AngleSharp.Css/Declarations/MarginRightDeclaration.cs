namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class MarginRightDeclaration
    {
        public static String Name = PropertyNames.MarginRight;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Margin,
        };

        public static IValueConverter Converter = Or(AutoLengthOrPercentConverter, AssignInitial(InitialValues.MarginRightDecl));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
