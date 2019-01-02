namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using static ValueConverters;

    static class GridDeclaration
    {
        public static readonly String Name = PropertyNames.Grid;

        public static readonly IValueConverter Converter = new GridAggregator();

        public static readonly String[] Longhands = new[]
        {
            PropertyNames.GridTemplateRows,
            PropertyNames.GridTemplateColumns,
            PropertyNames.GridTemplateAreas,
            PropertyNames.GridAutoRows,
            PropertyNames.GridAutoColumns,
            PropertyNames.GridAutoFlow,
            PropertyNames.GridRowGap,
            PropertyNames.GridColumnGap,
            PropertyNames.RowGap,
            PropertyNames.ColumnGap,
        };

        public static readonly PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class GridConverter : IValueConverter
        {
            public ICssValue Convert(StringSource source)
            {
                var template = source.ParseGridTemplate();

                if (template == null)
                {
                    var rows = source.ParseTrackList() ?? source.ParseAutoTrackList();

                    if (rows != null)
                    {
                        if (source.SkipSpacesAndComments() == Symbols.Solidus)
                        {
                            source.SkipCurrentAndSpaces();

                            if (source.IsIdentifier(CssKeywords.AutoFlow))
                            {
                                source.SkipSpacesAndComments();
                                var sizes = new List<ICssValue>();
                                var isDense = source.IsIdentifier(CssKeywords.Dense);
                                source.SkipSpacesAndComments();

                                while (!source.IsDone)
                                {
                                    var size = source.ParseTrackSize();

                                    if (size == null)
                                    {
                                        break;
                                    }

                                    source.SkipSpacesAndComments();
                                    sizes.Add(size);
                                }

                                //rows, sizes, isDense
                            }
                        }
                    }
                    else if (source.IsIdentifier(CssKeywords.AutoFlow))
                    {
                        source.SkipSpacesAndComments();
                        var sizes = new List<ICssValue>();
                        var isDense = source.IsIdentifier(CssKeywords.Dense);
                        source.SkipSpacesAndComments();

                        while (!source.IsDone)
                        {
                            var size = source.ParseTrackSize();

                            if (size == null)
                            {
                                break;
                            }

                            source.SkipSpacesAndComments();
                            sizes.Add(size);
                        }

                        if (source.Current == Symbols.Solidus)
                        {
                            source.SkipCurrentAndSpaces();
                            var columns = source.ParseTrackList() ?? source.ParseAutoTrackList();

                            if (columns != null)
                            {
                                //columns, sizes, isDense
                            }
                        }
                    }
                }

                return template;
            }
        }

        sealed class GridAggregator : IValueConverter, IValueAggregator
        {
            private static readonly IValueConverter converter = Or(new GridConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
            {
                var templateRows = values[0];
                var templateColumns = values[1];
                var templateAreas = values[2];

                if (templateRows != null && templateColumns != null)
                {
                    return new GridTemplate(templateRows, templateColumns, templateAreas);
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is GridTemplate)
                {
                    var gt = (GridTemplate)value;

                    return new[]
                    {
                        gt.TemplateRows,
                        gt.TemplateColumns,
                        gt.TemplateAreas,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                    };
                }

                return null;
            }
        }
    }
}
