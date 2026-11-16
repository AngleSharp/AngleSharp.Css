namespace AngleSharp.Css.TypedOm
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Values;
    using AngleSharp.Text;
    using System;
    using System.Linq;

    /// <summary>
    /// A writable view of style declarations as typed values. The typed values are
    /// serialized and validated by the converter of the property, i.e., the same way
    /// as values set via the CSSOM.
    /// </summary>
    sealed class StylePropertyMap : StylePropertyMapReadOnly, IStylePropertyMap
    {
        #region ctor

        public StylePropertyMap(ICssStyleDeclaration declaration, IBrowsingContext? context)
            : base(declaration, context, false)
        {
        }

        #endregion

        #region Methods

        public void Set(String property, params CssStyleValue[] values)
        {
            var name = Validate(property);
            Update(name, GetTexts(name, values), false);
        }

        public void Set(String property, String value)
        {
            var name = Validate(property);
            Update(name, new[] { value ?? throw new ArgumentNullException(nameof(value)) }, false);
        }

        public void Append(String property, params CssStyleValue[] values)
        {
            var name = Validate(property);
            Update(name, GetTexts(name, values), true);
        }

        public void Append(String property, String value)
        {
            var name = Validate(property);
            Update(name, new[] { value ?? throw new ArgumentNullException(nameof(value)) }, true);
        }

        public void Delete(String property)
        {
            var name = Validate(property);
            Declaration.RemoveProperty(name);
        }

        public void Clear() => Declaration.CssText = String.Empty;

        #endregion

        #region Helpers

        private static String[] GetTexts(String name, CssStyleValue[] values)
        {
            if (values is null)
                throw new ArgumentNullException(nameof(values));

            return values.Select(value => value switch
            {
                null => throw new ArgumentNullException(nameof(values)),
                CssUnsupportedValue unsupported when unsupported.Property != name =>
                    throw new ArgumentException($"The value was obtained for '{unsupported.Property}' and cannot be used for '{name}'.", nameof(values)),
                _ => value.ToString(),
            }).ToArray();
        }

        private void Update(String name, String[] texts, Boolean append)
        {
            var custom = TypedOmProperties.IsCustom(name);

            if (texts.Length == 0)
            {
                throw new ArgumentException("At least one value is required.", nameof(texts));
            }

            if (custom && (append || texts.Length > 1))
            {
                throw new ArgumentException($"The custom property '{name}' is not list-valued.", nameof(name));
            }

            var text = String.Join(", ", texts);

            if (append)
            {
                var existing = Declaration.GetPropertyValue(name);
                text = String.IsNullOrEmpty(existing) ? text : String.Concat(existing, ", ", text);
            }

            var property = TypedOmProperties.Parse(Context, name, text);

            if ((append || texts.Length > 1) && property.RawValue is not CssListValue<ICssValue> && property.RawValue is not CssReferenceValue)
            {
                throw new ArgumentException($"The property '{name}' is not list-valued.", nameof(name));
            }

            property.IsImportant = Declaration.GetPropertyPriority(name).Isi(CssKeywords.Important);

            if (Declaration is CssStyleDeclaration declaration)
            {
                // avoids parsing the text once more
                declaration.SetParsedProperty(property);
            }
            else
            {
                Declaration.SetProperty(name, text, property.IsImportant ? CssKeywords.Important : null);
            }
        }

        #endregion
    }
}
