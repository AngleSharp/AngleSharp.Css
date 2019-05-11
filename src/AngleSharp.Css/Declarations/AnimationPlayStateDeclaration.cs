namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AnimationPlayStateDeclaration
    {
        public static String Name = PropertyNames.AnimationPlayState;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Animation,
        };

        public static IValueConverter Converter = PlayStateConverter.FromList();

        public static ICssValue InitialValue = InitialValues.AnimationPlayStateDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
