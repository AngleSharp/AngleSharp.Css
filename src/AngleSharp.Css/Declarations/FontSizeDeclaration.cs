namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class FontSizeDeclaration
    {
        public static String Name = PropertyNames.FontSize;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Font,
        };

        public static IValueConverter Converter = Or(FontSizeConverter, AssignInitial(new Length(1f, Length.Unit.Em)));

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable;
    }
}
