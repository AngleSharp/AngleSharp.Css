namespace AngleSharp.Css.Parser
{
    using AngleSharp.Text;
    using System;

    static class StringSourceExtensions
    {
        public static String Substring(this StringSource source, Int32 startIndex)
        {
            if (startIndex == 0 && source.IsDone)
            {
                return source.Content;
            }
            else if (startIndex < source.Index)
            {
                return source.Content.Substring(startIndex, source.Index - startIndex);
            }

            return String.Empty;
        }

        public static String TakeUntilClosed(this StringSource source)
        {
            var start = source.Index;
            var open = 1;
            var current = source.Current;
            var lastNonWhitespace = start;

            while (!source.IsDone)
            {
                if (current == Symbols.RoundBracketOpen)
                {
                    open++;
                }
                else if (current == Symbols.RoundBracketClose && --open == 0)
                {
                    break;
                }
                else if (current == Symbols.Solidus && source.Peek() == Symbols.Asterisk)
                {
                    source.Next();
                    current = source.SkipCssComment();
                    continue;
                }
                else if (current.IsOneOf(Symbols.SingleQuote, Symbols.DoubleQuote))
                {
                    source.ParseString();
                    current = source.Current;
                    lastNonWhitespace = source.Index;
                    continue;
                }
                else if (current == Symbols.ReverseSolidus && source.IsValidEscape())
                {
                    source.ConsumeEscape();
                    current = source.Current;
                    lastNonWhitespace = source.Index;
                    continue;
                }
                else if (current.IsSpaceCharacter())
                {
                    current = source.SkipSpacesAndComments();
                    continue;
                }

                current = source.Next();
                lastNonWhitespace = source.Index;
            }

            var end = source.Index;
            source.BackTo(lastNonWhitespace);
            var content = source.Substring(start);
            source.NextTo(end);
            return content;
        }

        public static Char SkipGetSkip(this StringSource source)
        {
            var c = source.SkipSpacesAndComments();
            source.SkipCurrentAndSpaces();
            return c;
        }

        public static Char SkipSpacesAndComments(this StringSource source)
        {
            while (true)
            {
                var c = source.SkipSpaces();

                if (c == Symbols.Solidus && source.Peek() == Symbols.Asterisk)
                {
                    source.Next();
                    c = source.SkipCssComment();
                    continue;
                }

                return c;
            }
        }

        public static Char SkipCurrentAndSpaces(this StringSource source)
        {
            source.Next();
            return source.SkipSpacesAndComments();
        }

        public static Char BackTo(this StringSource source, Int32 index)
        {
            var diff = source.Index - index;
            var current = Symbols.Null;

            while (diff > 0)
            {
                current = source.Back();
                diff--;
            }

            return current;
        }

        public static Char NextTo(this StringSource source, Int32 index)
        {
            var diff = index - source.Index;
            var current = Symbols.Null;

            while (diff > 0)
            {
                current = source.Next();
                diff--;
            }

            return current;
        }
    }
}
