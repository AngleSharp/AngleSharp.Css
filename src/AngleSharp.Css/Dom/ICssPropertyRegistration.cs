#nullable enable
namespace AngleSharp.Css.Dom
{
    using System;

    /// <summary>
    /// The registration of a custom property, made by a valid @property rule or by
    /// CSS.registerProperty(). Both share this representation, which is what the
    /// cascade, the computed style, and the Typed OM use.
    /// https://www.w3.org/TR/css-properties-values-api-1/#custom-property-registration
    /// </summary>
    public interface ICssPropertyRegistration
    {
        /// <summary>
        /// Gets the name of the custom property, e.g., --my-color.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Gets the syntax values of the property have to match as specified, e.g.,
        /// &lt;length&gt; | auto, or * for the universal syntax.
        /// </summary>
        String Syntax { get; }

        /// <summary>
        /// Gets if the syntax is the universal syntax (*), which accepts any value.
        /// </summary>
        Boolean IsUniversal { get; }

        /// <summary>
        /// Gets if the syntax is list-valued, i.e., all of its components are multiplied
        /// by # (a comma separated list) or all by + (a space separated list), e.g.,
        /// &lt;length&gt;#.
        /// </summary>
        Boolean IsList { get; }

        /// <summary>
        /// Gets if the property is inherited.
        /// </summary>
        Boolean Inherits { get; }

        /// <summary>
        /// Gets the serialization of the initial value, or null if there is none, i.e.,
        /// the initial value is the guaranteed-invalid value (only for the universal syntax).
        /// </summary>
        String? InitialValue { get; }

        /// <summary>
        /// Gets the initial value as a value of the CSSOM, i.e., as matched against the
        /// syntax (e.g., a length for &lt;length&gt;), or null if there is none. Use
        /// ToStyleValue(Name) to get its typed (Typed OM) representation.
        /// </summary>
        ICssValue? RawInitialValue { get; }

        /// <summary>
        /// Gets what made the registration.
        /// </summary>
        CssPropertyRegistrationOrigin Origin { get; }

        /// <summary>
        /// Matches the given text against the syntax, using the same converters as the
        /// cascade does. References (e.g., var()) are not substituted, i.e., they only
        /// match the universal syntax.
        /// </summary>
        /// <param name="cssText">The text to match, e.g., 10px.</param>
        /// <returns>The value according to the first matching component, or null if none matches.</returns>
        ICssValue? Match(String cssText);
    }
}
