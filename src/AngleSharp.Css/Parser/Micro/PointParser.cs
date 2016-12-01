namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    static class PointParser
    {
        public static Point? Parse(String str)
        {
            var source = new StringSource(str);
            var result = source.ParsePoint();
            return source.IsDone ? result : null;
        }
        public static Point? ParsePoint(this StringSource source)
        {
            var pos = source.Index;
            var x = new Length(50f, Length.Unit.Percent);
            var y = new Length(50f, Length.Unit.Percent);
            var l = source.ParseIdent();
            source.SkipSpacesAndComments();
            var r = source.ParseIdent();

            if (r != null)
            {
                if (IsHorizontal(r) && IsVertical(l))
                {
                    var t = l;
                    l = r;
                    r = t;
                }

                if (IsHorizontal(l) && IsVertical(r))
                {
                    x = KeywordToLength(l).Value;
                    y = KeywordToLength(r).Value;
                    return new Point(x, y);
                }
            }
            else if (l != null)
            {
                var s = source.ParseDistance();

                if (IsHorizontal(l))
                {
                    x = KeywordToLength(l).Value;
                    return new Point(x, s ?? y);
                }
                else if (IsVertical(l))
                {
                    y = KeywordToLength(l).Value;
                    return new Point(s ?? x, y);
                }
            }
            else
            {
                var f = source.ParseDistance();
                source.SkipSpacesAndComments();
                var s = source.ParseDistance();

                if (s.HasValue)
                {
                    return new Point(f ?? x, s ?? y);
                }
                else if (f.HasValue)
                {
                    pos = source.Index;
                    r = source.ParseIdent();

                    if (r == null)
                    {
                        x = f.Value;
                        return new Point(x, y);
                    }
                    else if (IsVertical(r))
                    {
                        y = KeywordToLength(r).Value;
                        return new Point(f ?? x, y);
                    }
                    else if (IsHorizontal(r))
                    {
                        x = KeywordToLength(r).Value;
                        return new Point(x, f ?? y);
                    }
                    else
                    {
                        source.BackTo(pos);
                        return new Point(f ?? x, y);
                    }
                }
            }

            source.BackTo(pos);
            return null;
        }

        public static BackgroundSize? ParseSize(this StringSource source)
        {
            if (source.IsIdentifier(CssKeywords.Cover))
            {
                return BackgroundSize.Cover;
            }
            else if (source.IsIdentifier(CssKeywords.Contain))
            {
                return BackgroundSize.Contain;
            }
            else
            {
                var w = source.ParseDistance();

                if (!w.HasValue && !source.IsIdentifier(CssKeywords.Auto))
                {
                    return null;
                }

                source.SkipSpacesAndComments();
                var h = source.ParseDistance();

                if (!h.HasValue)
                {
                    source.IsIdentifier(CssKeywords.Auto);
                }
                
                return new BackgroundSize(w ?? Length.Auto, h ?? Length.Auto);
            }
        }

        private static Boolean IsHorizontal(String str)
        {
            return str.Isi(CssKeywords.Left) || str.Isi(CssKeywords.Right) || str.Isi(CssKeywords.Center);
        }

        private static Boolean IsVertical(String str)
        {
            return str.Isi(CssKeywords.Top) || str.Isi(CssKeywords.Bottom) || str.Isi(CssKeywords.Center);
        }

        private static Length? KeywordToLength(String keyword)
        {
            if (keyword.Isi(CssKeywords.Left) || keyword.Isi(CssKeywords.Top))
            {
                return new Length(0f, Length.Unit.Percent);
            }
            else if (keyword.Isi(CssKeywords.Right) || keyword.Isi(CssKeywords.Bottom))
            {
                return new Length(100f, Length.Unit.Percent);
            }
            else if (keyword.Isi(CssKeywords.Center))
            {
                return new Length(50f, Length.Unit.Percent);
            }

            return null;
        }
    }
}
