#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class FontVariantDeclaration
    {
        public static String Name = PropertyNames.FontVariant;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Font,
        };

        public static String[] Longhands = new[]
        {
            PropertyNames.FontVariantCaps,
        };

        public static IValueConverter Converter = new FontVariantAggregator();

        public static ICssValue InitialValue = InitialValues.FontVariantDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Shorthand;

        sealed class FontVariantAggregator : IValueAggregator, IValueConverter
        {
            public ICssValue Convert(StringSource source) => FontVariantCapsConverter.Convert(source);

            public ICssValue Merge(ICssValue[] values) => values[0];

            public ICssValue[] Split(ICssValue value) => new[] { value };
        }
    }
}
