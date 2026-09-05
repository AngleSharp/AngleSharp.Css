#nullable disable
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
            var left = ParseMulExpression(source);

            while (left != null && (source.Current == Symbols.Plus || source.Current == Symbols.Minus))
            {
                var add = source.Current == Symbols.Plus;
                source.SkipCurrentAndSpaces();
                var right = ParseMulExpression(source);

                if (right == null)
                {
                    return null;
                }

                left = add ?
                    new CssCalcAddExpression(left, right) :
                    (ICssValue)new CssCalcSubExpression(left, right);
            }

            return left;
        }

        private static ICssValue ParseMulExpression(this StringSource source)
        {
            var left = ParseBracketExpression(source);

            while (left != null && (source.Current == Symbols.Asterisk || source.Current == Symbols.Solidus))
            {
                var mul = source.Current == Symbols.Asterisk;
                source.SkipCurrentAndSpaces();
                var right = ParseBracketExpression(source);

                if (right == null)
                {
                    return null;
                }

                left = mul ?
                    new CssCalcMulExpression(left, right) :
                    (ICssValue)new CssCalcDivExpression(left, right);
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
                else if (CssAngleValue.GetUnit(unit.Dimension) != CssAngleValue.Unit.None)
                {
                    source.SkipSpacesAndComments();
                    return new CssAngleValue(result, CssAngleValue.GetUnit(unit.Dimension));
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
