namespace AngleSharp.Css.Extensions
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// A set of useful extension methods for the Element class.
    /// </summary>
    [DomExposed("Element")]
    public static class ElementExtensions
    {
        /// <summary>
        /// Creates a pseudo element for the current element.
        /// </summary>
        /// <param name="pseudoElement">
        /// The element to create (e.g. ::after).
        /// </param>
        /// <returns>The created element or null, if not possible.</returns>
        [DomName("pseudo")]
        public static IPseudoElement Pseudo(this IElement element, String pseudoElement)
        {
            var factory = element.Owner?.Context.GetService<IPseudoElementFactory>();
            return factory?.Create(element, pseudoElement);
        }
    }
}
