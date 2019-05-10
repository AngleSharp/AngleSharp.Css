namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class BackgroundRepeatXDeclaration
    {
        public static String Name = PropertyNames.BackgroundRepeatX;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BackgroundRepeat,
        };

        public static IValueConverter Converter = Or(BackgroundRepeatConverter.FromList(), AssignInitial(InitialValues.BackgroundRepeatHorizontalDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
