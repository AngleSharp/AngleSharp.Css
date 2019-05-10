namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class VerticalAlignDeclaration
    {
        public static String Name = PropertyNames.VerticalAlign;

        public static IValueConverter Converter = Or(LengthOrPercentConverter, VerticalAlignmentConverter, AssignInitial(InitialValues.VerticalAlignDecl));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
