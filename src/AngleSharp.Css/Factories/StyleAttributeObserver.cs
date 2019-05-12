namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Svg.Dom;
    using AngleSharp.Text;
    using System;

    sealed class StyleAttributeObserver : IAttributeObserver
    {
        void IAttributeObserver.NotifyChange(IElement host, String name, String value)
        {
            if (name.Is(AttributeNames.Style) && (host is IHtmlElement || host is ISvgElement))
            {
                host.UpdateStyle(value);
            }
        }
    }
}
