namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;

    [DomName("ScrollOptions")]
    public class ScrollOptions
    {
        [DomName("behavior")]
        public ScrollBehavior Behavior { get; set; } = ScrollBehavior.Auto;
    }
}
