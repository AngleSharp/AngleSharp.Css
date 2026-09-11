namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MarkerEndDeclaration
    {
        public static String Name = PropertyNames.MarkerEnd;

        public static IValueConverter Converter = Or(None, UrlConverter);

        public static ICssValue InitialValue = InitialValues.MarkerDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}