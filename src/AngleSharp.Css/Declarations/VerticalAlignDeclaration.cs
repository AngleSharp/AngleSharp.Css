namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class VerticalAlignDeclaration
    {
        public static String Name = PropertyNames.VerticalAlign;

        public static IValueConverter Converter = Or(LengthOrPercentConverter, VerticalAlignmentConverter);

        public static ICssValue InitialValue = InitialValues.VerticalAlignDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
