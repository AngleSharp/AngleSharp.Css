namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Declarations;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System.Collections.Generic;
    using System.Linq;

    sealed class ListStyleAggregator : IValueAggregator
    {
        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var type = properties.Where(m => m.Name == ListStyleTypeDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var position = properties.Where(m => m.Name == ListStylePositionDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var image = properties.Where(m => m.Name == ListStyleImageDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

            if (type != null || position != null || image != null)
            {
                return new OrderedOptions(new[] { type, position, image });
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
                    new CssProperty(ListStyleTypeDeclaration.Name, ListStyleTypeDeclaration.Converter, ListStyleTypeDeclaration.Flags, options.Options[0]),
                    new CssProperty(ListStylePositionDeclaration.Name, ListStylePositionDeclaration.Converter, ListStylePositionDeclaration.Flags, options.Options[1]),
                    new CssProperty(ListStyleImageDeclaration.Name, ListStyleImageDeclaration.Converter, ListStyleImageDeclaration.Flags, options.Options[2]),
                };
            }

            return null;
        }
    }
}