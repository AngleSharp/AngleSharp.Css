namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AnimationFillModeDeclaration
    {
        public static String Name = PropertyNames.AnimationFillMode;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Animation,
        };

        public static IValueConverter Converter = AnimationFillStyleConverter.FromList();

        public static ICssValue InitialValue = InitialValues.AnimationFillModeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
