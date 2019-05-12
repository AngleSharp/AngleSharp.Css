namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TransformDeclaration
    {
        public static String Name = PropertyNames.Transform;

        public static IValueConverter Converter = Or(TransformConverter.Many(), None);

        public static ICssValue InitialValue = InitialValues.TransformDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
