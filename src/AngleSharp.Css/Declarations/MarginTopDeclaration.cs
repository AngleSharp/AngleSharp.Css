namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class MarginTopDeclaration
    {
        public static String Name = PropertyNames.MarginTop;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Margin,
        };

        public static IValueConverter Converter = Or(AutoLengthOrPercentConverter, AssignInitial(InitialValues.MarginTopDecl));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
