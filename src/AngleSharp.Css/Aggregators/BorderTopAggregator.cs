namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Declarations;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System.Collections.Generic;
    using System.Linq;

    sealed class BorderTopAggregator : IValueAggregator
    {
        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var width = properties.Where(m => m.Name == BorderTopWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var style = properties.Where(m => m.Name == BorderTopStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var color = properties.Where(m => m.Name == BorderTopColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

            if (width != null || style != null || color != null)
            {
                return new OrderedOptions(new[] { width, style, color });
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
                    new CssProperty(BorderTopWidthDeclaration.Name, BorderTopWidthDeclaration.Converter, BorderTopWidthDeclaration.Flags, options.Options[0]),
                    new CssProperty(BorderTopStyleDeclaration.Name, BorderTopStyleDeclaration.Converter, BorderTopStyleDeclaration.Flags, options.Options[1]),
                    new CssProperty(BorderTopColorDeclaration.Name, BorderTopColorDeclaration.Converter, BorderTopColorDeclaration.Flags, options.Options[2]),
                };
            }

            return null;
        }
    }
}