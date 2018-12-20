namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class ClipDeclaration
    {
        public static String Name = PropertyNames.Clip;

        public static IValueConverter Converter = Or(ShapeConverter, Auto, AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
