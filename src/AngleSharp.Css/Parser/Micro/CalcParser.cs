namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Globalization;

    static class CalcParser
    {
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

        private static ICssValue ParseAtomicExpression(this StringSource source)
        {
            var unit = source.ParseUnit();

            if (unit != null && Double.TryParse(unit.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                if (String.IsNullOrEmpty(unit.Dimension))
                {
                    source.SkipSpacesAndComments();
                    return new Length(result, Length.Unit.None);
                }
                else if (Length.GetUnit(unit.Dimension) != Length.Unit.None)
                {
                    source.SkipSpacesAndComments();
                    return new Length(result, Length.GetUnit(unit.Dimension));
                }
                else if (Time.GetUnit(unit.Dimension) != Time.Unit.None)
                {
                    source.SkipSpacesAndComments();
                    return new Time(result, Time.GetUnit(unit.Dimension));
                }
                else if (Angle.GetUnit(unit.Dimension) != Angle.Unit.None)
                {
                    source.SkipSpacesAndComments();
                    return new Angle(result, Angle.GetUnit(unit.Dimension));
                }
                else if (Frequency.GetUnit(unit.Dimension) != Frequency.Unit.None)
                {
                    source.SkipSpacesAndComments();
                    return new Frequency(result, Frequency.GetUnit(unit.Dimension));
                }
                else if (Resolution.GetUnit(unit.Dimension) != Resolution.Unit.None)
                {
                    source.SkipSpacesAndComments();
                    return new Resolution(result, Resolution.GetUnit(unit.Dimension));
                }
            }

            return null;
        }
    }
}
