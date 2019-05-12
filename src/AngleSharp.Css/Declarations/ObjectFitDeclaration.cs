namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class ObjectFitDeclaration
    {
        public static String Name = PropertyNames.ObjectFit;

        public static IValueConverter Converter = ObjectFittingConverter;

        public static ICssValue InitialValue = InitialValues.ObjectFitDecl;

        public static PropertyFlags Flags = PropertyFlags.None;
    }
}
