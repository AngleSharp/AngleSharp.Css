namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
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
            Assign("sideways-lr", "sideways-lr"));

        public static ICssValue InitialValue = InitialValues.WritingModeDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}