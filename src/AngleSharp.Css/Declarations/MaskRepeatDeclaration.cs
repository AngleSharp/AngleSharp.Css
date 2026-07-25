namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MaskRepeatDeclaration
    {
        public static String Name = PropertyNames.MaskRepeat;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Mask,
        };

        public static IValueConverter Converter = MaskRepeatConverter;

        public static ICssValue InitialValue = InitialValues.MaskRepeatDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
