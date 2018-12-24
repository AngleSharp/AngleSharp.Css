namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Declarations;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System.Collections.Generic;
    using System.Linq;

    sealed class BorderColorAggregator : IValueAggregator
    {
        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var top = properties.Where(m => m.Name == BorderTopColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var right = properties.Where(m => m.Name == BorderRightColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var bottom = properties.Where(m => m.Name == BorderBottomColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var left = properties.Where(m => m.Name == BorderLeftColorDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

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
                    new CssProperty(BorderTopColorDeclaration.Name, BorderTopColorDeclaration.Converter, BorderTopColorDeclaration.Flags, periodic.Top),
                    new CssProperty(BorderRightColorDeclaration.Name, BorderRightColorDeclaration.Converter, BorderRightColorDeclaration.Flags, periodic.Right),
                    new CssProperty(BorderBottomColorDeclaration.Name, BorderBottomColorDeclaration.Converter, BorderBottomColorDeclaration.Flags, periodic.Bottom),
                    new CssProperty(BorderLeftColorDeclaration.Name, BorderLeftColorDeclaration.Converter, BorderLeftColorDeclaration.Flags, periodic.Left),
                };
            }

            return null;
        }
    }
}