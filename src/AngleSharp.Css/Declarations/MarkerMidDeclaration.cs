namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MarkerMidDeclaration
    {
        public static String Name = PropertyNames.MarkerMid;

        public static IValueConverter Converter = Or(None, UrlConverter);

        public static ICssValue InitialValue = InitialValues.MarkerDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}