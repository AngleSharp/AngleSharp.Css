namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents a value that is kept as text, e.g., the value of a custom property
    /// or a value referencing custom properties. The segments are either strings or
    /// references to custom properties, i.e., <see cref="CssVariableReferenceValue"/>.
    /// https://www.w3.org/TR/css-typed-om-1/#cssunparsedvalue
    /// </summary>
    [DomName("CSSUnparsedValue")]
    public sealed class CssUnparsedValue : CssStyleValue, IReadOnlyList<Object>
    {
        #region Fields

        private readonly List<Object> _segments;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new unparsed value from the given segments.
        /// </summary>
        /// <param name="members">The segments, i.e., strings or variable references.</param>
        /// <exception cref="ArgumentException">A segment is neither a string nor a variable reference.</exception>
        [DomConstructor]
        public CssUnparsedValue(IEnumerable<Object> members)
        {
            _segments = (members ?? throw new ArgumentNullException(nameof(members))).Select(Validate).ToList();
        }

        private CssUnparsedValue(List<Object> segments)
        {
            _segments = segments;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the segment at the given index. Setting the index right after
        /// the last segment appends a segment.
        /// </summary>
        /// <exception cref="ArgumentException">A segment is neither a string nor a variable reference.</exception>
        [DomName("item")]
        public Object this[Int32 index]
        {
            get => _segments[index];
            set
            {
                if (index == _segments.Count)
                {
                    _segments.Add(Validate(value));
                }
                else
                {
                    _segments[index] = Validate(value);
                }
            }
        }

        /// <summary>
        /// Gets the number of segments.
        /// </summary>
        [DomName("length")]
        public Int32 Length => _segments.Count;

        Int32 IReadOnlyCollection<Object>.Count => _segments.Count;

        #endregion

        #region Methods

        /// <summary>
        /// Gets an enumerator over the segments.
        /// </summary>
        public IEnumerator<Object> GetEnumerator() => _segments.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Serializes the value, i.e., concatenates the segments.
        /// </summary>
        public override String ToString()
        {
            var sb = new StringBuilder();

            foreach (var segment in _segments)
            {
                sb.Append(segment.ToString());
            }

            return sb.ToString();
        }

        #endregion

        #region Parsing

        /// <summary>
        /// Splits the given text into strings and references to custom properties.
        /// </summary>
        internal static CssUnparsedValue FromText(String text) => FromText(text, 0);

        // The fallback of a reference is parsed recursively, which stops (keeping the
        // fallback as text) at the depth of nested values the parsers allow.
        private static CssUnparsedValue FromText(String text, Int32 depth)
        {
            var segments = new List<Object>();
            var start = 0;
            var i = 0;

            while (i < text.Length)
            {
                var c = text[i];

                if (c is Symbols.DoubleQuote or Symbols.SingleQuote)
                {
                    i = SkipString(text, i);
                }
                else if (c == Symbols.ReverseSolidus)
                {
                    i = Math.Min(i + 2, text.Length);
                }
                else if (c == Symbols.Solidus && i + 1 < text.Length && text[i + 1] == Symbols.Asterisk)
                {
                    var end = text.IndexOf("*/", i + 2, StringComparison.Ordinal);
                    i = end < 0 ? text.Length : end + 2;
                }
                else if (IsVarFunction(text, i) && TryParseReference(text, i, depth, out var reference, out var next))
                {
                    if (i > start)
                    {
                        segments.Add(text.Substring(start, i - start));
                    }

                    segments.Add(reference!);
                    start = i = next;
                }
                else
                {
                    i++;
                }
            }

            if (start < text.Length)
            {
                segments.Add(text.Substring(start));
            }

            return new CssUnparsedValue(segments);
        }

        private static Boolean IsVarFunction(String text, Int32 index)
        {
            if (index + 4 > text.Length || String.Compare(text, index, "var(", 0, 4, StringComparison.OrdinalIgnoreCase) != 0)
            {
                return false;
            }

            // must not be the end of another name, e.g., somevar(
            if (index > 0)
            {
                var previous = text[index - 1];

                if (previous.IsName() || previous == Symbols.ReverseSolidus)
                {
                    return false;
                }
            }

            return true;
        }

        private static Boolean TryParseReference(String text, Int32 index, Int32 depth, out CssVariableReferenceValue? reference, out Int32 next)
        {
            var open = index + 4;
            var close = FindClosingParenthesis(text, open);
            reference = null;
            next = close + 1;

            if (close < 0)
            {
                return false;
            }

            var comma = FindTopLevelComma(text, open, close);
            var name = text.Substring(open, (comma < 0 ? close : comma) - open).Trim();

            if (!name.StartsWith("--", StringComparison.Ordinal) || name.Length < 3)
            {
                return false;
            }

            var fallback = default(CssUnparsedValue);

            if (comma >= 0)
            {
                var fallbackText = text.Substring(comma + 1, close - comma - 1);
                fallback = depth + 1 < ValueNesting.Limit ?
                    FromText(fallbackText, depth + 1) :
                    new CssUnparsedValue(new List<Object> { fallbackText });
            }

            reference = new CssVariableReferenceValue(name, fallback);
            return true;
        }

        private static Int32 FindClosingParenthesis(String text, Int32 start)
        {
            var level = 0;
            var i = start;

            while (i < text.Length)
            {
                var c = text[i];

                if (c is Symbols.DoubleQuote or Symbols.SingleQuote)
                {
                    i = SkipString(text, i);
                    continue;
                }
                else if (c == Symbols.ReverseSolidus)
                {
                    i += 2;
                    continue;
                }
                else if (c == Symbols.RoundBracketOpen)
                {
                    level++;
                }
                else if (c == Symbols.RoundBracketClose)
                {
                    if (level == 0)
                    {
                        return i;
                    }

                    level--;
                }

                i++;
            }

            return -1;
        }

        private static Int32 FindTopLevelComma(String text, Int32 start, Int32 end)
        {
            var level = 0;
            var i = start;

            while (i < end)
            {
                var c = text[i];

                if (c is Symbols.DoubleQuote or Symbols.SingleQuote)
                {
                    i = SkipString(text, i);
                    continue;
                }
                else if (c == Symbols.ReverseSolidus)
                {
                    i += 2;
                    continue;
                }
                else if (c == Symbols.RoundBracketOpen)
                {
                    level++;
                }
                else if (c == Symbols.RoundBracketClose)
                {
                    level--;
                }
                else if (c == Symbols.Comma && level == 0)
                {
                    return i;
                }

                i++;
            }

            return -1;
        }

        private static Int32 SkipString(String text, Int32 index)
        {
            var quote = text[index];
            var i = index + 1;

            while (i < text.Length)
            {
                var c = text[i];

                if (c == Symbols.ReverseSolidus)
                {
                    i += 2;
                }
                else if (c == quote)
                {
                    return i + 1;
                }
                else
                {
                    i++;
                }
            }

            return text.Length;
        }

        private static Object Validate(Object segment) => segment switch
        {
            String text => text,
            CssVariableReferenceValue reference => reference,
            null => throw new ArgumentNullException(nameof(segment)),
            _ => throw new ArgumentException("A segment has to be a string or a variable reference.", nameof(segment)),
        };

        #endregion
    }
}
