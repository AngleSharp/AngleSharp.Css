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
                return source.ParseGridTemplate();
            }
        }

        sealed class GridTemplateAggregator : IValueConverter, IValueAggregator
        {
            private static readonly IValueConverter converter = Or(new GridTemplateConverter(), AssignInitial());

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Merge(ICssValue[] values)
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
                    return new GridTemplate(rows, cols, areas);
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
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
