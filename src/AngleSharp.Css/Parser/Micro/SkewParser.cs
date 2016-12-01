namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class SkewParser
    {
        private static readonly Dictionary<String, Func<StringSource, SkewTransform>> SkewFunctions = new Dictionary<String, Func<StringSource, SkewTransform>>
        {
            { FunctionNames.Skew, ParseSkew2d },
            { FunctionNames.SkewX, ParseSkewX },
            { FunctionNames.SkewY, ParseSkewY },
        };

        public static SkewTransform Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParseSkew();
            return source.IsDone ? result : null;
        }

        public static SkewTransform ParseSkew(this StringSource source)
        {
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident != null)
            {
                if (source.Current == Symbols.RoundBracketOpen)
                {
                    var function = default(Func<StringSource, SkewTransform>);

                    if (SkewFunctions.TryGetValue(ident, out function))
                    {
                        source.SkipCurrentAndSpaces();
                        return function.Invoke(source);
                    }
                }
            }

            source.BackTo(pos);
            return null;
        }

        private static SkewTransform ParseSkew2d(StringSource source)
        {
            var x = source.ParseAngle();
            var c = source.SkipGetSkip();
            var y = source.ParseAngle();
            var f = source.SkipGetSkip();

            if (x.HasValue && y.HasValue && c == Symbols.Comma && f == Symbols.RoundBracketClose)
            {
                return new SkewTransform(x.Value, y.Value);
            }

            return null;
        }

        private static SkewTransform ParseSkewX(StringSource source)
        {
            var x = source.ParseAngle();
            var f = source.SkipGetSkip();

            if (x.HasValue && f == Symbols.RoundBracketClose)
            {
                return new SkewTransform(x.Value, Angle.Zero);
            }

            return null;
        }

        private static SkewTransform ParseSkewY(StringSource source)
        {
            var y = source.ParseAngle();
            var f = source.SkipGetSkip();

            if (y.HasValue && f == Symbols.RoundBracketClose)
            {
                return new SkewTransform(Angle.Zero, y.Value);
            }

            return null;
        }
    }
}
