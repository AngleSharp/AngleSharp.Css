namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A view of style declarations as typed values. Nothing is copied: the values
    /// are reified from the declarations whenever they are requested. The values of
    /// custom properties are typed according to their registration in the document,
    /// which is looked up when they are requested, too.
    /// </summary>
    class StylePropertyMapReadOnly : IStylePropertyMapReadOnly
    {
        #region Fields

        private readonly ICssStyleDeclaration _declaration;
        private readonly IBrowsingContext? _context;
        private readonly Boolean _sorted;

        #endregion

        #region ctor

        public StylePropertyMapReadOnly(ICssStyleDeclaration declaration, IBrowsingContext? context, Boolean sorted, IDocument? document = null)
        {
            _declaration = declaration;
            _context = context ?? (declaration as CssStyleDeclaration)?.Context;
            _sorted = sorted;
            Document = document;
        }

        #endregion

        #region Properties

        protected ICssStyleDeclaration Declaration => _declaration;

        protected IBrowsingContext? Context => _context;

        /// <summary>
        /// Gets or sets the document whose registered custom properties apply, which is
        /// otherwise the one of the style sheet of the declarations' rule, or the active
        /// document of the context.
        /// </summary>
        internal IDocument? Document { get; set; }

        public Int32 Size
        {
            get
            {
                var registry = new Lazy<CssPropertyRegistry?>(GetRegistry);
                return GetNames().Count(name => GetAllCore(name, registry).Count > 0);
            }
        }

        #endregion

        #region Methods

        public CssStyleValue? Get(String property)
        {
            var values = GetAll(property);
            return values.Count > 0 ? values[0] : null;
        }

        public IReadOnlyList<CssStyleValue> GetAll(String property) =>
            GetAllCore(Validate(property), new Lazy<CssPropertyRegistry?>(GetRegistry));

        public Boolean Has(String property) => !String.IsNullOrEmpty(_declaration.GetPropertyValue(Validate(property)));

        public IEnumerator<KeyValuePair<String, IReadOnlyList<CssStyleValue>>> GetEnumerator()
        {
            var registry = new Lazy<CssPropertyRegistry?>(GetRegistry);

            foreach (var name in GetNames())
            {
                var values = GetAllCore(name, registry);

                if (values.Count > 0)
                {
                    yield return new KeyValuePair<String, IReadOnlyList<CssStyleValue>>(name, values);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Helpers

        protected String Validate(String property)
        {
            var name = TypedOmProperties.Normalize(property);
            TypedOmProperties.GetInfo(_context, name);
            return name;
        }

        /// <summary>
        /// Gets the registration of the custom property in effect for the declarations.
        /// </summary>
        protected CssPropertyRegistration? GetRegistration(String name) =>
            TypedOmProperties.IsCustom(name) ? GetRegistry()?.Get(name) : null;

        private CssPropertyRegistry? GetRegistry() =>
            TypedOmProperties.GetRegistry(Document ?? _declaration.Parent?.Owner?.OwnerNode?.Owner ?? _context?.Active);

        private IReadOnlyList<CssStyleValue> GetAllCore(String name, Lazy<CssPropertyRegistry?> registry)
        {
            // the text also covers used values, e.g., of a computed style
            var text = _declaration.GetPropertyValue(name);

            if (String.IsNullOrEmpty(text))
            {
                return Array.Empty<CssStyleValue>();
            }

            var property = _declaration.GetProperty(name);
            var value = property is not null && property.RawValue is not null && property.Value == text ?
                property.RawValue :
                TryParse(name, text);

            // the registry is only needed (and collected) for custom properties
            var registration = TypedOmProperties.IsCustom(name) ? registry.Value?.Get(name) : null;

            return value is not null ?
                CssStyleValueReifier.ReifyAll(name, value, registration) :
                new CssStyleValue[] { new CssUnsupportedValue(name, text) };
        }

        private ICssValue? TryParse(String name, String text)
        {
            try
            {
                return TypedOmProperties.Parse(_context, name, text).RawValue;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        private List<String> GetNames()
        {
            var names = _declaration.Select(m => m.Name).Distinct(StringComparer.Ordinal).ToList();

            if (_sorted)
            {
                // the standard properties come first, followed by the custom properties
                names.Sort((a, b) =>
                {
                    var custom = TypedOmProperties.IsCustom(a).CompareTo(TypedOmProperties.IsCustom(b));
                    return custom != 0 ? custom : String.CompareOrdinal(a, b);
                });
            }

            return names;
        }

        #endregion
    }
}
