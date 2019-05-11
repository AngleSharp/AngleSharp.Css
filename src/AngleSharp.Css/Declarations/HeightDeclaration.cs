namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class HeightDeclaration
    {
        public static String Name = PropertyNames.Height;

        public static IValueConverter Converter = Or(AutoLengthOrPercentConverter, AssignInitial(InitialValues.HeightDecl));

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
