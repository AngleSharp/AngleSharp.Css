namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class OutlineDeclaration
    {
        public static String Name = PropertyNames.Outline;

        public static IValueConverter Converter = AggregateTuple(
            WithAny(
                LineWidthConverter.Option(InitialValues.OutlineWidthDecl),
                LineStyleConverter.Option(InitialValues.OutlineStyleDecl),
                InvertedColorConverter.Option(InitialValues.OutlineColorDecl)));

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Animatable | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.OutlineWidth,
            PropertyNames.OutlineStyle,
            PropertyNames.OutlineColor,
        };
    }
}
