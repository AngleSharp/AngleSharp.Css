namespace AngleSharp.Css.Values;

using System;

/// <summary>
/// A compute context that knows the references for resolving the relative lengths of a property.
/// </summary>
interface IRelativeComputeContext
{
    /// <summary>
    /// Gets the font size (in px) em resolves against, if known.
    /// </summary>
    Double? EmBase { get; }

    /// <summary>
    /// Gets the font size (in px) of the root element rem resolves against, if known.
    /// </summary>
    Double? RootFontSize { get; }

    /// <summary>
    /// Gets if percentages refer to a box that is only known during layout, i.e., if they
    /// must remain percentages in the computed value.
    /// </summary>
    Boolean KeepsPercentages { get; }

    /// <summary>
    /// Gets the length (in px) percentages resolve against, if known.
    /// </summary>
    Double? PercentageBasis { get; }
}
