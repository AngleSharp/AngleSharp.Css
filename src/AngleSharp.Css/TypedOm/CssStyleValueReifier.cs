namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MathFunction = AngleSharp.Css.Values.CssMathValue;

    /// <summary>
    /// Turns the values of the CSSOM into values of the Typed OM.
    /// https://www.w3.org/TR/css-typed-om-1/#reify-stylevalue
    /// </summary>
    static class CssStyleValueReifier
    {
        #region Values

        /// <summary>
        /// Reifies the value of the given property, which yields one item per entry
        /// of a comma separated list, e.g., of background-image.
        /// </summary>
        public static IReadOnlyList<CssStyleValue> ReifyAll(String property, ICssValue? value)
        {
            if (value is null)
            {
                return Array.Empty<CssStyleValue>();
            }

            if (!TypedOmProperties.IsCustom(property) && value is CssListValue<ICssValue> list)
            {
                return list.Items.Select(item => Reify(property, item)).ToArray();
            }

            return new[] { Reify(property, value) };
        }

        /// <summary>
        /// Reifies a single value of the given property.
        /// </summary>
        public static CssStyleValue Reify(String property, ICssValue value)
        {
            if (TypedOmProperties.IsCustom(property))
            {
                return CssUnparsedValue.FromText(value.CssText);
            }

            switch (value)
            {
                case CssReferenceValue:
                case CssAnyValue:
                    return CssUnparsedValue.FromText(value.CssText);
                case ICssSpecialValue:
                    return new CssKeywordValue(value.CssText);
                case CssPoint2D point:
                    return ReifyPosition(point) ?? Unsupported(property, value);
                case ICssImageValue:
                    return new CssImageValue(value.CssText);
                case ICssTransformFunctionValue transform:
                    return ReifyTransform(new ICssValue[] { transform }) ?? Unsupported(property, value);
                case CssTupleValue tuple when tuple.Count > 0 && tuple.Items.All(m => m is ICssTransformFunctionValue):
                    return ReifyTransform(tuple.Items) ?? Unsupported(property, value);
            }

            var numeric = ReifyNumeric(value);

            if (numeric is not null)
            {
                return numeric;
            }

            if (value is ICssPrimitiveValue && IsIdentifier(value.CssText))
            {
                return new CssKeywordValue(value.CssText);
            }

            return Unsupported(property, value);
        }

        private static CssStyleValue Unsupported(String property, ICssValue value) =>
            new CssUnsupportedValue(property, value.CssText);

        private static Boolean IsIdentifier(String text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return false;
            }

            var start = text[0] == Symbols.Minus ? 1 : 0;

            if (start == text.Length || !(text[start].IsNameStart() || (start == 1 && text[1] == Symbols.Minus)))
            {
                return false;
            }

            for (var i = start + 1; i < text.Length; i++)
            {
                if (!text[i].IsName())
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Numeric Values

        /// <summary>
        /// Reifies a numeric value, i.e., a number, a dimension, or a math function. Returns null
        /// for values that are not numeric.
        /// </summary>
        public static CssNumericValue? ReifyNumeric(ICssValue value)
        {
            try
            {
                return ReifyNumericCore(value);
            }
            catch (ArgumentException)
            {
                // e.g., an expression with incompatible types, which the parser accepted
                return null;
            }
        }

        private static CssNumericValue? ReifyNumericCore(ICssValue? value)
        {
            switch (value)
            {
                case null:
                    return null;
                case CssLengthValue length:
                    if (Double.IsNaN(length.Value))
                    {
                        // a keyword, e.g., auto
                        return null;
                    }

                    return ReifyUnit(length.Value, length.Type == CssLengthValue.Unit.None ? String.Empty : length.UnitString);
                case ICssMetricValue metric:
                    return Double.IsNaN(metric.Value) ? null : ReifyUnit(metric.Value, metric.UnitString);
                case CssCalcValue calc:
                    {
                        // calc() is always a math value, even around a single value
                        var expression = ReifyExpression(calc.Expression);
                        return expression is null or CssMathValue ? expression : new CssMathSum(expression);
                    }
                case MathFunction math:
                    return ReifyMathFunction(math);
                default:
                    return null;
            }
        }

        private static CssUnitValue? ReifyUnit(Double value, String unitString)
        {
            var unit = unitString.Length == 0 ? CssUnits.Number : CssUnits.Normalize(unitString);
            return CssUnits.IsKnown(unit) ? new CssUnitValue(value, unit) : null;
        }

        private static CssNumericValue? ReifyExpression(ICssValue value)
        {
            switch (value)
            {
                case CssCalcAddExpression:
                case CssCalcSubExpression:
                    {
                        var items = new List<CssNumericValue>();
                        return CollectSum(value, items) ? new CssMathSum(items.ToArray()) : null;
                    }
                case CssCalcMulExpression:
                case CssCalcDivExpression:
                    {
                        var items = new List<CssNumericValue>();
                        return CollectProduct(value, items) ? new CssMathProduct(items.ToArray()) : null;
                    }
                case CssCalcBracketExpression bracket:
                    return ReifyExpression(bracket.Value);
                default:
                    return ReifyNumericCore(value);
            }
        }

        // a + b - c is a single sum, but a bracket, e.g., a + (b + c), stays a nested sum
        private static Boolean CollectSum(ICssValue value, List<CssNumericValue> items)
        {
            if (value is CssCalcAddExpression add)
            {
                return CollectSum(add.Left, items) && Add(items, ReifyExpression(add.Right));
            }
            else if (value is CssCalcSubExpression sub)
            {
                var right = ReifyExpression(sub.Right);
                return CollectSum(sub.Left, items) && Add(items, right is null ? null : new CssMathNegate(right));
            }

            return Add(items, ReifyExpression(value));
        }

        private static Boolean CollectProduct(ICssValue value, List<CssNumericValue> items)
        {
            if (value is CssCalcMulExpression mul)
            {
                return CollectProduct(mul.Left, items) && Add(items, ReifyExpression(mul.Right));
            }
            else if (value is CssCalcDivExpression div)
            {
                var right = ReifyExpression(div.Right);
                return CollectProduct(div.Left, items) && Add(items, right is null ? null : new CssMathInvert(right));
            }

            return Add(items, ReifyExpression(value));
        }

        private static Boolean Add(List<CssNumericValue> items, CssNumericValue? item)
        {
            if (item is null)
            {
                return false;
            }

            items.Add(item);
            return true;
        }

        private static CssNumericValue? ReifyMathFunction(MathFunction math)
        {
            var arguments = new CssNumericValue[math.Arguments.Length];

            for (var i = 0; i < arguments.Length; i++)
            {
                var argument = ReifyExpression(math.Arguments[i]);

                if (argument is null)
                {
                    return null;
                }

                arguments[i] = argument;
            }

            if (math.Name.Isi(FunctionNames.Min))
            {
                return new CssMathMin(arguments);
            }
            else if (math.Name.Isi(FunctionNames.Max))
            {
                return new CssMathMax(arguments);
            }
            else if (math.Name.Isi(FunctionNames.Clamp) && arguments.Length == 3)
            {
                return new CssMathClamp(arguments[0], arguments[1], arguments[2]);
            }

            return null;
        }

        #endregion

        #region Positions

        private static CssPositionValue? ReifyPosition(CssPoint2D point)
        {
            var x = ReifyNumeric(point.X);
            var y = ReifyNumeric(point.Y);

            if (x is not null && y is not null && x.NumericType.MatchesLengthPercentage() && y.NumericType.MatchesLengthPercentage())
            {
                return new CssPositionValue(x, y);
            }

            return null;
        }

        #endregion

        #region Transforms

        private static CssTransformValue? ReifyTransform(IEnumerable<ICssValue> functions)
        {
            var components = new List<CssTransformComponent>();

            try
            {
                foreach (var function in functions)
                {
                    var component = ReifyTransformFunction(function);

                    if (component is null)
                    {
                        return null;
                    }

                    components.Add(component);
                }
            }
            catch (ArgumentException)
            {
                // e.g., a translation by an angle, which the typed component rejects
                return null;
            }

            return new CssTransformValue(components);
        }

        private static CssTransformComponent? ReifyTransformFunction(ICssValue function)
        {
            switch (function)
            {
                case CssTranslateValue translate:
                    {
                        var x = ReifyOptional(translate.ShiftX, Px(0.0));
                        var y = ReifyOptional(translate.ShiftY, Px(0.0));
                        var z = translate.ShiftZ is null ? null : ReifyNumeric(translate.ShiftZ);

                        if (x is null || y is null || (translate.ShiftZ is not null && z is null))
                        {
                            return null;
                        }

                        return new CssTranslate(x, y, z);
                    }
                case CssRotateValue rotate:
                    {
                        var angle = ReifyNumeric(rotate.Angle);

                        if (angle is null)
                        {
                            return null;
                        }

                        if (rotate.X is null && rotate.Y is null && rotate.Z is null)
                        {
                            return new CssRotate(angle);
                        }

                        var x = ReifyOptional(rotate.X, 0.0);
                        var y = ReifyOptional(rotate.Y, 0.0);
                        var z = ReifyOptional(rotate.Z, 0.0);
                        return x is null || y is null || z is null ? null : new CssRotate(x, y, z, angle);
                    }
                case CssScaleValue scale:
                    {
                        var x = ReifyOptional(scale.ScaleX, 1.0);
                        var y = ReifyOptional(scale.ScaleY, 1.0);
                        var z = scale.ScaleZ is null ? null : ReifyNumeric(scale.ScaleZ);

                        if (x is null || y is null || (scale.ScaleZ is not null && z is null))
                        {
                            return null;
                        }

                        return new CssScale(x, y, z);
                    }
                case CssSkewValue skew:
                    {
                        var name = skew.Name;

                        if (name.Is(FunctionNames.SkewX))
                        {
                            var ax = ReifyNumeric(skew.Alpha);
                            return ax is null ? null : new CssSkewX(ax);
                        }
                        else if (name.Is(FunctionNames.SkewY))
                        {
                            var ay = ReifyNumeric(skew.Beta);
                            return ay is null ? null : new CssSkewY(ay);
                        }
                        else
                        {
                            var ax = ReifyOptional(skew.Alpha, Deg(0.0));
                            var ay = ReifyOptional(skew.Beta, Deg(0.0));
                            return ax is null || ay is null ? null : new CssSkew(ax, ay);
                        }
                    }
                case CssPerspectiveValue perspective:
                    {
                        var length = ReifyNumeric(perspective.Distance);
                        return length is null ? null : new CssPerspective(length);
                    }
                case CssMatrixValue matrix:
                    return new CssMatrixComponent(matrix.ComputeMatrix(null!), matrix.Arguments.Length == 6);
                default:
                    return null;
            }
        }

        private static CssNumericValue? ReifyOptional(ICssValue? value, CssNumericValue fallback) =>
            value is null ? fallback : ReifyNumeric(value);

        private static CssNumericValue Px(Double value) => new CssUnitValue(value, "px");

        private static CssNumericValue Deg(Double value) => new CssUnitValue(value, "deg");

        #endregion
    }
}
