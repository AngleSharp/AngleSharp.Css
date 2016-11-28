namespace AngleSharp.Css.Dom
{
    using AngleSharp.Css;
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using static ValueConverters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/content
    /// </summary>
    sealed class CssContentProperty : CssProperty
    {
        #region Fields

        private static readonly Dictionary<String, IContentMode> ContentModes = new Dictionary<String, IContentMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.OpenQuote, new OpenQuoteContentMode() },
            { CssKeywords.NoOpenQuote, new NoOpenQuoteContentMode() },
            { CssKeywords.CloseQuote, new CloseQuoteContentMode() },
            { CssKeywords.NoCloseQuote, new NoCloseQuoteContentMode() }
        };
        private static readonly IContentMode[] Default = new[] { new NormalContentMode() };

        // Way of converting for an IElement element
        /*
            var values = ContentConverter.Convert(Value);
            var parts = values.Select(m => m.Stringify(element));
            return String.Join(String.Empty, parts);
        */
        private static readonly IValueConverter StyleConverter = Or(
            Or(
                Assign(CssKeywords.Normal, Default),
                None,
                ContentModes.ToConverter(),
                UrlConverter,
                StringConverter,
                AttrConverter,
                CounterFunctionConverter).Many(),
            Initial);

        #endregion

        #region ctor

        internal CssContentProperty()
            : base(PropertyNames.Content)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion

        #region Helpers

        private static IContentMode TransformUrl(String url)
        {
            return new UrlContentMode(url);
        }

        private static IContentMode TransformString(String str)
        {
            return new TextContentMode(str);
        }

        private static IContentMode TransformAttr(String attr)
        {
            return new AttributeContentMode(attr);
        }

        private static IContentMode TransformCounter(Counter counter)
        {
            return new CounterContentMode(counter);
        }

        #endregion

        #region Modes

        private interface IContentMode
        {
            String Stringify(IElement element);
        }

        /// <summary>
        /// Computes to none for the :before and :after pseudo-elements.
        /// </summary>
        private sealed class NormalContentMode : IContentMode
        {
            public String Stringify(IElement element)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// The value is replaced by the open quote string from the quotes
        /// property.
        /// </summary>
        private sealed class OpenQuoteContentMode : IContentMode
        {
            public String Stringify(IElement element)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// The value is replaced by the close string from the quotes
        /// property.
        /// </summary>
        private sealed class CloseQuoteContentMode : IContentMode
        {
            public String Stringify(IElement element)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Introduces no content, but increments the level of nesting for
        /// quotes.
        /// </summary>
        private sealed class NoOpenQuoteContentMode : IContentMode
        {
            public String Stringify(IElement element)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Introduces no content, but decrements the level of nesting for
        /// quotes.
        /// </summary>
        private sealed class NoCloseQuoteContentMode : IContentMode
        {
            public String Stringify(IElement element)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Text content.
        /// </summary>
        private sealed class TextContentMode : IContentMode
        {
            private readonly String _text;

            public TextContentMode(String text)
            {
                _text = text;
            }

            public String Stringify(IElement element)
            {
                return _text;
            }
        }

        /// <summary>
        /// The generated text is the value of all counters with the given name
        /// in scope at this pseudo-element, from outermost to innermost
        /// separated by the specified string.
        /// </summary>
        private sealed class CounterContentMode : IContentMode
        {
            private readonly Counter _counter;

            public CounterContentMode(Counter counter)
            {
                _counter = counter;
            }

            public String Stringify(IElement element)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Returns the value of the element's attribute X as a string. If
        /// there is no attribute X, an empty string is returned.
        /// </summary>
        private sealed class AttributeContentMode : IContentMode
        {
            private readonly String _attribute;

            public AttributeContentMode(String attribute)
            {
                _attribute = attribute;
            }

            public String Stringify(IElement element)
            {
                return element.GetAttribute(_attribute) ?? String.Empty;
            }
        }

        /// <summary>
        /// The value is a URI that designates an external resource (such as an
        /// image). If the resource or image can't be displayed, it is either
        /// ignored or some placeholder shows up.
        /// </summary>
        private sealed class UrlContentMode : IContentMode
        {
            private readonly String _url;

            public UrlContentMode(String url)
            {
                _url = url;
            }

            public String Stringify(IElement element)
            {
                return String.Empty;
            }
        }

        #endregion
    }
}
