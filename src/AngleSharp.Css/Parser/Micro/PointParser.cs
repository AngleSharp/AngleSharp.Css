using System.Text.RegularExpressions;

namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents extensions to for point values.
    /// </summary>
    public static class PointParser
    {
        /// <summary>
        /// Parses a point's x value.
        /// </summary>
        public static ICssValue ParsePointX(this StringSource source) =>
            source.ParsePointDir(IsHorizontal);

        /// <summary>
        /// Parses a point's y value.
        /// </summary>
        public static ICssValue ParsePointY(this StringSource source) =>
            source.ParsePointDir(IsVertical);

        /// <summary>
        /// Parses a point's direction value.
        /// </summary>
        private static ICssValue ParsePointDir(this StringSource source, Predicate<String> checkKeyword)
        {
            var pos = source.Index;
            var x = new Length(50f, Length.Unit.Percent);
            var l = source.ParseIdent();

            if (l == null)
            {
                var f = source.ParseDistanceOrCalc();

                if (f != null)
                {
                    return f;
                }
            }
            else if (checkKeyword(l))
            {
                return KeywordToLength(l);
            }

            source.BackTo(pos);
            return null;
        }

        /// <summary>
        /// Parses a point's origin value.
        /// </summary>
        public static CssOriginValue ParseOrigin(this StringSource source)
        {
            var pt = source.ParsePoint();
            source.SkipSpacesAndComments();
            var z = source.ParseLengthOrCalc();

            if (pt.HasValue)
            {
                return new CssOriginValue(pt.Value.X, pt.Value.Y, z);
            }

            return null;
        }

        /// <summary>
        /// Parses a point value.
        /// </summary>
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
                var s = source.ParseDistanceOrCalc();

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
                var f = source.ParseDistanceOrCalc();
                source.SkipSpacesAndComments();
                var s = source.ParseDistanceOrCalc();

                if (s != null)
                {
                    return new Point(f ?? x, s ?? y);
                }
                else if (f != null)
                {
                    pos = source.Index;
                    r = source.ParseIdent();

                    if (r == null)
                    {
                        return new Point(f, y);
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

        /// <summary>
        /// Parses a size value.
        /// </summary>
        public static CssBackgroundSizeValue ParseSize(this StringSource source)
        {
            if (source.Content.IndexOf("/") != -1)
            {
                var opts = RegexOptions.Singleline | RegexOptions.IgnoreCase;
                var match1 = Regex.Match(source.Content, RegExPatterns.center_cover_pattern, opts);
                var match2 = Regex.Match(source.Content, RegExPatterns.cover_center_pattern, opts);

                if (match1.Success || match2.Success)
                {
                    source.NextTo(source.Content.Length + 1);
                    return new CssBackgroundSizeValue(source.Content);
                }
            }

            if (source.IsIdentifier(CssKeywords.Cover))
            {
                return CssBackgroundSizeValue.Cover;
            }
            else if (source.IsIdentifier(CssKeywords.Contain))
            {
                return CssBackgroundSizeValue.Contain;
            }
            else
            {
                var w = source.ParseDistanceOrCalc();

                if (w == null && !source.IsIdentifier(CssKeywords.Auto))
                {
                    return null;
                }

                source.SkipSpacesAndComments();
                var h = source.ParseDistanceOrCalc();

                if (h == null)
                {
                    source.IsIdentifier(CssKeywords.Auto);
                }

                return new CssBackgroundSizeValue(w ?? Length.Auto, h ?? Length.Auto);
            }
        }

        private static Boolean IsHorizontal(String str) =>
            str.Isi(CssKeywords.Left) || str.Isi(CssKeywords.Right) || str.Isi(CssKeywords.Center);

        private static Boolean IsVertical(String str) =>
            str.Isi(CssKeywords.Top) || str.Isi(CssKeywords.Bottom) || str.Isi(CssKeywords.Center);

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
