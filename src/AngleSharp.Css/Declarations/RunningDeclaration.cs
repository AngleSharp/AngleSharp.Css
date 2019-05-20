namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class RunningDeclaration
    {
        public static String Name = PropertyNames.Running;

        public static IValueConverter Converter = IdentifierConverter;

        public static ICssValue InitialValue = InitialValues.RunningDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
