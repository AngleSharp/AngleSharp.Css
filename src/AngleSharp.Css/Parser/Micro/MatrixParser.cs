namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class MatrixParser
    {
        private static readonly Dictionary<String, Func<StringSource, MatrixTransform>> MatrixFunctions = new Dictionary<String, Func<StringSource, MatrixTransform>>
        {
            { FunctionNames.Matrix, ParseMatrix2d },
            { FunctionNames.Matrix3d, ParseMatrix3d }
        };

        public static MatrixTransform Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParseMatrix();
            return source.IsDone ? result : null;
        }

        public static MatrixTransform ParseMatrix(this StringSource source)
        {
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident != null)
            {
                if (source.Current == Symbols.RoundBracketOpen)
                {
                    var function = default(Func<StringSource, MatrixTransform>);

                    if (MatrixFunctions.TryGetValue(ident, out function))
                    {
                        source.SkipCurrentAndSpaces();
                        return function.Invoke(source);
                    }
                }
            }

            source.BackTo(pos);
            return null;
        }

        private static MatrixTransform ParseMatrix2d(StringSource source)
        {
            return ParseMatrix(source, 6);
        }

        private static MatrixTransform ParseMatrix3d(StringSource source)
        {
            return ParseMatrix(source, 16);
        }

        private static MatrixTransform ParseMatrix(StringSource source, Int32 count)
        {
            var numbers = new Single[count];
            var num = source.ParseNumber();

            if (num.HasValue)
            {
                numbers[0] = num.Value;
                var index = 1;

                while (index < numbers.Length)
                {
                    var c = source.SkipGetSkip();
                    num = source.ParseNumber();

                    if (c != Symbols.Comma || !num.HasValue)
                        break;

                    numbers[index++] = num.Value;
                }

                var f = source.SkipGetSkip();

                if (index == count && f == Symbols.RoundBracketClose)
                {
                    return new MatrixTransform(numbers);
                }
            }

            return null;
        }
    }
}
