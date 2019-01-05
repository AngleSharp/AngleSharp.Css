namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a list of pseudo elements.
    /// </summary>
    [DomName("CSSPseudoElementList")]
    public interface ICssPseudoElementList
    {
        /// <summary>
        /// Gets the number of pseudo elements in the list.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }

        /// <summary>
        /// Gets the pseudo element at the given index.
        /// </summary>
        /// <param name="index">The 0-based index.</param>
        /// <returns>The pseudo element, if any.</returns>
        [DomName("item")]
        ICssPseudoElement this[Int32 index] { get; }

        /// <summary>
        /// Gets the pseudo element of the given type.
        /// </summary>
        /// <param name="type">The type of pseudo element.</param>
        /// <returns>The pseudo element, if any.</returns>
        [DomName("getByType")]
        ICssPseudoElement this[String type] { get; }
    }
}
