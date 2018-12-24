namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A set of useful extension methods for the Element class.
    /// </summary>
    [DomExposed("Element")]
    public static class ElementExtensions
    {
        /// <summary>
        /// Creates a pseudo element for the current element.
        /// </summary>
        /// <param name="element">The element to extend.</param>
        /// <param name="pseudoElement">
        /// The element to create (e.g. ::after).
        /// </param>
        /// <returns>The created element or null, if not possible.</returns>
        [DomName("pseudo")]
        public static IPseudoElement Pseudo(this IElement element, String pseudoElement)
        {
            var factory = element.Owner?.Context.GetService<IPseudoElementFactory>();
            return factory?.Create(element, pseudoElement);
        }

        /// <summary>
        /// Gets the innerText of an element.
        /// </summary>
        /// <param name="element">The element to extend.</param>
        /// <returns>The computed inner text.</returns>
        [DomName("innerText")]
        [DomAccessor(Accessors.Getter)]
        public static String GetInnerText(this IElement element)
        {
            var hidden = new Nullable<Boolean>();

            if (element.Owner == null)
            {
                hidden = true;
            }

            if (!hidden.HasValue)
            {
                var css = element.ComputeCurrentStyle();

                if (!String.IsNullOrEmpty(css?.GetDisplay()))
                {
                    hidden = css.GetDisplay() == "none";
                }
            }

            if (!hidden.HasValue)
            {
                hidden = (element as IHtmlElement)?.IsHidden;
            }

            if (!hidden.Value)
            {
                var sb = StringBuilderPool.Obtain();
                var requiredLineBreakCounts = new Dictionary<Int32, Int32>();
                InnerTextCollection(element, sb, requiredLineBreakCounts, element.ParentElement?.ComputeCurrentStyle());

                // Remove any runs of consecutive required line break count items at the start or end of results.
                requiredLineBreakCounts.Remove(0);
                requiredLineBreakCounts.Remove(sb.Length);
                var offset = 0;

                // SortedDictionary would be nicer
                foreach (var keyval in requiredLineBreakCounts.OrderBy(kv => kv.Key))
                {
                    var index = keyval.Key + offset;
                    sb.Insert(index, new String(Symbols.LineFeed, keyval.Value));
                    offset += keyval.Value;
                }

                return sb.ToPool();
            }

            return element.TextContent;
        }

        /// <summary>
        /// Sets the innerText of an element.
        /// </summary>
        /// <param name="element">The element to extend.</param>
        /// <param name="value">The inner text to set.</param>
        [DomName("innerText")]
        [DomAccessor(Accessors.Setter)]

        public static void SetInnerText(this IElement element, String value)
        {
            if (String.IsNullOrEmpty(value))
            {
                element.TextContent = String.Empty;
            }
            else
            {
                var document = element.Owner;
                var fragment = document.CreateDocumentFragment();
                var sb = StringBuilderPool.Obtain();

                for (var i = 0; i < value.Length; i++)
                {
                    var c = value[i];

                    if (c == Symbols.LineFeed || c == Symbols.CarriageReturn)
                    {
                        if (c == Symbols.CarriageReturn && i + 1 < value.Length && value[i + 1] == Symbols.LineFeed)
                        {
                            // ignore carriage return if the next char is a line feed
                            continue;
                        }

                        if (sb.Length > 0)
                        {
                            fragment.AppendChild(document.CreateTextNode(sb.ToString()));
                            sb.Clear();
                        }

                        fragment.AppendChild(document.CreateElement(TagNames.Br));
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }

                var remaining = sb.ToPool();

                if (remaining.Length > 0)
                {
                    fragment.AppendChild(document.CreateTextNode(remaining));
                }

                while (element.HasChildNodes)
                {
                    element.RemoveChild(element.FirstChild);
                }

                element.AppendChild(fragment);
            }
        }

        private static void InnerTextCollection(INode node, StringBuilder sb, Dictionary<Int32, Int32> requiredLineBreakCounts, ICssStyleDeclaration parentStyle)
        {
            if (HasCssBox(node))
            {
                var element = node as IElement;
                var elementCss = element?.ComputeCurrentStyle();
                ItcInCssBox(elementCss, parentStyle, node, sb, requiredLineBreakCounts);
            }
        }

        private static void ItcInCssBox(ICssStyleDeclaration elementStyle, ICssStyleDeclaration parentStyle, INode node, StringBuilder sb, Dictionary<Int32, Int32> requiredLineBreakCounts)
        {
            var elementHidden = new Nullable<Boolean>();

            if (elementStyle != null)
            {
                if (!String.IsNullOrEmpty(elementStyle.GetDisplay()))
                {
                    elementHidden = elementStyle.GetDisplay() == "none";
                }
                if (!String.IsNullOrEmpty(elementStyle.GetVisibility()) && elementHidden != true)
                {
                    elementHidden = elementStyle.GetVisibility() != "visible";
                }
            }

            if (!elementHidden.HasValue)
            {
                elementHidden = (node as IHtmlElement)?.IsHidden ?? false;
            }

            if (!elementHidden.Value)
            {
                var isBlockLevel = new Nullable<Boolean>();
                var startIndex = sb.Length;

                foreach (var child in node.ChildNodes)
                {
                    InnerTextCollection(child, sb, requiredLineBreakCounts, elementStyle);
                }

                if (node is IText)
                {
                    var textElement = (IText)node;
                    ProcessText(textElement.Data, sb, parentStyle);
                }
                else if (node is IHtmlBreakRowElement)
                {
                    sb.Append(Symbols.LineFeed);
                }
                else if ((node is IHtmlTableCellElement && String.IsNullOrEmpty(elementStyle.GetDisplay())) || elementStyle.GetDisplay() == "table-cell")
                {
                    var nextSibling = node.NextSibling as IElement;

                    if (nextSibling != null)
                    {
                        var nextSiblingCss = nextSibling.ComputeCurrentStyle();

                        if (nextSibling is IHtmlTableCellElement && String.IsNullOrEmpty(nextSiblingCss.GetDisplay()) || nextSiblingCss.GetDisplay() == "table-cell")
                        {
                            sb.Append(Symbols.Tab);
                        }
                    }
                }
                else if ((node is IHtmlTableRowElement && String.IsNullOrEmpty(elementStyle.GetDisplay())) || elementStyle.GetDisplay() == "table-row")
                {
                    var nextSibling = node.NextSibling as IElement;

                    if (nextSibling != null)
                    {
                        var nextSiblingCss = nextSibling.ComputeCurrentStyle();

                        if (nextSibling is IHtmlTableRowElement && String.IsNullOrEmpty(nextSiblingCss.GetDisplay()) || nextSiblingCss.GetDisplay() == "table-row")
                        {
                            sb.Append(Symbols.LineFeed);
                        }
                    }
                }
                else if (node is IHtmlParagraphElement)
                {
                    var startIndexCount = 0;
                    requiredLineBreakCounts.TryGetValue(startIndex, out startIndexCount);

                    if (startIndexCount < 2)
                    {
                        requiredLineBreakCounts[startIndex] = 2;
                    }

                    var endIndexCount = 0;
                    requiredLineBreakCounts.TryGetValue(sb.Length, out endIndexCount);

                    if (endIndexCount < 2)
                    {
                        requiredLineBreakCounts[sb.Length] = 2;
                    }
                }

                if (elementStyle != null)
                {
                    if (IsBlockLevelDisplay(elementStyle.GetDisplay()))
                    {
                        isBlockLevel = true;
                    }
                }

                if (!isBlockLevel.HasValue)
                {
                    isBlockLevel = IsBlockLevel(node);
                }

                if (isBlockLevel.Value)
                {
                    var startIndexCount = 0;
                    requiredLineBreakCounts.TryGetValue(startIndex, out startIndexCount);

                    if (startIndexCount < 1)
                    {
                        requiredLineBreakCounts[startIndex] = 1;
                    }

                    var endIndexCount = 0;
                    requiredLineBreakCounts.TryGetValue(sb.Length, out endIndexCount);

                    if (endIndexCount < 1)
                    {
                        requiredLineBreakCounts[sb.Length] = 1;
                    }
                }
            }
        }

        private static Boolean HasCssBox(INode node)
        {
            switch (node.NodeName)
            {
                case "CANVAS":
                case "COL":
                case "COLGROUP":
                case "DETAILS":
                case "FRAME":
                case "FRAMESET":
                case "IFRAME":
                case "IMG":
                case "INPUT":
                case "LINK":
                case "METER":
                case "PROGRESS":
                case "TEMPLATE":
                case "TEXTAREA":
                case "VIDEO":
                case "WBR":
                case "SCRIPT":
                case "STYLE":
                case "NOSCRIPT":
                    return false;
                default:
                    return true;
            }
        }

        private static Boolean IsBlockLevelDisplay(String display)
        {
            // https://www.w3.org/TR/css-display-3/#display-value-summary
            // https://hg.mozilla.org/mozilla-central/file/0acceb224b7d/servo/components/layout/query.rs#l1016
            switch (display)
            {
                case "block":
                case "flow-root":
                case "flex":
                case "grid":
                case "table":
                case "table-caption":
                    return true;
                default:
                    return false;
            }
        }

        private static Boolean IsBlockLevel(INode node)
        {
            // https://developer.mozilla.org/en-US/docs/Web/HTML/Block-level_elements
            switch (node.NodeName)
            {
                case "ADDRESS":
                case "ARTICLE":
                case "ASIDE":
                case "BLOCKQUOTE":
                case "CANVAS":
                case "DD":
                case "DIV":
                case "DL":
                case "DT":
                case "FIELDSET":
                case "FIGCAPTION":
                case "FIGURE":
                case "FOOTER":
                case "FORM":
                case "H1":
                case "H2":
                case "H3":
                case "H4":
                case "H5":
                case "H6":
                case "HEADER":
                case "GROUP":
                case "HR":
                case "LI":
                case "MAIN":
                case "NAV":
                case "NOSCRIPT":
                case "OL":
                case "OPTION":
                case "OUTPUT":
                case "P":
                case "PRE":
                case "SECTION":
                case "TABLE":
                case "TFOOT":
                case "UL":
                case "VIDEO":
                    return true;
                default:
                    return false;
            }
        }

        private static void ProcessText(String text, StringBuilder sb, ICssStyleDeclaration style)
        {
            var startIndex = sb.Length;
            var whiteSpace = style?.GetWhiteSpace();
            var textTransform = style?.GetTextTransform();
            var isWhiteSpace = startIndex > 0 ? Char.IsWhiteSpace(sb[startIndex - 1]) && sb[startIndex - 1] != Symbols.NoBreakSpace : true;

            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (Char.IsWhiteSpace(c) && c != Symbols.NoBreakSpace)
                {
                    // https://drafts.csswg.org/css-text/#white-space-property
                    switch (whiteSpace)
                    {
                        case "pre":
                        case "pre-wrap":
                            break;
                        case "pre-line":
                            if (c == Symbols.Space || c == Symbols.Tab)
                            {
                                if (isWhiteSpace)
                                {
                                    continue;
                                }

                                c = Symbols.Space;
                            }
                            break;
                        case "nowrap":
                        case "normal":
                        default:
                            if (isWhiteSpace)
                            {
                                continue;
                            }

                            c = Symbols.Space;
                            break;
                    }

                    isWhiteSpace = true;
                }
                else
                {
                    // https://drafts.csswg.org/css-text/#propdef-text-transform
                    switch (textTransform)
                    {
                        case "uppercase":
                            c = Char.ToUpperInvariant(c);
                            break;
                        case "lowercase":
                            c = Char.ToLowerInvariant(c);
                            break;
                        case "capitalize":
                            if (isWhiteSpace)
                            {
                                c = Char.ToUpperInvariant(c);
                            }
                            break;
                        case "none":
                        default:
                            break;
                    }

                    isWhiteSpace = false;
                }

                sb.Append(c);
            }

            if (isWhiteSpace) // ended with whitespace
            {
                for (var offset = sb.Length - 1; offset >= startIndex; offset--)
                {
                    var c = sb[offset];
                    if (!Char.IsWhiteSpace(c) || c == Symbols.NoBreakSpace)
                    {
                        sb.Remove(offset + 1, sb.Length - 1 - offset);
                        break;
                    }
                }
            }
        }
    }
}
