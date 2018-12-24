namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    abstract class BoxBaseAggregator : IValueAggregator
    {
        private readonly String _topName;
        private readonly String _rightName;
        private readonly String _bottomName;
        private readonly String _leftName;
        private readonly IValueConverter _converter;
        private readonly PropertyFlags _flags;

        public BoxBaseAggregator(String topName, String rightName, String bottomName, String leftName, IValueConverter converter, PropertyFlags flags)
        {
            _topName = topName;
            _rightName = rightName;
            _bottomName = bottomName;
            _leftName = leftName;
            _converter = converter;
            _flags = flags;
        }

        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var top = properties.Where(m => m.Name == _topName).Select(m => m.RawValue).FirstOrDefault();
            var right = properties.Where(m => m.Name == _rightName).Select(m => m.RawValue).FirstOrDefault();
            var bottom = properties.Where(m => m.Name == _bottomName).Select(m => m.RawValue).FirstOrDefault();
            var left = properties.Where(m => m.Name == _leftName).Select(m => m.RawValue).FirstOrDefault();

            if (top != null && right != null && bottom != null && left != null)
            {
                return new Periodic<ICssValue>(new[] { top, right, bottom, left });
            }

            return null;
        }

        public IEnumerable<ICssProperty> Distribute(ICssValue value)
        {
            var period = value as Periodic<ICssValue>;

            if (period != null)
            {
                return CreateLonghands(period.Top, period.Right, period.Bottom, period.Left);
            }

            return CreateLonghands(null, null, null, null);
        }

        private IEnumerable<ICssProperty> CreateLonghands(ICssValue top, ICssValue right, ICssValue bottom, ICssValue left)
        {
            return new[]
            {
                new CssProperty(_topName, _converter, _flags, top),
                new CssProperty(_rightName, _converter, _flags, right),
                new CssProperty(_bottomName, _converter, _flags, bottom),
                new CssProperty(_leftName, _converter, _flags, left),
            };
        }
    }
}
