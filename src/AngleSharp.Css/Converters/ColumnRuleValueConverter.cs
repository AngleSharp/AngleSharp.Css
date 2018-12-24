namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;

    sealed class ColumnRuleValueConverter : IValueConverter
    {
        public ICssValue Convert(StringSource source)
        {
            var color = default(ICssValue);
            var width = default(ICssValue);
            var style = default(ICssValue);
            var pos = 0;

            do
            {
                pos = source.Index;

                if (color == null)
                {
                    color = ColorParser.ParseColor(source);
                    source.SkipSpacesAndComments();
                }

                if (width == null)
                {
                    width = source.ParseLineWidth();
                    source.SkipSpacesAndComments();
                }

                if (style == null)
                {
                    style = source.ParseConstant(Map.LineStyles);
                    source.SkipSpacesAndComments();
                }
            }
            while (pos != source.Index);

            return new OrderedOptions(new[] { color, width, style });
        }
    }
}
