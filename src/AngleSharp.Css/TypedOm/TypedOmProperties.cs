namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Helpers for resolving and parsing properties for the Typed OM, which use the
    /// declarations (and converters) of the browsing context.
    /// </summary>
    static class TypedOmProperties
    {
        // used without a browsing context, e.g., for CSSStyleValue.parse
        private static readonly DefaultDeclarationFactory DefaultFactory = new();

        public static Boolean IsCustom(String name) => name.StartsWith("--", StringComparison.Ordinal);

        public static String Normalize(String name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return IsCustom(name) ? name : name.ToLowerFast();
        }

        /// <summary>
        /// Gets the declaration info of a known property, or throws (TypeError).
        /// </summary>
        public static DeclarationInfo GetInfo(IBrowsingContext? context, String name)
        {
            var info = context is not null ? context.GetDeclarationInfo(name) : DefaultFactory.Create(name);

            if (info is null || (info.Flags == PropertyFlags.Unknown && !IsCustom(name)))
            {
                throw new ArgumentException($"The property '{name}' is not supported.", nameof(name));
            }

            return info;
        }

        /// <summary>
        /// Gets the registrations in effect for the document, if any.
        /// </summary>
        public static CssPropertyRegistry? GetRegistry(IDocument? document) =>
            document is not null ? CssPropertyRegistry.ForDocument(document) : null;

        /// <summary>
        /// Gets the registration of the custom property in the document of the context.
        /// </summary>
        public static CssPropertyRegistration? GetRegistration(IBrowsingContext? context, String name) =>
            IsCustom(name) ? GetRegistry(context?.Active)?.Get(name) : null;

        /// <summary>
        /// Parses the text for the given property, or throws (TypeError) if it is invalid.
        /// The value of a registered custom property has to match its syntax, unless it is
        /// a CSS-wide keyword or has references (e.g., var()), which are only resolved at
        /// computed-value time.
        /// </summary>
        public static CssProperty Parse(IBrowsingContext? context, String name, String text, CssPropertyRegistration? registration = null)
        {
            name = Normalize(name);
            var info = GetInfo(context, name);
            var property = new CssProperty(name, info.Converter, info.Flags)
            {
                Value = text,
            };

            if (property.RawValue is null || !MatchesRegistration(registration, property.RawValue))
            {
                throw new ArgumentException($"The value '{text}' is not valid for the property '{name}'.", nameof(text));
            }

            return property;
        }

        private static Boolean MatchesRegistration(CssPropertyRegistration? registration, ICssValue value) =>
            registration is null || registration.IsUniversal || value is ICssSpecialValue ||
            CssPropertySyntax.IsCssWideKeyword(value.CssText.Trim()) ||
            CssPropertySyntax.HasReferences(value.CssText) || registration.Syntax.Match(value) is not null;
    }
}
