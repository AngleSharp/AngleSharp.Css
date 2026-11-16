namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Css.Dom;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A view of style declarations as typed values. Nothing is copied: the values
    /// are reified from the declarations whenever they are requested.
    /// </summary>
    class StylePropertyMapReadOnly : IStylePropertyMapReadOnly
    {
        #region Fields

        private readonly ICssStyleDeclaration _declaration;
        private readonly IBrowsingContext? _context;
        private readonly Boolean _sorted;

        #endregion

        #region ctor

        public StylePropertyMapReadOnly(ICssStyleDeclaration declaration, IBrowsingContext? context, Boolean sorted)
        {
            _declaration = declaration;
            _context = context ?? (declaration as CssStyleDeclaration)?.Context;
            _sorted = sorted;
        }

        #endregion

        #region Properties

        protected ICssStyleDeclaration Declaration => _declaration;

        protected IBrowsingContext? Context => _context;

        public Int32 Size => GetNames().Count(name => GetAllCore(name).Count > 0);

        #endregion

        #region Methods

        public CssStyleValue? Get(String property)
        {
            var values = GetAll(property);
            return values.Count > 0 ? values[0] : null;
        }

        public IReadOnlyList<CssStyleValue> GetAll(String property) => GetAllCore(Validate(property));

        public Boolean Has(String property) => !String.IsNullOrEmpty(_declaration.GetPropertyValue(Validate(property)));

        public IEnumerator<KeyValuePair<String, IReadOnlyList<CssStyleValue>>> GetEnumerator()
        {
            foreach (var name in GetNames())
            {
                var values = GetAllCore(name);

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

        private IReadOnlyList<CssStyleValue> GetAllCore(String name)
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

            return value is not null ?
                CssStyleValueReifier.ReifyAll(name, value) :
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
