namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class TopDeclaration
    {
        public static String Name = PropertyNames.Top;

        public static IValueConverter Converter = Or(AutoLengthOrPercentConverter, AssignInitial(InitialValues.TopDecl));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
