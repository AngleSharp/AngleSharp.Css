namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ShapeImageThresholdDeclaration
    {
        public static String Name = PropertyNames.ShapeImageThreshold;

        public static IValueConverter Converter = ShapeImageThresholdConverter;

        public static ICssValue InitialValue = InitialValues.ShapeImageThresholdDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
