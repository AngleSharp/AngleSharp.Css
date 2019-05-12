namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BackgroundAttachmentDeclaration
    {
        public static String Name = PropertyNames.BackgroundAttachment;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Background,
        };

        public static IValueConverter Converter = BackgroundAttachmentConverter.FromList();

        public static ICssValue InitialValue = InitialValues.BackgroundAttachmentDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
