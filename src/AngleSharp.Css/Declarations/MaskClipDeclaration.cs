namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaskClipDeclaration
    {
        public static String Name = PropertyNames.MaskClip;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Mask,
        };

        public static IValueConverter Converter = MaskClipConverter;

        public static ICssValue InitialValue = InitialValues.MaskClipDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
