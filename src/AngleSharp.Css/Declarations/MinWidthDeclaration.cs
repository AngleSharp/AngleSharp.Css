namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class MinWidthDeclaration
    {
        public static String Name = PropertyNames.MinWidth;

        public static IValueConverter Converter = Or(LengthOrPercentConverter, AssignInitial(InitialValues.MinWidthDecl));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
