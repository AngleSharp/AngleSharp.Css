namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Declarations;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System.Collections.Generic;
    using System.Linq;

    sealed class BorderLeftAggregator : IValueAggregator
    {
        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var width = properties.Where(m => m.Name == BorderLeftWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var style = properties.Where(m => m.Name == BorderLeftStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var color = properties.Where(m => m.Name == BorderLeftColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

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
                    new CssProperty(BorderLeftWidthDeclaration.Name, BorderLeftWidthDeclaration.Converter, BorderLeftWidthDeclaration.Flags, options.Options[0]),
                    new CssProperty(BorderLeftStyleDeclaration.Name, BorderLeftStyleDeclaration.Converter, BorderLeftStyleDeclaration.Flags, options.Options[1]),
                    new CssProperty(BorderLeftColorDeclaration.Name, BorderLeftColorDeclaration.Converter, BorderLeftColorDeclaration.Flags, options.Options[2]),
                };
            }

            return null;
        }
    }
}