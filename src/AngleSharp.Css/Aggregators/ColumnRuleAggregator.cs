namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Declarations;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System.Collections.Generic;
    using System.Linq;

    sealed class ColumnRuleAggregator : IValueAggregator
    {
        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var color = properties.Where(m => m.Name == ColumnRuleColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var width = properties.Where(m => m.Name == ColumnRuleWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var style = properties.Where(m => m.Name == ColumnRuleStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

            if (color != null || width != null || style != null)
            {
                return new OrderedOptions(new[] { color, width, style });
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
                    new CssProperty(ColumnRuleColorDeclaration.Name, ColumnRuleColorDeclaration.Converter, ColumnRuleColorDeclaration.Flags, options.Options[0]),
                    new CssProperty(ColumnRuleWidthDeclaration.Name, ColumnRuleWidthDeclaration.Converter, ColumnRuleWidthDeclaration.Flags, options.Options[1]),
                    new CssProperty(ColumnRuleStyleDeclaration.Name, ColumnRuleStyleDeclaration.Converter, ColumnRuleStyleDeclaration.Flags, options.Options[2]),
                };
            }

            return null;
        }
    }
}