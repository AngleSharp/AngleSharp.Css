namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class AnimationTimingFunctionDeclaration
    {
        public static String Name = PropertyNames.AnimationTimingFunction;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Animation,
        };

        public static IValueConverter Converter = Or(TransitionConverter.FromList(), AssignInitial(InitialValues.AnimationTimingFunctionDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
