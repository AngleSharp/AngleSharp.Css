namespace AngleSharp.Css.Parser
{
    using AngleSharp.Text;
    using System;
    using System.Text;

    static class IdentParser
    {
        public static String Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParseIdent();
            return source.IsDone ? result : null;
        }

        public static String ParseIdent(this StringSource source)
        {
            var current = source.Current;

            if (current == Symbols.Minus)
            {
                current = source.Next();

                if (current.IsNameStart() || source.IsValidEscape())
                {
                    var buffer = StringBuilderPool.Obtain();
                    buffer.Append(Symbols.Minus);
                    return Rest(source, current, buffer);
                }

                source.Back();
                return null;
            }
            else if (current.IsNameStart())
            {
                var buffer = StringBuilderPool.Obtain();
                buffer.Append(current);
                return Rest(source, source.Next(), buffer);
            }
            else if (current == Symbols.ReverseSolidus && source.IsValidEscape())
            {
                var buffer = StringBuilderPool.Obtain();
                buffer.Append(source.ConsumeEscape());
                return Rest(source, source.Next(), buffer);
            }

            return null;
        }
        
        private static String Rest(StringSource source, Char current, StringBuilder buffer)
        {
            while (true)
            {
                if (current.IsName())
                {
                    buffer.Append(current);
                }
                else if (source.IsValidEscape())
                {
                    buffer.Append(source.ConsumeEscape());
                }
                else
                {
                    return buffer.ToPool();
                }

                current = source.Next();
            }
        }
    }
}
