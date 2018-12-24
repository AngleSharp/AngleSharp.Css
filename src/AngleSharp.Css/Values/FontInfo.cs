namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    public sealed class FontInfo : ICssValue
    {
        private readonly ICssValue _fontFamilies;
        private readonly ICssValue _lineHeight;
        private readonly ICssValue _size;
        private readonly ICssValue _stretch;
        private readonly ICssValue _style;
        private readonly ICssValue _variant;
        private readonly ICssValue _weight;

        public FontInfo(ICssValue style, ICssValue variant, ICssValue weight, ICssValue stretch, ICssValue size, ICssValue lineHeight, ICssValue fontFamilies)
        {
            _style = style;
            _variant = variant;
            _weight = weight;
            _stretch = stretch;
            _size = size;
            _lineHeight = lineHeight;
            _fontFamilies = fontFamilies;
        }

        public ICssValue FontFamilies
        {
            get { return _fontFamilies; }
        }

        public ICssValue LineHeight
        {
            get { return _lineHeight; }
        }

        public ICssValue Size
        {
            get { return _size; }
        }

        public ICssValue Stretch
        {
            get { return _stretch; }
        }

        public ICssValue Style
        {
            get { return _style; }
        }

        public ICssValue Variant
        {
            get { return _variant; }
        }

        public ICssValue Weight
        {
            get { return _weight; }
        }

        public String CssText
        {
            get { return ToString(); }
        }

        public override String ToString()
        {
            var sb = StringBuilderPool.Obtain();

            if (_style != null)
            {
                if (sb.Length > 0) sb.Append(Symbols.Space);
                sb.Append(_style.CssText);
            }

            if (_variant != null)
            {
                if (sb.Length > 0) sb.Append(Symbols.Space);
                sb.Append(_variant.CssText);
            }

            if (_weight != null)
            {
                if (sb.Length > 0) sb.Append(Symbols.Space);
                sb.Append(_weight.CssText);
            }

            if (_stretch != null)
            {
                if (sb.Length > 0) sb.Append(Symbols.Space);
                sb.Append(_stretch.CssText);
            }

            if (_size != null)
            {
                if (sb.Length > 0) sb.Append(Symbols.Space);
                sb.Append(_size.CssText);
            }

            if (_lineHeight != null)
            {
                sb.Append(" / ");
                sb.Append(_lineHeight.CssText);
            }

            if (sb.Length > 0) sb.Append(Symbols.Space);
            sb.Append(_fontFamilies.CssText);
            return sb.ToPool();
        }
    }
}
