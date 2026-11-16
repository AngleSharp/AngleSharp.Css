namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The base class of all values of the CSS Typed OM. A value that has no more
    /// specific representation (e.g., a color) is given as an instance of this
    /// class, which only knows its serialization.
    /// https://www.w3.org/TR/css-typed-om-1/#cssstylevalue
    /// </summary>
    [DomName("CSSStyleValue")]
    public abstract class CssStyleValue
    {
        #region ctor

        internal CssStyleValue()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses the given text as a value of the given property and returns the
        /// first of the resulting values.
        /// </summary>
        /// <param name="property">The name of the property, e.g., width.</param>
        /// <param name="cssText">The text to parse, e.g., 10px.</param>
        /// <returns>The typed value.</returns>
        /// <exception cref="ArgumentException">The property is unknown or the text is not valid for it.</exception>
        [DomName("parse")]
        public static CssStyleValue Parse(String property, String cssText) => Parse(property, cssText, null);

        /// <summary>
        /// Parses the given text as a value of the given property and returns the
        /// first of the resulting values.
        /// </summary>
        /// <param name="property">The name of the property, e.g., width.</param>
        /// <param name="cssText">The text to parse, e.g., 10px.</param>
        /// <param name="context">The browsing context determining the known properties, if any. A custom
        /// property registered for its active document is parsed and typed according to its syntax.</param>
        /// <returns>The typed value.</returns>
        /// <exception cref="ArgumentException">The property is unknown or the text is not valid for it.</exception>
        public static CssStyleValue Parse(String property, String cssText, IBrowsingContext? context) =>
            ParseAll(property, cssText, context)[0];

        /// <summary>
        /// Parses the given text as a value of the given property and returns all the
        /// resulting values, e.g., the single images of a background-image.
        /// </summary>
        /// <param name="property">The name of the property, e.g., background-image.</param>
        /// <param name="cssText">The text to parse, e.g., url(a.png), url(b.png).</param>
        /// <returns>The typed values.</returns>
        /// <exception cref="ArgumentException">The property is unknown or the text is not valid for it.</exception>
        [DomName("parseAll")]
        public static IReadOnlyList<CssStyleValue> ParseAll(String property, String cssText) => ParseAll(property, cssText, null);

        /// <summary>
        /// Parses the given text as a value of the given property and returns all the
        /// resulting values, e.g., the single images of a background-image.
        /// </summary>
        /// <param name="property">The name of the property, e.g., background-image.</param>
        /// <param name="cssText">The text to parse, e.g., url(a.png), url(b.png).</param>
        /// <param name="context">The browsing context determining the known properties, if any. A custom
        /// property registered for its active document is parsed and typed according to its syntax.</param>
        /// <returns>The typed values.</returns>
        /// <exception cref="ArgumentException">The property is unknown or the text is not valid for it.</exception>
        public static IReadOnlyList<CssStyleValue> ParseAll(String property, String cssText, IBrowsingContext? context)
        {
            if (property is null)
                throw new ArgumentNullException(nameof(property));

            if (cssText is null)
                throw new ArgumentNullException(nameof(cssText));

            var registration = TypedOmProperties.GetRegistration(context, TypedOmProperties.Normalize(property));
            var declaration = TypedOmProperties.Parse(context, property, cssText, registration);
            return CssStyleValueReifier.ReifyAll(declaration.Name, declaration.RawValue, registration);
        }

        /// <summary>
        /// Serializes the value, i.e., gets its CSS text representation.
        /// </summary>
        /// <returns>The CSS text of the value.</returns>
        public abstract override String ToString();

        #endregion
    }
}
