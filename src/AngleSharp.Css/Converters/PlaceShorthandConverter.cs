#nullable disable
namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Converts the place-* shorthands: <c>&lt;align&gt; &lt;justify&gt;?</c>, where a single value
    /// sets both longhands (or the fallback, if the justify longhand does not take it).
    /// </summary>
    sealed class PlaceShorthandConverter : IValueConverter
    {
        private readonly IValueConverter _align;
        private readonly IValueConverter _justify;
        private readonly String _fallback;

        public PlaceShorthandConverter(IValueConverter align, IValueConverter justify, String fallback)
        {
            _align = align;
            _justify = justify;
            _fallback = fallback;
        }

        public ICssValue Convert(StringSource source)
        {
            var align = _align.Convert(source);

            if (align is null)
            {
                return null;
            }

            source.SkipSpacesAndComments();
            var justify = source.IsDone ? null : _justify.Convert(source);

            return new CssTupleValue(new[] { align, justify ?? GetJustify(align) });
        }

        /// <summary>
        /// The justify longhand the shorthand sets for just the given align value.
        /// </summary>
        public ICssValue GetJustify(ICssValue align)
        {
            var justify = align is null ? null : _justify.Convert(new StringSource(align.CssText));

            if (justify is null && _fallback is not null)
            {
                justify = _justify.Convert(new StringSource(_fallback));
            }

            return justify;
        }

        /// <summary>
        /// Merges the longhands into the shorthand's value, leaving out the justify value if
        /// the align value alone sets it (e.g. place-self: end, not end end).
        /// </summary>
        public ICssValue Merge(ICssValue align, ICssValue justify)
        {
            if (align is null && justify is null)
            {
                return null;
            }

            if (align is not null && justify is not null && justify.CssText == GetJustify(align)?.CssText)
            {
                justify = null;
            }

            return new CssTupleValue(new[] { align, justify });
        }

        /// <summary>
        /// Splits the shorthand's value into the longhands.
        /// </summary>
        public ICssValue[] Split(ICssValue value)
        {
            if (value is CssTupleValue options)
            {
                var align = options.Items[0];
                return new[] { align, options.Items.Length > 1 && options.Items[1] is not null ? options.Items[1] : GetJustify(align) };
            }

            return null;
        }
    }
}
