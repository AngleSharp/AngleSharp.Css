namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaskBorderRepeatDeclaration
    {
        public static String Name = PropertyNames.MaskBorderRepeat;

        public static String[] Shorthands = new[]
        {
            PropertyNames.MaskBorder,
        };

        public static IValueConverter Converter = MaskBorderRepeatConverter;

        public static ICssValue InitialValue = InitialValues.MaskBorderRepeatDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
