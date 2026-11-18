namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// A map from properties to their typed values, which writes to the underlying
    /// declarations, e.g., the inline style of an element.
    /// https://www.w3.org/TR/css-typed-om-1/#stylepropertymap
    /// </summary>
    [DomName("StylePropertyMap")]
    public interface IStylePropertyMap : IStylePropertyMapReadOnly
    {
        /// <summary>
        /// Sets the value of the given property. Multiple values are only allowed for
        /// list-valued properties, e.g., background-image.
        /// </summary>
        /// <param name="property">The name of the property, e.g., width.</param>
        /// <param name="values">The values to set.</param>
        /// <exception cref="ArgumentException">The property is not supported or the values are not valid for it.</exception>
        [DomName("set")]
        void Set(String property, params CssStyleValue[] values);

        /// <summary>
        /// Sets the value of the given property from its CSS text.
        /// </summary>
        /// <param name="property">The name of the property, e.g., width.</param>
        /// <param name="value">The CSS text of the value, e.g., 10px.</param>
        /// <exception cref="ArgumentException">The property is not supported or the value is not valid for it.</exception>
        [DomName("set")]
        void Set(String property, String value);

        /// <summary>
        /// Appends the values to the given list-valued property, e.g., background-image.
        /// </summary>
        /// <param name="property">The name of the property, e.g., background-image.</param>
        /// <param name="values">The values to append.</param>
        /// <exception cref="ArgumentException">The property is not list-valued or the values are not valid for it.</exception>
        [DomName("append")]
        void Append(String property, params CssStyleValue[] values);

        /// <summary>
        /// Appends the value given by its CSS text to the given list-valued property.
        /// </summary>
        /// <param name="property">The name of the property, e.g., background-image.</param>
        /// <param name="value">The CSS text of the value(s) to append.</param>
        /// <exception cref="ArgumentException">The property is not list-valued or the value is not valid for it.</exception>
        [DomName("append")]
        void Append(String property, String value);

        /// <summary>
        /// Removes the value of the given property.
        /// </summary>
        /// <param name="property">The name of the property, e.g., width.</param>
        /// <exception cref="ArgumentException">The property is not supported.</exception>
        [DomName("delete")]
        void Delete(String property);

        /// <summary>
        /// Removes all values.
        /// </summary>
        [DomName("clear")]
        void Clear();
    }
}
