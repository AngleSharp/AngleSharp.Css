namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Parser.Tokens;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Text;

    // A flat token stream keeps both dependency discovery and nested fallback
    // substitution off the CLR stack. Strings, URLs and comments are opaque.
    sealed class CssVariableValue
    {
        // Bound expansion of small, exponentially growing variable definitions.
        internal const Int32 MaxSubstitutionLength = 1024 * 1024;

        private readonly List<CssToken> _tokens = new();
        private readonly Dictionary<Int32, Reference> _references = new();

        public CssVariableValue(String text)
        {
            Text = text;
            var tokenizer = new CssTokenizer(new TextSource(text));
            var blocks = new Stack<Int32>();
            var ends = new Dictionary<Int32, Int32>();

            while (true)
            {
                var token = tokenizer.Get();

                if (token.Type == CssTokenType.EndOfFile)
                {
                    break;
                }

                var index = _tokens.Count;
                _tokens.Add(token);

                if (IsOpen(token.Type))
                {
                    blocks.Push(index);
                }
                else if (IsClose(token.Type) && blocks.Count > 0)
                {
                    var start = blocks.Pop();
                    IsValid &= Matches(_tokens[start].Type, token.Type);
                    ends[start] = index;
                }
            }

            // CSS syntax closes outstanding blocks at EOF. Keep the original
            // text for CSSOM serialization, including incomplete URL strings.
            while (blocks.Count > 0)
            {
                var start = blocks.Pop();
                ends[start] = _tokens.Count;
                _tokens.Add(new CssToken(CloseType(_tokens[start].Type), String.Empty)
                {
                    Position = new TextPosition(0, 0, text.Length + 1),
                });
            }

            for (var i = 0; i < _tokens.Count; i++)
            {
                var token = _tokens[i];

                if (token.Type == CssTokenType.Function && token.Data.Equals(FunctionNames.Var, StringComparison.OrdinalIgnoreCase))
                {
                    var name = SkipTrivia(i + 1);
                    var separator = SkipTrivia(name + 1);
                    var valid = ends.TryGetValue(i, out var end) &&
                        name < end && _tokens[name].Type == CssTokenType.Ident &&
                        _tokens[name].Data.StartsWith("--", StringComparison.Ordinal) &&
                        _tokens[name].Data.Length > 2 &&
                        (separator == end || _tokens[separator].Type == CssTokenType.Comma);

                    IsValid &= valid;

                    if (valid)
                    {
                        _references.Add(i, new Reference(_tokens[name].Data, end, separator < end ? separator + 1 : -1));
                    }
                }
            }
        }

        public String Text { get; }

        public Boolean IsValid { get; } = true;

        public Boolean HasReferences => _references.Count > 0;

        public IEnumerable<String> Dependencies
        {
            get
            {
                foreach (var reference in _references.Values)
                {
                    yield return reference.Name;
                }
            }
        }

        public String? Keyword
        {
            get
            {
                var index = SkipTrivia(0);
                return index < _tokens.Count && _tokens[index].Type == CssTokenType.Ident &&
                    SkipTrivia(index + 1) == _tokens.Count ? _tokens[index].Data : null;
            }
        }

        public String? Substitute(Func<String, ICssValue?> resolve)
        {
            if (!IsValid)
            {
                return null;
            }

            if (!HasReferences)
            {
                return Text;
            }

            var result = new StringBuilder();
            var fallbacks = new Stack<Int32>();
            var cursor = 0;

            for (var i = 0; i < _tokens.Count; i++)
            {
                if (fallbacks.Count > 0 && fallbacks.Peek() == i)
                {
                    if (!Append(result, cursor, Offset(i)) || !Separate(result, NeedsSeparator(EndOffset(i))))
                    {
                        return null;
                    }

                    cursor = EndOffset(i);
                    fallbacks.Pop();
                }
                else if (_references.TryGetValue(i, out var reference))
                {
                    if (!Append(result, cursor, Offset(i)) || !Separate(result, result.Length > 0))
                    {
                        return null;
                    }

                    var value = resolve(reference.Name);

                    if (value is not null)
                    {
                        var text = value.CssText;

                        if (text.Length > MaxSubstitutionLength - result.Length)
                        {
                            return null;
                        }

                        result.Append(text);
                        cursor = EndOffset(reference.End);
                        i = reference.End;

                        if (!Separate(result, NeedsSeparator(cursor)))
                        {
                            return null;
                        }
                    }
                    else if (reference.Fallback >= 0)
                    {
                        cursor = Offset(reference.Fallback - 1) + 1;
                        i = reference.Fallback - 1;
                        fallbacks.Push(reference.End);
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return Append(result, cursor, Text.Length) ? result.ToString().Trim() : null;
        }

        private Boolean Append(StringBuilder result, Int32 start, Int32 end)
        {
            var length = end - start;

            if (length > MaxSubstitutionLength - result.Length)
            {
                return false;
            }

            result.Append(Text, start, length);
            return true;
        }

        private static Boolean Separate(StringBuilder result, Boolean needed)
        {
            if (needed && result.Length > 0 && !result[result.Length - 1].IsSpaceCharacter())
            {
                if (result.Length > MaxSubstitutionLength - 4)
                {
                    return false;
                }

                // Substitution must not turn adjacent tokens into a new token
                // (for example, var(--number)px must not become a dimension).
                result.Append("/**/");
            }

            return true;
        }

        private Int32 Offset(Int32 index) => _tokens[index].Position.Position - 1;

        private Int32 EndOffset(Int32 index) => Math.Min(Offset(index) + 1, Text.Length);

        private Boolean NeedsSeparator(Int32 index) => index < Text.Length && !Text[index].IsSpaceCharacter();

        private Int32 SkipTrivia(Int32 index)
        {
            while (index < _tokens.Count && (_tokens[index].Type == CssTokenType.Whitespace || _tokens[index].Type == CssTokenType.Comment))
            {
                index++;
            }

            return index;
        }

        private static Boolean IsOpen(CssTokenType type) =>
            type == CssTokenType.Function || type == CssTokenType.RoundBracketOpen ||
            type == CssTokenType.SquareBracketOpen || type == CssTokenType.CurlyBracketOpen;

        private static Boolean IsClose(CssTokenType type) =>
            type == CssTokenType.RoundBracketClose || type == CssTokenType.SquareBracketClose ||
            type == CssTokenType.CurlyBracketClose;

        private static CssTokenType CloseType(CssTokenType type) =>
            type == CssTokenType.SquareBracketOpen ? CssTokenType.SquareBracketClose :
            type == CssTokenType.CurlyBracketOpen ? CssTokenType.CurlyBracketClose : CssTokenType.RoundBracketClose;

        private static Boolean Matches(CssTokenType open, CssTokenType close) => CloseType(open) == close;

        private readonly struct Reference
        {
            public Reference(String name, Int32 end, Int32 fallback)
            {
                Name = name;
                End = end;
                Fallback = fallback;
            }

            public String Name { get; }
            public Int32 End { get; }
            public Int32 Fallback { get; }
        }
    }
}
