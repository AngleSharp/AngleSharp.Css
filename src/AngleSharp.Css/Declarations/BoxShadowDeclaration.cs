namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BoxShadowDeclaration
    {
        public static String Name = PropertyNames.BoxShadow;

        public static IValueConverter Converter = MultipleShadowConverter;

        public static ICssValue InitialValue = InitialValues.BoxShadowDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
