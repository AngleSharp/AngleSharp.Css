namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    static class FunctionParser
    {
        /// <summary>
        /// Represents an attribute retriever object.
        /// http://dev.w3.org/csswg/css-values/#funcdef-attr
        /// </summary>
        public static String ParseAttr(this StringSource source)
        {
            if (source.IsFunction(FunctionNames.Attr))
            {
                var content = source.ParseString() ?? source.ParseIdent();
                var f = source.SkipGetSkip();

                if (content != null && f == Symbols.RoundBracketClose)
                {
                    return content;
                }
            }

            return null;
        }

        /// <summary>
        /// Represents a counter object.
        /// http://www.w3.org/TR/CSS2/syndata.html#value-def-counter
        /// </summary>
        public static CounterDefinition? ParseCounter(this StringSource source)
        {
            if (source.IsFunction(FunctionNames.Counter))
            {
                var ident = source.ParseIdent();
                var f = source.SkipGetSkip();

                if (ident != null)
                {
                    return ParseCounterStyle(source, ident, null, f);
                }
            }
            else if (source.IsFunction(FunctionNames.Counters))
            {
                var ident = source.ParseIdent();
                var c = source.SkipGetSkip();
                var separator = source.ParseString();
                var f = source.SkipGetSkip();

                if (ident != null && separator != null && c == Symbols.Comma)
                {
                    return ParseCounterStyle(source, ident, separator, f);
                }
            }

            return null;
        }

        private static CounterDefinition? ParseCounterStyle(StringSource source, String ident, String separator, Char f)
        {
            var style = CssKeywords.Decimal;

            if (f == Symbols.Comma)
            {
                style = source.ParseIdent();
                f = source.SkipGetSkip();
            }

            if (f == Symbols.RoundBracketClose)
            {
                return new CounterDefinition(ident, style, separator);
            }

            return null;
        }
    }
}
