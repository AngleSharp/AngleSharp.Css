namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ValueConverters;

    static class FlexFlowDeclaration
    {
        public static String Name = PropertyNames.FlexFlow;

        public static String[] Longhands = new[]
        {
            PropertyNames.FlexDirection,
            PropertyNames.FlexWrap,
        };

        public static IValueConverter Converter = new FlexFlowAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        sealed class FlexFlowAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = WithAny(
                FlexDirectionConverter,
                FlexWrapConverter);

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var direction = properties.Where(m => m.Name == FlexDirectionDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var wrap = properties.Where(m => m.Name == FlexWrapDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

                if (direction != null || wrap != null)
                {
                    return new CssTupleValue(new[] { direction, wrap });
                }

                return null;
            }

            public IEnumerable<ICssProperty> Distribute(ICssValue value)
            {
                var options = value as CssTupleValue;

                if (options != null)
                {
                    return new[]
                    {
                        new CssProperty(FlexDirectionDeclaration.Name, FlexDirectionDeclaration.Converter, FlexDirectionDeclaration.Flags, options.Items[0]),
                        new CssProperty(FlexWrapDeclaration.Name, FlexWrapDeclaration.Converter, FlexWrapDeclaration.Flags, options.Items[1]),
                    };
                }

                return null;
            }
        }
    }
}
