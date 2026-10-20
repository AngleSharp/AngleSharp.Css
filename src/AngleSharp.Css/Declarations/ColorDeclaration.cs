namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class ColorDeclaration
    {
        public static String Name = PropertyNames.Color;

        public static IValueConverter Converter = new ColorValueConverter();

        public static ICssValue InitialValue = InitialValues.ColorDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Hashless | PropertyFlags.Animatable;

        sealed class ColorValueConverter : IValueConverter
        {
            public ICssValue? Convert(StringSource source) =>
                source.IsIdentifier(CssKeywords.CurrentColor) ? CurrentColorValue.Instance : ColorConverter.Convert(source);
        }

        /// <summary>
        /// The currentcolor keyword on the color property itself, which computes like inherit.
        /// </summary>
        sealed class CurrentColorValue : ICssValue
        {
            public static readonly CurrentColorValue Instance = new();

            public String CssText => CssKeywords.CurrentColor;

            public ICssValue Compute(ICssComputeContext context) => CssInheritValue.Instance;

            public Boolean Equals(ICssValue? other) => other is CurrentColorValue;
        }
    }
}
