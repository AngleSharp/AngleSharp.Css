namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class BackgroundRepeatYDeclaration
    {
        public static String Name = PropertyNames.BackgroundRepeatY;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BackgroundRepeat,
        };

        public static IValueConverter Converter = Or(BackgroundRepeatConverter.FromList(), AssignInitial(InitialValues.BackgroundRepeatVerticalDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
