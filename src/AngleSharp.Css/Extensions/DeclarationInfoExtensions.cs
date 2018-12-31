namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    static class DeclarationInfoExtensions
    {
        public static IEnumerable<ICssProperty> GetLonghands(this DeclarationInfo info, ICssValue value)
        {
            var aggregator = info.Converter as IValueAggregator;
            return aggregator?.Distribute(value);
        }

        public static IEnumerable<String> GetMappings(this DeclarationInfo info)
        {
            return info.Longhands.Length > 0 ? info.Longhands : Enumerable.Repeat(info.Name, 1);
        }
    }
}
