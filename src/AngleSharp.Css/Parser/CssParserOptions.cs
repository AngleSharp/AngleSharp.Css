namespace AngleSharp.Css.Parser
{
    using System;

    /// <summary>
    /// Defines additional options for parsing CSS.
    /// </summary>
    public struct CssParserOptions
    {
        /// <summary>
        /// Gets or sets if unknown declarations (CSS properties) should be included.
        /// If they are not included unknown property names will be ignored.
        /// </summary>
        public Boolean IsIncludingUnknownDeclarations
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if unknown rules (@-rules) should be included.
        /// If they are not included unknown @-rules will be ignored.
        /// </summary>
        public Boolean IsIncludingUnknownRules
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if invalid CSS selectors are tolerated.
        /// If they are not tolerated the associated rule is ignored.
        /// </summary>
        public Boolean IsToleratingInvalidSelectors
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if nesting of style rules should be
        /// disabled; if disabled the interpretation is closer to
        /// older browser also accepting declarations such as
        /// "*x-overflow", which would otherwise be an invalid
        /// nesting rule.
        /// </summary>
        public Boolean IsExcludingNesting
        {
            get;
            set;
        }
    }
}