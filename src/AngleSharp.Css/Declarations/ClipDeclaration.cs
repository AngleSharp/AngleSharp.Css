namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ClipDeclaration
    {
        public static String Name = PropertyNames.Clip;

        public static IValueConverter Converter = Or(ShapeConverter, Auto);

        public static ICssValue InitialValue = InitialValues.ClipDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
