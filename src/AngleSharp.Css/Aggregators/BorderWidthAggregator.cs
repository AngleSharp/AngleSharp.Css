namespace AngleSharp.Css.Aggregators
{
    using AngleSharp.Css.Declarations;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using System.Collections.Generic;
    using System.Linq;

    sealed class BorderWidthAggregator : IValueAggregator
    {
        public ICssValue Collect(IEnumerable<ICssProperty> properties)
        {
            var top = properties.Where(m => m.Name == BorderTopWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var right = properties.Where(m => m.Name == BorderRightWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var bottom = properties.Where(m => m.Name == BorderBottomWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();
            var left = properties.Where(m => m.Name == BorderLeftWidthDeclaration.Name).Select(m => m.RawValue).FirstOrDefault();

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
                    new CssProperty(BorderTopWidthDeclaration.Name, BorderTopWidthDeclaration.Converter, BorderTopWidthDeclaration.Flags, periodic.Top),
                    new CssProperty(BorderRightWidthDeclaration.Name, BorderRightWidthDeclaration.Converter, BorderRightWidthDeclaration.Flags, periodic.Right),
                    new CssProperty(BorderBottomWidthDeclaration.Name, BorderBottomWidthDeclaration.Converter, BorderBottomWidthDeclaration.Flags, periodic.Bottom),
                    new CssProperty(BorderLeftWidthDeclaration.Name, BorderLeftWidthDeclaration.Converter, BorderLeftWidthDeclaration.Flags, periodic.Left),
                };
            }

            return null;
        }
    }
}