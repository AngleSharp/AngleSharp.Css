namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ListStyleDeclaration
    {
        public static String Name = PropertyNames.ListStyle;

        public static IValueConverter Converter = AggregateTuple(
            WithAny(
                ListStyleConverter.Option(InitialValues.ListStyleTypeDecl),
                ListPositionConverter.Option(InitialValues.ListStylePositionDecl),
                OptionalImageSourceConverter.Option(InitialValues.ListStyleImageDecl)));

        public static ICssValue InitialValue = null;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.ListStyleType,
            PropertyNames.ListStylePosition,
            PropertyNames.ListStyleImage,
        };
    }
}
