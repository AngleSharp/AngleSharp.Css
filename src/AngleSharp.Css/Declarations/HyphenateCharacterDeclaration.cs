namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class HyphenateCharacterDeclaration
    {
        public static String Name = PropertyNames.HyphenateCharacter;

        public static IValueConverter Converter = HyphenateCharacterConverter;

        public static ICssValue InitialValue = InitialValues.HyphenateCharacterDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
