namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Declarations;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System.Collections.Generic;
    using System.Linq;

    sealed class OutlineAggregator : IValueAggregator
    {
        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var width = properties.Where(m => m.Name == OutlineWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var style = properties.Where(m => m.Name == OutlineStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var color = properties.Where(m => m.Name == OutlineColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

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
                    new CssProperty(OutlineWidthDeclaration.Name, OutlineWidthDeclaration.Converter, OutlineWidthDeclaration.Flags, options.Options[0]),
                    new CssProperty(OutlineStyleDeclaration.Name, OutlineStyleDeclaration.Converter, OutlineStyleDeclaration.Flags, options.Options[1]),
                    new CssProperty(OutlineColorDeclaration.Name, OutlineColorDeclaration.Converter, OutlineColorDeclaration.Flags, options.Options[2]),
                };
            }

            return null;
        }
    }
}