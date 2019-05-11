namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class VisibilityDeclaration
    {
        public static String Name = PropertyNames.Visibility;

        public static IValueConverter Converter = VisibilityConverter;

        public static ICssValue InitialValue = InitialValues.VisibilityDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}
