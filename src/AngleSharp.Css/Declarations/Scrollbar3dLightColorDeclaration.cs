namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class Scrollbar3dLightColorDeclaration
    {
        public static String Name = PropertyNames.Scrollbar3dLightColor;

        public static IValueConverter Converter = ColorConverter;

        public static ICssValue InitialValue = InitialValues.Scrollbar3dLightColorDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
