namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class LeftDeclaration
    {
        public static String Name = PropertyNames.Left;

        public static IValueConverter Converter = Or(AutoLengthOrPercentConverter, AssignInitial(InitialValues.LeftDecl));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
