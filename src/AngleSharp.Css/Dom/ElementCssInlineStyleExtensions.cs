namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Used to mark elements that may have inline style,
    /// usually set and defined over an attribute.
    /// </summary>
    [DomExposed("HTMLElement")]
    [DomExposed("SVGElement")]
    public static class ElementCssInlineStyleExtensions
    {
        private static readonly ConditionalWeakTable<IElement, ICssStyleDeclaration> _styles = new ConditionalWeakTable<IElement, ICssStyleDeclaration>();

        /// <summary>
        /// Gets the style declaration of an element.
        /// </summary>
        [DomName("style")]
        [DomAccessor(Accessors.Getter)]
        public static ICssStyleDeclaration GetStyle(this IElement element)
        {
            return _styles.GetValue(element, CreateStyle);
        }

        /// <summary>
        /// Sets the style declaration of an element.
        /// </summary>
        [DomName("style")]
        [DomAccessor(Accessors.Setter)]
        public static void SetStyle(this IElement element, String value)
        {
            element.SetAttribute(AttributeNames.Style, value);
        }

        internal static void UpdateStyle(this IElement element, String value)
        {
            var style = default(ICssStyleDeclaration);

            if (_styles.TryGetValue(element, out style))
            {
                style.Update(value);
            }
            else
            {
                style = CreateStyle(element);
                _styles.Add(element, style);
            }
        }

        private static ICssStyleDeclaration CreateStyle(IElement element)
        {
            return CreateStyle(element, null);
        }

        private static ICssStyleDeclaration CreateStyle(IElement element, String source)
        {
            var document = element.Owner;
            var context = document.Context;
            var style = new CssStyleDeclaration(context);
            style.Update(source ?? element.GetAttribute(AttributeNames.Style));
            style.Changed += value => element.SetAttribute(AttributeNames.Style, value);
            return style;
        }
    }
}
