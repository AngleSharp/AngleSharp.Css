#nullable enable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The :fullscreen pseudo-class, which only the host knows to match: it forces it on the
    /// element it shows fullscreen (see ElementExtensions.SetPseudoClass).
    /// </summary>
    sealed class FullscreenPseudoClassSelector : ISelector
    {
        public Priority Specificity => Priority.OneClass;

        public String Text => PseudoClassNames.Separator + CssPseudoClassSelectorFactory.Fullscreen;

        public void Accept(ISelectorVisitor visitor) => visitor.PseudoClass(CssPseudoClassSelectorFactory.Fullscreen);

        public Boolean Match(IElement element, IElement? scope) => false;
    }
}
