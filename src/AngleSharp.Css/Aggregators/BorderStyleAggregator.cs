namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Declarations;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System.Collections.Generic;
    using System.Linq;

    sealed class BorderStyleAggregator : IValueAggregator
    {
        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var top = properties.Where(m => m.Name == BorderTopStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var right = properties.Where(m => m.Name == BorderRightStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var bottom = properties.Where(m => m.Name == BorderBottomStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var left = properties.Where(m => m.Name == BorderLeftStyleDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

            if (top != null && right != null && bottom != null && left != null)
            {
                return new Periodic<ICssValue>(new[] { top, right, bottom, left });
            }

            return null;
        }

        public IEnumerable<ICssProperty> Distribute(ICssValue value)
        {
            var periodic = value as Periodic<ICssValue>;

            if (periodic != null)
            {
                return new[]
                {
                    new CssProperty(BorderTopStyleDeclaration.Name, BorderTopStyleDeclaration.Converter, BorderTopStyleDeclaration.Flags, periodic.Top),
                    new CssProperty(BorderRightStyleDeclaration.Name, BorderRightStyleDeclaration.Converter, BorderRightStyleDeclaration.Flags, periodic.Right),
                    new CssProperty(BorderBottomStyleDeclaration.Name, BorderBottomStyleDeclaration.Converter, BorderBottomStyleDeclaration.Flags, periodic.Bottom),
                    new CssProperty(BorderLeftStyleDeclaration.Name, BorderLeftStyleDeclaration.Converter, BorderLeftStyleDeclaration.Flags, periodic.Left),
                };
            }

            return null;
        }
    }
}