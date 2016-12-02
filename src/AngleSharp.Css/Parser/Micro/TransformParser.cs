namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class TransformParser
    {
        private static readonly Dictionary<String, Func<StringSource, ITransform>> TransformFunctions = new Dictionary<String, Func<StringSource, ITransform>>
        {
            { FunctionNames.Skew, ParseSkew2d },
            { FunctionNames.SkewX, ParseSkewX },
            { FunctionNames.SkewY, ParseSkewY },
            { FunctionNames.Matrix, ParseMatrix2d },
            { FunctionNames.Matrix3d, ParseMatrix3d },
            { FunctionNames.Rotate, ParseRotate2d },
            { FunctionNames.Rotate3d, ParseRotate3d },
            { FunctionNames.RotateX, ParseRotateX },
            { FunctionNames.RotateY, ParseRotateY },
            { FunctionNames.RotateZ, ParseRotateZ },
            { FunctionNames.Scale, ParseScale2d },
            { FunctionNames.Scale3d, ParseScale3d },
            { FunctionNames.ScaleX, ParseScaleX },
            { FunctionNames.ScaleY, ParseScaleY },
            { FunctionNames.ScaleZ, ParseScaleZ },
            { FunctionNames.Translate, ParseTranslate2d },
            { FunctionNames.Translate3d, ParseTranslate3d },
            { FunctionNames.TranslateX, ParseTranslateX },
            { FunctionNames.TranslateY, ParseTranslateY },
            { FunctionNames.TranslateZ, ParseTranslateZ },
            { FunctionNames.Perspective, ParsePerspective },
        };

        public static ITransform Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParseTransform();
            return source.IsDone ? result : null;
        }

        public static ITransform ParseTransform(this StringSource source)
        {
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident != null)
            {
                if (source.Current == Symbols.RoundBracketOpen)
                {
                    var function = default(Func<StringSource, ITransform>);

                    if (TransformFunctions.TryGetValue(ident, out function))
                    {
                        source.SkipCurrentAndSpaces();
                        return function.Invoke(source);
                    }
                }
            }

            source.BackTo(pos);
            return null;
        }

        /// <summary>
        /// A broad variety of skew transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-skew
        /// </summary>
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

        /// <summary>
        /// A broad variety of skew transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-skew
        /// </summary>
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

        /// <summary>
        /// A broad variety of skew transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-skew
        /// </summary>
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

        /// <summary>
        /// Sets the transformation matrix explicitly.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-matrix3d
        /// </summary>
        private static MatrixTransform ParseMatrix2d(StringSource source)
        {
            return ParseMatrix(source, 6);
        }

        /// <summary>
        /// Sets the transformation matrix explicitly.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-matrix3d
        /// </summary>
        private static MatrixTransform ParseMatrix3d(StringSource source)
        {
            return ParseMatrix(source, 16);
        }

        /// <summary>
        /// Sets the transformation matrix explicitly.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-matrix3d
        /// </summary>
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

        /// <summary>
        /// A broad variety of rotate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-rotate3d
        /// </summary>
        private static RotateTransform ParseRotate2d(StringSource source)
        {
            return ParseRotate(source, Single.NaN, Single.NaN, Single.NaN);
        }

        /// <summary>
        /// A broad variety of rotate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-rotate3d
        /// </summary>
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

        /// <summary>
        /// A broad variety of rotate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-rotate3d
        /// </summary>
        private static RotateTransform ParseRotateX(StringSource source)
        {
            return ParseRotate(source, 1f, 0f, 0f);
        }

        /// <summary>
        /// A broad variety of rotate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-rotate3d
        /// </summary>
        private static RotateTransform ParseRotateY(StringSource source)
        {
            return ParseRotate(source, 0f, 1f, 0f);
        }

        /// <summary>
        /// A broad variety of rotate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-rotate3d
        /// </summary>
        private static RotateTransform ParseRotateZ(StringSource source)
        {
            return ParseRotate(source, 0f, 0f, 1f);
        }

        /// <summary>
        /// A broad variety of rotate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-rotate3d
        /// </summary>
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

        /// <summary>
        /// A broad variety of scale transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-scale3d
        /// </summary>
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

        /// <summary>
        /// A broad variety of scale transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-scale3d
        /// </summary>
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

        /// <summary>
        /// A broad variety of scale transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-scale3d
        /// </summary>
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

        /// <summary>
        /// A broad variety of scale transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-scale3d
        /// </summary>
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

        /// <summary>
        /// A broad variety of scale transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-scale3d
        /// </summary>
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

        /// <summary>
        /// A broad variety of translate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-translate3d
        /// </summary>
        private static TranslateTransform ParseTranslate2d(StringSource source)
        {
            var x = source.ParseDistance();
            var f = source.SkipGetSkip();

            if (x.HasValue)
            {
                var y = default(Length?);

                if (f == Symbols.Comma)
                {
                    y = source.ParseDistance();
                    f = source.SkipGetSkip();
                }

                if (f == Symbols.RoundBracketClose)
                {
                    return new TranslateTransform(x.Value, y ?? Length.Zero, Length.Zero);
                }
            }

            return null;
        }

        /// <summary>
        /// A broad variety of translate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-translate3d
        /// </summary>
        private static TranslateTransform ParseTranslate3d(StringSource source)
        {
            var x = source.ParseDistance();
            var f = source.SkipGetSkip();

            if (x.HasValue)
            {
                var y = default(Length?);
                var z = default(Length?);

                if (f == Symbols.Comma)
                {
                    y = source.ParseDistance();
                    f = source.SkipGetSkip();

                    if (!y.HasValue)
                    {
                        return null;
                    }

                    if (f == Symbols.Comma)
                    {
                        z = source.ParseDistance();
                        f = source.SkipGetSkip();
                    }
                }

                if (f == Symbols.RoundBracketClose)
                {
                    return new TranslateTransform(x.Value, y ?? Length.Zero, z ?? Length.Zero);
                }
            }

            return null;
        }

        /// <summary>
        /// A broad variety of translate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-translate3d
        /// </summary>
        private static TranslateTransform ParseTranslateX(StringSource source)
        {
            var x = source.ParseDistance();
            var f = source.SkipGetSkip();

            if (x.HasValue && f == Symbols.RoundBracketClose)
            {
                return new TranslateTransform(x.Value, Length.Zero, Length.Zero);
            }

            return null;
        }

        /// <summary>
        /// A broad variety of translate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-translate3d
        /// </summary>
        private static TranslateTransform ParseTranslateY(StringSource source)
        {
            var y = source.ParseDistance();
            var f = source.SkipGetSkip();

            if (y.HasValue && f == Symbols.RoundBracketClose)
            {
                return new TranslateTransform(Length.Zero, y.Value, Length.Zero);
            }

            return null;
        }

        /// <summary>
        /// A broad variety of translate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-translate3d
        /// </summary>
        private static TranslateTransform ParseTranslateZ(StringSource source)
        {
            var z = source.ParseDistance();
            var f = source.SkipGetSkip();

            if (z.HasValue && f == Symbols.RoundBracketClose)
            {
                return new TranslateTransform(Length.Zero, Length.Zero, z.Value);
            }

            return null;
        }

        /// <summary>
        /// A perspective for 3D transformations.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-perspective
        /// </summary>
        private static ITransform ParsePerspective(StringSource source)
        {
            var l = source.ParseLength();
            var f = source.SkipGetSkip();

            if (l.HasValue && f == Symbols.RoundBracketClose)
            {
                return new PerspectiveTransform(l.Value);
            }

            return null;
        }
    }
}
