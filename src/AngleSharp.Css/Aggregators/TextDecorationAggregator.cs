namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Declarations;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System.Collections.Generic;
    using System.Linq;

    sealed class TextDecorationAggregator : IValueAggregator
    {
        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var color = properties.Where(m => m.Name == TextDecorationColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var style = properties.Where(m => m.Name == TextDecorationStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var line = properties.Where(m => m.Name == TextDecorationLineDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

            if (color != null || style != null || line != null)
            {
                return new OrderedOptions(new[] { color, style, line });
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
                    new CssProperty(TextDecorationColorDeclaration.Name, TextDecorationColorDeclaration.Converter, TextDecorationColorDeclaration.Flags, options.Options[0]),
                    new CssProperty(TextDecorationStyleDeclaration.Name, TextDecorationStyleDeclaration.Converter, TextDecorationStyleDeclaration.Flags, options.Options[1]),
                    new CssProperty(TextDecorationLineDeclaration.Name, TextDecorationLineDeclaration.Converter, TextDecorationLineDeclaration.Flags, options.Options[2]),
                };
            }

            return null;
        }
    }
}