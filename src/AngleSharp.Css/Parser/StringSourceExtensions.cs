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

        public static Char SkipSpacesAndComments(this StringSource source)
        {
            while (true)
            {
                var c = source.SkipSpaces();

                if (c == Symbols.Solidus && source.Peek() == Symbols.Asterisk)
                {
                    source.Next(2);
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
    }
}
