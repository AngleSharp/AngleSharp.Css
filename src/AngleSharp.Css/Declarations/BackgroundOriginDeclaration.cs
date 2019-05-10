namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class BackgroundOriginDeclaration
    {
        public static String Name = PropertyNames.BackgroundOrigin;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Background,
        };

        public static IValueConverter Converter = Or(BoxModelConverter.FromList(), AssignInitial(InitialValues.BackgroundOriginDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
