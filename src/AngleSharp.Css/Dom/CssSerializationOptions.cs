namespace AngleSharp.Css.Dom;

using System;

/// <summary>
/// Options for serializing a CSS stylesheet.
/// </summary>
public sealed class CssSerializationOptions
{
    /// <summary>
    /// Gets or sets whether parsed comments should be preserved in the output.
    /// </summary>
    public Boolean PreserveComments { get; set; }
}
