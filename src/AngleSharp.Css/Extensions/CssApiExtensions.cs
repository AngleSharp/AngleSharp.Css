#nullable enable
namespace AngleSharp.Dom
{
    using AngleSharp.Common;
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using AngleSharp.Html.Dom;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
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
            element = element ?? throw new ArgumentNullException(nameof(element));
            var document = element.Owner;
            var window = document?.DefaultView;

            if (window is null)
            {
                var ctx = document?.Context;
                var device = ctx?.GetService<IRenderDevice>();
                var defaultStyleSheetProvider = ctx?.GetServices<ICssDefaultStyleSheetProvider>() ?? Enumerable.Empty<ICssDefaultStyleSheetProvider>();
                var defaultSheets = defaultStyleSheetProvider.Select(m => m.Default).Where(m => m != null);
                var currentSheets = document?.GetStyleSheets().OfType<ICssStyleSheet>() ?? Enumerable.Empty<ICssStyleSheet>();
                var styleCollection = new StyleCollection(defaultSheets, currentSheets, device!);
                return styleCollection.ComputeDeclarations(element);
            }

            return window.GetComputedStyle(element);
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
            where T : class, IEnumerable<IElement>
        {
            elements = elements ?? throw new ArgumentNullException(nameof(elements));
            propertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));

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
            where T : class, IEnumerable<IElement>
        {
            elements = elements ?? throw new ArgumentNullException(nameof(elements));
            properties = properties ?? throw new ArgumentNullException(nameof(properties));

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
        /// <typeparam name="TProperties">The declared type whose properties are the declarations.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="properties">
        /// An enumeration of properties in form of an anonymous object, that
        /// carries key-value pairs.
        /// </param>
        /// <returns>The collection itself.</returns>
        public static T Css<T, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TProperties>(this T elements, TProperties properties)
            where T : class, IEnumerable<IElement>
        {
            // Typed as the interface so this binds to the key value pair overload, not back to itself.
            IEnumerable<KeyValuePair<String, String>> realProperties = properties.ToDictionary();
            return elements.Css(realProperties);
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
        [Obsolete(RuntimePropertiesObsolete)]
        [RequiresUnreferencedCode(RuntimePropertiesTrimming)]
        public static T Css<T>(this T elements, Object properties)
            where T : class, IEnumerable<IElement>
        {
            // Kept so code compiled against it keeps working; a recompile binds everything but a
            // value declared as Object to the generic overload. Being obsolete itself, it may call
            // the obsolete Object overload of ToDictionary without a warning.
            IEnumerable<KeyValuePair<String, String>> realProperties = properties.ToDictionary();
            return elements.Css(realProperties);
        }

        private const String RuntimePropertiesObsolete = "Reads the properties of the value's runtime type, which trimming can remove. Pass the value with its own type, such as an anonymous object, or as a dictionary, to use the generic overload.";

        private const String RuntimePropertiesTrimming = "Reads the public properties of the value's runtime type, which trimming can remove.";
    }
}
