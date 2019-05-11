namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderImageRepeatDeclaration
    {
        public static String Name = PropertyNames.BorderImageRepeat;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BorderImage,
        };

        public static IValueConverter Converter = BorderImageRepeatConverter;

        public static ICssValue InitialValue = InitialValues.BorderImageRepeatDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
