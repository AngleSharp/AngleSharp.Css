namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BackgroundPositionYDeclaration
    {
        public static String Name = PropertyNames.BackgroundPositionY;

        public static String[] Shorthands = new[]
        {
            PropertyNames.BackgroundPosition,
        };

        public static IValueConverter Converter = PointYConverter.FromList();

        public static ICssValue InitialValue = InitialValues.BackgroundPositionYDecl;

        public static PropertyFlags Flags = PropertyFlags.Animatable;
    }
}
