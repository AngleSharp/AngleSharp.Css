namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ZIndexDeclaration
    {
        public static String Name = PropertyNames.ZIndex;

        public static IValueConverter Converter = OptionalIntegerConverter;

        public static ICssValue InitialValue = InitialValues.ZIndexDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
