namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class PerspectiveOriginDeclaration
    {
        public static String Name = PropertyNames.PerspectiveOrigin;

        public static IValueConverter Converter = PointConverter;

        public static ICssValue InitialValue = InitialValues.PerspectiveOriginDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
