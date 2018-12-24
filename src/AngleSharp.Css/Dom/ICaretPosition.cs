namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Defines the CaretPosition DOM interface.
    /// </summary>
    [DomName("CaretPosition")]
    [DomNoInterfaceObject]
    public interface ICaretPosition
    {
        /// <summary>
        /// Gets the corresponding (potentially text) node.
        /// </summary>
        [DomName("offsetNode")]
        INode Node { get; }

        /// <summary>
        /// Gets the character offset of the caret within the node.
        /// </summary>
        [DomName("offset")]
        Int32 Offset { get; }
    }
}
