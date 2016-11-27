namespace AngleSharp.Css.Parser
{
    using AngleSharp.Text;
    using System;
    using System.Text;

    static class UriParser
    {
        public static Url Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParseUri();
            return source.IsDone ? result : null;
        }

        public static Url ParseUri(this StringSource source)
        {
            var url = FunctionNames.Url;
            var pos = source.Index;
            var rest = source.Content.Length - pos;

            if (rest >= url.Length + 2)
            {
                var length = 0;
                var current = source.Current;

                while (length < url.Length)
                {
                    if (Char.ToLowerInvariant(current) != url[length])
                        break;

                    length++;
                    current = source.Next();
                }

                if (length == url.Length && current == Symbols.RoundBracketOpen)
                {
                    source.Next();
                    return Start(source);
                }

                source.BackTo(pos);
            }

            return null;
        }

        private static Url Start(StringSource source)
        {
            var current = source.SkipSpacesAndComments();

            switch (current)
            {
                case Symbols.DoubleQuote:
                    return DoubleQuoted(source);

                case Symbols.SingleQuote:
                    return SingleQuoted(source);

                case Symbols.RoundBracketClose:
                    return new Url(String.Empty);

                case Symbols.EndOfFile:
                    return new Url(String.Empty);

                default:
                    return Unquoted(source);
            }
        }

        private static Url DoubleQuoted(StringSource source)
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
                    return new Url(buffer.ToPool());
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
                        return new Url(buffer.ToPool());
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

        private static Url SingleQuoted(StringSource source)
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
                    return new Url(buffer.ToPool());
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
                        return new Url(buffer.ToPool());
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

        private static Url Unquoted(StringSource source)
        {
            var buffer = StringBuilderPool.Obtain();
            var current = source.Current;

            while (true)
            {
                if (current.IsSpaceCharacter())
                {
                    return End(source, buffer);
                }
                else if (current.IsOneOf(Symbols.RoundBracketClose, Symbols.EndOfFile))
                {
                    source.Next();
                    return new Url(buffer.ToPool());
                }
                else if (current.IsOneOf(Symbols.DoubleQuote, Symbols.SingleQuote, Symbols.RoundBracketOpen) || current.IsNonPrintable())
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

        private static Url End(StringSource source, StringBuilder buffer)
        {
            var current = source.SkipCurrentAndSpaces();

            if (current == Symbols.RoundBracketClose)
            {
                source.Next();
                return new Url(buffer.ToPool());
            }

            return Bad(source, buffer);
        }

        private static Url Bad(StringSource source, StringBuilder buffer)
        {
            var current = source.Current;
            var curly = 0;
            var round = 1;

            while (current != Symbols.EndOfFile)
            {
                if (current == Symbols.Semicolon)
                {
                    return new Url(buffer.ToPool());
                }
                else if (current == Symbols.CurlyBracketClose && --curly == -1)
                {
                    return new Url(buffer.ToPool());
                }
                else if (current == Symbols.RoundBracketClose && --round == 0)
                {
                    source.Next();
                    return new Url(buffer.ToPool());
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
                    else if (curly == Symbols.CurlyBracketOpen)
                    {
                        ++curly;
                    }

                    buffer.Append(current);
                }

                current = source.Next();
            }
            
            return new Url(buffer.ToPool());
        }
    }
}
