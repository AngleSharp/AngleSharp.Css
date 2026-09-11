namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ClipPathDeclaration
    {
        public static String Name = PropertyNames.ClipPath;

        public static IValueConverter Converter = Any;

        public static ICssValue InitialValue = InitialValues.ClipPathDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}