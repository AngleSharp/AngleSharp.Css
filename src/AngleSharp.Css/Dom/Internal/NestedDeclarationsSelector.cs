#nullable disable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The selector of the declarations in a conditional group rule nested in a style rule
    /// (CSS Nesting 1, the nested declarations rule): it matches what the style rule matches,
    /// including the style rules it is nested in, with the same specificity.
    /// </summary>
    internal sealed class NestedDeclarationsSelector : ISelector
    {
        private readonly CssStyleRule _parent;

        public NestedDeclarationsSelector(CssStyleRule parent)
        {
            _parent = parent;
        }

        public String Text => "&";

        public Priority Specificity => _parent.Selector?.Specificity ?? Priority.Zero;

        public void Accept(ISelectorVisitor visitor)
        {
        }

        public Boolean Match(IElement element, IElement scope) => _parent.TryMatch(element, scope, out _);
    }
}
