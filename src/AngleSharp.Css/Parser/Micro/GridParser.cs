namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class GridParser
    {
        public static ICssValue ParseLineNames(this StringSource source)
        {
            var pos = source.Index;

            if (source.Current == Symbols.SquareBracketOpen)
            {
                source.SkipCurrentAndSpaces();
                var names = new List<String>();

                while (!source.IsDone)
                {
                    var name = source.ParseIdent();

                    if (name == null)
                    {
                        break;
                    }

                    names.Add(name);
                    var current = source.SkipSpacesAndComments();

                    if (current == Symbols.SquareBracketClose)
                    {
                        source.Next();
                        return new LineNames(names);
                    }
                }
            }

            source.BackTo(pos);
            return null;
        }

        public static ICssValue ParseFixedSize(this StringSource source)
        {
            var length = source.ParseDistance();

            if (length == null)
            {
                var pos = source.Index;
                var ident = source.ParseIdent();

                if (ident.Isi(CssKeywords.Minmax) && source.Current == Symbols.RoundBracketOpen)
                {
                    var c = source.SkipCurrentAndSpaces();
                    var min = source.ParseTrackBreadth(false);
                    c = source.SkipSpaces();

                    if (min != null && c == Symbols.Comma)
                    {
                        source.SkipCurrentAndSpaces();
                        var max = source.ParseTrackBreadth();
                        c = source.SkipSpacesAndComments();

                        if (max != null && c == Symbols.RoundBracketClose && (min is Length? || max is Length?))
                        {
                            source.Next();
                            return new FunctionValue(CssKeywords.Minmax, new[] { min, max });
                        }
                    }
                }

                source.BackTo(pos);
                return null;
            }

            return length;
        }

        public static ICssValue ParseTrackSize(this StringSource source)
        {
            var length = source.ParseTrackBreadth();

            if (length == null)
            {
                var pos = source.Index;
                var ident = source.ParseIdent();

                if (ident.Isi(CssKeywords.FitContent) && source.Current == Symbols.RoundBracketOpen)
                {
                    source.SkipCurrentAndSpaces();
                    var dim = source.ParseDistance();
                    var c = source.SkipSpacesAndComments();

                    if (dim.HasValue && c == Symbols.RoundBracketClose)
                    {
                        source.Next();
                        return new FunctionValue(CssKeywords.FitContent, new ICssValue[] { dim });
                    }
                }
                else if (ident.Isi(CssKeywords.Minmax) && source.Current == Symbols.RoundBracketOpen)
                {
                    var c = source.SkipCurrentAndSpaces();
                    var min = source.ParseTrackBreadth(false);
                    c = source.SkipSpaces();

                    if (min != null && c == Symbols.Comma)
                    {
                        source.SkipCurrentAndSpaces();
                        var max = source.ParseTrackBreadth();
                        c = source.SkipSpacesAndComments();

                        if (max != null && c == Symbols.RoundBracketClose)
                        {
                            source.Next();
                            return new FunctionValue(CssKeywords.Minmax, new [] { min, max });
                        }
                    }
                }

                source.BackTo(pos);
                return null;
            }

            return length;
        }

        public static ICssValue ParseFixedRepeat(this StringSource source)
        {
            return source.ParseRepeat(ParseIntegerCount, ParseFixedRepeatValue);
        }

        public static ICssValue ParseAutoRepeat(this StringSource source)
        {
            return source.ParseRepeat(ParseAutoCount, ParseFixedRepeatValue);
        }

        public static ICssValue ParseTrackRepeat(this StringSource source)
        {
            return source.ParseRepeat(ParseIntegerCount, ParseTrackRepeatValue);
        }

        private static ICssValue ParseAutoCount(this StringSource source)
        {
            var arg = source.ParseIdent();

            if (arg != null)
            {
                if (arg.Isi(CssKeywords.AutoFill))
                {
                    return new Identifier(CssKeywords.AutoFill);
                }
                else if (arg.Isi(CssKeywords.AutoFit))
                {
                    return new Identifier(CssKeywords.AutoFit);
                }
            }

            return null;
        }

        private static ICssValue ParseIntegerCount(this StringSource source)
        {
            var arg = source.ParsePositiveInteger();

            if (arg.HasValue)
            {
                return new Length(arg.Value, Length.Unit.None);
            }

            return null;
        }

        private static ICssValue ParseRepeat(this StringSource source, Func<StringSource, ICssValue> parseCount, Func<StringSource, ICssValue> parseValue)
        {
            var pos = source.Index;
            var ident = source.ParseIdent();

            if (ident.Isi(CssKeywords.Repeat) && source.Current == Symbols.RoundBracketOpen)
            {
                var c = source.SkipCurrentAndSpaces();
                var count = parseCount(source);
                c = source.SkipSpaces();

                if (count != null && c == Symbols.Comma)
                {
                    source.SkipCurrentAndSpaces();
                    var value = parseValue(source);
                    c = source.SkipSpacesAndComments();

                    if (value != null && c == Symbols.RoundBracketClose)
                    {
                        source.Next();
                        return new FunctionValue(CssKeywords.Repeat, new ICssValue[] { count, value });
                    }
                }
            }

            source.BackTo(pos);
            return null;
        }

        private static ICssValue ParseFixedRepeatValue(this StringSource source)
        {
            return source.ParseRepeatValue(ParseFixedSize);
        }

        private static ICssValue ParseTrackRepeatValue(this StringSource source)
        {
            return source.ParseRepeatValue(ParseTrackSize);
        }

        private static ICssValue ParseRepeatValue(this StringSource source, Func<StringSource, ICssValue> parseTrack)
        {
            var values = new List<ICssValue>();
            var hasSize = false;

            while (!source.IsDone)
            {
                var names = source.ParseLineNames();
                source.SkipSpacesAndComments();

                if (names != null)
                {
                    values.Add(names);
                }

                var size = parseTrack(source);
                source.SkipSpacesAndComments();

                if (size == null)
                {
                    break;
                }

                hasSize = true;
                values.Add(size);
            }

            if (hasSize)
            {
                return new OrderedOptions(values.ToArray());
            }

            return null;
        }
    }
}
