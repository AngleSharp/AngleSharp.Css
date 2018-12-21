namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class BorderRadiusAggregator : IValueAggregator
    {
        private static String Left(String parts)
        {
            var idx = parts.IndexOf('/');
            return idx != -1 ? parts.Substring(0, idx) : parts;
        }

        private static String Right(String parts)
        {
            var idx = parts.IndexOf('/');
            return idx != -1 ? parts.Substring(idx + 1) : String.Empty;
        }

        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var topLeft = properties.Where(m => m.Name == PropertyNames.BorderTopLeftRadius).Select(m => m.Value).FirstOrDefault();
            var topRight = properties.Where(m => m.Name == PropertyNames.BorderTopRightRadius).Select(m => m.Value).FirstOrDefault();
            var bottomRight = properties.Where(m => m.Name == PropertyNames.BorderBottomRightRadius).Select(m => m.Value).FirstOrDefault();
            var bottomLeft = properties.Where(m => m.Name == PropertyNames.BorderBottomLeftRadius).Select(m => m.Value).FirstOrDefault();
            var valueLeft = $"{Left(topLeft)} {Left(topRight)} {Left(bottomRight)} {Left(bottomLeft)}".Trim();
            var valueRight = $"{Right(topLeft)} {Right(topRight)} {Right(bottomRight)} {Right(bottomLeft)}".Trim();
            var converter = new BorderRadiusValueConverter();
            return converter.Convert(valueRight.Length > 0 ? $"{valueLeft} / {valueRight}" : valueLeft);
        }

        public IEnumerable<ICssProperty> Distribute(ICssValue value)
        {
            return null;
        }
    }
}