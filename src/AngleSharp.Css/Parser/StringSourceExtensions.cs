namespace AngleSharp.Css.Parser
{
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Extensions to simplify parsing efforts using a StringSource instance.
    /// </summary>
    public static class StringSourceExtensions
    {
        /// <summary>
        /// Gets the substring starting at the given string.
        /// </summary>
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

        /// <summary>
        /// Takes a string out until a round bracket is closed.
        /// Considers newly opened round brackets to keep it balanced.
        /// </summary>
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
                else if (current is Symbols.SingleQuote or Symbols.DoubleQuote)
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

        /// <summary>
        /// Skips spaces and comments, as well as one non-space token that will be obtained.
        /// </summary>
        public static Char SkipGetSkip(this StringSource source)
        {
            var c = source.SkipSpacesAndComments();
            source.SkipCurrentAndSpaces();
            return c;
        }

        /// <summary>
        /// Skips all spaces and comments.
        /// </summary>
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

        /// <summary>
        /// Skips the current character, as well as spaces and comments.
        /// </summary>
        public static Char SkipCurrentAndSpaces(this StringSource source)
        {
            source.Next();
            return source.SkipSpacesAndComments();
        }

        /// <summary>
        /// Goes back in the source until the given index is reached.
        /// </summary>
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

        /// <summary>
        /// Goes forward in the source until the given index is reached.
        /// </summary>
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
