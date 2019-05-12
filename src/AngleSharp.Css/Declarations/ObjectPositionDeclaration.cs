namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ObjectPositionDeclaration
    {
        public static String Name = PropertyNames.ObjectPosition;

        public static IValueConverter Converter = PointConverter;

        public static ICssValue InitialValue = InitialValues.ObjectPositionDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
