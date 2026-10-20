#nullable disable
namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents extensions to for general CSS compount values.
    /// </summary>
    public static class CompoundParser
    {
        /// <summary>
        /// Parse for a CSS quote value.
        /// </summary>
        public static CssTupleValue ParseQuotes(this StringSource source)
        {
            var quotes = new List<ICssValue>();

            while (!source.IsDone)
            {
                var open = source.ParseString();
                source.SkipSpacesAndComments();
                var close = source.ParseString();
                source.SkipSpacesAndComments();

                if (open == null || close == null)
                {
                    return null;
                }

                quotes.Add(new CssQuoteValue(open, close));
            }

            return new CssTupleValue(quotes.ToArray());
        }

        /// <summary>
        /// Parse for a CSS border image slice.
        /// </summary>
        public static CssBorderImageSliceValue ParseBorderImageSlice(this StringSource source)
        {
            var lengths = new ICssValue[4];
            var filled = false;
            var completed = 0;
            var pos = 0;

            do
            {
                pos = source.Index;

                if (completed < 4)
                {
                    var length = source.ParsePercentOrNumber();

                    if (length.HasValue)
                    {
                        lengths[completed++] = length.Value;
                        source.SkipCurrentAndSpaces();
                    }
                }

                if (!filled)
                {
                    filled = source.IsIdentifier(CssKeywords.Fill);
                    source.SkipSpacesAndComments();
                }
            }
            while (source.Index != pos);

            if (completed > 0 || filled)
            {
                while (completed < 4)
                {
                    lengths[completed++] = CssLengthValue.Auto;
                }

                return new CssBorderImageSliceValue(lengths[0], lengths[1], lengths[2], lengths[3], filled);
            }

            return null;
        }

        /// <summary>
        /// Parse for a CSS background repeat.
        /// </summary>
        public static CssImageRepeatsValue ParseBackgroundRepeat(this StringSource source)
        {
            if (source.IsIdentifier(CssKeywords.RepeatX))
            {
                var h = new CssConstantValue<BackgroundRepeat>(CssKeywords.Repeat, BackgroundRepeat.Repeat);
                var v = new CssConstantValue<BackgroundRepeat>(CssKeywords.NoRepeat, BackgroundRepeat.NoRepeat);
                return new CssImageRepeatsValue(h, v);
            }
            else if (source.IsIdentifier(CssKeywords.RepeatY))
            {
                var h = new CssConstantValue<BackgroundRepeat>(CssKeywords.NoRepeat, BackgroundRepeat.NoRepeat);
                var v = new CssConstantValue<BackgroundRepeat>(CssKeywords.Repeat, BackgroundRepeat.Repeat);
                return new CssImageRepeatsValue(h, v);
            }
            else
            {
                var repeatX = source.ParseConstant(Map.BackgroundRepeats);
                source.SkipSpacesAndComments();
                var repeatY = source.ParseConstant(Map.BackgroundRepeats);

                if (repeatY != null)
                {
                    return new CssImageRepeatsValue(repeatX, repeatY);
                }
                else if (repeatX != null)
                {
                    return new CssImageRepeatsValue(repeatX, repeatX);
                }
            }

            return null;
        }

        /// <summary>
        /// Parse for a CSS image source.
        /// </summary>
        public static ICssImageValue ParseImageSource(this StringSource source)
        {
            var url = source.ParseUri();

            if (url == null)
            {
                return source.ParseGradient() ?? source.ParseImageSet();
            }

            return url;
        }

        /// <summary>
        /// Parses an image-set function, where each option is an image (or a string with its
        /// URL) followed by an optional resolution and an optional type().
        /// </summary>
        internal static ICssImageValue ParseImageSet(this StringSource source)
        {
            var pos = source.Index;

            if (source.IsFunction(FunctionNames.ImageSet))
            {
                var options = new List<CssTupleValue>();

                while (true)
                {
                    var parts = new List<ICssValue>();
                    var image = source.ParseUri() ?? source.ParseGradient() ?? (ICssValue)ParseStringValue(source);

                    if (image is null)
                    {
                        break;
                    }

                    parts.Add(image);
                    source.SkipSpacesAndComments();
                    var resolution = source.ParseResolution();

                    if (resolution.HasValue)
                    {
                        parts.Add(resolution.Value);
                        source.SkipSpacesAndComments();
                    }

                    if (source.IsFunction(FunctionNames.Type))
                    {
                        var type = ParseStringValue(source);

                        if (type is null || source.SkipGetSkip() != Symbols.RoundBracketClose)
                        {
                            break;
                        }

                        parts.Add(new CssImageSetValue.TypeValue(type));
                    }

                    options.Add(new CssTupleValue(parts.ToArray()));
                    var c = source.SkipGetSkip();

                    if (c == Symbols.RoundBracketClose)
                    {
                        return new CssImageSetValue(options.ToArray());
                    }
                    else if (c != Symbols.Comma)
                    {
                        break;
                    }
                }
            }

            source.BackTo(pos);
            return null;
        }

        private static ICssValue ParseStringValue(StringSource source)
        {
            var str = source.ParseString();
            return str is not null ? new CssStringValue(str) : null;
        }

        /// <summary>
        /// Parse for a generic periodic [u, y, d, x] value.
        /// </summary>
        public static CssPeriodicValue<T> ParsePeriodic<T>(this StringSource source, Func<StringSource, T> converter)
            where T : ICssValue
        {
            var values = new List<T>(4);

            while (values.Count < 4)
            {
                var result = converter.Invoke(source);

                if (result == null)
                    break;

                values.Add(result);
                source.SkipSpacesAndComments();
            }

            return values.Count > 0 ? new CssPeriodicValue<T>(values.ToArray()) : null;
        }
    }
}
