namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Values;

    static class IdentParser
    {
        public static String Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParseIdent();
            return source.IsDone ? result : null;
        }

        public static String ParseNormalizedIdent(this StringSource source)
        {
            var result = source.ParseIdent();
            return result != null ? result.ToLowerInvariant() : result;
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

        public static T? ParseConstant<T>(this StringSource source, IDictionary<String, T> values)
            where T : struct
        {
            var pos = source.Index;
            var ident = source.ParseIdent();
            var mode = default(T);

            if (ident != null && values.TryGetValue(ident, out mode))
            {
                return mode;
            }

            source.BackTo(pos);
            return null;
        }

        public static T ParseStatic<T>(this StringSource source, IDictionary<String, T> values)
            where T : class
        {
            var ident = source.ParseIdent();
            var mode = default(T);
            return ident != null && values.TryGetValue(ident, out mode) ? mode : null;
        }

        public static Boolean IsFunction(this StringSource source, String name)
        {
            var rest = source.Content.Length - source.Index;

            if (rest >= name.Length + 2)
            {
                var length = 0;
                var current = source.Current;
                var pos = source.Index;

                while (length < name.Length)
                {
                    if (current == Symbols.ReverseSolidus && source.IsValidEscape())
                    {
                        var next = source.ConsumeEscape();

                        if (next.Length != 1)
                            break;

                        current = next[0];
                    }

                    if (Char.ToLowerInvariant(current) != Char.ToLowerInvariant(name[length]))
                        break;

                    length++;
                    current = source.Next();
                }

                if (length == name.Length && current == Symbols.RoundBracketOpen)
                {
                    source.SkipCurrentAndSpaces();
                    return true;
                }

                source.BackTo(pos);
            }

            return false;
        }

        public static Boolean IsIdentifier(this StringSource source, String identifier)
        {
            var pos = source.Index;
            var test = source.ParseIdent();

            if (test != null && test.Isi(identifier))
            {
                return true;
            }

            source.BackTo(pos);
            return false;
        }

        public static ICssValue ParseFontFamily(this StringSource source)
        {
            var str = source.ParseString();

            if (str == null)
            {
                var literal = source.ParseLiteral();

                if (literal != null)
                {
                    return new Identifier(literal);
                }

                return null;
            }

            return new StringValue(str);
        }

        public static ICssValue[] ParseFontFamilies(this StringSource source)
        {
            var family = source.ParseFontFamily();

            if (family != null)
            {
                var families = new List<ICssValue>();
                var c = source.SkipSpacesAndComments();
                families.Add(family);

                while (!source.IsDone)
                {
                    if (c != Symbols.Comma)
                    {
                        return null;
                    }
                    
                    source.SkipCurrentAndSpaces();
                    family = source.ParseFontFamily();
                    families.Add(family);
                    c = source.SkipSpacesAndComments();
                }

                return families.ToArray();
            }

            return null;
        }

        public static String ParseLiteral(this StringSource source)
        {
            var content = StringBuilderPool.Obtain();

            while (!source.IsDone)
            {
                source.SkipSpacesAndComments();
                var item = source.ParseIdent();

                if (item == null)
                    break;

                if (content.Length > 0)
                {
                    content.Append(Symbols.Space);
                }

                content.Append(item);
            }

            var result = content.ToPool();
            return result.Length > 0 ? result : null;
        }

        public static String ParseAnimatableIdent(this StringSource source)
        {
            var pos = source.Index;
            var test = source.ParseNormalizedIdent();

            if (test != null && (Map.Animatables.Contains(test)))
            {
                return test;
            }

            source.BackTo(pos);
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
