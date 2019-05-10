namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class AnimationFillModeDeclaration
    {
        public static String Name = PropertyNames.AnimationFillMode;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Animation,
        };

        public static IValueConverter Converter = Or(AnimationFillStyleConverter.FromList(), AssignInitial(InitialValues.AnimationFillModeDecl));

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
