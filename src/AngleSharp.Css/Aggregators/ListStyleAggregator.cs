namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Dom;
    using System.Collections.Generic;
    using System.Linq;

    sealed class ListStyleAggregator : IValueAggregator
    {
        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var image = properties.Where(m => m.Name == PropertyNames.ListStyleImage).Select(m => m.Value).FirstOrDefault();
            var position = properties.Where(m => m.Name == PropertyNames.ListStylePosition).Select(m => m.Value).FirstOrDefault();
            var type = properties.Where(m => m.Name == PropertyNames.ListStyleType).Select(m => m.Value).FirstOrDefault();
            return null;
        }

        public IEnumerable<ICssProperty> Distribute(ICssValue value)
        {
            return null;
        }
    }
}