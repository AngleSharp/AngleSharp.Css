namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ValueConverters;

    static class FlexDeclaration
    {
        public static String Name = PropertyNames.Flex;

        public static String[] Longhands = new[]
        {
            PropertyNames.FlexGrow,
            PropertyNames.FlexShrink,
            PropertyNames.FlexBasis,
        };

        public static IValueConverter Converter = new FlexAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class FlexAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(None, WithAny(
                FlexGrowDeclaration.Converter,
                FlexShrinkDeclaration.Converter,
                FlexBasisDeclaration.Converter));

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var grow = properties.Where(m => m.Name == FlexGrowDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var shrink = properties.Where(m => m.Name == FlexShrinkDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var basis = properties.Where(m => m.Name == FlexBasisDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

                if (grow != null || shrink != null || basis != null)
                {
                    return new OrderedOptions(new[] { grow, shrink, basis });
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
                        new CssProperty(FlexGrowDeclaration.Name, FlexGrowDeclaration.Converter, FlexGrowDeclaration.Flags, options.Options[0]),
                        new CssProperty(FlexShrinkDeclaration.Name, FlexShrinkDeclaration.Converter, FlexShrinkDeclaration.Flags, options.Options[1]),
                        new CssProperty(FlexBasisDeclaration.Name, FlexBasisDeclaration.Converter, FlexBasisDeclaration.Flags, options.Options[2]),
                    };
                }

                return null;
            }
        }
    }
}
