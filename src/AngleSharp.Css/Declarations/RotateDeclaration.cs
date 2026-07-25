namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class RotateDeclaration
    {
        public static String Name = PropertyNames.Rotate;

        public static IValueConverter Converter = RotateConverter;

        public static ICssValue InitialValue = InitialValues.RotateDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
