namespace AngleSharp.Css.Dom
{
    using System;

    public interface ICssValue : IStyleFormattable
    {
        String CssText { get; }
    }
}
