#nullable disable
namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class AnimationCompositionDeclaration
    {
        public static String Name = PropertyNames.AnimationComposition;

        public static IValueConverter Converter = AnimationCompositionConverter;

        public static ICssValue InitialValue = InitialValues.AnimationCompositionDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
