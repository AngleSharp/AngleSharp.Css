namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    /// <summary>
    /// For more information, see:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/scroll-snap-type
    /// </summary>
    static class ScrollSnapTypeDeclaration
    {
        private static readonly ICssValue defaultStrictness = new Constant<ScrollSnapStrictness>(CssKeywords.Proximity, ScrollSnapStrictness.Proximity);

        public static String Name = PropertyNames.ScrollSnapType;

        public static IValueConverter Converter = Or(None, WithOrder(ScrollSnapAxisConverter, ScrollSnapStrictnessConverter.Option(defaultStrictness)));

        public static ICssValue InitialValue = InitialValues.ScrollSnapTypeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
