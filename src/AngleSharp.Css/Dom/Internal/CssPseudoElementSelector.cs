#nullable enable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Matches the pseudo-element with the given name, e.g. `::placeholder`.
    /// </summary>
    sealed class CssPseudoElementSelector : ISelector
    {
        private readonly String _name;

        public CssPseudoElementSelector(String name)
        {
            _name = name;
        }

        public Priority Specificity => Priority.OneTag;

        public String Text => PseudoElementNames.Separator + CssUtilities.Escape(_name);

        public void Accept(ISelectorVisitor visitor) => visitor.PseudoElement(_name);

        public Boolean Match(IElement element, IElement? scope) => element.IsPseudo(_name);
    }
}
