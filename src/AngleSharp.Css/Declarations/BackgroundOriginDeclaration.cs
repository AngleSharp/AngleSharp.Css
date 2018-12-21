namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BackgroundOriginDeclaration
    {
        public static String Name = PropertyNames.BackgroundOrigin;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Background,
        };

        public static IValueConverter Converter = Or(BoxModelConverter.FromList(), AssignInitial(BoxModel.PaddingBox));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
