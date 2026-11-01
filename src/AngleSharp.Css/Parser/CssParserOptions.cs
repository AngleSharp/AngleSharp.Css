namespace AngleSharp.Css.Parser
{
    using System;

    /// <summary>
    /// Defines additional options for parsing CSS.
    /// </summary>
    public struct CssParserOptions
    {
        /// <summary>
        /// The number of blocks a rule may be nested in when <see cref="MaxNestingDepth"/> is
        /// not set: 64.
        /// </summary>
        public const Int32 DefaultMaxNestingDepth = 64;

        /// <summary>
        /// Gets or sets the number of blocks a rule may be nested in, counting grouping rules
        /// such as @media, @supports, @layer or @container, and style rules holding nested
        /// style rules. A rule nested deeper is dropped like an invalid rule, raising
        /// <see cref="CssParseError.NestingTooDeep"/>; parsing continues after it. 0, the
        /// default, uses <see cref="DefaultMaxNestingDepth"/>.
        /// <para>
        /// This is a mitigation, not a fix: rules are still parsed recursively, once per level,
        /// and only this limit keeps a hostile style sheet from overflowing the stack, which
        /// ends the process. Raising it brings that crash back: without a limit, about 580
        /// nested @media rules overflow a 1 MB stack and about 125 a 256 KB thread stack (.NET 8,
        /// before the JIT optimizes the code).
        /// </para>
        /// </summary>
        public Int32 MaxNestingDepth
        {
            get;
            set;
        }

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

        /// <summary>
        /// Gets or sets if relative URLs in the declarations of a style
        /// sheet with an address of its own (linked or imported) should
        /// not be resolved against that address, as browsers do. Otherwise
        /// they stay as written and resolve against the document. Enabled
        /// by default.
        /// </summary>
        public Boolean IsNotResolvingRelativeUrls
        {
            get;
            set;
        }
    }
}