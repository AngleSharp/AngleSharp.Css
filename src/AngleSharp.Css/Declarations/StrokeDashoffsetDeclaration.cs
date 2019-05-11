namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class StrokeDashoffsetDeclaration
    {
        public static String Name = PropertyNames.StrokeDashoffset;

        public static IValueConverter Converter = LengthOrPercentConverter;

        public static ICssValue InitialValue = InitialValues.StrokeDashoffsetDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
