namespace AngleSharp.Css.Dom;

using AngleSharp.Dom;

/// <summary>
/// A set of useful extensions for pseudo-elements.
/// </summary>
public static class PseudoElementExtensions
{
    /// <summary>
    /// Gets the host element of a pseudo-element.
    /// </summary>
    public static IElement? GetHost(this IPseudoElement pseudoElement)
    {
        return pseudoElement is PseudoElement pseudo ? pseudo.Host : null;
    }
}
