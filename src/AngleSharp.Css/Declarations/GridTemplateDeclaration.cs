namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using static ValueConverters;

    static class GridTemplateDeclaration
    {
        public static readonly String Name = PropertyNames.GridTemplate;

        public static readonly String[] Longhands = new[]
        {
            PropertyNames.GridTemplateRows,
            PropertyNames.GridTemplateColumns,
            PropertyNames.GridTemplateAreas,
        };

        public static readonly IValueConverter Converter = new GridTemplateAggregator();

        public static readonly PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class GridTemplateConverter : IValueConverter
        {
            public ICssValue Convert(StringSource source)
            {
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

                }

                return null;
            }
        }

        sealed class GridTemplateAggregator : IValueConverter, IValueAggregator
        {
            private static readonly IValueConverter converter = Or(new GridTemplateConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Collect(ICssValue[] values)
            {
                var rows = values[0];
                var cols = values[1];
                var areas = values[2];

                if (cols == rows && cols == areas)
                {
                    return cols;
                }
                else if (cols != null || rows != null || areas != null)
                {
                    return new GridTemplate(cols, rows, areas);
                }

                return null;
            }

            public ICssValue[] Distribute(ICssValue value)
            {
                var template = value as GridTemplate;

                if (template != null)
                {
                    return new[] { template.TemplateRows, template.TemplateColumns, template.TemplateAreas };
                }
                else if (value is Identifier)
                {
                    return new[] { value, value, value };
                }

                return null;
            }
        }
    }
}
