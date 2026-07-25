#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class JustifyItemsDeclaration
    {
        public static String Name = PropertyNames.JustifyItems;

        public static IValueConverter Converter = JustifyItemsConverter;

        public static ICssValue InitialValue = InitialValues.JustifyItemsDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
