namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    static class GridDeclaration
    {
        public static readonly String Name = PropertyNames.Grid;

        public static readonly IValueConverter Converter = new GridAggregator();

        public static readonly ICssValue InitialValue = null;

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

                if (template is null)
                {
                    var rows = source.ParseTrackList() ?? source.ParseAutoTrackList();

                    if (rows is not null)
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

                                return new CssGridValue(rows, null, sizes, isDense);
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
                                return new CssGridValue(null, columns, sizes, isDense);
                            }
                        }

                        return new CssGridValue(null, null, sizes, isDense);
                    }
                }

                return template;
            }
        }

        sealed class GridAggregator : IValueConverter, IValueAggregator
        {
            private static readonly IValueConverter converter = new GridConverter();

            public ICssValue Convert(StringSource source) => converter.Convert(source);

            public ICssValue Merge(ICssValue[] values)
            {
                var templateRows = values[0];
                var templateColumns = values[1];
                var templateAreas = values[2];
                var autoRows = values[3];
                var autoColumns = values[4];
                var autoFlow = values[5];
                var rowGap = values[6];
                var columnGap = values[7];

                if (templateRows == templateColumns && templateRows == templateAreas)
                {
                    return templateRows;
                }
                else if (templateRows != null || templateColumns != null)
                {
                    if ((autoRows ?? autoColumns) is CssTupleValue tuple)
                    {
                        return new CssGridValue(templateRows, templateColumns, tuple.Items, autoFlow != null);
                    }

                    return new CssGridTemplateValue(templateRows, templateColumns, templateAreas);
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssGridTemplateValue gt)
                {
                    return new[]
                    {
                        gt.TemplateRows,
                        gt.TemplateColumns,
                        gt.TemplateAreas,
                        null, //new Identifier(CssKeywords.Auto),
                        null, //new Identifier(CssKeywords.Auto),
                        null, //new Identifier(CssKeywords.Row),
                        null, //Length.Zero,
                        null, //Length.Zero,
                        null, //new Identifier(CssKeywords.Normal),
                        null, //new Identifier(CssKeywords.Normal),
                    };
                }
                else if (value is CssGridValue grid)
                {
                    var dense = grid.Rows is not null ? CssKeywords.Row : CssKeywords.Column;
                    return new[]
                    {
                        grid.Rows,
                        grid.Columns,
                        null, //new Identifier(CssKeywords.None),
                        grid.Columns is not null ? new CssTupleValue(grid.Sizes) : null, //new Identifier(CssKeywords.Auto),
                        grid.Rows is not null ? new CssTupleValue(grid.Sizes) : null, //new Identifier(CssKeywords.Auto),
                        grid.IsDense ? new Identifier(dense) : null,
                        null, //Length.Zero,
                        null, //Length.Zero,
                        null, //new Identifier(CssKeywords.Normal),
                        null, //new Identifier(CssKeywords.Normal),
                    };
                }
                else if (value is Identifier)
                {
                    return new[]
                    {
                        value,
                        value,
                        value,
                        null, //new Identifier(CssKeywords.Auto),
                        null, //new Identifier(CssKeywords.Auto),
                        null, //new Identifier(CssKeywords.Row),
                        null, //Length.Zero,
                        null, //Length.Zero,
                        null, //new Identifier(CssKeywords.Normal),
                        null, //new Identifier(CssKeywords.Normal),
                    };
                }

                return null;
            }
        }
    }
}
