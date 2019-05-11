namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class MaxWidthDeclaration
    {
        public static String Name = PropertyNames.MaxWidth;

        public static IValueConverter Converter = Or(OptionalLengthOrPercentConverter, AssignInitial(InitialValues.MaxWidthDecl));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
