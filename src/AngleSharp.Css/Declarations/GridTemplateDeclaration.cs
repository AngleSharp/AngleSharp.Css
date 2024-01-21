namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;

    static class GridTemplateDeclaration
    {
        public static readonly String Name = PropertyNames.GridTemplate;

        public static readonly String[] Longhands =
        [
            PropertyNames.GridTemplateRows,
            PropertyNames.GridTemplateColumns,
            PropertyNames.GridTemplateAreas,
        ];

        public static readonly IValueConverter Converter = new GridTemplateAggregator();

        public static readonly ICssValue InitialValue = null;

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
            private static readonly IValueConverter converter = new GridTemplateConverter();

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
                    if (cols is not CssReferenceValue && rows is not CssReferenceValue && areas is not CssReferenceValue)
                    {
                        return new CssGridTemplateValue(rows, cols, areas);
                    }
                }

                return null;
            }

            public ICssValue[] Split(ICssValue value)
            {
                if (value is CssGridTemplateValue template)
                {
                    return [template.TemplateRows, template.TemplateColumns, template.TemplateAreas];
                }
                else if (value is CssIdentifierValue)
                {
                    return [value, value, value];
                }

                return null;
            }
        }
    }
}
