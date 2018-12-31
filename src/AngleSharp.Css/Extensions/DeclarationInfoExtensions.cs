namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    static class DeclarationInfoExtensions
    {
        public static ICssProperty[] CreateLonghands(this DeclarationInfo info, ICssValue value, Func<String, ICssValue, ICssProperty> createProperty)
        {
            var aggregator = info.Converter as IValueAggregator;
            var longhands = info.Longhands;

            if (value is ICssRawValue)
            {
                var child = new CssChildValue(value);
                var values = Enumerable.Repeat(child, longhands.Length).ToArray();
                return CreateProperties(longhands, values, createProperty);
            }

            return CreateProperties(longhands, aggregator?.Split(value), createProperty);
        }

        public static IEnumerable<String> GetMappings(this DeclarationInfo info)
        {
            return info.Longhands.Length > 0 ? info.Longhands : Enumerable.Repeat(info.Name, 1);
        }

        private static ICssProperty[] CreateProperties(String[] names, ICssValue[] values, Func<String, ICssValue, ICssProperty> createProperty)
        {
            if (values != null && values.Length == names.Length)
            {
                var properties = new ICssProperty[names.Length];

                for (var i = 0; i < names.Length; i++)
                {
                    properties[i] = createProperty(names[i], values[i]);
                }

                return properties;
            }

            return null;
        }
    }
}
