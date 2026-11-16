#nullable enable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a @property CSS rule.
    /// </summary>
    [DomName("CSSPropertyRule")]
    public interface ICssPropertyRule : ICssRule, ICssProperties
    {
        /// <summary>
        /// Gets the custom property name.
        /// </summary>
        [DomName("name")]
        String Name { get; }

        /// <summary>
        /// Gets the syntax of the custom property as specified (without the quotes of
        /// the string), e.g., &lt;color&gt;, or an empty string if it has no valid syntax.
        /// </summary>
        [DomName("syntax")]
        String Syntax { get; }

        /// <summary>
        /// Gets if the custom property is inherited.
        /// </summary>
        [DomName("inherits")]
        Boolean Inherits { get; }

        /// <summary>
        /// Gets the initial value of the custom property, if any.
        /// </summary>
        [DomName("initialValue")]
        String? InitialValue { get; }

        /// <summary>
        /// Gets if the rule is valid, i.e., registers the custom property: it needs a valid
        /// syntax and inherits descriptor, and an initial value that matches the syntax
        /// and is computationally independent, unless the syntax is universal (*).
        /// </summary>
        Boolean IsValid { get; }
    }
}
