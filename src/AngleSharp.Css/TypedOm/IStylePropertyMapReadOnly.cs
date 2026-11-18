namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A read-only map from properties to their typed values, e.g., of the
    /// computed style of an element.
    /// https://www.w3.org/TR/css-typed-om-1/#stylepropertymapreadonly
    /// </summary>
    [DomName("StylePropertyMapReadOnly")]
    public interface IStylePropertyMapReadOnly : IEnumerable<KeyValuePair<String, IReadOnlyList<CssStyleValue>>>
    {
        /// <summary>
        /// Gets the (first) value of the given property.
        /// </summary>
        /// <param name="property">The name of the property, e.g., width.</param>
        /// <returns>The value or null if the property has no value.</returns>
        /// <exception cref="ArgumentException">The property is not supported.</exception>
        [DomName("get")]
        CssStyleValue? Get(String property);

        /// <summary>
        /// Gets all values of the given property, e.g., the single images of a
        /// background-image.
        /// </summary>
        /// <param name="property">The name of the property, e.g., background-image.</param>
        /// <returns>The values, which are empty if the property has no value.</returns>
        /// <exception cref="ArgumentException">The property is not supported.</exception>
        [DomName("getAll")]
        IReadOnlyList<CssStyleValue> GetAll(String property);

        /// <summary>
        /// Checks if the given property has a value.
        /// </summary>
        /// <param name="property">The name of the property, e.g., width.</param>
        /// <returns>True if the property has a value, otherwise false.</returns>
        /// <exception cref="ArgumentException">The property is not supported.</exception>
        [DomName("has")]
        Boolean Has(String property);

        /// <summary>
        /// Gets the number of properties with a value.
        /// </summary>
        [DomName("size")]
        Int32 Size { get; }
    }
}
