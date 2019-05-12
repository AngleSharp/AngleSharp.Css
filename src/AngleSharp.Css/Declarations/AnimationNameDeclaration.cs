namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AnimationNameDeclaration
    {
        public static String Name = PropertyNames.AnimationName;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Animation,
        };

        public static IValueConverter Converter = Or(IdentifierConverter.FromList(), None);

        public static ICssValue InitialValue = InitialValues.AnimationNameDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
