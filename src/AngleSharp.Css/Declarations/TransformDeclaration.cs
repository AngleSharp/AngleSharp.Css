namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class TransformDeclaration
    {
        public static String Name = PropertyNames.Transform;

        public static IValueConverter Converter = Or(TransformConverter.Many(), None, AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
