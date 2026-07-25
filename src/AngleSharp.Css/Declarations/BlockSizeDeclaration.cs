namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BlockSizeDeclaration
    {
        public static String Name = PropertyNames.BlockSize;

        public static IValueConverter Converter = WidthConverter;

        public static ICssValue InitialValue = InitialValues.BlockSizeDecl;

        public static PropertyFlags Flags = PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
