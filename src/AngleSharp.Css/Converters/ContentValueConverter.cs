namespace AngleSharp.Css.Converters
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    sealed class ContentValueConverter : IValueConverter
    {
        private static readonly Dictionary<String, IContentMode> ContentModes = new Dictionary<String, IContentMode>(StringComparer.OrdinalIgnoreCase)
        {
            { CssKeywords.OpenQuote, new OpenQuoteContentMode() },
            { CssKeywords.NoOpenQuote, new NoOpenQuoteContentMode() },
            { CssKeywords.CloseQuote, new CloseQuoteContentMode() },
            { CssKeywords.NoCloseQuote, new NoCloseQuoteContentMode() }
        };

        public ICssValue Convert(StringSource source)
        {
            var modes = default(IContentMode[]);

            if (source.IsIdentifier(CssKeywords.Normal))
            {
                modes = new IContentMode[] { new NormalContentMode() };
            }
            else if (source.IsIdentifier(CssKeywords.None))
            {
                modes = new IContentMode[] { };
            }
            else
            {
                var ms = new List<IContentMode>();

                while (!source.IsDone)
                {
                    var t = source.ParseString();

                    if (t != null)
                    {
                        ms.Add(new TextContentMode(t));
                        source.SkipSpacesAndComments();
                        continue;
                    }

                    var u = source.ParseUri();

                    if (u != null)
                    {
                        ms.Add(new UrlContentMode(u));
                        source.SkipSpacesAndComments();
                        continue;
                    }

                    var c = source.ParseCounter();

                    if (c.HasValue)
                    {
                        ms.Add(new CounterContentMode(c.Value));
                        source.SkipSpacesAndComments();
                        continue;
                    }

                    var a = source.ParseAttr();

                    if (a != null)
                    {
                        ms.Add(new AttributeContentMode(a));
                        source.SkipSpacesAndComments();
                        continue;
                    }

                    var m = source.ParseStatic(ContentModes);

                    if (m != null)
                    {
                        ms.Add(m);
                        source.SkipSpacesAndComments();
                        continue;
                    }

                    return null;

                }

                modes = ms.ToArray();
            }

            if (modes != null)
            {
                return new ContentValue(modes);
            }

            return null;
        }

        private sealed class ContentValue : ICssValue
        {
            private IContentMode[] _modes;

            public ContentValue(IContentMode[] modes)
            {
                _modes = modes;
            }

            public String CssText
            {
                get { return _modes.Length == 0 ? CssKeywords.None : _modes.Join(" "); }
            }
        }

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

            public override String ToString()
            {
                return CssKeywords.Normal;
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

            public override String ToString()
            {
                return CssKeywords.OpenQuote;
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

            public override String ToString()
            {
                return CssKeywords.CloseQuote;
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

            public override String ToString()
            {
                return CssKeywords.NoOpenQuote;
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

            public override String ToString()
            {
                return CssKeywords.NoCloseQuote;
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

            public override String ToString()
            {
                return _text.CssString();
            }
        }

        /// <summary>
        /// The generated text is the value of all counters with the given name
        /// in scope at this pseudo-element, from outermost to innermost
        /// separated by the specified string.
        /// </summary>
        private sealed class CounterContentMode : IContentMode
        {
            private readonly CounterDefinition _counter;

            public CounterContentMode(CounterDefinition counter)
            {
                _counter = counter;
            }

            public String Stringify(IElement element)
            {
                return String.Empty;
            }

            public override String ToString()
            {
                return _counter.ToString();
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

            public override String ToString()
            {
                return FunctionNames.Attr.CssFunction(_attribute);
            }
        }

        /// <summary>
        /// The value is a URI that designates an external resource (such as an
        /// image). If the resource or image can't be displayed, it is either
        /// ignored or some placeholder shows up.
        /// </summary>
        private sealed class UrlContentMode : IContentMode
        {
            private readonly UrlReference _url;

            public UrlContentMode(UrlReference url)
            {
                _url = url;
            }

            public String Stringify(IElement element)
            {
                return String.Empty;
            }

            public override String ToString()
            {
                return _url.ToString();
            }
        }
    }
}
