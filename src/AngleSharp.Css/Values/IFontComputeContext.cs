namespace AngleSharp.Css.Values;

using System;

/// <summary>
/// A compute context that knows the font sizes relevant for resolving relative lengths of an element.
/// </summary>
interface IFontComputeContext
{
    /// <summary>
    /// Gets the computed font size (in px) of the parent element.
    /// </summary>
    Double ParentFontSize { get; }

    /// <summary>
    /// Gets the computed font size (in px) of the element itself.
    /// </summary>
    Double FontSize { get; }

    /// <summary>
    /// Gets the computed font size (in px) of the root element.
    /// </summary>
    Double RootFontSize { get; }
}
