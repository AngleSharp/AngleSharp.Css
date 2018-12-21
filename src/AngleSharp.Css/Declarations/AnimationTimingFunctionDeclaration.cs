namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class AnimationTimingFunctionDeclaration
    {
        public static String Name = PropertyNames.AnimationTimingFunction;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Animation,
        };

        public static IValueConverter Converter = Or(TransitionConverter.FromList(), AssignInitial(CubicBezierTimingFunction.Ease));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
