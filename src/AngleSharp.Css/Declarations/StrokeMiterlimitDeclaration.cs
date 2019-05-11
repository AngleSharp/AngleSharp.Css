namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class StrokeMiterlimitDeclaration
    {
        public static String Name = PropertyNames.StrokeMiterlimit;

        public static IValueConverter Converter = StrokeMiterlimitConverter;

        public static ICssValue InitialValue = InitialValues.StrokeMiterlimitDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
