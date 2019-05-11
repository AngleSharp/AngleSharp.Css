namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BackgroundRepeatYDeclaration
    {
        public static String Name = PropertyNames.BackgroundRepeatY;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BackgroundRepeat,
        };

        public static IValueConverter Converter = BackgroundRepeatConverter.FromList();

        public static ICssValue InitialValue = InitialValues.BackgroundRepeatVerticalDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
