namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a @position-try CSS rule.
    /// </summary>
    [DomName("CSSPositionTryRule")]
    public interface ICssPositionTryRule : ICssRule
    {
        /// <summary>
        /// Gets the position try name.
        /// </summary>
        [DomName("name")]
        String Name { get; }

        /// <summary>
        /// Gets the style declarations.
        /// </summary>
        [DomName("style")]
        ICssStyleDeclaration Style { get; }
    }
}
