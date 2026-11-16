namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Extensions to get typed values (CSS Typed OM) from the CSSOM.
    /// </summary>
    public static class CssTypedOmExtensions
    {
        private static readonly ConditionalWeakTable<ICssStyleDeclaration, IStylePropertyMap> Maps = new();

        /// <summary>
        /// Gets the typed view of the given declarations. Changes made via the map are
        /// applied to the declarations, and vice versa.
        /// </summary>
        /// <param name="declaration">The declarations, e.g., the style of a rule.</param>
        /// <returns>The (always same) map of the declarations.</returns>
        public static IStylePropertyMap GetStyleMap(this ICssStyleDeclaration declaration)
        {
            if (declaration is null)
                throw new ArgumentNullException(nameof(declaration));

            return Maps.GetValue(declaration, m => new StylePropertyMap(m, null));
        }

        /// <summary>
        /// Gets the (always same) typed view of the declarations, whose registered custom
        /// properties are the ones of the given document.
        /// </summary>
        internal static IStylePropertyMap GetStyleMap(this ICssStyleDeclaration declaration, IDocument? document)
        {
            var map = declaration.GetStyleMap();

            if (map is StylePropertyMapReadOnly typed && typed.Document is null)
            {
                typed.Document = document;
            }

            return map;
        }

        /// <summary>
        /// Gets a read-only typed view of the given declarations, e.g., of a computed style.
        /// The entries are sorted by name, with the custom properties last.
        /// </summary>
        /// <param name="declaration">The declarations, e.g., a computed style.</param>
        /// <returns>The read-only map of the declarations.</returns>
        public static IStylePropertyMapReadOnly GetReadOnlyStyleMap(this ICssStyleDeclaration declaration)
        {
            if (declaration is null)
                throw new ArgumentNullException(nameof(declaration));

            return new StylePropertyMapReadOnly(declaration, null, true);
        }

        /// <summary>
        /// Gets the typed values of the given declaration, which are multiple for a
        /// list-valued property, e.g., background-image.
        /// </summary>
        /// <param name="property">The declaration.</param>
        /// <returns>The typed values, which are empty if the declaration has no value.</returns>
        public static IReadOnlyList<CssStyleValue> ToStyleValues(this ICssProperty property)
        {
            if (property is null)
                throw new ArgumentNullException(nameof(property));

            return CssStyleValueReifier.ReifyAll(property.Name, property.RawValue);
        }

        /// <summary>
        /// Gets the (first) typed value of the given declaration.
        /// </summary>
        /// <param name="property">The declaration.</param>
        /// <returns>The typed value or null if the declaration has no value.</returns>
        public static CssStyleValue? ToStyleValue(this ICssProperty property)
        {
            var values = property.ToStyleValues();
            return values.Count > 0 ? values[0] : null;
        }

        /// <summary>
        /// Gets the typed value of the given CSSOM value of a property.
        /// </summary>
        /// <param name="value">The value, e.g., the raw value of a declaration.</param>
        /// <param name="property">The name of the property the value belongs to.</param>
        /// <returns>The typed value.</returns>
        public static CssStyleValue ToStyleValue(this ICssValue value, String property)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            return CssStyleValueReifier.Reify(TypedOmProperties.Normalize(property), value);
        }
    }
}
