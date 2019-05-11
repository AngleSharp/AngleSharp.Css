namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class RightDeclaration
    {
        public static String Name = PropertyNames.Right;

        public static IValueConverter Converter = Or(AutoLengthOrPercentConverter, AssignInitial(InitialValues.RightDecl));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
