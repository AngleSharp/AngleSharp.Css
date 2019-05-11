namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextShadowDeclaration
    {
        public static String Name = PropertyNames.TextShadow;

        public static IValueConverter Converter = MultipleShadowConverter;

        public static ICssValue InitialValue = InitialValues.TextShadowDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}
