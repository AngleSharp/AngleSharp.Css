namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BackgroundPositionXDeclaration
    {
        public static String Name = PropertyNames.BackgroundPositionX;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BackgroundPosition,
        };

        public static IValueConverter Converter = PointXConverter.FromList();

        public static ICssValue InitialValue = InitialValues.BackgroundPositionXDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
