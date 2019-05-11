namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class LineHeightDeclaration
    {
        public static String Name = PropertyNames.LineHeight;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Font,
        };

        public static IValueConverter Converter = Or(LineHeightConverter, AssignInitial(InitialValues.LineHeightDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}
