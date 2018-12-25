namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static ValueConverters;

    static class PaddingDeclaration
    {
        public static String Name = PropertyNames.Padding;

        public static IValueConverter Converter = new PaddingAggregator();

        public static PropertyFlags Flags = PropertyFlags.Shorthand;

        public static String[] Longhands = new[]
        {
            PropertyNames.PaddingBottom,
            PropertyNames.PaddingLeft,
            PropertyNames.PaddingTop,
            PropertyNames.PaddingRight,
        };

        sealed class PaddingAggregator : IValueAggregator, IValueConverter
        {
            private static readonly IValueConverter converter = Or(LengthOrPercentConverter.Periodic(), AssignInitial(Length.Zero));

            public ICssValue Convert(StringSource source)
            {
                return converter.Convert(source);
            }

            public ICssValue Collect(IEnumerable<ICssProperty> properties)
            {
                var top = properties.Where(m => m.Name == PaddingTopDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var right = properties.Where(m => m.Name == PaddingRightDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var bottom = properties.Where(m => m.Name == PaddingBottomDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
                var left = properties.Where(m => m.Name == PaddingLeftDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

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
                    new CssProperty(PaddingTopDeclaration.Name, PaddingTopDeclaration.Converter, PaddingTopDeclaration.Flags, top),
                    new CssProperty(PaddingRightDeclaration.Name, PaddingRightDeclaration.Converter, PaddingRightDeclaration.Flags, right),
                    new CssProperty(PaddingBottomDeclaration.Name, PaddingBottomDeclaration.Converter, PaddingBottomDeclaration.Flags, bottom),
                    new CssProperty(PaddingLeftDeclaration.Name, PaddingLeftDeclaration.Converter, PaddingLeftDeclaration.Flags, left),
                };
            }
        }
    }
}
