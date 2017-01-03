namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using System;

    [DomName("CSSPseudoElementList")]
    public interface ICssPseudoElementList
    {
        [DomName("length")]
        Int32 Length { get; }

        [DomName("item")]
        ICssPseudoElement this[Int32 index] { get; }

        [DomName("getByType")]
        ICssPseudoElement this[String type] { get; }
    }
}
