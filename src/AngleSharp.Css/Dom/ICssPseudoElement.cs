namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;

    [DomName("CSSPseudoElement")]
    public interface ICssPseudoElement : IEventTarget
    {
        [DomName("type")]
        String Type { get; }

        [DomName("style")]
        ICssStyleDeclaration Style { get; }
    }
}
