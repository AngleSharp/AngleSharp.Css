#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AnimationRangeEndDeclaration
    {
        public static String Name = PropertyNames.AnimationRangeEnd;

        public static String[] Shorthands = new[]
        {
            PropertyNames.AnimationRange,
        };

        public static IValueConverter Converter = AnimationRangeConverter;

        public static ICssValue InitialValue = InitialValues.AnimationRangeEndDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
