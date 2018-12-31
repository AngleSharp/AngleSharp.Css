namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ValueConverters;

    static class GridTemplateDeclaration
    {
        public static readonly String Name = PropertyNames.GridTemplate;

        public static readonly String[] Longhands = new[]
        {
            PropertyNames.GridTemplateAreas,
            PropertyNames.GridTemplateColumns,
            PropertyNames.GridTemplateRows,
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

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var cols = properties.Where(m => m.Name == GridTemplateColumnsDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var rows = properties.Where(m => m.Name == GridTemplateRowsDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var areas = properties.Where(m => m.Name == GridTemplateAreasDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

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

            public IEnumerable<ICssProperty> Distribute(ICssValue value)
            {
                var template = value as GridTemplate;

                if (template != null)
                {
                    return CreateProperties(template.TemplateColumns, template.TemplateRows, template.TemplateAreas);
                }
                else if (value is Identifier)
                {
                    return CreateProperties(value, value, value);
                }

                return null;
            }

            private IEnumerable<ICssProperty> CreateProperties(ICssValue columns, ICssValue rows, ICssValue areas)
            {
                return new[]
                {
                    new CssProperty(GridTemplateColumnsDeclaration.Name, GridTemplateColumnsDeclaration.Converter, GridTemplateColumnsDeclaration.Flags, columns),
                    new CssProperty(GridTemplateRowsDeclaration.Name, GridTemplateRowsDeclaration.Converter, GridTemplateRowsDeclaration.Flags, rows),
                    new CssProperty(GridTemplateAreasDeclaration.Name, GridTemplateAreasDeclaration.Converter, GridTemplateAreasDeclaration.Flags, areas),
                };
            }
        }
    }
}
