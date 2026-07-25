#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class JustifySelfDeclaration
    {
        public static String Name = PropertyNames.JustifySelf;

        public static IValueConverter Converter = JustifySelfConverter;

        public static ICssValue InitialValue = InitialValues.JustifySelfDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
