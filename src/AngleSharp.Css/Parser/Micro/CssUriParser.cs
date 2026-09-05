#nullable disable
namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Text;

    /// <summary>
    /// Represents extensions to for URI values.
    /// </summary>
    public static class CssUriParser
    {
        /// <summary>
        /// Parse a CSS url() value.
        /// </summary>
        public static CssUrlValue ParseUri(this StringSource source)
        {
            var start = source.Index;

            if (source.IsFunction(FunctionNames.Url))
            {
                var current = source.SkipSpacesAndComments();

                var result = current switch
                {
                    Symbols.DoubleQuote => DoubleQuoted(source),
                    Symbols.SingleQuote => SingleQuoted(source),
                    Symbols.RoundBracketClose => Empty(source),
                    Symbols.EndOfFile => new CssUrlValue(String.Empty),
                    _ => Unquoted(source),
                };

                if (result is null)
                {
                    // A bad url yields no value at all. Nothing is consumed either, so
                    // that the caller sees the url() as unparsed instead of as absent.
                    source.BackTo(start);
                }

                return result;
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
                    return Bad(buffer);
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
                    return Bad(buffer);
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
                    return Bad(buffer);
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
                    return Bad(buffer);
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

            return Bad(buffer);
        }

        private static CssUrlValue Empty(StringSource source)
        {
            source.Next();
            return new CssUrlValue(String.Empty);
        }

        private static CssUrlValue Bad(StringBuilder buffer)
        {
            buffer.ToPool();
            return null;
        }
    }
}
