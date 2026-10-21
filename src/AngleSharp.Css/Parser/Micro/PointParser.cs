#nullable disable
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
            var x = new CssLengthValue(50f, CssLengthValue.Unit.Percent);
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
                var offset = source.ParseEdgeOffset(l);
                return offset is not null ? new CssEdgeOffsetValue(l.ToLowerFast(), offset) : KeywordToLength(l);
            }

            source.BackTo(pos);
            return null;
        }

        /// <summary>
        /// Parses a point's origin value.
        /// </summary>
        public static CssOriginValue ParseOrigin(this StringSource source)
        {
            // An origin has no edge offsets, i.e., a trailing length is the z-component.
            var pt = source.ParseKeywordPoint();
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
        public static CssPoint2D? ParsePoint(this StringSource source) =>
            source.ParseEdgeOffsetPoint() ?? source.ParseKeywordPoint();

        /// <summary>
        /// Parses a point value given by one or two components, i.e., without edge offsets.
        /// </summary>
        private static CssPoint2D? ParseKeywordPoint(this StringSource source)
        {
            var pos = source.Index;
            var x = new CssLengthValue(50f, CssLengthValue.Unit.Percent);
            var y = new CssLengthValue(50f, CssLengthValue.Unit.Percent);
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
                    return new CssPoint2D(x, y);
                }
            }
            else if (l != null)
            {
                var s = source.ParseDistanceOrCalc();

                if (IsHorizontal(l))
                {
                    x = KeywordToLength(l).Value;
                    return new CssPoint2D(x, s ?? y);
                }
                else if (IsVertical(l))
                {
                    y = KeywordToLength(l).Value;
                    return new CssPoint2D(s ?? x, y);
                }
            }
            else
            {
                var f = source.ParseDistanceOrCalc();
                source.SkipSpacesAndComments();
                var s = source.ParseDistanceOrCalc();

                if (s != null)
                {
                    return new CssPoint2D(f ?? x, s ?? y);
                }
                else if (f != null)
                {
                    pos = source.Index;
                    r = source.ParseIdent();

                    if (r == null)
                    {
                        return new CssPoint2D(f, y);
                    }
                    else if (IsVertical(r))
                    {
                        y = KeywordToLength(r).Value;
                        return new CssPoint2D(f ?? x, y);
                    }
                    else if (IsHorizontal(r))
                    {
                        x = KeywordToLength(r).Value;
                        return new CssPoint2D(x, f ?? y);
                    }
                    else
                    {
                        source.BackTo(pos);
                        return new CssPoint2D(f ?? x, y);
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

                if (w is null && !source.IsIdentifier(CssKeywords.Auto))
                {
                    return null;
                }

                source.SkipSpacesAndComments();
                var h = source.ParseDistanceOrCalc();

                if (h is null)
                {
                    source.IsIdentifier(CssKeywords.Auto);
                }
                
                return new CssBackgroundSizeValue(w ?? CssLengthValue.Auto, h ?? CssLengthValue.Auto);
            }
        }

        /// <summary>
        /// Parses the three- and four-value syntax of a position, e.g., right 10px bottom 20px,
        /// where at least one of the edges has an offset.
        /// </summary>
        private static CssPoint2D? ParseEdgeOffsetPoint(this StringSource source)
        {
            var pos = source.Index;
            var first = source.ParseIdent();
            var firstOffset = first is not null ? source.ParseEdgeOffset(first) : null;
            source.SkipSpacesAndComments();
            var second = first is not null ? source.ParseIdent() : null;
            var secondOffset = second is not null ? source.ParseEdgeOffset(second) : null;

            if (second is not null && (firstOffset is not null || secondOffset is not null))
            {
                if (!IsHorizontal(first) || !IsVertical(second))
                {
                    var edge = first;
                    var offset = firstOffset;
                    first = second;
                    firstOffset = secondOffset;
                    second = edge;
                    secondOffset = offset;
                }

                var valid = IsHorizontal(first) && IsVertical(second) &&
                    (firstOffset is null || !first.Isi(CssKeywords.Center)) &&
                    (secondOffset is null || !second.Isi(CssKeywords.Center));

                if (valid)
                {
                    var x = firstOffset is not null ? new CssEdgeOffsetValue(first.ToLowerFast(), firstOffset) : (ICssValue)KeywordToLength(first).Value;
                    var y = secondOffset is not null ? new CssEdgeOffsetValue(second.ToLowerFast(), secondOffset) : (ICssValue)KeywordToLength(second).Value;
                    return new CssPoint2D(x, y);
                }
            }

            source.BackTo(pos);
            return null;
        }

        /// <summary>
        /// Parses the optional offset following an edge keyword (not center).
        /// </summary>
        private static ICssValue ParseEdgeOffset(this StringSource source, String edge)
        {
            if (!edge.Isi(CssKeywords.Center))
            {
                var pos = source.Index;
                source.SkipSpacesAndComments();
                var offset = source.ParseDistanceOrCalc();

                if (offset is not null)
                {
                    return offset;
                }

                source.BackTo(pos);
            }

            return null;
        }

        private static Boolean IsHorizontal(String str) =>
            str.Isi(CssKeywords.Left) || str.Isi(CssKeywords.Right) || str.Isi(CssKeywords.Center);

        private static Boolean IsVertical(String str) =>
            str.Isi(CssKeywords.Top) || str.Isi(CssKeywords.Bottom) || str.Isi(CssKeywords.Center);

        private static CssLengthValue? KeywordToLength(String keyword)
        {
            if (keyword.Isi(CssKeywords.Left) || keyword.Isi(CssKeywords.Top))
            {
                return new CssLengthValue(0f, CssLengthValue.Unit.Percent);
            }
            else if (keyword.Isi(CssKeywords.Right) || keyword.Isi(CssKeywords.Bottom))
            {
                return new CssLengthValue(100f, CssLengthValue.Unit.Percent);
            }
            else if (keyword.Isi(CssKeywords.Center))
            {
                return new CssLengthValue(50f, CssLengthValue.Unit.Percent);
            }

            return null;
        }
    }
}
