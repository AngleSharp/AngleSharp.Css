namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class BackgroundClipDeclaration
    {
        public static String Name = PropertyNames.BackgroundClip;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Background,
        };

        public static IValueConverter Converter = Or(BoxModelConverter.FromList(), AssignInitial(InitialValues.BackgroundClipDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
