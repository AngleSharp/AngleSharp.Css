namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class TransformParser
    {
        private static readonly Dictionary<String, Func<StringSource, ICssTransformFunctionValue>> TransformFunctions = new Dictionary<String, Func<StringSource, ICssTransformFunctionValue>>
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

        public static ICssTransformFunctionValue ParseTransform(this StringSource source)
        {
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident != null)
            {
                if (source.Current == Symbols.RoundBracketOpen)
                {
                    var function = default(Func<StringSource, ICssTransformFunctionValue>);

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
        private static CssSkewValue ParseSkew2d(StringSource source)
        {
            var x = source.ParseAngleOrCalc();
            var c = source.SkipGetSkip();
            var y = source.ParseAngleOrCalc();
            var f = source.SkipGetSkip();

            if (x != null && y != null && c == Symbols.Comma && f == Symbols.RoundBracketClose)
            {
                return new CssSkewValue(x, y);
            }

            return null;
        }

        /// <summary>
        /// A broad variety of skew transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-skew
        /// </summary>
        private static CssSkewValue ParseSkewX(StringSource source)
        {
            var x = source.ParseAngleOrCalc();
            var f = source.SkipGetSkip();

            if (x != null && f == Symbols.RoundBracketClose)
            {
                return new CssSkewValue(x, null);
            }

            return null;
        }

        /// <summary>
        /// A broad variety of skew transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-skew
        /// </summary>
        private static CssSkewValue ParseSkewY(StringSource source)
        {
            var y = source.ParseAngleOrCalc();
            var f = source.SkipGetSkip();

            if (y != null && f == Symbols.RoundBracketClose)
            {
                return new CssSkewValue(null, y);
            }

            return null;
        }

        /// <summary>
        /// Sets the transformation matrix explicitly.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-matrix3d
        /// </summary>
        private static CssMatrixValue ParseMatrix2d(StringSource source)
        {
            return ParseMatrix(source, 6);
        }

        /// <summary>
        /// Sets the transformation matrix explicitly.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-matrix3d
        /// </summary>
        private static CssMatrixValue ParseMatrix3d(StringSource source)
        {
            return ParseMatrix(source, 16);
        }

        /// <summary>
        /// Sets the transformation matrix explicitly.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-matrix3d
        /// </summary>
        private static CssMatrixValue ParseMatrix(StringSource source, Int32 count)
        {
            var numbers = new Double[count];
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
                    return new CssMatrixValue(numbers);
                }
            }

            return null;
        }

        /// <summary>
        /// A broad variety of rotate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-rotate3d
        /// </summary>
        private static CssRotateValue ParseRotate2d(StringSource source)
        {
            return ParseRotate(source, Double.NaN, Double.NaN, Double.NaN);
        }

        /// <summary>
        /// A broad variety of rotate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-rotate3d
        /// </summary>
        private static CssRotateValue ParseRotate3d(StringSource source)
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
        private static CssRotateValue ParseRotateX(StringSource source)
        {
            return ParseRotate(source, 1f, 0f, 0f);
        }

        /// <summary>
        /// A broad variety of rotate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-rotate3d
        /// </summary>
        private static CssRotateValue ParseRotateY(StringSource source)
        {
            return ParseRotate(source, 0f, 1f, 0f);
        }

        /// <summary>
        /// A broad variety of rotate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-rotate3d
        /// </summary>
        private static CssRotateValue ParseRotateZ(StringSource source)
        {
            return ParseRotate(source, 0f, 0f, 1f);
        }

        /// <summary>
        /// A broad variety of rotate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-rotate3d
        /// </summary>
        private static CssRotateValue ParseRotate(StringSource source, Double x, Double y, Double z)
        {
            var angle = source.ParseAngleOrCalc();
            var f = source.SkipGetSkip();

            if (angle != null && f == Symbols.RoundBracketClose)
            {
                return new CssRotateValue(x, z, y, angle);
            }

            return null;
        }

        /// <summary>
        /// A broad variety of scale transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-scale3d
        /// </summary>
        private static CssScaleValue ParseScale2d(StringSource source)
        {
            var x = source.ParseNumber();
            var f = source.SkipGetSkip();

            if (x.HasValue)
            {
                var y = default(Double?);

                if (f == Symbols.Comma)
                {
                    y = source.ParseNumber();
                    f = source.SkipGetSkip();
                }

                if (f == Symbols.RoundBracketClose)
                {
                    return new CssScaleValue(x.Value, y ?? x.Value, 1.0);
                }
            }

            return null;
        }

        /// <summary>
        /// A broad variety of scale transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-scale3d
        /// </summary>
        private static CssScaleValue ParseScale3d(StringSource source)
        {
            var x = source.ParseNumber();
            var f = source.SkipGetSkip();

            if (x.HasValue)
            {
                var y = default(Double?);
                var z = default(Double?);

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
                    return new CssScaleValue(x.Value, y ?? x.Value, z ?? x.Value);
                }
            }

            return null;
        }

        /// <summary>
        /// A broad variety of scale transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-scale3d
        /// </summary>
        private static CssScaleValue ParseScaleX(StringSource source)
        {
            var x = source.ParseNumber();
            var f = source.SkipGetSkip();

            if (x.HasValue && f == Symbols.RoundBracketClose)
            {
                return new CssScaleValue(x.Value, 1.0, 1.0);
            }

            return null;
        }

        /// <summary>
        /// A broad variety of scale transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-scale3d
        /// </summary>
        private static CssScaleValue ParseScaleY(StringSource source)
        {
            var y = source.ParseNumber();
            var f = source.SkipGetSkip();

            if (y.HasValue && f == Symbols.RoundBracketClose)
            {
                return new CssScaleValue(1.0, y.Value, 1.0);
            }

            return null;
        }

        /// <summary>
        /// A broad variety of scale transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-scale3d
        /// </summary>
        private static CssScaleValue ParseScaleZ(StringSource source)
        {
            var z = source.ParseNumber();
            var f = source.SkipGetSkip();

            if (z.HasValue && f == Symbols.RoundBracketClose)
            {
                return new CssScaleValue(1.0, 1.0, z.Value);
            }

            return null;
        }

        /// <summary>
        /// A broad variety of translate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-translate3d
        /// </summary>
        private static CssTranslateValue ParseTranslate2d(StringSource source)
        {
            var x = source.ParseDistanceOrCalc();
            var f = source.SkipGetSkip();

            if (x != null)
            {
                var y = default(ICssValue);

                if (f == Symbols.Comma)
                {
                    y = source.ParseDistanceOrCalc();
                    f = source.SkipGetSkip();
                }

                if (f == Symbols.RoundBracketClose)
                {
                    return new CssTranslateValue(x, y, null);
                }
            }

            return null;
        }

        /// <summary>
        /// A broad variety of translate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-translate3d
        /// </summary>
        private static CssTranslateValue ParseTranslate3d(StringSource source)
        {
            var x = source.ParseDistanceOrCalc();
            var f = source.SkipGetSkip();

            if (x != null)
            {
                var y = default(ICssValue);
                var z = default(ICssValue);

                if (f == Symbols.Comma)
                {
                    y = source.ParseDistanceOrCalc();
                    f = source.SkipGetSkip();

                    if (y == null)
                    {
                        return null;
                    }

                    if (f == Symbols.Comma)
                    {
                        z = source.ParseDistanceOrCalc();
                        f = source.SkipGetSkip();
                    }
                }

                if (f == Symbols.RoundBracketClose)
                {
                    return new CssTranslateValue(x, y, z);
                }
            }

            return null;
        }

        /// <summary>
        /// A broad variety of translate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-translate3d
        /// </summary>
        private static CssTranslateValue ParseTranslateX(StringSource source)
        {
            var x = source.ParseDistanceOrCalc();
            var f = source.SkipGetSkip();

            if (x != null && f == Symbols.RoundBracketClose)
            {
                return new CssTranslateValue(x, null, null);
            }

            return null;
        }

        /// <summary>
        /// A broad variety of translate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-translate3d
        /// </summary>
        private static CssTranslateValue ParseTranslateY(StringSource source)
        {
            var y = source.ParseDistanceOrCalc();
            var f = source.SkipGetSkip();

            if (y != null && f == Symbols.RoundBracketClose)
            {
                return new CssTranslateValue(null, y, null);
            }

            return null;
        }

        /// <summary>
        /// A broad variety of translate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-translate3d
        /// </summary>
        private static CssTranslateValue ParseTranslateZ(StringSource source)
        {
            var z = source.ParseDistanceOrCalc();
            var f = source.SkipGetSkip();

            if (z != null && f == Symbols.RoundBracketClose)
            {
                return new CssTranslateValue(null, null, z);
            }

            return null;
        }

        /// <summary>
        /// A perspective for 3D transformations.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-perspective
        /// </summary>
        private static ICssTransformFunctionValue ParsePerspective(StringSource source)
        {
            var l = source.ParseLengthOrCalc();
            var f = source.SkipGetSkip();

            if (l != null && f == Symbols.RoundBracketClose)
            {
                return new CssPerspectiveValue(l);
            }

            return null;
        }
    }
}
