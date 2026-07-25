namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OverflowClipMarginDeclaration
    {
        public static String Name = PropertyNames.OverflowClipMargin;

        public static IValueConverter Converter = OverflowClipMarginConverter;

        public static ICssValue InitialValue = InitialValues.OverflowClipMarginDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
