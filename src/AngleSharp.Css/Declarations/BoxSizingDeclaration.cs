namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BoxSizingDeclaration
    {
        public static String Name = PropertyNames.BoxSizing;

        public static IValueConverter Converter = BoxModelConverter;

        public static ICssValue InitialValue = InitialValues.BoxSizingDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
