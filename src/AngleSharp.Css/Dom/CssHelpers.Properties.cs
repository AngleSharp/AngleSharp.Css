#nullable enable
namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The registration of custom properties (CSS Properties and Values API).
    /// https://www.w3.org/TR/css-properties-values-api-1/#the-registerproperty-function
    /// </summary>
    public static partial class CssHelpers
    {
        /// <summary>
        /// Registers a custom property for the document of the window, which wins over the
        /// registrations of @property rules for the same name. Styles computed afterwards
        /// validate the values of the property against its syntax, give it the initial value
        /// and inherit it only if it is registered as inherited.
        /// </summary>
        /// <param name="window">The hosting window.</param>
        /// <param name="definition">The definition of the custom property.</param>
        /// <exception cref="DomException">
        /// A SyntaxError if the name, the syntax, or the initial value is not valid, or an
        /// InvalidModificationError if the name has been registered already.
        /// </exception>
        [DomName("registerProperty")]
        public static void RegisterProperty(this IWindow window, PropertyDefinition definition)
        {
            if (window is null)
                throw new ArgumentNullException(nameof(window));

            window.Document.RegisterProperty(definition);
        }

        /// <summary>
        /// Registers a custom property for the document, which wins over the registrations
        /// of @property rules for the same name.
        /// </summary>
        /// <param name="document">The document to register the property for.</param>
        /// <param name="definition">The definition of the custom property.</param>
        /// <exception cref="DomException">
        /// A SyntaxError if the name, the syntax, or the initial value is not valid, or an
        /// InvalidModificationError if the name has been registered already.
        /// </exception>
        public static void RegisterProperty(this IDocument document, PropertyDefinition definition)
        {
            if (document is null)
                throw new ArgumentNullException(nameof(document));

            if (definition is null)
                throw new ArgumentNullException(nameof(definition));

            if (!CssPropertyRegistration.IsCustomPropertyName(definition.Name))
                throw new DomException(DomError.Syntax);

            // checked before the definition is validated, as the specification does
            if (CssScriptPropertyRegistrations.Get(document)?.Contains(definition.Name) == true)
                throw new DomException(DomError.InvalidModification);

            var syntax = CssPropertySyntax.Parse(definition.Syntax ?? String.Empty) ?? throw new DomException(DomError.Syntax);
            var registration = CssPropertyRegistration.Create(definition.Name, syntax, definition.Inherits, definition.InitialValue, CssPropertyRegistrationOrigin.Script) ??
                throw new DomException(DomError.Syntax);

            if (!CssScriptPropertyRegistrations.TryAdd(document, registration))
                throw new DomException(DomError.InvalidModification);
        }
    }
}
