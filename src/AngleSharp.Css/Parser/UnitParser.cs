namespace AngleSharp.Css.Parser
{
    using AngleSharp.Text;
    using System;
    using System.Text;

    static class UnitParser
    {
        public static Unit Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParseUnit();
            return source.IsDone ? result : null;
        }

        public static Unit ParseUnit(this StringSource source)
        {
            var pos = source.Index;
            var result = Start(source);

            if (result == null)
            {
                source.BackTo(pos);
            }

            return result;
        }

        private static Unit Start(StringSource source)
        {
            var current = source.Current;

            if (current.IsOneOf(Symbols.Plus, Symbols.Minus))
            {
                var next = source.Next();

                if (next == Symbols.Dot)
                {
                    var buffer = StringBuilderPool.Obtain().Append(current).Append(next);
                    return Fraction(source, buffer);
                }
                else if (next.IsDigit())
                {
                    var buffer = StringBuilderPool.Obtain().Append(current).Append(next);
                    return Rest(source, buffer);
                }
            }
            else if (current == Symbols.Dot)
            {
                return Fraction(source, StringBuilderPool.Obtain().Append(current));
            }
            else if (current.IsDigit())
            {
                return Rest(source, StringBuilderPool.Obtain().Append(current));
            }

            return null;
        }

        private static Unit Rest(StringSource source, StringBuilder buffer)
        {
            var current = source.Next();

            while (true)
            {
                if (current.IsDigit())
                {
                    buffer.Append(current);
                }
                else if (current.IsNameStart() || source.IsValidEscape())
                {
                    var number = buffer.ToString();
                    return Dimension(source, number, buffer.Clear());
                }
                else
                {
                    break;
                }

                current = source.Next();
            }

            switch (current)
            {
                case Symbols.Dot:
                    current = source.Next();

                    if (current.IsDigit())
                    {
                        buffer.Append(Symbols.Dot).Append(current);
                        return Fraction(source, buffer);
                    }

                    return new Unit(buffer.ToPool(), String.Empty);

                case '%':
                    source.Next();
                    return new Unit(buffer.ToPool(), "%");

                case 'e':
                case 'E':
                    return NumberExponential(source, buffer);

                case Symbols.Minus:
                    return NumberDash(source, buffer);

                default:
                    return new Unit(buffer.ToPool(), String.Empty);
            }
        }

        private static Unit Fraction(StringSource source, StringBuilder buffer)
        {
            var current = source.Next();

            while (true)
            {
                if (current.IsDigit())
                {
                    buffer.Append(current);
                }
                else if (current.IsNameStart() || source.IsValidEscape())
                {
                    var number = buffer.ToString();
                    return Dimension(source, number, buffer.Clear());
                }
                else
                {
                    break;
                }

                current = source.Next();
            }

            switch (current)
            {
                case 'e':
                case 'E':
                    return NumberExponential(source, buffer);

                case '%':
                    source.Next();
                    return new Unit(buffer.ToPool(), "%");

                case Symbols.Minus:
                    return NumberDash(source, buffer);

                default:
                    return new Unit(buffer.ToPool(), String.Empty);
            }
        }

        private static Unit Dimension(StringSource source, String number, StringBuilder buffer)
        {
            var current = source.Current;

            while (true)
            {
                if (current.IsLetter())
                {
                    buffer.Append(current);
                }
                else if (source.IsValidEscape())
                {
                    current = source.Next();
                    buffer.Append(source.ConsumeEscape());
                }
                else
                {
                    return new Unit(number, buffer.ToPool());
                }

                current = source.Next();
            }
        }

        private static Unit SciNotation(StringSource source, StringBuilder buffer)
        {
            while (true)
            {
                var current = source.Next();

                if (current.IsDigit())
                {
                    buffer.Append(current);
                }
                else
                {
                    return new Unit(buffer.ToPool(), String.Empty);
                }
            }
        }

        private static Unit NumberDash(StringSource source, StringBuilder buffer)
        {
            var current = source.Next();

            if (current.IsNameStart() || source.IsValidEscape())
            {
                var number = buffer.ToString();
                return Dimension(source, number, buffer.Clear().Append(Symbols.Minus));
            }
            else
            {
                source.Back();
                return new Unit(buffer.ToPool(), String.Empty);
            }
        }

        private static Unit NumberExponential(StringSource source, StringBuilder buffer)
        {
            var letter = source.Current;
            var current = source.Next();

            if (current.IsDigit())
            {
                buffer.Append(letter).Append(current);
                return SciNotation(source, buffer);
            }
            else if (current == Symbols.Plus || current == Symbols.Minus)
            {
                var op = current;
                current = source.Next();

                if (current.IsDigit())
                {
                    buffer.Append(letter).Append(op).Append(current);
                    return SciNotation(source, buffer);
                }

                source.Back();
            }

            var number = buffer.ToString();
            return Dimension(source, number, buffer.Clear().Append(letter));
        }
    }
}
