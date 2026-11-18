#nullable enable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// The definition of a custom property to register via CSS.registerProperty().
    /// https://www.w3.org/TR/css-properties-values-api-1/#dictdef-propertydefinition
    /// </summary>
    [DomName("PropertyDefinition")]
    public class PropertyDefinition
    {
        /// <summary>
        /// Gets or sets the name of the custom property, e.g., --my-color.
        /// </summary>
        [DomName("name")]
        public String Name { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the syntax values of the property have to match, e.g., &lt;color&gt;.
        /// The default is the universal syntax (*), which accepts any value.
        /// </summary>
        [DomName("syntax")]
        public String Syntax { get; set; } = "*";

        /// <summary>
        /// Gets or sets if the property is inherited.
        /// </summary>
        [DomName("inherits")]
        public Boolean Inherits { get; set; }

        /// <summary>
        /// Gets or sets the initial value, which is required unless the syntax is universal.
        /// It has to match the syntax and must not depend on the element, e.g., by em or var().
        /// </summary>
        [DomName("initialValue")]
        public String? InitialValue { get; set; }
    }
}
