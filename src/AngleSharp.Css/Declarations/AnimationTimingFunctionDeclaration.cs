namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AnimationTimingFunctionDeclaration
    {
        public static String Name = PropertyNames.AnimationTimingFunction;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Animation,
        };

        public static IValueConverter Converter = TransitionConverter.FromList();

        public static ICssValue InitialValue = InitialValues.AnimationTimingFunctionDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
