namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class RotateParser
    {
        private static readonly Dictionary<String, Func<StringSource, RotateTransform>> RotateFunctions = new Dictionary<String, Func<StringSource, RotateTransform>>
        {
            { FunctionNames.Rotate, ParseRotate2d },
            { FunctionNames.Rotate3d, ParseRotate3d },
            { FunctionNames.RotateX, ParseRotateX },
            { FunctionNames.RotateY, ParseRotateY },
            { FunctionNames.RotateZ, ParseRotateZ },
        };

        public static RotateTransform Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParseRotate();
            return source.IsDone ? result : null;
        }

        public static RotateTransform ParseRotate(this StringSource source)
        {
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident != null)
            {
                if (source.Current == Symbols.RoundBracketOpen)
                {
                    var function = default(Func<StringSource, RotateTransform>);

                    if (RotateFunctions.TryGetValue(ident, out function))
                    {
                        source.SkipCurrentAndSpaces();
                        return function.Invoke(source);
                    }
                }
            }

            source.BackTo(pos);
            return null;
        }

        private static RotateTransform ParseRotate2d(StringSource source)
        {
            return ParseRotate(source, Single.NaN, Single.NaN, Single.NaN);
        }

        private static RotateTransform ParseRotate3d(StringSource source)
        {
            var x = source.ParseNumber();
            var c1 = source.SkipGetSkip();
            var y = source.ParseNumber();
            var c2 = source.SkipGetSkip();
            var z = source.ParseNumber();
            var c3 = source.SkipGetSkip();

            if (x.HasValue && y.HasValue && z.HasValue && c1 == c2 && c1 == c3 && c1 == Symbols.Comma)
            {
                return ParseRotate(source, x.Value, y.Value, z.Value);
            }

            return null;
        }

        private static RotateTransform ParseRotateX(StringSource source)
        {
            return ParseRotate(source, 1f, 0f, 0f);
        }

        private static RotateTransform ParseRotateY(StringSource source)
        {
            return ParseRotate(source, 0f, 1f, 0f);
        }

        private static RotateTransform ParseRotateZ(StringSource source)
        {
            return ParseRotate(source, 0f, 0f, 1f);
        }

        private static RotateTransform ParseRotate(StringSource source, Single x, Single y, Single z)
        {
            var angle = source.ParseAngle();
            var f = source.SkipGetSkip();

            if (angle.HasValue && f == Symbols.RoundBracketClose)
            {
                return new RotateTransform(x, z, y, angle.Value);
            }

            return null;
        }
    }
}
