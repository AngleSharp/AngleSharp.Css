namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class TextTransformDeclaration
    {
        public static String Name = PropertyNames.TextTransform;

        public static IValueConverter Converter = Or(TextTransformConverter, AssignInitial(TextTransform.None));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
