#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ContainerTypeDeclaration
    {
        public static String Name = PropertyNames.ContainerType;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Container,
        };

        public static IValueConverter Converter = Or(
            Assign<Object>(CssKeywords.Normal, null),
            Assign<Object>(CssKeywords.Size, null),
            Assign<Object>(CssKeywords.InlineSize, null));

        public static ICssValue InitialValue = InitialValues.ContainerTypeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
