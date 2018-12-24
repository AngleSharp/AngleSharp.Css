namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Declarations;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System.Collections.Generic;
    using System.Linq;

    sealed class ColumnsAggregator : IValueAggregator
    {
        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var width = properties.Where(m => m.Name == ColumnWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var count = properties.Where(m => m.Name == ColumnCountDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

            if (width != null || count != null)
            {
                return new OrderedOptions(new[] { width, count });
            }

            return null;
        }

        public IEnumerable<ICssProperty> Distribute(ICssValue value)
        {
            var options = value as OrderedOptions;

            if (options != null)
            {
                return new[]
                {
                    new CssProperty(ColumnWidthDeclaration.Name, ColumnWidthDeclaration.Converter, ColumnWidthDeclaration.Flags, options.Options[0]),
                    new CssProperty(ColumnCountDeclaration.Name, ColumnCountDeclaration.Converter, ColumnCountDeclaration.Flags, options.Options[1]),
                };
            }

            return null;
        }
    }
}