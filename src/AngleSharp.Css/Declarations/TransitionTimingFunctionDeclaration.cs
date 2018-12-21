namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class TransitionTimingFunctionDeclaration
    {
        public static String Name = PropertyNames.TransitionTimingFunction;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Transition,
        };

        public static IValueConverter Converter = Or(TransitionConverter.FromList(), AssignInitial(CubicBezierTimingFunction.Ease));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
