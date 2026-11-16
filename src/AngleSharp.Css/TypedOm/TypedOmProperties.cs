namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Css.Dom;
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
        /// Parses the text for the given property, or throws (TypeError) if it is invalid.
        /// </summary>
        public static CssProperty Parse(IBrowsingContext? context, String name, String text)
        {
            name = Normalize(name);
            var info = GetInfo(context, name);
            var property = new CssProperty(name, info.Converter, info.Flags)
            {
                Value = text,
            };

            if (property.RawValue is null)
            {
                throw new ArgumentException($"The value '{text}' is not valid for the property '{name}'.", nameof(text));
            }

            return property;
        }
    }
}
