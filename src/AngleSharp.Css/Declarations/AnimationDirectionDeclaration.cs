namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AnimationDirectionDeclaration
    {
        public static String Name = PropertyNames.AnimationDirection;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Animation,
        };

        public static IValueConverter Converter = AnimationDirectionConverter.FromList();

        public static ICssValue InitialValue = InitialValues.AnimationDirectionDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
