namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Extensions;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class KeyframeParser
    {
        public static IKeyframeSelector Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParseKeyframeSelector();
            return source.IsDone ? result : null;
        }

        public static IKeyframeSelector ParseKeyframeSelector(this StringSource source)
        {
            var stops = new List<Percent>();
            var current = source.SkipSpacesAndComments();

            while (current != Symbols.EndOfFile)
            {
                var start = source.Index;
                var id = source.ParseIdent();

                if (id == null)
                {
                    source.BackTo(start);
                    var pc = source.ToPercent();

                    if (!pc.HasValue)
                    {
                        return null;
                    }

                    stops.Add(pc.Value);
                }
                else if (id.Is(CssKeywords.From))
                {
                    stops.Add(Percent.Zero);
                }
                else if (id.Is(CssKeywords.To))
                {
                    stops.Add(Percent.Hundred);
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
