namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class MixBlendModeDeclaration
    {
        public static String Name = PropertyNames.MixBlendMode;

        public static IValueConverter Converter = MixBlendModeConverter;

        public static ICssValue InitialValue = InitialValues.MixBlendModeDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
