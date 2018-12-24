namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Defines the DOM pseudo element interface.
    /// </summary>
    [DomName("CSSPseudoElement")]
    public interface ICssPseudoElement : IEventTarget
    {
        /// <summary>
        /// Gets the type of the pseudo element.
        /// </summary>
        [DomName("type")]
        String Type { get; }

        /// <summary>
        /// Gets the element's associated style.
        /// </summary>
        [DomName("style")]
        ICssStyleDeclaration Style { get; }
    }
}
