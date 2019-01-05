namespace AngleSharp.Dom
{
    using AngleSharp.Common;
    using AngleSharp.Css.Dom;
    using AngleSharp.Html.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// General CSS API extension methods.
    /// </summary>
    public static class CssApiExtensions
    {
        /// <summary>
        /// Gets the computed style of the given element from the current view.
        /// </summary>
        /// <param name="element">The element to compute the style for.</param>
        /// <returns>The computed style declaration if available.</returns>
        public static ICssStyleDeclaration ComputeCurrentStyle(this IElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var document = element.Owner;
            var window = document?.DefaultView;
            return window?.GetComputedStyle(element);
        }

        /// <summary>
        /// Extends the CSS of the given elements with the specified 
        /// declarations.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="propertyValue">
        /// The value of the property to set.
        /// </param>
        /// <returns>The collection itself.</returns>
        public static T Css<T>(this T elements, String propertyName, String propertyValue)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            foreach (var element in elements.OfType<IHtmlElement>())
            {
                element.GetStyle()?.SetProperty(propertyName, propertyValue);
            }

            return elements;
        }

        /// <summary>
        /// Extends the CSS of the given elements with the specified 
        /// declarations.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="properties">
        /// An enumeration of properties in form of key-value pairs.
        /// </param>
        /// <returns>The collection itself.</returns>
        public static T Css<T>(this T elements, IEnumerable<KeyValuePair<String, String>> properties)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            foreach (var element in elements.OfType<IHtmlElement>())
            {
                foreach (var property in properties)
                {
                    element.GetStyle()?.SetProperty(property.Key, property.Value);
                }
            }

            return elements;
        }

        /// <summary>
        /// Extends the CSS of the given elements with the specified
        /// declarations.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="properties">
        /// An enumeration of properties in form of an anonymous object, that
        /// carries key-value pairs.
        /// </param>
        /// <returns>The collection itself.</returns>
        public static T Css<T>(this T elements, Object properties)
            where T : IEnumerable<IElement>
        {
            var realProperties = properties.ToDictionary();
            return elements.Css(realProperties);
        }
    }
}
