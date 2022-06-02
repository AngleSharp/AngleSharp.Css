namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents extensions to for keyframe values.
    /// </summary>
    public static class KeyframeParser
    {
        internal static IKeyframeSelector Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParseKeyframeSelector();
            return source.IsDone ? result : null;
        }

        /// <summary>
        /// Parses the keyframe selector.
        /// </summary>
        public static IKeyframeSelector ParseKeyframeSelector(this StringSource source)
        {
            var stops = new List<Double>();
            var current = source.SkipSpacesAndComments();

            while (current != Symbols.EndOfFile)
            {
                var start = source.Index;
                var id = source.ParseIdent();

                if (id == null)
                {
                    source.BackTo(start);
                    var test = source.ParsePercent();

                    if (!test.HasValue)
                    {
                        return null;
                    }

                    stops.Add(test.Value);
                }
                else if (id.Is(CssKeywords.From))
                {
                    stops.Add(0f);
                }
                else if (id.Is(CssKeywords.To))
                {
                    stops.Add(1f);
                }
                else
                {
                    return null;
                }

                current = source.SkipSpacesAndComments();

                if (current != Symbols.Comma)
                {
                    break;
                }

                current = source.SkipCurrentAndSpaces();
            }

            return new KeyframeSelector(stops);
        }
    }
}
