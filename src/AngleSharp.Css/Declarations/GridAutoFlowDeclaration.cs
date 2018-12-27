namespace AngleSharp.Css.Declarations
{
    using System;
    using static ValueConverters;

    static class GridAutoFlowDeclaration
    {
        public static readonly String Name = PropertyNames.GridAutoFlow;

        public static readonly IValueConverter Converter = Or(WithAny(Toggle(CssKeywords.Column, CssKeywords.Row), Assign(CssKeywords.Dense, true)), AssignInitial());

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
