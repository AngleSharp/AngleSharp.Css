namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class TransformOriginDeclaration
    {
        public static String Name = PropertyNames.TransformOrigin;

        public static IValueConverter Converter = Or(OriginConverter, AssignInitial(InitialValues.TransformOriginDecl));

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
