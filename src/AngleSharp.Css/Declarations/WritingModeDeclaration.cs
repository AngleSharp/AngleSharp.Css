namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using System;
    using static ValueConverters;

    static class WritingModeDeclaration
    {
        public static String Name = PropertyNames.WritingMode;

        public static IValueConverter Converter = Or(
            Assign("horizontal-tb", "horizontal-tb"),
            Assign("vertical-rl", "vertical-rl"),
            Assign("vertical-lr", "vertical-lr"),
            Assign("sideways-rl", "sideways-rl"),
            Assign("sideways-lr", "sideways-lr"),
            // Legacy SVG values, see CSS Writing Modes 3 (10.1)
            Alias("lr", "horizontal-tb"),
            Alias("lr-tb", "horizontal-tb"),
            Alias("rl", "horizontal-tb"),
            Alias("rl-tb", "horizontal-tb"),
            Alias("tb", "vertical-rl"),
            Alias("tb-rl", "vertical-rl"));

        public static ICssValue InitialValue = InitialValues.WritingModeDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;

        private static IValueConverter Alias(String legacy, String value) =>
            new IdentifierValueConverter(source => source.IsIdentifier(legacy) ? value : null);
    }
}
