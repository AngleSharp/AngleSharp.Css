namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class TextTransformDeclaration
    {
        public static String Name = PropertyNames.TextTransform;

        public static IValueConverter Converter = Or(TextTransformConverter, AssignInitial(InitialValues.TextTransformDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
