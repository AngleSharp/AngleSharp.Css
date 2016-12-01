namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class ScaleParser
    {
        private static readonly Dictionary<String, Func<StringSource, ScaleTransform>> ScaleFunctions = new Dictionary<String, Func<StringSource, ScaleTransform>>
        {
            { FunctionNames.Scale, ParseScale2d },
            { FunctionNames.Scale3d, ParseScale3d },
            { FunctionNames.ScaleX, ParseScaleX },
            { FunctionNames.ScaleY, ParseScaleY },
            { FunctionNames.ScaleZ, ParseScaleZ },
        };

        public static ScaleTransform Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParseScale();
            return source.IsDone ? result : null;
        }

        public static ScaleTransform ParseScale(this StringSource source)
        {
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident != null)
            {
                if (source.Current == Symbols.RoundBracketOpen)
                {
                    var function = default(Func<StringSource, ScaleTransform>);

                    if (ScaleFunctions.TryGetValue(ident, out function))
                    {
                        source.SkipCurrentAndSpaces();
                        return function.Invoke(source);
                    }
                }
            }

            source.BackTo(pos);
            return null;
        }

        private static ScaleTransform ParseScale2d(StringSource source)
        {
            var x = source.ParseNumber();
            var f = source.SkipGetSkip();

            if (x.HasValue)
            {
                var y = default(Single?);

                if (f == Symbols.Comma)
                {
                    y = source.ParseNumber();
                    f = source.SkipGetSkip();
                }

                if (f == Symbols.RoundBracketClose)
                {
                    return new ScaleTransform(x.Value, y ?? x.Value, 1f);
                }
            }

            return null;
        }

        private static ScaleTransform ParseScale3d(StringSource source)
        {
            var x = source.ParseNumber();
            var f = source.SkipGetSkip();

            if (x.HasValue)
            {
                var y = default(Single?);
                var z = default(Single?);

                if (f == Symbols.Comma)
                {
                    y = source.ParseNumber();
                    f = source.SkipGetSkip();

                    if (!y.HasValue)
                    {
                        return null;
                    }

                    if (f == Symbols.Comma)
                    {
                        z = source.ParseNumber();
                        f = source.SkipGetSkip();
                    }
                }

                if (f == Symbols.RoundBracketClose)
                {
                    return new ScaleTransform(x.Value, y ?? x.Value, z ?? x.Value);
                }
            }

            return null;
        }

        private static ScaleTransform ParseScaleX(StringSource source)
        {
            var x = source.ParseNumber();
            var f = source.SkipGetSkip();

            if (x.HasValue && f == Symbols.RoundBracketClose)
            {
                return new ScaleTransform(x.Value, 1f, 1f);
            }

            return null;
        }

        private static ScaleTransform ParseScaleY(StringSource source)
        {
            var y = source.ParseNumber();
            var f = source.SkipGetSkip();

            if (y.HasValue && f == Symbols.RoundBracketClose)
            {
                return new ScaleTransform(1f, y.Value, 1f);
            }

            return null;
        }

        private static ScaleTransform ParseScaleZ(StringSource source)
        {
            var z = source.ParseNumber();
            var f = source.SkipGetSkip();

            if (z.HasValue && f == Symbols.RoundBracketClose)
            {
                return new ScaleTransform(1f, 1f, z.Value);
            }

            return null;
        }
    }
}
