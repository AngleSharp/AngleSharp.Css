#nullable enable
namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Converts the keywords of the CSS Box Alignment properties, including the
    /// multi-keyword forms `[ safe | unsafe ] &lt;position&gt;`,
    /// `[ first | last ] baseline`, and `legacy [ left | right | center ]`.
    /// </summary>
    sealed class BoxAlignmentValueConverter : IValueConverter
    {
        private static readonly HashSet<String> PositionKeywords = new(StringComparer.OrdinalIgnoreCase)
        {
            CssKeywords.Center,
            CssKeywords.Start,
            CssKeywords.End,
            CssKeywords.SelfStart,
            CssKeywords.SelfEnd,
            CssKeywords.FlexStart,
            CssKeywords.FlexEnd,
            CssKeywords.Left,
            CssKeywords.Right,
        };

        private static readonly HashSet<String> LegacyKeywords = new(StringComparer.OrdinalIgnoreCase)
        {
            CssKeywords.Left,
            CssKeywords.Right,
            CssKeywords.Center,
        };

        private readonly IDictionary<String, FlexContentMode> _values;

        public BoxAlignmentValueConverter(IDictionary<String, FlexContentMode> values)
        {
            _values = values;
        }

        public ICssValue? Convert(StringSource source)
        {
            var start = source.Index;
            var first = source.ParseIdent();

            if (first is not null)
            {
                var keyword = first.ToLowerFast();

                if (keyword.Is(CssKeywords.First) || keyword.Is(CssKeywords.Last))
                {
                    if (_values.TryGetValue(CssKeywords.Baseline, out var baseline) && TryParseKeyword(source, CssKeywords.Baseline))
                    {
                        // first baseline serializes as baseline
                        var text = keyword.Is(CssKeywords.First) ? CssKeywords.Baseline : String.Concat(keyword, " ", CssKeywords.Baseline);
                        return new CssConstantValue<FlexContentMode>(text, baseline);
                    }
                }
                else if (keyword.Is(CssKeywords.Safe) || keyword.Is(CssKeywords.Unsafe))
                {
                    var pos = source.Index;
                    source.SkipSpacesAndComments();
                    var position = source.ParseIdent();

                    if (position is not null && PositionKeywords.Contains(position) && _values.TryGetValue(position, out var mode))
                    {
                        return new CssConstantValue<FlexContentMode>(String.Concat(keyword, " ", position.ToLowerFast()), mode);
                    }

                    source.BackTo(pos);
                }
                else if (_values.TryGetValue(keyword, out var mode))
                {
                    if (_values.ContainsKey(CssKeywords.Legacy))
                    {
                        var legacy = TryParseLegacy(source, keyword);

                        if (legacy is not null)
                        {
                            return new CssConstantValue<FlexContentMode>(legacy, mode);
                        }
                    }

                    return new CssConstantValue<FlexContentMode>(keyword, mode);
                }
            }

            source.BackTo(start);
            return null;
        }

        private static String? TryParseLegacy(StringSource source, String keyword)
        {
            var pos = source.Index;
            source.SkipSpacesAndComments();
            var other = source.ParseIdent();

            if (other is not null)
            {
                if (keyword.Is(CssKeywords.Legacy) && LegacyKeywords.Contains(other))
                {
                    return String.Concat(CssKeywords.Legacy, " ", other.ToLowerFast());
                }
                else if (other.Isi(CssKeywords.Legacy) && LegacyKeywords.Contains(keyword))
                {
                    return String.Concat(CssKeywords.Legacy, " ", keyword);
                }
            }

            source.BackTo(pos);
            return null;
        }

        private static Boolean TryParseKeyword(StringSource source, String keyword)
        {
            var pos = source.Index;
            source.SkipSpacesAndComments();
            var ident = source.ParseIdent();

            if (ident is not null && ident.Isi(keyword))
            {
                return true;
            }

            source.BackTo(pos);
            return false;
        }
    }
}
