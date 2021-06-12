namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Text;

    static class CssUriParser
    {
        public static CssUrlValue ParseUri(this StringSource source)
        {
            if (source.IsFunction(FunctionNames.Url))
            {
                var current = source.SkipSpacesAndComments();

                switch (current)
                {
                    case Symbols.DoubleQuote:
                        return DoubleQuoted(source);

                    case Symbols.SingleQuote:
                        return SingleQuoted(source);

                    case Symbols.RoundBracketClose:
                        return new CssUrlValue(String.Empty);

                    case Symbols.EndOfFile:
                        return new CssUrlValue(String.Empty);

                    default:
                        return Unquoted(source);
                }
            }

            return null;
        }

        private static CssUrlValue DoubleQuoted(StringSource source)
        {
            var buffer = StringBuilderPool.Obtain();

            while (true)
            {
                var current = source.Next();

                if (current.IsLineBreak())
                {
                    return Bad(source, buffer);
                }
                else if (Symbols.EndOfFile == current)
                {
                    return new CssUrlValue(buffer.ToPool());
                }
                else if (current == Symbols.DoubleQuote)
                {
                    return End(source, buffer);
                }
                else if (current != Symbols.ReverseSolidus)
                {
                    buffer.Append(current);
                }
                else
                {
                    current = source.Next();

                    if (current == Symbols.EndOfFile)
                    {
                        source.Back();
                        return new CssUrlValue(buffer.ToPool());
                    }
                    else if (current.IsLineBreak())
                    {
                        buffer.AppendLine();
                    }
                    else
                    {
                        source.Back();
                        buffer.Append(source.ConsumeEscape());
                    }
                }
            }
        }

        private static CssUrlValue SingleQuoted(StringSource source)
        {
            var buffer = StringBuilderPool.Obtain();

            while (true)
            {
                var current = source.Next();

                if (current.IsLineBreak())
                {
                    return Bad(source, buffer);
                }
                else if (current == Symbols.EndOfFile)
                {
                    return new CssUrlValue(buffer.ToPool());
                }
                else if (current == Symbols.SingleQuote)
                {
                    return End(source, buffer);
                }
                else if (current != Symbols.ReverseSolidus)
                {
                    buffer.Append(current);
                }
                else
                {
                    current = source.Next();

                    if (current == Symbols.EndOfFile)
                    {
                        source.Back();
                        return new CssUrlValue(buffer.ToPool());
                    }
                    else if (current.IsLineBreak())
                    {
                        buffer.AppendLine();
                    }
                    else
                    {
                        buffer.Append(source.ConsumeEscape());
                    }
                }
            }
        }

        private static CssUrlValue Unquoted(StringSource source)
        {
            var buffer = StringBuilderPool.Obtain();
            var current = source.Current;

            while (true)
            {
                if (current.IsSpaceCharacter())
                {
                    return End(source, buffer);
                }
                else if (current is Symbols.RoundBracketClose or Symbols.EndOfFile) 
                {
                    source.Next();
                    return new CssUrlValue(buffer.ToPool());
                }
                else if (current is Symbols.DoubleQuote or Symbols.SingleQuote or Symbols.RoundBracketOpen || current.IsNonPrintable())
                {
                    return Bad(source, buffer);
                }
                else if (current != Symbols.ReverseSolidus)
                {
                    buffer.Append(current);
                }
                else if (source.IsValidEscape())
                {
                    buffer.Append(source.ConsumeEscape());
                }
                else
                {
                    return Bad(source, buffer);
                }

                current = source.Next();
            }
        }

        private static CssUrlValue End(StringSource source, StringBuilder buffer)
        {
            var current = source.SkipCurrentAndSpaces();

            if (current == Symbols.RoundBracketClose)
            {
                source.Next();
                return new CssUrlValue(buffer.ToPool());
            }

            return Bad(source, buffer);
        }

        private static CssUrlValue Bad(StringSource source, StringBuilder buffer)
        {
            var current = source.Current;
            var curly = 0;
            var round = 1;

            while (current != Symbols.EndOfFile)
            {
                if (current == Symbols.Semicolon)
                {
                    return new CssUrlValue(buffer.ToPool());
                }
                else if (current == Symbols.CurlyBracketClose && --curly == -1)
                {
                    return new CssUrlValue(buffer.ToPool());
                }
                else if (current == Symbols.RoundBracketClose && --round == 0)
                {
                    source.Next();
                    return new CssUrlValue(buffer.ToPool());
                }
                else if (source.IsValidEscape())
                {
                    buffer.Append(source.ConsumeEscape());
                }
                else
                {
                    if (current == Symbols.RoundBracketOpen)
                    {
                        ++round;
                    }
                    else if (current == Symbols.CurlyBracketOpen)
                    {
                        ++curly;
                    }

                    buffer.Append(current);
                }

                current = source.Next();
            }
            
            return new CssUrlValue(buffer.ToPool());
        }
    }
}
