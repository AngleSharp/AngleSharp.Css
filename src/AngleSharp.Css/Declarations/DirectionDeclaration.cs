namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class DirectionDeclaration
    {
        public static String Name = PropertyNames.Direction;

        public static IValueConverter Converter = DirectionModeConverter;

        public static ICssValue InitialValue = InitialValues.DirectionDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
