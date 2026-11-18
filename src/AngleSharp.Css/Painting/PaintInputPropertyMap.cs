namespace AngleSharp.Css.Painting
{
    using AngleSharp.Css.TypedOm;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The typed computed style of an element limited to the input properties of a paint
    /// definition: other properties are not in the map.
    /// https://www.w3.org/TR/css-paint-api-1/#invoke-a-paint-callback
    /// </summary>
    sealed class PaintInputPropertyMap : IStylePropertyMapReadOnly
    {
        private readonly IStylePropertyMapReadOnly _computed;
        private readonly String[] _names;

        public PaintInputPropertyMap(IStylePropertyMapReadOnly computed, String[] names)
        {
            _computed = computed;
            _names = names;
        }

        public Int32 Size => _names.Count(name => _computed.GetAll(name).Count > 0);

        public CssStyleValue? Get(String property) => Find(property) is String name ? _computed.Get(name) : null;

        public IReadOnlyList<CssStyleValue> GetAll(String property) =>
            Find(property) is String name ? _computed.GetAll(name) : Array.Empty<CssStyleValue>();

        public Boolean Has(String property) => Find(property) is String name && _computed.Has(name);

        public IEnumerator<KeyValuePair<String, IReadOnlyList<CssStyleValue>>> GetEnumerator()
        {
            foreach (var name in _names)
            {
                var values = _computed.GetAll(name);

                if (values.Count > 0)
                {
                    yield return new KeyValuePair<String, IReadOnlyList<CssStyleValue>>(name, values);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private String? Find(String property)
        {
            var name = TypedOmProperties.Normalize(property);
            return Array.IndexOf(_names, name) >= 0 ? name : null;
        }
    }
}
