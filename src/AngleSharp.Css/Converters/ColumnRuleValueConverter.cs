namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    sealed class ColumnRuleValueConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var color = default(Color?);
            var width = default(Length?);
            var style = default(LineStyle?);
            var pos = 0;

            do
            {
                pos = source.Index;

                if (!color.HasValue)
                {
                    color = ColorParser.ParseColor(source);
                    source.SkipSpacesAndComments();
                }

                if (!width.HasValue)
                {
                    width = source.ParseLineWidth();
                    source.SkipSpacesAndComments();
                }

                if (!style.HasValue)
                {
                    style = source.ParseConstant(Map.LineStyles);
                    source.SkipSpacesAndComments();
                }
            }
            while (pos != source.Index);

            return new ColumnRuleValue(color, width, style);
        }

        sealed class ColumnRuleValue : ICssValue
        {
            private readonly Color? _color;
            private readonly Length? _length;
            private readonly LineStyle? _lineStyle;

            public ColumnRuleValue(Color? color, Length? length, LineStyle? lineStyle)
            {
                _color = color;
                _length = length;
                _lineStyle = lineStyle;
            }

            public Color RuleColor
            {
                get { return _color ?? Color.Black; }
            }

            public Length RuleWidth
            {
                get { return _length ?? Length.Zero; }
            }

            public LineStyle RuleStyle
            {
                get { return _lineStyle ?? LineStyle.None; }
            }

            public String CssText
            {
                get
                {
                    var sb = StringBuilderPool.Obtain();

                    if (_color.HasValue)
                    {
                        if (sb.Length > 0) sb.Append(' ');
                        sb.Append(_color.ToString());
                    }

                    if (_length.HasValue)
                    {
                        if (sb.Length > 0) sb.Append(' ');
                        sb.Append(_length.ToString());
                    }

                    if (_lineStyle.HasValue)
                    {
                        if (sb.Length > 0) sb.Append(' ');
                        sb.Append(_lineStyle.Value.ToString(Map.LineStyles));
                    }

                    return sb.ToPool();
                }
            }
        }
    }
}
