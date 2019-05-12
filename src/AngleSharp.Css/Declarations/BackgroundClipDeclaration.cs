namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BackgroundClipDeclaration
    {
        public static String Name = PropertyNames.BackgroundClip;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Background,
        };

        public static IValueConverter Converter = BoxModelConverter.FromList();

        public static ICssValue InitialValue = InitialValues.BackgroundClipDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
