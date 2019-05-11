namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class WidthDeclaration
    {
        public static String Name = PropertyNames.Width;

        public static IValueConverter Converter = Or(AutoLengthOrPercentConverter, AssignInitial(InitialValues.WidthDecl));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
