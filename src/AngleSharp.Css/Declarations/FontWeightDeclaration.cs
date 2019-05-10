namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class FontWeightDeclaration
    {
        public static String Name = PropertyNames.FontWeight;

        public static String[] Shorthands = new[]
        {
            PropertyNames.Font,
        };

        public static IValueConverter Converter = Or(FontWeightConverter, WeightIntegerConverter, AssignInitial(InitialValues.FontWeightDecl));

        public static PropertyFlags Flags = PropertyFlags.Inherited | PropertyFlags.Animatable;
    }
}
