namespace AngleSharp.Css.Parser
{
    using System;

    public struct CssParserOptions
    {
        public Boolean IsIncludingUnknownDeclarations
        {
            get;
            set;
        }

        public Boolean IsIncludingUnknownRules
        {
            get;
            set;
        }

        public Boolean IsToleratingInvalidSelectors
        {
            get;
            set;
        }
    }
}