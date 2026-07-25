namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class UserSelectDeclaration
    {
        public static String Name = PropertyNames.UserSelect;

        public static IValueConverter Converter = UserSelectConverter;

        public static ICssValue InitialValue = InitialValues.UserSelectDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
