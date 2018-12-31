namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

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

        public static VarReferences ParseVars(this StringSource source)
        {
            var index = source.Index;
            var length = FunctionNames.Var.Length;
            var refs = default(List<Tuple<TextRange, VarReference>>);

            while (!source.IsDone)
            {
                index = source.Content.IndexOf(FunctionNames.Var, index, StringComparison.OrdinalIgnoreCase) + length;

                if (index >= length)
                {
                    source.NextTo(index);
                    var c = source.SkipSpacesAndComments();

                    if (c == Symbols.RoundBracketOpen)
                    {
                        source.SkipCurrentAndSpaces();
                        var s = new TextPosition(0, 0, source.Index);
                        var reference = ParseVar(source);

                        if (reference == null)
                        {
                            refs = null;
                            break;
                        }

                        if (refs == null)
                        {
                            refs = new List<Tuple<TextRange, VarReference>>();
                        }

                        var e = new TextPosition(0, 0, source.Index);
                        refs.Add(Tuple.Create(new TextRange(s, e), reference));
                        continue;
                    }
                }
                
                break;
            }

            if (refs != null)
            {
                return new VarReferences(source.Content, refs);
            }

            return null;
        }

        public static VarReference ParseVar(this StringSource source)
        {
            var name = source.ParseCustomIdent();
            var f = source.SkipGetSkip();

            if (name != null)
            {
                switch (f)
                {
                    case Symbols.RoundBracketClose:
                        return new VarReference(name);
                    case Symbols.Comma:
                        var defaultValue = source.TakeUntilClosed();
                        source.SkipCurrentAndSpaces();
                        return new VarReference(name, defaultValue);
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
