namespace AngleSharp.Css.Parser
{
    using System;

    /// <summary>
    /// The nesting depth the value and condition parsers (calc(), @media and @supports
    /// conditions) allow. Those parsers run inside the value converters, which do not see the
    /// parser options, so a parser makes its CssParserOptions.MaxValueNestingDepth current for
    /// the time it builds, which is synchronous; everything else (e.g. SetProperty on the CSSOM)
    /// gets the default.
    /// </summary>
    static class ValueNesting
    {
        [ThreadStatic]
        private static Int32 _limit;

        /// <summary>
        /// The depth allowed on this thread right now.
        /// </summary>
        public static Int32 Limit => _limit > 0 ? _limit : CssParserOptions.DefaultMaxValueNestingDepth;

        /// <summary>
        /// Makes the limit of the given options current until the returned scope is disposed.
        /// </summary>
        public static Scope Use(CssParserOptions options) => new Scope(options.MaxValueNestingDepth);

        public readonly struct Scope : IDisposable
        {
            private readonly Int32 _previous;

            public Scope(Int32 limit)
            {
                _previous = _limit;
                _limit = limit;
            }

            public void Dispose() => _limit = _previous;
        }
    }
}
