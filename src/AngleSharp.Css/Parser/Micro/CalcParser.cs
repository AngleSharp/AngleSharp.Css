namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents extensions to for calc values.
    /// </summary>
    public static class CalcParser
    {
        /// <summary>
        /// Parses a calc value, if any.
        /// </summary>
        public static CssCalcValue ParseCalc(this StringSource source)
        {
            var pos = source.Index;

            if (source.IsFunction(FunctionNames.Calc))
            {
                var expression = source.ParseExpression();

                if (expression != null)
                {
                    return new CssCalcValue(expression);
                }
            }

            source.BackTo(pos);
            return null;
        }

        private static ICssValue ParseExpression(this StringSource source)
        {
            var expression = source.ParseAddExpression();

            if (expression != null && source.Current == Symbols.RoundBracketClose)
            {
                source.SkipCurrentAndSpaces();
                return expression;
            }

            return null;
        }

        private static ICssValue ParseAddExpression(this StringSource source)
        {
            var left = ParseSubExpression(source);

            if (source.Current == Symbols.Plus)
            {
                source.SkipCurrentAndSpaces();
                var right = ParseAddExpression(source);

                if (right == null)
                {
                    return null;
                }

                return new CssCalcAddExpression(left, right);
            }

            return left;
        }

        private static ICssValue ParseSubExpression(this StringSource source)
        {
            var left = ParseMulExpression(source);

            if (source.Current == Symbols.Minus)
            {
                source.SkipCurrentAndSpaces();
                var right = ParseSubExpression(source);

                if (right == null)
                {
                    return null;
                }

                return new CssCalcSubExpression(left, right);
            }

            return left;
        }

        private static ICssValue ParseMulExpression(this StringSource source)
        {
            var left = ParseDivExpression(source);

            if (source.Current == Symbols.Asterisk)
            {
                source.SkipCurrentAndSpaces();
                var right = ParseMulExpression(source);

                if (right == null)
                {
                    return null;
                }

                return new CssCalcMulExpression(left, right);
            }

            return left;
        }

        private static ICssValue ParseDivExpression(this StringSource source)
        {
            var left = ParseBracketExpression(source);

            if (source.Current == Symbols.Solidus)
            {
                source.SkipCurrentAndSpaces();
                var right = ParseDivExpression(source);

                if (right == null)
                {
                    return null;
                }

                return new CssCalcDivExpression(left, right);
            }

            return left;
        }

        private static ICssValue ParseBracketExpression(this StringSource source)
        {
            if (source.Current == Symbols.RoundBracketOpen)
            {
                source.SkipCurrentAndSpaces();
                var content = source.ParseExpression();
                return new CssCalcBracketExpression(content);
            }
            else if (source.IsFunction(FunctionNames.Calc))
            {
                var content = source.ParseExpression();
                return new CssCalcBracketExpression(content);
            }

            return source.ParseAtomicExpression();
        }
        
        /// <summary>
        /// Parses a unit value into a ICssValue.
        /// </summary>
        public static ICssValue ParseAtomicExpression(this StringSource source)
        {
            var unit = source.ParseUnit();

            if (unit != null && Double.TryParse(unit.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                if (String.IsNullOrEmpty(unit.Dimension))
                {
                    source.SkipSpacesAndComments();
                    return new CssLengthValue(result, CssLengthValue.Unit.None);
                }
                else if (CssLengthValue.GetUnit(unit.Dimension) != CssLengthValue.Unit.None)
                {
                    source.SkipSpacesAndComments();
                    return new CssLengthValue(result, CssLengthValue.GetUnit(unit.Dimension));
                }
                else if (CssTimeValue.GetUnit(unit.Dimension) != CssTimeValue.Unit.None)
                {
                    source.SkipSpacesAndComments();
                    return new CssTimeValue(result, CssTimeValue.GetUnit(unit.Dimension));
                }
                else if (Angle.GetUnit(unit.Dimension) != Angle.Unit.None)
                {
                    source.SkipSpacesAndComments();
                    return new Angle(result, Angle.GetUnit(unit.Dimension));
                }
                else if (CssFrequencyValue.GetUnit(unit.Dimension) != CssFrequencyValue.Unit.None)
                {
                    source.SkipSpacesAndComments();
                    return new CssFrequencyValue(result, CssFrequencyValue.GetUnit(unit.Dimension));
                }
                else if (CssResolutionValue.GetUnit(unit.Dimension) != CssResolutionValue.Unit.None)
                {
                    source.SkipSpacesAndComments();
                    return new CssResolutionValue(result, CssResolutionValue.GetUnit(unit.Dimension));
                }
            }

            return null;
        }
    }
}
