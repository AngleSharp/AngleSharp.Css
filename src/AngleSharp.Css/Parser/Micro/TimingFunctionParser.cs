#nullable disable
namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents extensions to for timing values.
    /// </summary>
    public static class TimingFunctionParser
    {
        private static readonly Dictionary<String, Func<StringSource, ICssTimingFunctionValue>> TimingFunctions = new(StringComparer.OrdinalIgnoreCase)
        {
            { FunctionNames.Steps, ParseSteps },
            { FunctionNames.CubicBezier, ParseCubicBezier },
            { CssKeywords.Linear, ParseLinear },
        };

        private static readonly HashSet<String> StepPositions = new(StringComparer.OrdinalIgnoreCase)
        {
            CssKeywords.Start,
            CssKeywords.End,
            CssKeywords.JumpStart,
            CssKeywords.JumpEnd,
            CssKeywords.JumpNone,
            CssKeywords.JumpBoth,
        };

        /// <summary>
        /// Parses the timing value.
        /// </summary>
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
                var position = default(String);

                if (c == Symbols.Comma)
                {
                    position = source.ParseIdent()?.ToLowerFast();
                    c = source.SkipGetSkip();

                    if (position is null || !StepPositions.Contains(position))
                    {
                        return null;
                    }
                }

                // jump-none requires at least two intervals
                if (c == Symbols.RoundBracketClose && (intervals.Value.IntValue > 1 || (intervals.Value.IntValue == 1 && !CssKeywords.JumpNone.Is(position))))
                {
                    return new CssStepsValue(intervals.Value, position);
                }
            }

            return null;
        }

        private static ICssTimingFunctionValue ParseLinear(StringSource source)
        {
            var stops = new List<CssTupleValue>();

            while (true)
            {
                var output = source.ParseNumber();

                if (!output.HasValue)
                {
                    return null;
                }

                var parts = new List<ICssValue> { output.Value };
                source.SkipSpacesAndComments();

                // Each stop has an optional input progress, given as one or two percentages.
                for (var i = 0; i < 2; i++)
                {
                    var input = source.ParsePercent();

                    if (!input.HasValue)
                    {
                        break;
                    }

                    parts.Add(new CssPercentageValue(input.Value * 100.0));
                    source.SkipSpacesAndComments();
                }

                stops.Add(new CssTupleValue(parts.ToArray()));
                var c = source.SkipGetSkip();

                if (c == Symbols.RoundBracketClose)
                {
                    return stops.Count > 1 ? new CssLinearEasingValue(stops.ToArray()) : null;
                }
                else if (c != Symbols.Comma)
                {
                    return null;
                }
            }
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
