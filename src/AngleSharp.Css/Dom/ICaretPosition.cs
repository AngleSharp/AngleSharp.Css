namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;

    [DomName("CaretPosition")]
    [DomNoInterfaceObject]
    public interface ICaretPosition
    {
        [DomName("offsetNode")]
        INode Node { get; }

        [DomName("offset")]
        Int32 Offset { get; }
    }
}
