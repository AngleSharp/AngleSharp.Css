namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Values;
    using System;
    using static ValueConverters;

    static class LineHeightDeclaration
    {
        public static String Name = PropertyNames.LineHeight;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Font,
        };

        public static IValueConverter Converter = Or(LineHeightConverter, AssignInitial(new Length(120f, Length.Unit.Percent)));

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}
