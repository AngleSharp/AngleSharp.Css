namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class TimingFunctionParser
    {
        private static readonly Dictionary<String, Func<StringSource, ICssTimingFunctionValue>> TimingFunctions = new Dictionary<String, Func<StringSource, ICssTimingFunctionValue>>
        {
            { FunctionNames.Steps, ParseSteps },
            { FunctionNames.CubicBezier, ParseCubicBezier },
        };

        public static ICssTimingFunctionValue ParseTimingFunction(this StringSource source)
        {
            var pos = source.Index;
            var result = default(ICssTimingFunctionValue);
            var ident = source.ParseIdent();

            if (ident != null)
            {
                if (source.Current == Symbols.RoundBracketOpen)
                {
                    var function = default(Func<StringSource, ICssTimingFunctionValue>);

                    if (TimingFunctions.TryGetValue(ident, out function))
                    {
                        source.SkipCurrentAndSpaces();
                        result = function.Invoke(source);
                    }
                }
                else
                {
                    Map.TimingFunctions.TryGetValue(ident, out result);
                }
            }

            if (result == null)
            {
                source.BackTo(pos);
            }

            return result;
        }

        private static ICssTimingFunctionValue ParseSteps(StringSource source)
        {
            var intervals = source.ParseInteger();
            var c = source.SkipGetSkip();

            if (intervals.HasValue)
            {
                var start = false;
                var end = true;

                if (c == Symbols.Comma)
                {
                    start = source.IsIdentifier(CssKeywords.Start);
                    end = source.IsIdentifier(CssKeywords.End);
                    c = source.SkipGetSkip();
                }

                if (start != end && c == Symbols.RoundBracketClose)
                {
                    return new CssStepsValue(intervals.Value, start);
                }
            }
            
            return null;
        }

        private static ICssTimingFunctionValue ParseCubicBezier(StringSource source)
        {
            var p1 = source.ParseNumber();
            var c1 = source.SkipGetSkip();
            var p2 = source.ParseNumber();
            var c2 = source.SkipGetSkip();
            var p3 = source.ParseNumber();
            var c3 = source.SkipGetSkip();
            var p4 = source.ParseNumber();
            var f = source.SkipGetSkip();

            if (p1.HasValue && p2.HasValue && p3.HasValue && p4.HasValue && c1 == c2 && c1 == c3 && c1 == Symbols.Comma && f == Symbols.RoundBracketClose)
            {
                return new CssCubicBezierValue(p1.Value, p2.Value, p3.Value, p4.Value);
            }
            
            return null;
        }
    }
}
