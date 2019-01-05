namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using System;
    using static ValueConverters;

    static class AnimationNameDeclaration
    {
        public static String Name = PropertyNames.AnimationName;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Animation,
        };

        public static IValueConverter Converter = Or(IdentifierConverter.FromList(), None, AssignInitial());

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
