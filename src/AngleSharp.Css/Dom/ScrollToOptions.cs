namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using System;

    [DomName("ScrollToOptions")]
    public class ScrollToOptions : ScrollOptions
    {
        [DomName("left")]
        public Double Left { get; set; }

        [DomName("top")]
        public Double Top { get; set; }
    }
}
