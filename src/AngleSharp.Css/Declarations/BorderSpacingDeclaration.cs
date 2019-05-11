namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BorderSpacingDeclaration
    {
        public static String Name = PropertyNames.BorderSpacing;

        public static IValueConverter Converter = LengthConverter.Many(1, 2);

        public static ICssValue InitialValue = InitialValues.BorderSpacingDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited;
    }
}
