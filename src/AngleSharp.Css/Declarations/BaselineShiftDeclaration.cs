namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class BaselineShiftDeclaration
    {
        public static String Name = PropertyNames.BaselineShift;

        public static IValueConverter Converter = Or(
            LengthOrPercentConverter,
            Assign("baseline", "baseline"),
            Assign(CssKeywords.Sub, CssKeywords.Sub),
            Assign(CssKeywords.Super, CssKeywords.Super));

        public static ICssValue InitialValue = InitialValues.BaselineShiftDecl;

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}