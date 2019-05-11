namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BackgroundRepeatXDeclaration
    {
        public static String Name = PropertyNames.BackgroundRepeatX;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BackgroundRepeat,
        };

        public static IValueConverter Converter = BackgroundRepeatConverter.FromList();

        public static ICssValue InitialValue = InitialValues.BackgroundRepeatHorizontalDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
