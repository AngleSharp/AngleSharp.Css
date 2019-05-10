namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class AnimationDirectionDeclaration
    {
        public static String Name = PropertyNames.AnimationDirection;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Animation,
        };

        public static IValueConverter Converter = Or(AnimationDirectionConverter.FromList(), AssignInitial(InitialValues.AnimationDirectionDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
