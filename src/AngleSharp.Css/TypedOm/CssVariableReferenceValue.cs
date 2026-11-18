namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a reference to a custom property, i.e., var(--name, fallback),
    /// within an unparsed value.
    /// https://www.w3.org/TR/css-typed-om-1/#cssvariablereferencevalue
    /// </summary>
    [DomName("CSSVariableReferenceValue")]
    public sealed class CssVariableReferenceValue
    {
        private String _variable;

        /// <summary>
        /// Creates a new reference to a custom property.
        /// </summary>
        /// <param name="variable">The name of the custom property, e.g., --main-color.</param>
        /// <param name="fallback">The optional fallback value.</param>
        /// <exception cref="ArgumentException">The name is not a custom property name.</exception>
        [DomConstructor]
        public CssVariableReferenceValue(String variable, CssUnparsedValue? fallback = null)
        {
            _variable = Validate(variable);
            Fallback = fallback;
        }

        /// <summary>
        /// Gets or sets the name of the referenced custom property.
        /// </summary>
        /// <exception cref="ArgumentException">The name is not a custom property name.</exception>
        [DomName("variable")]
        public String Variable
        {
            get => _variable;
            set => _variable = Validate(value);
        }

        /// <summary>
        /// Gets the fallback value, if any.
        /// </summary>
        [DomName("fallback")]
        public CssUnparsedValue? Fallback { get; }

        /// <summary>
        /// Serializes the reference, i.e., var(--name) or var(--name,fallback).
        /// </summary>
        public override String ToString() => Fallback is null ?
            String.Concat("var(", _variable, ")") :
            String.Concat("var(", _variable, ",", Fallback.ToString(), ")");

        private static String Validate(String variable)
        {
            if (variable is null || !variable.StartsWith("--", StringComparison.Ordinal))
            {
                throw new ArgumentException("The name of a custom property has to start with '--'.", nameof(variable));
            }

            return variable;
        }
    }
}
