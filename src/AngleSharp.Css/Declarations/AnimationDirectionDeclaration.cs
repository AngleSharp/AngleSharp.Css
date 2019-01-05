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

        public static IValueConverter Converter = Or(AnimationDirectionConverter.FromList(), AssignInitial(AnimationDirection.Normal));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
