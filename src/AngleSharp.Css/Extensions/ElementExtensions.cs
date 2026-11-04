#nullable enable
namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.RenderTree;
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A set of useful extension methods for the Element class.
    /// </summary>
    [DomExposed("Element")]
    public static class ElementExtensions
    {
        /// <summary>
        /// Creates a pseudo element for the current element.
        /// </summary>
        /// <param name="element">The element to extend.</param>
        /// <param name="pseudoElement">
        /// The element to create (e.g. ::after).
        /// </param>
        /// <returns>The created element or null, if not possible.</returns>
        [DomName("pseudo")]
        public static IPseudoElement? Pseudo(this IElement element, String pseudoElement)
        {
            var factory = element.Owner?.Context.GetService<IPseudoElementFactory>();
            return factory?.Create(element, pseudoElement);
        }

        /// <summary>
        /// Forces the given pseudo-class to be considered active (or inactive) for this element
        /// during selector matching and style computation, e.g. to preview a `:hover` or
        /// `:active` state without real pointer/keyboard interaction.
        /// </summary>
        /// <param name="element">The element to force the pseudo-class on.</param>
        /// <param name="pseudoClass">The pseudo-class name, with or without the leading colon.</param>
        /// <param name="value">True to force it as matching, false to force it as not matching.</param>
        /// <remarks>
        /// The <c>focus</c> pseudo-class is backed by real focus state instead of a forced
        /// override; setting it calls <see cref="IHtmlElement.DoFocus"/> / <see cref="IHtmlElement.DoBlur"/>.
        /// Forcing is explicit and does not propagate to ancestors or descendants.
        /// </remarks>
        public static void SetPseudoClass(this IElement element, String pseudoClass, Boolean value = true)
        {
            pseudoClass = pseudoClass.TrimStart(':');

            if (pseudoClass.Equals(PseudoClassNames.Focus, StringComparison.OrdinalIgnoreCase))
            {
                if (element is IHtmlElement html)
                {
                    if (value)
                    {
                        html.DoFocus();
                    }
                    else
                    {
                        html.DoBlur();
                    }
                }

                return;
            }

            PseudoClassStateStore.Set(element, pseudoClass, value);
        }

        /// <summary>
        /// Gets the forced state of the given pseudo-class for this element, or null if it has
        /// not been forced (in which case normal matching rules apply).
        /// </summary>
        /// <param name="element">The element to inspect.</param>
        /// <param name="pseudoClass">The pseudo-class name, with or without the leading colon.</param>
        public static Boolean? GetPseudoClass(this IElement element, String pseudoClass)
        {
            pseudoClass = pseudoClass.TrimStart(':');

            if (pseudoClass.Equals(PseudoClassNames.Focus, StringComparison.OrdinalIgnoreCase))
            {
                return element.IsFocused;
            }

            return PseudoClassStateStore.TryGet(element, pseudoClass, out var value) ? value : null;
        }

        /// <summary>
        /// Removes a previously forced pseudo-class state, reverting to normal matching rules.
        /// </summary>
        /// <param name="element">The element to reset.</param>
        /// <param name="pseudoClass">The pseudo-class name, with or without the leading colon.</param>
        public static void RemovePseudoClass(this IElement element, String pseudoClass)
        {
            pseudoClass = pseudoClass.TrimStart(':');

            if (pseudoClass.Equals(PseudoClassNames.Focus, StringComparison.OrdinalIgnoreCase))
            {
                (element as IHtmlElement)?.DoBlur();
                return;
            }

            PseudoClassStateStore.Remove(element, pseudoClass);
        }

        /// <summary>
        /// Removes all forced pseudo-class states for this element.
        /// </summary>
        /// <param name="element">The element to reset.</param>
        public static void ClearPseudoClasses(this IElement element) => PseudoClassStateStore.Clear(element);

        /// <summary>
        /// Gets the innerText of an element.
        /// </summary>
        /// <param name="element">The element to extend.</param>
        /// <returns>The computed inner text.</returns>
        [DomName("innerText")]
        [DomAccessor(Accessors.Getter)]
        public static String GetInnerText(this IElement element) => InnerText.Get(element);

        /// <summary>
        /// Sets the innerText of an element.
        /// </summary>
        /// <param name="element">The element to extend.</param>
        /// <param name="value">The inner text to set.</param>
        [DomName("innerText")]
        [DomAccessor(Accessors.Setter)]

        public static void SetInnerText(this IElement element, String? value)
        {
            if (String.IsNullOrEmpty(value))
            {
                element.TextContent = String.Empty;
            }
            else
            {
                var document = element.Owner;
                var fragment = document!.CreateDocumentFragment();
                var sb = StringBuilderPool.Obtain();

                for (var i = 0; i < value!.Length; i++)
                {
                    var c = value[i];

                    if (c == Symbols.LineFeed || c == Symbols.CarriageReturn)
                    {
                        if (c == Symbols.CarriageReturn && i + 1 < value.Length && value[i + 1] == Symbols.LineFeed)
                        {
                            // ignore carriage return if the next char is a line feed
                            continue;
                        }

                        if (sb.Length > 0)
                        {
                            fragment.AppendChild(document.CreateTextNode(sb.ToString()));
                            sb.Clear();
                        }

                        fragment.AppendChild(document.CreateElement(TagNames.Br));
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }

                var remaining = sb.ToPool();

                if (remaining.Length > 0)
                {
                    fragment.AppendChild(document.CreateTextNode(remaining));
                }

                while (element.HasChildNodes)
                {
                    element.RemoveChild(element.FirstChild);
                }

                element.AppendChild(fragment);
            }
        }

        /// <summary>
        /// Sets the style on all elements of the collection.
        /// </summary>
        /// <typeparam name="TElement">The type of elements.</typeparam>
        /// <param name="elements">The collection to go over.</param>
        /// <param name="change">The action to trigger for each element style.</param>
        /// <returns>The collection for chaining.</returns>
        public static IEnumerable<TElement> SetStyle<TElement>(this IEnumerable<TElement> elements, Action<ICssStyleDeclaration> change)
            where TElement : IElement
        {
            change = change ?? throw new ArgumentNullException(nameof(change));

            foreach (var element in elements)
            {
                change.Invoke(element.GetStyle());
            }

            return elements;
        }
    }
}
