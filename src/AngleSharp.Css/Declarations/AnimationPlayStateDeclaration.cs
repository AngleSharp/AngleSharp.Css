namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class AnimationPlayStateDeclaration
    {
        public static String Name = PropertyNames.AnimationPlayState;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Animation,
        };

        public static IValueConverter Converter = Or(PlayStateConverter.FromList(), AssignInitial(InitialValues.AnimationPlayStateDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
