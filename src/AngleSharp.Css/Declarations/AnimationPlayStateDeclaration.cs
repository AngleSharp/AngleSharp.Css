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

        public static IValueConverter Converter = Or(PlayStateConverter.FromList(), AssignInitial(PlayState.Running));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
