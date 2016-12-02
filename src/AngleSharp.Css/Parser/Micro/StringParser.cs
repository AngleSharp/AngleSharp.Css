namespace AngleSharp.Css.Parser
{
    using AngleSharp.Text;
    using System;

    static class StringParser
    {
        public static String Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParseString();
            return source.IsDone ? result : null;
        }

        /// <summary>
        /// Represents a string object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/string
        /// </summary>
        public static String ParseString(this StringSource source)
        {
            var current = source.Current;

            switch (current)
            {
                case Symbols.DoubleQuote: return DoubleQuoted(source);
                case Symbols.SingleQuote: return SingleQuoted(source);
            }

            return null;
        }

        private static String DoubleQuoted(StringSource source)
        {
            var buffer = StringBuilderPool.Obtain();

            while (true)
            {
                var current = source.Next();

                switch (current)
                {
                    case Symbols.DoubleQuote:
                    case Symbols.EndOfFile:
                        source.Next();
                        return buffer.ToPool();

                    case Symbols.FormFeed:
                    case Symbols.LineFeed:
                        buffer.ToPool();
                        return null;

                    case Symbols.ReverseSolidus:
                        current = source.Next();

                        if (current.IsLineBreak())
                        {
                            buffer.AppendLine();
                        }
                        else if (current != Symbols.EndOfFile)
                        {
                            source.Back();
                            buffer.Append(source.ConsumeEscape());
                        }
                        else
                        {
                            return buffer.ToPool();
                        }

                        break;

                    default:
                        buffer.Append(current);
                        break;
                }
            }
        }

        private static String SingleQuoted(StringSource source)
        {
            var buffer = StringBuilderPool.Obtain();

            while (true)
            {
                var current = source.Next();

                switch (current)
                {
                    case Symbols.SingleQuote:
                    case Symbols.EndOfFile:
                        source.Next();
                        return buffer.ToPool();

                    case Symbols.FormFeed:
                    case Symbols.LineFeed:
                        buffer.ToPool();
                        return null;

                    case Symbols.ReverseSolidus:
                        current = source.Next();

                        if (current.IsLineBreak())
                        {
                            buffer.AppendLine();
                        }
                        else if (current != Symbols.EndOfFile)
                        {
                            source.Back();
                            buffer.Append(source.ConsumeEscape());
                        }
                        else
                        {
                            return buffer.ToPool();
                        }

                        break;

                    default:
                        buffer.Append(current);
                        break;
                }
            }
        }
    }
}
