namespace AngleSharp.Css.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Parser.Tokens;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The syntax definition of a registered custom property, e.g., "&lt;length&gt; | auto"
    /// or "&lt;color&gt;#".
    /// https://www.w3.org/TR/css-properties-values-api-1/#syntax-strings
    /// </summary>
    sealed class CssPropertySyntax
    {
        #region Fields

        private static readonly Dictionary<String, IValueConverter> DataTypes = new(StringComparer.Ordinal)
        {
            { "length", ValueConverters.LengthConverter },
            { "number", ValueConverters.NumberConverter },
            { "percentage", ValueConverters.PercentConverter },
            { "length-percentage", ValueConverters.LengthOrPercentConverter },
            { "color", ValueConverters.CurrentColorConverter },
            { "image", ValueConverters.Or(ValueConverters.UrlConverter, ValueConverters.GradientConverter, new ClassValueConverter<ICssImageValue>(CompoundParser.ParseImageSet)) },
            { "url", ValueConverters.UrlConverter },
            { "integer", ValueConverters.IntegerConverter },
            { "angle", new ClassValueConverter<ICssValue>(UnitParser.ParseAngleOrCalc) },
            { "time", ValueConverters.TimeConverter },
            { "resolution", ValueConverters.ResolutionConverter },
            { "transform-function", ValueConverters.TransformConverter },
            { "transform-list", ValueConverters.TransformConverter.Many() },
            { "custom-ident", new IdentConverter(null) },
            { "string", ValueConverters.StringConverter },
        };

        // the units of lengths depending on the font or the root element, see 2.5
        private static readonly HashSet<String> DependentUnits = new(StringComparer.OrdinalIgnoreCase)
        {
            "em", "ex", "cap", "ch", "ic", "lh", "rem", "rex", "rcap", "rch", "ric", "rlh",
        };

        private readonly String _text;
        private readonly Component[] _components;

        #endregion

        #region ctor

        private CssPropertySyntax(String text, Component[] components)
        {
            _text = text;
            _components = components;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the syntax string as specified.
        /// </summary>
        public String Text => _text;

        /// <summary>
        /// Gets if the syntax is the universal syntax definition, i.e., "*".
        /// </summary>
        public Boolean IsUniversal => _components.Length == 0;

        #endregion

        #region Parsing

        /// <summary>
        /// Parses the given syntax string.
        /// https://www.w3.org/TR/css-properties-values-api-1/#consume-syntax-definition
        /// </summary>
        /// <returns>The syntax definition or null if the string is not valid.</returns>
        public static CssPropertySyntax? Parse(String text)
        {
            var definition = TrimWhitespace(text);

            if (definition.Length == 0)
            {
                return null;
            }

            if (definition == "*")
            {
                return new CssPropertySyntax(text, Array.Empty<Component>());
            }

            var parts = definition.Split('|');
            var components = new Component[parts.Length];

            for (var i = 0; i < parts.Length; i++)
            {
                var component = ParseComponent(TrimWhitespace(parts[i]));

                if (component is null)
                {
                    return null;
                }

                components[i] = component;
            }

            return new CssPropertySyntax(text, components);
        }

        private static Component? ParseComponent(String text)
        {
            if (text.Length == 0)
            {
                return null;
            }

            // the multiplier has to follow the name immediately, e.g., <length>+
            var multiplier = text[text.Length - 1] is Symbols.Plus or Symbols.Num ? text[text.Length - 1] : default(Char?);
            var name = multiplier.HasValue ? text.Substring(0, text.Length - 1) : text;

            if (name.Length > 2 && name[0] == Symbols.LessThan && name[name.Length - 1] == Symbols.GreaterThan)
            {
                var type = name.Substring(1, name.Length - 2);

                // <transform-list> is already a list, i.e., cannot be multiplied
                if (!DataTypes.TryGetValue(type, out var converter) || (type == "transform-list" && multiplier.HasValue))
                {
                    return null;
                }

                return new Component(Multiply(converter, multiplier));
            }

            return IsCustomIdent(name) ? new Component(Multiply(new IdentConverter(name), multiplier)) : null;
        }

        private static IValueConverter Multiply(IValueConverter converter, Char? multiplier) => multiplier switch
        {
            Symbols.Plus => converter.Many(),
            Symbols.Num => converter.FromList(),
            _ => converter,
        };

        private static Boolean IsCustomIdent(String text)
        {
            var source = new StringSource(text);
            var ident = source.ParseCustomIdent();
            return ident is not null && ident.Length == text.Length && source.IsDone && !IsCssWideKeyword(ident);
        }

        private static Boolean IsCssWideKeyword(String ident) =>
            ident.Isi(CssKeywords.Initial) || ident.Isi(CssKeywords.Inherit) || ident.Isi(CssKeywords.Unset) ||
            ident.Isi(CssKeywords.Revert) || ident.Isi(CssKeywords.RevertLayer) || ident.Isi(CssKeywords.Default);

        private static String TrimWhitespace(String text) => text.Trim(' ', '\t', '\n', '\r', '\f');

        #endregion

        #region Matching

        /// <summary>
        /// Matches the value against the syntax, trying the components in their order.
        /// </summary>
        /// <param name="value">The value, e.g., the substituted value of a declaration.</param>
        /// <returns>The value according to the first matching component, or null if none matches.</returns>
        public ICssValue? Match(ICssValue value)
        {
            if (IsUniversal)
            {
                return value;
            }

            var text = value.CssText;

            foreach (var component in _components)
            {
                var source = new StringSource(text);
                source.SkipSpacesAndComments();
                var result = component.Converter.Convert(source);
                source.SkipSpacesAndComments();

                if (result is not null && source.IsDone)
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if the value can be computed without the element, i.e., if it does not
        /// refer to custom properties or to the font of the element or the root.
        /// https://www.w3.org/TR/css-properties-values-api-1/#computationally-independent
        /// </summary>
        public static Boolean IsComputationallyIndependent(String text)
        {
            var tokenizer = new CssTokenizer(new TextSource(text));

            while (true)
            {
                var token = tokenizer.Get();

                switch (token.Type)
                {
                    case CssTokenType.EndOfFile:
                        return true;
                    case CssTokenType.Function when IsReference(token.Data):
                        return false;
                    case CssTokenType.Dimension when DependentUnits.Contains(token.Data.CssUnit(out _)):
                        return false;
                }
            }
        }

        private static Boolean IsReference(String name)
        {
            var function = name.TrimEnd(Symbols.RoundBracketOpen);
            return function.Isi(FunctionNames.Var) || function.Isi(FunctionNames.Env) || function.Isi(FunctionNames.Attr);
        }

        #endregion

        #region Components

        private sealed class Component
        {
            public Component(IValueConverter converter)
            {
                Converter = converter;
            }

            public IValueConverter Converter { get; }
        }

        /// <summary>
        /// Converts a &lt;custom-ident&gt;, or exactly the given identifier (case-sensitive).
        /// </summary>
        private sealed class IdentConverter : IValueConverter
        {
            private readonly String? _ident;

            public IdentConverter(String? ident)
            {
                _ident = ident;
            }

            public ICssValue? Convert(StringSource source)
            {
                var start = source.Index;
                var ident = source.ParseCustomIdent();

                if (ident is not null && !IsCssWideKeyword(ident) && (_ident is null || String.Equals(ident, _ident, StringComparison.Ordinal)))
                {
                    return new CssIdentifierValue(ident);
                }

                source.BackTo(start);
                return null;
            }
        }

        #endregion
    }
}
