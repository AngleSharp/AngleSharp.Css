#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AnimationRangeStartDeclaration
    {
        public static String Name = PropertyNames.AnimationRangeStart;

        public static String[] Shorthands = new[]
        {
            PropertyNames.AnimationRange,
        };

        public static IValueConverter Converter = AnimationRangeConverter;

        public static ICssValue InitialValue = InitialValues.AnimationRangeStartDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
