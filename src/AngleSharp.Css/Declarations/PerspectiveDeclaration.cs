namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class PerspectiveDeclaration
    {
        public static String Name = PropertyNames.Perspective;

        public static IValueConverter Converter = Or(LengthConverter, None);

        public static ICssValue InitialValue = InitialValues.PerspectiveDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
