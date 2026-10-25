#nullable enable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Wraps a pseudo-class selector so that a caller-forced state (set via
    /// <see cref="AngleSharp.Dom.ElementExtensions.SetPseudoClass"/>) takes precedence over the
    /// wrapped selector's normal matching logic.
    /// </summary>
    sealed class ForcingPseudoClassSelector : ISelector
    {
        private readonly String _name;
        private readonly ISelector _inner;

        public ForcingPseudoClassSelector(String name, ISelector inner)
        {
            _name = name;
            _inner = inner;
        }

        public Priority Specificity => _inner.Specificity;

        public String Text => _inner.Text;

        public void Accept(ISelectorVisitor visitor) => _inner.Accept(visitor);

        public Boolean Match(IElement element, IElement? scope)
        {
            // in li:first-child::after the pseudo-class refers to the originating element
            if (element is PseudoElement pseudo && !PseudoElementSubjectVisitor.IsLegacyPseudoElement(_name))
            {
                element = pseudo.Host;
            }

            return PseudoClassStateStore.TryGet(element, _name, out var forced) ? forced : _inner.Match(element, scope);
        }
    }
}
