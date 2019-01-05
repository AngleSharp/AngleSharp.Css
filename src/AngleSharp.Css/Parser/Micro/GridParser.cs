namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    static class GridParser
    {
        public static ICssValue ParseGridTemplate(this StringSource source)
        {
            var pos = source.Index;

            if (source.IsIdentifier(CssKeywords.None))
            {
                return new Identifier(CssKeywords.None);
            }

            var rows = source.ParseTrackList() ?? source.ParseAutoTrackList();

            if (rows != null)
            {
                var c = source.SkipSpacesAndComments();

                if (c == Symbols.Solidus)
                {
                    source.SkipCurrentAndSpaces();
                    var cols = source.ParseTrackList() ?? source.ParseAutoTrackList();

                    if (cols != null)
                    {
                        source.SkipSpacesAndComments();
                        return new GridTemplate(rows, cols, null);
                    }
                }
            }
            else
            {
                var rowValues = new List<ICssValue>();
                var col = default(ICssValue);
                var areaValues = new List<ICssValue>();
                var hasValue = false;

                while (!source.IsDone)
                {
                    var value = source.ParseGridTemplateAlternative();

                    if (value == null)
                    {
                        break;
                    }

                    hasValue = true;
                    source.SkipSpacesAndComments();
                    rowValues.Add(new CssTupleValue(new[] { value.Item1, value.Item3, value.Item4 }));
                    areaValues.Add(new StringValue(value.Item2));
                }

                if (hasValue)
                {
                    if (source.Current == Symbols.Solidus)
                    {
                        source.SkipCurrentAndSpaces();
                        col = source.ParseExplicitTrackList();

                        if (col == null)
                        {
                            source.BackTo(pos);
                            return null;
                        }
                    }

                    var row = new CssTupleValue(rowValues.ToArray());
                    var area = new CssTupleValue(areaValues.ToArray());
                    return new GridTemplate(row, col, area);
                }
            }

            source.BackTo(pos);
            return null;
        }

        private static Tuple<LineNames, String, ICssValue, LineNames> ParseGridTemplateAlternative(this StringSource source)
        {
            var namesHead = source.ParseLineNames();
            source.SkipSpacesAndComments();
            var str = source.ParseString();
            source.SkipSpacesAndComments();

            if (str != null)
            {
                var trackSize = source.ParseTrackSize();
                source.SkipSpacesAndComments();
                var namesTail = source.ParseLineNames();
                source.SkipSpacesAndComments();
                return Tuple.Create(namesHead, str, trackSize, namesTail);
            }

            return null;
        }

        public static LineNames ParseLineNames(this StringSource source)
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
            var length = source.ParseDistanceOrCalc();

            if (length == null)
            {
                var pos = source.Index;
                var ident = source.ParseIdent();

                if (ident.Isi(FunctionNames.Minmax) && source.Current == Symbols.RoundBracketOpen)
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
                            return new Minmax(min, max);
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

                if (ident.Isi(FunctionNames.FitContent) && source.Current == Symbols.RoundBracketOpen)
                {
                    source.SkipCurrentAndSpaces();
                    var dim = source.ParseDistanceOrCalc();
                    var c = source.SkipSpacesAndComments();

                    if (dim != null && c == Symbols.RoundBracketClose)
                    {
                        source.Next();
                        return new FitContent(dim);
                    }
                }
                else if (ident.Isi(FunctionNames.Minmax) && source.Current == Symbols.RoundBracketOpen)
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
                            return new Minmax(min, max);
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

            if (ident.Isi(FunctionNames.Repeat) && source.Current == Symbols.RoundBracketOpen)
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
                        return new Repeat(count, value);
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

        public static ICssValue ParseTrackList(this StringSource source)
        {
            return source.ParseRepeatValue(s => s.ParseTrackSize() ?? s.ParseTrackRepeat());
        }

        public static ICssValue ParseExplicitTrackList(this StringSource source)
        {
            return source.ParseRepeatValue(s => s.ParseTrackSize());
        }

        public static ICssValue ParseAutoTrackList(this StringSource source)
        {
            var values = new List<ICssValue>();
            var pos = source.Index;
            var head = source.ParseRepeatValue(s => s.ParseFixedSize() ?? s.ParseFixedRepeat(), true);

            if (head != null)
            {
                values.Add(head);
            }

            source.SkipSpacesAndComments();
            var repeat = source.ParseAutoRepeat();

            if (repeat == null)
            {
                source.BackTo(pos);
                return null;
            }

            values.Add(repeat);
            source.SkipSpacesAndComments();
            var tail = source.ParseRepeatValue(s => s.ParseFixedSize() ?? s.ParseFixedRepeat(), true);
            source.SkipSpacesAndComments();

            if (tail != null)
            {
                values.Add(tail);
            }

            return new CssTupleValue(values.ToArray());
        }

        private static ICssValue ParseRepeatValue(this StringSource source, Func<StringSource, ICssValue> parseTrack, Boolean hasSize = false)
        {
            var values = new List<ICssValue>();
            var pos = source.Index;

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
                return new CssTupleValue(values.ToArray());
            }

            source.BackTo(pos);
            return null;
        }
    }
}
