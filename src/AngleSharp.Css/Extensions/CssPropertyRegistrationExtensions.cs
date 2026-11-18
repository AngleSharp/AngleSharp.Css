#nullable enable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extensions to get the registered custom properties (CSS Properties and Values API).
    /// </summary>
    public static class CssPropertyRegistrationExtensions
    {
        /// <summary>
        /// Gets the custom properties registered for the document, i.e., by the valid
        /// @property rules of its style sheets (which follow the conditions of enclosing
        /// rules, e.g., of @media) and by CSS.registerProperty(), which wins for the same
        /// name. The registrations reflect the style sheets at the time of the call.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>The registrations in effect, one per name.</returns>
        public static IEnumerable<ICssPropertyRegistration> GetRegisteredProperties(this IDocument document)
        {
            if (document is null)
                throw new ArgumentNullException(nameof(document));

            return CssPropertyRegistry.ForDocument(document).Registrations.ToArray();
        }

        /// <summary>
        /// Gets the registration in effect for the given custom property of the document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="name">The name of the custom property, e.g., --my-color.</param>
        /// <returns>The registration or null if the property is not registered.</returns>
        public static ICssPropertyRegistration? GetRegisteredProperty(this IDocument document, String name)
        {
            if (document is null)
                throw new ArgumentNullException(nameof(document));

            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return CssPropertyRegistration.IsCustomPropertyName(name) ? CssPropertyRegistry.ForDocument(document).Get(name) : null;
        }

        /// <summary>
        /// Gets the registration the @property rule makes, regardless of whether it is in
        /// effect for a document, e.g., if a later rule or script registers the same name.
        /// </summary>
        /// <param name="rule">The @property rule.</param>
        /// <returns>The registration or null if the rule is not valid.</returns>
        public static ICssPropertyRegistration? GetRegistration(this ICssPropertyRule rule)
        {
            if (rule is null)
                throw new ArgumentNullException(nameof(rule));

            return (rule as CssPropertyRule)?.CreateRegistration();
        }
    }
}
