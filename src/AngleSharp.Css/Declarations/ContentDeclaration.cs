namespace AngleSharp.Css.Declarations
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    static class ContentDeclaration
    {
        public static String Name = PropertyNames.Content;

        public static IValueConverter Converter = new ContentValueConverter();

        public static ICssValue InitialValue = InitialValues.ContentDecl;

        public static PropertyFlags Flags = PropertyFlags.None;

        sealed class ContentValueConverter : IValueConverter
        {
            private static readonly Dictionary<String, ContentMode> ContentModes = new(StringComparer.OrdinalIgnoreCase)
            {
                { CssKeywords.OpenQuote, new OpenQuoteContentMode() },
                { CssKeywords.NoOpenQuote, new NoOpenQuoteContentMode() },
                { CssKeywords.CloseQuote, new CloseQuoteContentMode() },
                { CssKeywords.NoCloseQuote, new NoCloseQuoteContentMode() },
            };

            public ICssValue Convert(StringSource source)
            {
                var modes = default(ICssValue[]);

                if (source.IsIdentifier(CssKeywords.Normal))
                {
                    modes = [new NormalContentMode()];
                }
                else if (source.IsIdentifier(CssKeywords.None))
                {
                    modes = [];
                }
                else
                {
                    var ms = new List<ICssValue>();

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
                            ms.Add(new AttributeContentMode(a.Attribute));
                            source.SkipSpacesAndComments();
                            continue;
                        }

                        var m = source.ParseStatic(ContentModes);

                        if (m.HasValue)
                        {
                            ms.Add(m.Value);
                            source.SkipSpacesAndComments();
                            continue;
                        }

                        return null;

                    }

                    modes = [.. ms];
                }

                if (modes != null)
                {
                    return new ContentValue(modes);
                }

                return null;
            }

            private sealed class ContentValue(ICssValue[] modes) : ICssValue, IEquatable<ContentValue>
            {
                private readonly ICssValue[] _modes = modes;

                public String CssText => _modes.Length == 0 ? CssKeywords.None : _modes.Join(" ");

                public ICssValue Compute(ICssComputeContext context)
                {
                    var modes = _modes.Select(mode => mode.Compute(context)).ToArray();
                    return new ContentValue(modes);
                }
                public Boolean Equals(ContentValue other)
                {
                    var l = _modes.Length;

                    if (l == other._modes.Length)
                    {
                        for (var i = 0; i < l; i++)
                        {
                            var a = _modes[i];
                            var b = other._modes[i];

                            if (!a.Equals(b))
                            {
                                return false;
                            }
                        }

                        return true;
                    }

                    return false;
                }

                Boolean IEquatable<ICssValue>.Equals(ICssValue other) => other is ContentValue value && Equals(value);
            }

            private abstract class ContentMode : ICssValue
            {
                public String CssText => GetCssText();

                public abstract String Stringify(IElement element);

                public abstract String GetCssText();

                ICssValue ICssValue.Compute(ICssComputeContext context) => this;

                public virtual Boolean Equals(ICssValue other) => Object.ReferenceEquals(this, other);
            }

            /// <summary>
            /// Computes to none for the :before and :after pseudo-elements.
            /// </summary>
            private sealed class NormalContentMode : ContentMode
            {
                public override String GetCssText() => CssKeywords.Normal;

                public override String Stringify(IElement element) => String.Empty;
            }

            /// <summary>
            /// The value is replaced by the open quote string from the quotes
            /// property.
            /// </summary>
            private sealed class OpenQuoteContentMode : ContentMode
            {
                public override String GetCssText() => CssKeywords.OpenQuote;

                public override String Stringify(IElement element) => String.Empty;
            }

            /// <summary>
            /// The value is replaced by the close string from the quotes
            /// property.
            /// </summary>
            private sealed class CloseQuoteContentMode : ContentMode
            {
                public override String GetCssText() => CssKeywords.CloseQuote;

                public override String Stringify(IElement element) => String.Empty;
            }

            /// <summary>
            /// Introduces no content, but increments the level of nesting for
            /// quotes.
            /// </summary>
            private sealed class NoOpenQuoteContentMode : ContentMode
            {
                public override String GetCssText() => CssKeywords.NoOpenQuote;

                public override String Stringify(IElement element) => String.Empty;
            }

            /// <summary>
            /// Introduces no content, but decrements the level of nesting for
            /// quotes.
            /// </summary>
            private sealed class NoCloseQuoteContentMode : ContentMode
            {
                public override String GetCssText() => CssKeywords.NoCloseQuote;

                public override String Stringify(IElement element) => String.Empty;
            }

            /// <summary>
            /// Text content.
            /// </summary>
            private sealed class TextContentMode(String text) : ContentMode
            {
                private readonly String _text = text;

                public override String GetCssText() => _text.CssString();

                public override String Stringify(IElement element) => _text;

                public override Boolean Equals(ICssValue other)
                {
                    if (other is TextContentMode o)
                    {
                        return _text.Equals(o._text);
                    }

                    return false;
                }
            }

            /// <summary>
            /// The generated text is the value of all counters with the given name
            /// in scope at this pseudo-element, from outermost to innermost
            /// separated by the specified string.
            /// </summary>
            private sealed class CounterContentMode(CssCounterDefinitionValue counter) : ContentMode
            {
                private readonly CssCounterDefinitionValue _counter = counter;

                public override String GetCssText() => _counter.CssText;

                public override String Stringify(IElement element) => String.Empty;

                public override Boolean Equals(ICssValue other)
                {
                    if (other is CounterContentMode o)
                    {
                        return _counter.Equals(o._counter);
                    }

                    return false;
                }
            }

            /// <summary>
            /// Returns the value of the element's attribute X as a string. If
            /// there is no attribute X, an empty string is returned.
            /// </summary>
            private sealed class AttributeContentMode(String attribute) : ContentMode
            {
                private readonly String _attribute = attribute;

                public override String GetCssText() => FunctionNames.Attr.CssFunction(_attribute);

                public override String Stringify(IElement element) => element.GetAttribute(_attribute) ?? String.Empty;

                public override Boolean Equals(ICssValue other)
                {
                    if (other is AttributeContentMode o)
                    {
                        return _attribute.Equals(o._attribute);
                    }

                    return false;
                }
            }

            /// <summary>
            /// The value is a URI that designates an external resource (such as an
            /// image). If the resource or image can't be displayed, it is either
            /// ignored or some placeholder shows up.
            /// </summary>
            private sealed class UrlContentMode(CssUrlValue url) : ContentMode
            {
                private readonly CssUrlValue _url = url;

                public override String GetCssText() => _url.CssText;

                public override String Stringify(IElement element) => String.Empty;

                public override Boolean Equals(ICssValue other)
                {
                    if (other is UrlContentMode o)
                    {
                        return _url.Equals(o._url);
                    }

                    return false;
                }
            }
        }
    }
}
