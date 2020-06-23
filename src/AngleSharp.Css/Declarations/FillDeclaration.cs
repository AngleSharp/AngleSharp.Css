namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class FillDeclaration
    {
        public static String Name = PropertyNames.Fill;

        public static IValueConverter Converter = PaintConverter;

        public static ICssValue InitialValue = InitialValues.ColorDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Hashless | PropertyFlags.Animatable;
    }
}
