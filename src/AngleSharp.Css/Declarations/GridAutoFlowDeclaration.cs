namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using System;
    using static ValueConverters;

    static class GridAutoFlowDeclaration
    {
        public static readonly String Name = PropertyNames.GridAutoFlow;

        public static readonly IValueConverter Converter = WithAny(
            Toggle(CssKeywords.Column, CssKeywords.Row),
            Assign(CssKeywords.Dense, true));

        public static readonly ICssValue InitialValue = InitialValues.GridAutoFlowDecl;

        public static readonly PropertyFlags Flags = PropertyFlags.None;
    }
}
