namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class BackgroundImageDeclaration
    {
        public static String Name = PropertyNames.BackgroundImage;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Background,
        };

        public static IValueConverter Converter = Or(MultipleImageSourceConverter, AssignInitial(InitialValues.BackgroundImageDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
